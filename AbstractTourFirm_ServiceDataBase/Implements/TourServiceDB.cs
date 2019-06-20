using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractTourFirm_Models;
using AbstractTourFirm_ServiceDAL.BindingModel;
using AbstractTourFirm_ServiceDAL.Interfaces;
using AbstractTourFirm_ServiceDAL.ViewModel;

namespace AbstractTourFirm_ServiceDataBase.Implements
{
    public class TourServiceDB : ITourService
    {
        private TourFirmDbContext context;

        public TourServiceDB(TourFirmDbContext context)
        {
            this.context = context;
        }

        public void AddElement(TourBindingModel model)
        {
            Tour element = context.Tours.FirstOrDefault(rec => rec.TourName ==
           model.TourName);
            if (element != null)
            {
                throw new Exception("Уже есть бланк с таким названием");
            }
            context.Tours.Add(new Tour
            {
                TourName = model.TourName,
                Country = model.Country,
                Cost = model.Cost,
                Credit = model.Credit
            });
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Tour element = context.Tours.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Tours.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public TourViewModel GetElement(int id)
        {
            Tour element = context.Tours.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new TourViewModel
                {
                    Id = element.Id,
                    TourName = element.TourName,
                    Country = element.Country,
                    Cost = element.Cost,
                    Credit = element.Credit
                };
            }
            throw new Exception("Элемент не найден");
        }

        public List<TourViewModel> GetList()
        {
            List<TourViewModel> result = context.Tours.Select(rec => new
           TourViewModel
            {
                Id = rec.Id,
                TourName = rec.TourName,
                Country = rec.Country,
                Cost = rec.Cost,
                Credit = rec.Credit
            })
            .ToList();
            return result;
        }

        public void UpdElement(TourBindingModel model)
        {
            Tour element = context.Tours.FirstOrDefault(rec => rec.TourName ==
            model.TourName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть тур с таким названием");
            }
            element = context.Tours.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.TourName = model.TourName;
            context.SaveChanges();
        }
    }
}
