using CMSProject.Data.Context;
using CMSProject.Data.Repositories.Concrete.Base;
using CMSProject.Data.Repositories.Interfaces.EntityTypeRepositories;
using CMSProject.Entity.Entities.Concrete;

namespace CMSProject.Data.Repositories.Concrete.EntityTypeRepositories
{
    public class AppUserRepository: KernelRepository<AppUser>, IAppUserRepository
    {
        // DIP Pattern'a göre Repositoryler birbirinden bağımsız olması için "IAppUserRepository" 'den kalıtım aldık ve contructor method ile "ApplicationDbContext" inject edildi. 
        public AppUserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }
       
    }
}
