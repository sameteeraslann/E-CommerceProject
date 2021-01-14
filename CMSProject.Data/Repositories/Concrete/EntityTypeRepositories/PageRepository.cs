using CMSProject.Data.Context;
using CMSProject.Data.Repositories.Concrete.Base;
using CMSProject.Data.Repositories.Interfaces.EntityTypeRepositories;
using CMSProject.Entity.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMSProject.Data.Repositories.Concrete.EntityTypeRepositories
{
    public class PageRepository : KernelRepository<Page>, IPageRepository
    {
        public PageRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }
    }
}
