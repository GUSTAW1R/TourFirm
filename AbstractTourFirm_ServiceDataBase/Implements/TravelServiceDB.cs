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
    public class TravelServiceDB : ITravelService
    {
        private TourFirmDbContext context;

        public TravelServiceDB(TourFirmDbContext context)
        {
            this.context = context;
        }
        public void AddElement(TravelBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Travel element = context.Travels.FirstOrDefault(rec =>
                   rec.TravelName == model.TravelName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть путешествие с таким названием");
                    }
                    element = new Travel
                    {
                        TravelName = model.TravelName,
                        Final_Cost = model.Final_Cost,
                        Additional_services = model.Additional_services,
                        IsCreadit = model.IsCreadit
                    };
                    context.Travels.Add(element);
                    context.SaveChanges();
                    // убираем дубли по компонентам
                    var groupComponents = model.TourForTravel
                     .GroupBy(rec => rec.TourId)
                    .Select(rec => new
                    {
                        TourId = rec.Key,
                        Count = rec.Sum(r => r.Count)
                    });
                    // добавляем компоненты
                    foreach (var groupComponent in groupComponents)
                    {
                        context.TourForTravels.Add(new TourForTravel
                        {
                            TravelId = element.Id,
                            TourId = groupComponent.TourId,
                            Count = groupComponent.Count
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Travel element = context.Travels.FirstOrDefault(rec => rec.Id ==
                   id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.TourForTravels.RemoveRange(context.TourForTravels.Where(rec =>
                        rec.TravelId == id));
                        context.Travels.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public TravelViewModel GetElement(int id)
        {
            Travel element = context.Travels.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new TravelViewModel
                {
                    Id = element.Id,
                    TravelName = element.TravelName,
                    Final_Cost = element.Final_Cost,
                    Additional_services = element.Additional_services,
                    IsCreadit = element.IsCreadit,
                    TourForTravel = context.TourForTravels
 .Where(recPC => recPC.TravelId == element.Id)
 .Select(recPC => new TourForTravelViewModel
 {
     Id = recPC.Id,
     TravelId = recPC.TravelId,
     TourId = recPC.TourId,
     TourName = recPC.Tour.TourName,
     Count = recPC.Count
 })
 .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public List<TravelViewModel> GetList()
        {
            List<TravelViewModel> result = context.Travels.Select(rec => new
           TravelViewModel
            {
                Id = rec.Id,
                TravelName = rec.TravelName,
                Final_Cost = rec.Final_Cost,
                Additional_services = rec.Additional_services,
                IsCreadit = rec.IsCreadit,
                TourForTravel = context.TourForTravels
            .Where(recPC => recPC.TravelId == rec.Id)
           .Select(recPC => new TourForTravelViewModel
           {
               Id = recPC.Id,
               TravelId = recPC.TravelId,
               TourId = recPC.TourId,
               TourName = recPC.Tour.TourName,
               Count = recPC.Count
           })
           .ToList()
            })
            .ToList();
            return result;
        }

        public void UpdElement(TravelBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Travel element = context.Travels.FirstOrDefault(rec =>
                   rec.TravelName == model.TravelName && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть путешествие с таким названием");
                    }
                    element = context.Travels.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.TravelName = model.TravelName;
                    context.SaveChanges();
                    // обновляем существуюущие компоненты
                    var compIds = model.TourForTravel.Select(rec =>
                   rec.TourId).Distinct();
                    var updateComponents = context.TourForTravels.Where(rec =>
                   rec.TourId == model.Id && compIds.Contains(rec.TourId));
                    foreach (var updateComponent in updateComponents)
                    {
                        updateComponent.Count =
                       model.TourForTravel.FirstOrDefault(rec => rec.Id == updateComponent.Id).Count;
                    }
                    context.SaveChanges();
                    context.TourForTravels.RemoveRange(context.TourForTravels.Where(rec =>
                    rec.TourId == model.Id && !compIds.Contains(rec.TourId)));
                    context.SaveChanges();
                    // новые записи
                    var groupComponents = model.TourForTravel
                    .Where(rec => rec.Id == 0)
                   .GroupBy(rec => rec.TravelId)
                   .Select(rec => new
                   {
                       ComponentId = rec.Key,
                       Count = rec.Sum(r => r.Count)
                   });
                    foreach (var groupComponent in groupComponents)
                    {
                        TourForTravel elementPC =
                       context.TourForTravels.FirstOrDefault(rec => rec.TourId == model.Id &&
                       rec.TourId == groupComponent.ComponentId);
                        if (elementPC != null)
                        {
                            elementPC.Count += groupComponent.Count;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.TourForTravels.Add(new TourForTravel
                            {
                                TravelId = model.Id,
                                TourId = groupComponent.ComponentId,
                                Count = groupComponent.Count
                            });
                            context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
