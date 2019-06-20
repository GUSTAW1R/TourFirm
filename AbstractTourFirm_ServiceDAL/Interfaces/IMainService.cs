using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractTourFirm_ServiceDAL.BindingModel;
using AbstractTourFirm_ServiceDAL.ViewModel;

namespace AbstractTourFirm_ServiceDAL.Interfaces
{
    public interface IMainService
    {
        List<OrderViewModel> GetList();
        void CreateOrder(OrderBindingModel model);
        void ConfirmOrder(OrderBindingModel model);
        void TakeCreditOnOrder(OrderBindingModel model);
        void PayOrder(OrderBindingModel model);
    }
}
