using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractTourFirm_ServiceDAL.BindingModel;
using AbstractTourFirm_ServiceDAL.ViewModel;

namespace AbstractTourFirm_ServiceDAL.Interfaces
{
    public interface ICustomerService
    {
        List<CustomerViewModel> GetList();
        CustomerViewModel GetElement(int id);
        void AddElement(CustomerBindingModel model);
        void UpdElement(CustomerBindingModel model);
        void DelElement(int id);
    }
}
