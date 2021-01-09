using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Models;
using Repository.Interfaces;
using Resources.Constants;
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
            var catalog = await _context.Catalog.FindAsync(_catalog.CatalogId);
            if (catalog == null)
                throw new RestException(HttpStatusCode.NotFound, new { catalog = Messages.NotFound });

            var duplicate = await _context.Catalog
                .FirstOrDefaultAsync(x => (x.Description == _catalog.Description || x.Alias == _catalog.Alias) && x.CatalogId == _catalog.CatalogId) != null;
            if (duplicate)
                throw new RestException(HttpStatusCode.BadRequest, new { catalog = Messages.Duplicated });

            _context.Catalog.Add(_catalog);
            await _context.SaveChangesAsync();

            return _catalog.Id;
        }

        public async Task<CatalogModel> Get(long id)
        {
            return await _context.Catalog.FindAsync(id);
        }

        public async Task<List<CatalogModel>> Get(short id)
        {
            return await _context.Catalog.Where(x => x.CatalogId == id).ToListAsync();
        }

        public async Task<bool> Put(CatalogModel _catalog)
        {
            var catalog = await _context.Catalog.FindAsync(_catalog.Id);
            if (catalog == null)
                throw new RestException(HttpStatusCode.NotFound, new { catalog = Messages.NotFound });

            if (catalog.Protected)
                throw new RestException(HttpStatusCode.NotFound, new { catalog = Messages.ProtectedChanged });

            var duplicate = await _context.Catalog
                .FirstOrDefaultAsync(x => (x.Description == _catalog.Description || x.Alias == _catalog.Alias)
                && x.CatalogId == _catalog.CatalogId && x.Id != _catalog.Id) != null;
            if (duplicate)
                throw new RestException(HttpStatusCode.BadRequest, new { catalog = Messages.Duplicated });

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
                .OrderBy(x => x.Catalogo).ThenByDescending(x => x.Cabecera).ThenBy(x => x.Alias).ToListAsync();

            if (catalogs.Count == 0)
                throw new RestException(HttpStatusCode.NotFound, new { catalog = Messages.NothingToExport });

            return catalogs;
        }
    }
}
