using Resources.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IClientService
    {
        Task<short> Post(ClientDTO _client);
        Task<bool> Put(ClientDTO _client);
        Task<List<ClientDTO>> Get();
        Task<ClientDTO> Get(short id);
        Task<byte[]> Download();
    }
}
