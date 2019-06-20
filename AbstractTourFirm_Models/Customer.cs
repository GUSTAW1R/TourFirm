using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTourFirm_Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerLogin { get; set; }
        [Required]
        public string CustomerPassword { get; set; }
        [ForeignKey("CustomerId")]
        public virtual List<Order> Orders { get; set; }
    }
}
