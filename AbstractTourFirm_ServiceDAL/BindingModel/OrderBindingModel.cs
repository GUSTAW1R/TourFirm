using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTourFirm_ServiceDAL.BindingModel
{
    public class OrderBindingModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int TravelId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
