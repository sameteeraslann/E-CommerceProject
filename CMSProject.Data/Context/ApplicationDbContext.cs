using CMSProject.Data.SeedData;
using CMSProject.Entity.Entities.Concrete;
using CMSProject.Map.Mapping.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMSProject.Data.Context
{
    public class ApplicationDbContext: IdentityDbContext <AppUser>
    {
        // IdentyDbContext<AppUser> => AppUser Entity katmanında IdentytyUser olarak belirlendiğnden dolayı Context işlemleri gerçekleşti ve bir daha burada "DbSet<AppUser>" olarak ekstra belirtmedik.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Page>  Pages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new AppUserMap());
            builder.ApplyConfiguration(new PageMap());
            builder.ApplyConfiguration(new ProductMap());

            builder.ApplyConfiguration(new SeedPage());

            base.OnModelCreating(builder);
        }
    }
}
