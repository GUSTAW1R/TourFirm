using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTourFirm_Models
{
    public class Tour
    {
        public int Id { get; set; }
        [Required]
        public string TourName { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public decimal Cost { get; set; }
        [Required]
        public bool Credit { get; set; }
        [ForeignKey("TourId")]
        public virtual List<TourForTravel> TourForTravels { get; set; }
    }
}
