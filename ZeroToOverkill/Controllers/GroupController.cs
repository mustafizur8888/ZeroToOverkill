using System.Threading;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZeroToOverkill.Mapping;
using ZeroToOverkill.Models;

namespace ZeroToOverkill.Controllers
{
    [ApiController]
    [Route("Group")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetllAsync(CancellationToken ct)
        {
            var result = await _groupService.GetAllAsync(ct);
            return Ok(result.ToModel());
        }

        [HttpGet("{id}", Name = nameof(GetByIdAsync))]
        public async Task<IActionResult> GetByIdAsync(long id, CancellationToken ct)
        {
            var group = await _groupService.GetByIdAsync(id, ct);
            if (group == null)
            {
                return NotFound();
            }
            return Ok(group.ToModel());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, GroupModel model, CancellationToken ct)
        {
            var group = await _groupService.UpdateAsync(model.ToServiceModel(), ct);
            return Ok(group.ToModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(GroupModel model, CancellationToken ct)
        {
            var group = await _groupService.AddAsync(model.ToServiceModel(), ct);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = group.Id }, group);
        }
       
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(long id,CancellationToken ct)
        {
         await   _groupService.RemoveAsync(id, ct);
         return NoContent();
        }
    }
}