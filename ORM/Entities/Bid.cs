using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Entities
{
    public partial class Bid
    {
        public Bid() { }

        public int BidId { get; set; } 
        public decimal Price { get; set; }
        public DateTime DateOfBid { get; set; }

        public int LotId { get; set; }
        public int UserId { get; set; }

        public virtual Lot Lot { get; set; }
        public virtual User User { get; set; }
    }
}
