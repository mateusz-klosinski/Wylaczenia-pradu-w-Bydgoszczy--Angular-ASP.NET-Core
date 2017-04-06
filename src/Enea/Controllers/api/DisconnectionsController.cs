using Enea.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enea.Controllers.api
{
    [Route("/api/disconnections")]
    public class DisconnectionsController : Controller
    {
        private DisconnectionService _service;

        public DisconnectionsController(DisconnectionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _service.DownloadAndFormatDisconnectionsIntoListAsync();

            return Ok(data);
        }
    }
}
