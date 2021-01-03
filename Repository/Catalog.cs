using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Models;
using Repository.Interfaces;
using Resources.Errors;
using Resources.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Catalog : ICatalog
    {
        private readonly DataContext _context;

        public Catalog(DataContext context)
        {
            _context = context;
        }

        public async Task<long> Post(CatalogModel _catalog)
        {
            var catalog = await _context.Catalog.AsNoTracking().FirstOrDefaultAsync(x => x.Id == _catalog.CatalogId);
            if (catalog == null)
                throw new RestException(HttpStatusCode.NotFound, new { catalog = "Not found" });

            var duplicate = await _context.Catalog
                .AnyAsync(x => (x.Description == _catalog.Description || x.Alias == _catalog.Alias) && x.CatalogId == _catalog.CatalogId);
            if (duplicate)
                throw new RestException(HttpStatusCode.BadRequest, new { catalog = "Already exists" });

            _context.Catalog.Add(_catalog);
            await _context.SaveChangesAsync();

            return _catalog.Id;
        }

        public async Task<CatalogModel> Get(long id)
        {
            var catalog = await _context.Catalog.FindAsync(id);
            if (catalog == null)
                throw new RestException(HttpStatusCode.NotFound, new { catalog = "Not found" });

            return catalog;
        }

        public async Task<List<CatalogModel>> Get(short id)
        {
            return await _context.Catalog.Where(x => x.CatalogId == id).ToListAsync();
        }

        public async Task<bool> Put(CatalogModel _catalog)
        {
            var catalog = await _context.Catalog.AsNoTracking().FirstOrDefaultAsync(x => x.Id == _catalog.Id);
            if (catalog == null)
                throw new RestException(HttpStatusCode.NotFound, new { catalog = "Not found" });

            var duplicate = await _context.Catalog
                .AnyAsync(x => (x.Description == _catalog.Description || x.Alias == _catalog.Alias)
                && x.CatalogId == _catalog.CatalogId && x.Id != _catalog.Id);
            if (duplicate)
                throw new RestException(HttpStatusCode.BadRequest, new { catalog = "Already exists" });

            if (catalog.Protected)
                throw new RestException(HttpStatusCode.NotFound, new { catalog = "Protected catalogs cannot be modified" });

            var entry = _context.Entry(_catalog);
            entry.State = EntityState.Modified;
            entry.Property(x => x.Protected).IsModified = false;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<object> Download()
        {
            var catalogs =
                await _context.Catalog
                .Select(x => new
                {
                    Catalogo = x.CatalogId == null ? x.Alias : x.Catalog.Alias,
                    x.Alias,
                    Descripcion = x.Description.SplitWords(),
                    Cabecera = x.CatalogId == null ? "Sí" : "No",
                    Protegido = x.Protected ? "Sí" : "No",
                    Activo = x.Active ? "Sí" : "No",
                })
                .OrderBy(x => x.Catalogo).ThenByDescending(x=>x.Cabecera).ThenBy(x => x.Alias).ToListAsync();

            if (catalogs.Count == 0)
                throw new RestException(HttpStatusCode.NotFound, new { GeneralCatalogs = "No hay registros para exportar" });

            return catalogs;
        }
    }
}
