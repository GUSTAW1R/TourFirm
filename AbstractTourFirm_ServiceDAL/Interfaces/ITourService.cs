using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractTourFirm_ServiceDAL.BindingModel;
using AbstractTourFirm_ServiceDAL.ViewModel;

namespace AbstractTourFirm_ServiceDAL.Interfaces
{
    public interface ITourService
    {
        List<TourViewModel> GetList();
        TourViewModel GetElement(int id);
        void AddElement(TourBindingModel model);
        void UpdElement(TourBindingModel model);
        void DelElement(int id);
    }
}
