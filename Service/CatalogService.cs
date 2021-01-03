using AutoMapper;
using Persistence.Models;
using Repository.Interfaces;
using Resources.DTO;
using Resources.Extension;
using Resources.Reports;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CatalogService : ICatalogService
    {
        private readonly ICatalog _catalog_;
        private readonly IMapper _mapper;

        public CatalogService(IMapper mapper, ICatalog catalog)
        {
            _catalog_ = catalog;
            _mapper = mapper;
        }

        public async Task<long> Post(CatalogDTO _catalog)
        {
            var catalog = _mapper.Map<CatalogModel>(_catalog);
            return await _catalog_.Post(catalog);
        }

        public async Task<CatalogDTO> Get(long id)
        {
            var catalog = await _catalog_.Get(id);
            return _mapper.Map<CatalogDTO>(catalog);
        }

        public async Task<List<CatalogDTO>> Get(short id)
        {
            var catalogs = await _catalog_.Get(id);
            return _mapper.Map<List<CatalogDTO>>(catalogs);
        }

        public async Task<bool> Put(CatalogDTO _catalog)
        {
            var catalog = _mapper.Map<CatalogModel>(_catalog);
            return await _catalog_.Put(catalog);
        }

        public async Task<byte[]> Download()
        {
            var catalogs = await _catalog_.Download();
            var file = catalogs.ToTable("Usuarios").ToExcel();

            return file;
        }
    }
}
