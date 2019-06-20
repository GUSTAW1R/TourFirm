using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTourFirm_ServiceDAL.ViewModel
{
    public class TravelViewModel
    {
        public int Id { get; set; }
        public string TravelName { get; set; }
        public bool Additional_services { get; set; }
        public bool IsCreadit { get; set; }
        public decimal Final_Cost { get; set; }
        public List<TourForTravelViewModel> TourForTravel { get; set; }
    }
}
