using Business.Services;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            return View(_groupService.GetAll().ToViewModel());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Details(long id)
        {
            var group = _groupService.GetById(id);
            if (group == null)
            {
                return NotFound();
            }
            return View(group.ToViewModel());
        }

        [HttpPost]
        [Route("{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, GroupViewModel model)
        {
            var group = _groupService.Update(model.ToServiceModel());
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
        public IActionResult Create(GroupViewModel model)
        {
            _groupService.Add(model.ToServiceModel());
            return RedirectToAction("Index");
        }
    }
}