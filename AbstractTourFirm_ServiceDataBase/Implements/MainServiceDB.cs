using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractTourFirm_Models;
using AbstractTourFirm_ServiceDAL.BindingModel;
using AbstractTourFirm_ServiceDAL.Interfaces;
using AbstractTourFirm_ServiceDAL.ViewModel;

namespace AbstractTourFirm_ServiceDataBase.Implements
{
    public class MainServiceDB : IMainService
    {
        private TourFirmDbContext context;

        public MainServiceDB(TourFirmDbContext context)
        {
            this.context = context;
        }

        public void ConfirmOrder(OrderBindingModel model)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.Id ==
                    model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != OrderStatus.Рассматривается)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }

            element.DateImplement = DateTime.Now;
            element.Status = OrderStatus.Готов_и_ждёт_оплаты;
            context.SaveChanges();
        }

        public void CreateOrder(OrderBindingModel model)
        {
            context.Orders.Add(new Order
            {
                CustomerId = model.CustomerId,
                TravelId = model.TravelId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = OrderStatus.Рассматривается
            });
            context.SaveChanges();
        }

        public List<OrderViewModel> GetList()
        {
            List<OrderViewModel> result = context.Orders.Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                CustomerId = rec.CustomerId,
                TravelId = rec.TravelId,
                DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
            SqlFunctions.DateName("mm", rec.DateCreate) + " " +
            SqlFunctions.DateName("yyyy", rec.DateCreate),
                DateImplement = rec.DateImplement == null ? "" :
            SqlFunctions.DateName("dd",
           rec.DateImplement.Value) + " " +
            SqlFunctions.DateName("mm",
           rec.DateImplement.Value) + " " +
            SqlFunctions.DateName("yyyy",
           rec.DateImplement.Value),
                Status = rec.Status.ToString(),
                Count = rec.Count,
                Sum = rec.Sum,
                CustomerName = rec.Customer.CustomerName,
                TravelName = rec.Travel.TravelName
            })
            .ToList();
            return result;
        }

        public void PayOrder(OrderBindingModel model)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != OrderStatus.Готов_и_ждёт_оплаты)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            if(element.Status == OrderStatus.Готов_и_ждёт_оплаты || element.Status == OrderStatus.В_кредите)
            element.Status = OrderStatus.Оплачен;
            context.SaveChanges();
        }

        public void TakeCreditOnOrder(OrderBindingModel model)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != OrderStatus.Рассматривается)
            {
                throw new Exception("Заказ не в статусе \"Рассматривается\"");
            }
            element.Status = OrderStatus.В_кредите;
            context.SaveChanges();
        }
    }
}
