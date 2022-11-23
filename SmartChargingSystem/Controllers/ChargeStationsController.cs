using Microsoft.AspNetCore.Mvc;
using SCS.BLL;
using SCS.Domain;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChargeStationsController : ControllerBase
    {
        private readonly IChargeStationService _chargeStationService;

        public ChargeStationsController(IChargeStationService chargeStationService)
        {
            _chargeStationService = chargeStationService;
        }

        // GET: api/<ChargeStationsController>
        [HttpGet("GetAllChargeStations")]
        public async Task<IActionResult> GetAllChargeStations()
        {
            try
            {
                var result = await _chargeStationService.GetAllChargeStationsAsync();
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


        // GET api/<ChargeStationsController>/5
        [HttpGet("GetChargeStation")]
        public async Task<IActionResult> GetChargeStation(int id)
        {
            try
            {
                var result = await _chargeStationService.GetChargeStationAsync(id);

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

        // POST api/<ChargeStationsController>
        [HttpPost("SaveChargeStation")]
        public async Task<IActionResult> SaveChargeStation(ChargeStation chargeStation)
        {
            try
            {
                var result = await _chargeStationService.SaveChargeStationAsync(chargeStation);

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

        // PUT api/<ChargeStationsController>/5
        [HttpPut("UpdateChargeStation")]
        public async Task<IActionResult> UpdateChargeStation(int id, string chargeStationName)
        {
            try
            {
                var chargeStation = new ChargeStation() { ChargeStationName = chargeStationName };

                var result = await _chargeStationService.UpdateChargeStationAsync(id, chargeStation);
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


        // DELETE api/<ChargeStationsController>/5
        [HttpDelete("DeleteChargeStation")]
        public async Task<IActionResult> DeleteChargeStation(int id)
        {
            try
            {
                var result = await _chargeStationService.DeleteChargeStationAsync(id);
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
