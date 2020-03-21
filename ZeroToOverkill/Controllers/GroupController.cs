using System.Threading;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZeroToOverkill.Mapping;
using ZeroToOverkill.Models;

namespace ZeroToOverkill.Controllers
{
    [Route("Group")]
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> IndexAsync(CancellationToken ct)
        {
            var result = await _groupService.GetAllAsync(ct);
            return View(result.ToViewModel());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> DetailsAsync(long id, CancellationToken ct)
        {
            var group = await _groupService.GetByIdAsync(id, ct);
            if (group == null)
            {
                return NotFound();
            }
            return View(group.ToViewModel());
        }

        [HttpPost]
        [Route("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(long id, GroupViewModel model, CancellationToken ct)
        {
            var group = await _groupService.UpdateAsync(model.ToServiceModel(), ct);
            if (group == null)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(GroupViewModel model, CancellationToken ct)
        {
            await _groupService.AddAsync(model.ToServiceModel(), ct);
            return RedirectToAction("Index");
        }
    }
}