﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    public class DalBid : IEntity
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime DateOfBid { get; set; }
        public int LotId { get; set; }
        public int UserId { get; set; }
    }
}
