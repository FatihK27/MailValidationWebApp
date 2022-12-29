using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Huawei.WebUIMailValidate.SharedModels
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; } = false;

        public string CreatedBy { get; set; } = null;
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public int? ModifiedBy { get; set; } = null;
        public DateTime? ModifiedAt { get; set; } = null;
        
    }
}
