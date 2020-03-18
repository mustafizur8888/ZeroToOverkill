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
        public async Task<IActionResult> IndexAsync()
        {
            var result = await _groupService.GetAllAsync();
            return View(result.ToViewModel());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> DetailsAsync(long id)
        {
            var group = await _groupService.GetByIdAsync(id);
            if (group == null)
            {
                return NotFound();
            }
            return View(group.ToViewModel());
        }

        [HttpPost]
        [Route("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(long id, GroupViewModel model)
        {
            var group = await _groupService.UpdateAsync(model.ToServiceModel());
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
        public async Task<IActionResult> CreateAsync(GroupViewModel model)
        {
            await _groupService.AddAsync(model.ToServiceModel());
            return RedirectToAction("Index");
        }
    }
}