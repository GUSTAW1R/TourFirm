using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTourFirm_Models
{
    public class Travel
    {
        public int Id { get; set; }
        [Required]
        public string TravelName { get; set; }
        [Required]
        public bool Additional_services { get; set; }
        public bool IsCreadit { get; set; }
        public decimal Final_Cost { get; set; }
        [ForeignKey("TravelId")]
        public virtual List<TourForTravel> TourForTravels { get; set; }
        [ForeignKey("TravelId")]
        public virtual List<Order> Orders { get; set; }
    }
}
