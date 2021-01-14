using CMSProject.Entity.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMSProject.Data.Repositories.Interfaces.Base
{
     public interface IKernelRepository<T> where T:class, IBaseEntity
    {
        // NOT: Bu projede tamamiyle DIP pattern'a uyulduğundan dolayı oluşturacağımız her Repository için ayrı ayrı interfaceler oluşturuldu. 



        Task<List<T>> GetAll();// Asenkron programlama yapmak istediğimiz methodlarımızı "TASK" olarak işaretlenir.
        Task<List<T>> Get(Expression<Func<T, bool>> expression);// Bir linq to sorgusunu dinamik yapmak için Get methodumuzun içine expression yazdık.
        Task<T> GetById(int id);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> expression);
        Task<bool> Any(Expression<Func<T, bool>> expression);

        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);

    }
}
