using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibrahim.Data.Core.Domain
{
    [Table("Themes")]
    public class Theme
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(30)]
        public string Name { get; set; }

        public virtual ICollection<UserSettings> Settings { get; set; }

        public Theme()
        {
            Settings = new HashSet<UserSettings>();
        }
    }
}
