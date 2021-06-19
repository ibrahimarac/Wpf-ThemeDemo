using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibrahim.Data.Core.Domain
{
    [Table("Logins")]
    public class LoginUser
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        [Column(TypeName ="varchar")]
        [StringLength(10)]
        public string Username { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(10)]
        public string Password { get; set; }

    }
}
