using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibrahim.Data.Core.Domain
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(30)]
        public string Name { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(30)]
        public string Surname { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(150)]
        public string Email { get; set; }
    }
}
