using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Models
{
    public class BidViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        public int LotId { get; set; }
        public string LotTitle { get; set; }
        public decimal Price { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfBid { get; set; }               
    }
}