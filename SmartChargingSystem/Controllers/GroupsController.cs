using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SCS.BLL;
using SCS.Domain;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;
        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        // GET: api/<GroupsController>
        [HttpGet("GetAllGroups")]
        public async Task<IActionResult> GetAllGroups()
        {
            try
            {
                var result = await _groupService.GetAllGroupsAsync();
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

        // GET api/<GroupsController>/5
        [HttpGet("GetGroup")]
        public async Task<IActionResult> GetGroup(int Id)
        {
            try
            {
                var result = await _groupService.GetGroupAsync(Id);

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

        // POST api/<GroupsController>
        [HttpPost("SaveGroup")]
        public async Task<IActionResult> SaveGroup(Group group)
        {
            try
            {
                if(group.CapacityAmps <= 0)
                {
                    return BadRequest("CapacityAmps should be greter than zero");
                }

                var result = await _groupService.SaveGroupAsync(group);
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

        // PUT api/<GroupsController>/5
        [HttpPut("UpdateGroup")]
        public async Task<IActionResult> UpdateGroup(int id, string groupName, int capacity)
        {
            try
            {
                var group = new Group() {  GroupName = groupName, CapacityAmps = capacity };

                var result = await _groupService.UpdateGroupAsync(id, group);
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

        // DELETE api/<GroupsController>/5
        [HttpDelete("DeleteGroup")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            try
            {
                var result = await _groupService.DeleteGroupAsync(id);
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
