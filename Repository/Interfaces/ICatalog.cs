using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ICatalog
    {
        Task<long> Post(CatalogModel _catalog);
        Task<bool> Put(CatalogModel _role);
        Task<List<CatalogModel>> Get(short catalogId);
        Task<CatalogModel> Get(long id);
        Task<object> Download();
    }
}
