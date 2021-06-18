using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibrahim.Data.Core.Domain
{
    [Table("Settings")]
    public class UserSettings
    {
        [ForeignKey("User")]
        [Key]
        public int UserId { get; set; }

        public User User { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(15)]
        public string Theme { get; set; }
    }
}
