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
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _catalog;

        public CatalogController(ICatalogService catalog)
        {
            _catalog = catalog;
        }

        [HttpGet("index")]
        [Authorize("Get")]
        public void Index() { return; }

        [HttpPost]
        [Authorize("Post")]
        public async Task<long> Post(CatalogDTO catalog) => await _catalog.Post(catalog);

        [HttpGet("detail/{id}")]
        [Authorize("Get")]
        public async Task<CatalogDTO> Get(long id) => await _catalog.Get(id);

        [HttpGet("{id}")]
        [Authorize("Get")]
        public async Task<List<CatalogDTO>> Get(short id) => await _catalog.Get(id);

        [HttpPut]
        [Authorize("Put")]
        public async Task<bool> Put(CatalogDTO catalog) => await _catalog.Put(catalog);

        [HttpGet("download")]
        public async Task<IActionResult> Download() => File(await _catalog.Download(), MimeType.XLSX);
    }
}
