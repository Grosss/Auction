using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Entities
{
    public partial class User
    {
        public User()
        {
            Bids = new HashSet<Bid>();
            Lots = new HashSet<Lot>();
            Roles = new HashSet<Role>();
        }

        public int UserId { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string Login { get; set; }
        [Required]
        [StringLength(128)]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Money { get; set; }
        
        public virtual ICollection<Bid> Bids { get; set; }
        public virtual ICollection<Lot> Lots { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
