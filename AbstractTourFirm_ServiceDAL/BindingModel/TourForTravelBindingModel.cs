using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTourFirm_ServiceDAL.BindingModel
{
    public class TourForTravelBindingModel
    {
        public int Id { get; set; }
        public int TravelId { get; set; }
        public int TourId { get; set; }
        public int Count { get; set; }
    }
}
