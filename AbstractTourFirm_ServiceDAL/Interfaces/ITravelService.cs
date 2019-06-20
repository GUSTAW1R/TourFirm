using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractTourFirm_ServiceDAL.BindingModel;
using AbstractTourFirm_ServiceDAL.ViewModel;

namespace AbstractTourFirm_ServiceDAL.Interfaces
{
    public interface ITravelService
    {
        List<TravelViewModel> GetList();
        TravelViewModel GetElement(int id);
        void AddElement(TravelBindingModel model);
        void UpdElement(TravelBindingModel model);
        void DelElement(int id);
    }
}
