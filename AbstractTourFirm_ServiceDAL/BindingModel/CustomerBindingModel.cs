using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTourFirm_ServiceDAL.BindingModel
{
    public class CustomerBindingModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLogin { get; set; }
        public string CustomerPassword { get; set; }
    }
}
