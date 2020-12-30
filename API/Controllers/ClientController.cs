using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resources.Constants;
using Resources.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _client;

        public ClientController(IClientService client)
        {
            _client = client;
        }

        [HttpGet("index")]
        [Authorize("Get")]
        public void Index() { return; }

        [HttpPost]
        [Authorize("Post")]
        public async Task<short> Post(ClientDTO client) => await _client.Post(client);

        [HttpGet("{id}")]
        [Authorize("Get")]
        public async Task<ClientDTO> Get(short id) => await _client.Get(id);

        [HttpGet]
        [Authorize("Get")]
        public async Task<List<ClientDTO>> Get() => await _client.Get();

        [HttpPut]
        [Authorize("Put")]
        public async Task<bool> Put(ClientDTO client) => await _client.Put(client);

        [HttpGet("download")]
        public async Task<IActionResult> Download() => File(await _client.Download(), MimeType.XLSX);
    }
}
