using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class BidEntity
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime DateOfBid { get; set; }
        public int LotId { get; set; }
        public int UserId { get; set; }
    }
}
