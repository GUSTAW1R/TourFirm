using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTourFirm_ServiceDAL.BindingModel
{
    public class TourBindingModel
    {
        public int Id { get; set; }
        public string TourName { get; set; }
        public string Country { get; set; }
        public decimal Cost { get; set; }
        public bool Credit { get; set; }
    }
}
