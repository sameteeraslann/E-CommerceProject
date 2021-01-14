using CMSProject.Entity.Entities.Interfaces;
using CMSProject.Entity.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMSProject.Entity.Entities.Concrete
{
    public class AppUser : IdentityUser, IBaseEntity
    {
        //IdentityUser => Microsoft hazırladığı bir sınıftır. User ile ilgili işlemleri hızlı ve pratik olması açısından hazır bir yapıdır.


        public string Occupation { get; set; }
        // Occupation => IdentityUser sınıfının içermediği ama projede ihtiyaç duyulan özellikleri buradan eklenebilir.


        private DateTime _createDate = DateTime.Now;
        public DateTime CreateDate { get=> _createDate; set => _createDate = value; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        private Status _status = Status.Active;
        public Status Status { get => _status; set => _status=value; }
    }
}
