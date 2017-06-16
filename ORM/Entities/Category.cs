using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Entities
{
    public partial class Category
    {
        public Category()
        {
            Lots = new HashSet<Lot>();
        }

        public int CategoryId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        public virtual ICollection<Lot> Lots { get; set; }
    }
}
