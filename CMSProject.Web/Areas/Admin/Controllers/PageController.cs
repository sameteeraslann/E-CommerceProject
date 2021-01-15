using CMSProject.Data.Repositories.Interfaces.EntityTypeRepositories;
using CMSProject.Entity.Entities.Concrete;
using CMSProject.Entity.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSProject.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PageController : Controller
    {
        private readonly IPageRepository _repo;

        public PageController(IPageRepository pageRepository)
        {
          _repo = pageRepository;
        }

        public async Task<IActionResult> List()
        {
            return View(await _repo.Get(x => x.Status != Status.Passive));
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create (Page page)
        {
            if(ModelState.IsValid)
            {
                // SLug => bir sayfanın URL'sinde alan adınızdan sonra görünen bir metin bitidir . Temel olarak, sitenizdeki URL’lerin, sitenizdeki her bir sayfayı tanımlayan bölümüdür.
                page.Slug = page.Title.ToLower().Replace(" ", "-");
                var slug = _repo.Any(x => x.Slug == page.Slug);
                if(slug != null)
                {
                    ModelState.AddModelError("", "There is already a page..!");
                    return View(page);
                }
                await _repo.Add(page);
                TempData["Success"] = "The page added..!";
                return RedirectToAction("List");
            }
            else
            {
                TempData["Error"] = "The page hasn't been added..!";
                return RedirectToAction("List");
            }
        }

    }
}
