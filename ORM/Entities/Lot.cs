using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Entities
{
    public partial class Lot
    {
        public Lot()
        {
            Bids = new HashSet<Bid>();
        }

        public int LotId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public byte[] Image { get; set; }
        public DateTime ExpirationTime { get; set; }
        [StringLength(300)]
        public string Description { get; set; }

        public int UserId { get; set; }
        public int CategoryId { get; set; }
                
        public virtual User User { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
    }
}
