using CMSProject.Entity.Entities.Concrete;
using CMSProject.Map.Mapping.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMSProject.Map.Mapping.Concrete
{
    public class AppUserMap:BaseMap<AppUser>
    {  

        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Occupation).IsRequired(true);
            base.Configure(builder);
        }
    }
}
