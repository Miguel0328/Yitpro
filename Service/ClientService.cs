using AutoMapper;
using Persistence.Models;
using Repository.Interfaces;
using Resources.Constants;
using Resources.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Resources.Extension;
using Resources.Reports;

namespace Service
{
    public class ClientService : IClientService
    {
        private readonly IClient _client_;
        private readonly IMapper _mapper;

        public ClientService(IMapper mapper, IClient client)
        {
            _client_ = client;
            _mapper = mapper;
        }

        public async Task<short> Post(ClientDTO _client)
        {
            var client = _mapper.Map<ClientModel>(_client);
            return await _client_.Post(client);
        }

        public async Task<ClientDTO> Get(short id)
        {
            var client = await _client_.Get(id);
            return _mapper.Map<ClientDTO>(client);
        }

        public async Task<List<ClientDTO>> Get()
        {
            var clients = await _client_.Get();
            return _mapper.Map<List<ClientDTO>>(clients);
        }

        public async Task<bool> Put(ClientDTO _client)
        {
            var client = _mapper.Map<ClientModel>(_client);
            return await _client_.Put(client);
        }

        public async Task<byte[]> Download()
        {
            var roles = await _client_.Download();
            var file = roles.ToTable("Usuarios").ToExcel();

            return file;
        }
    }
}
