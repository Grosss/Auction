using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    public class LotViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "The field can not be empty!")]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Starting price")]
        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "0", "10000000", ErrorMessage = "Invalid price")]
        [Required(ErrorMessage = "The field must be filled completely!")]
        public decimal Price { get; set; }
        public byte[] Image { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Expiration time")]
        [Required(ErrorMessage = "The field can not be empty!")]
        public DateTime ExpirationTime { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "The category is required!")]
        public string CategoryId { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }        
    }
}