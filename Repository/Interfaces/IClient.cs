using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IClient
    {
        Task<short> Post(ClientModel _client);
        Task<bool> Put(ClientModel _client);
        Task<List<ClientModel>> Get();
        Task<ClientModel> Get(short id);
        Task<object> Download();
    }
}
