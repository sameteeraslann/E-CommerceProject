using CMSProject.Data.Repositories.Interfaces.EntityTypeRepositories;
using CMSProject.Entity.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSProject.Web.Models.Components
{
    public class MainMenuViewComponent :ViewComponent
    {
        private readonly IPageRepository _pageRepo;

        public MainMenuViewComponent(IPageRepository pageRepository) => _pageRepo = pageRepository;

        public async Task<IViewComponentResult> InvokeAsync() => View(await _pageRepo.Get(x => x.Status != Status.Passive));
    }
}
