using Persistence.Models;
using Resources.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICatalogService
    {
        Task<long> Post(CatalogDTO _catalog);
        Task<bool> Put(CatalogDTO _role);
        Task<List<CatalogDTO>> Get(short catalogId);
        Task<CatalogDTO> Get(long id);
        Task<byte[]> Download();
    }
}
