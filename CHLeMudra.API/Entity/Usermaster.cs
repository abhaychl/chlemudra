using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHLeMudra.Api.Entity
{
    public class Usermaster : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Role { get; set; }


        [NotMapped]
        public string OldUserName { get; set; }
       

        
    }


    public class TokenProxy
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class ChangePassword
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
    public class ProfilePictureModel
    {
        public int UserId { get; set; }
        public string Base64str { get; set; }

    }
}
