using Microsoft.AspNetCore.Mvc;
using SCS.BLL;
using SCS.Domain;
using System.Diagnostics.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectorsController : ControllerBase
    {
        private readonly IConnectorService _connectorService;

        public ConnectorsController(IConnectorService connectorService)
        {
            _connectorService = connectorService;
        }

        // GET: api/<ConnectorsController>
        [HttpGet("GetAllConnectors")]
        public async Task<IActionResult> GetAllConnectors()
        {
            try
            {
                var result = await _connectorService.GetAllConnectorsAsync();
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<ConnectorsController>/5
        [HttpGet("GetConnector")]
        public async Task<IActionResult> GetConnector(int id)
        {
            try
            {
                var result = await _connectorService.GetConnectorAsync(id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<ConnectorsController>
        [HttpPost("SaveConnector")]
        public async Task<IActionResult> SaveConnector(Connector connector)
        {
            try
            {
                var result = await _connectorService.SaveConnectorAsync(connector);
                if (result > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<ConnectorsController>/5
        [HttpPut("UpdateConnector")]
        public async Task<IActionResult> UpdateConnector(int id, int maxCurrentAmps)
        {
            try
            {
                var connector = new Connector() { MaxCurrentAmps = maxCurrentAmps };

                var result = await _connectorService.UpdateConnectorAsync(id, connector);
                if (result > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/<ConnectorsController>/5
        [HttpDelete("DeleteConnector")]
        public async Task<IActionResult> DeleteConnector(int id)
        {
            try
            {
                var result = await _connectorService.DeleteConnectorAsync(id);
                if (result > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
