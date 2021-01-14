using CMSProject.Data.Repositories.Interfaces.Base;
using CMSProject.Entity.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMSProject.Data.Repositories.Interfaces.EntityTypeRepositories
{
    public interface IProductRepository: IKernelRepository<Product>
    {
    }
}
