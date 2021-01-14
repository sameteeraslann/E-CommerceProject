using CMSProject.Data.Context;
using CMSProject.Data.Repositories.Concrete.Base;
using CMSProject.Data.Repositories.Interfaces.EntityTypeRepositories;
using CMSProject.Entity.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMSProject.Data.Repositories.Concrete.EntityTypeRepositories
{
    public class CategoryRepository:KernelRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }
    }
}
