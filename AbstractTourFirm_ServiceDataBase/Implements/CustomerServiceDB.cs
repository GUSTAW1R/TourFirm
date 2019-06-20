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
    public class CustomerServiceDB : ICustomerService
    {
        private TourFirmDbContext context;

        public CustomerServiceDB(TourFirmDbContext context)
        {
            this.context = context;
        }

        public void AddElement(CustomerBindingModel model)
        {
            Customer element = context.Customers.FirstOrDefault(rec => rec.CustomerName ==
           model.CustomerName);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            context.Customers.Add(new Customer
            {
                CustomerName = model.CustomerName,
                CustomerLogin = model.CustomerLogin,
                CustomerPassword = model.CustomerPassword
            });
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Customer element = context.Customers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Customers.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public CustomerViewModel GetElement(int id)
        {
            Customer element = context.Customers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new CustomerViewModel
                {
                    Id = element.Id,
                    CustomerName = element.CustomerName,
                    CustomerLogin = element.CustomerLogin,
                    CustomerPassword = element.CustomerPassword
                };
            }
            throw new Exception("Элемент не найден");
        }

        public List<CustomerViewModel> GetList()
        {
            List<CustomerViewModel> result = context.Customers.Select(rec => new
           CustomerViewModel
            {
                Id = rec.Id,
                CustomerName = rec.CustomerName,
                CustomerLogin = rec.CustomerLogin
            })
            .ToList();
            return result;
        }

        public void UpdElement(CustomerBindingModel model)
        {
            Customer element = context.Customers.FirstOrDefault(rec => rec.CustomerName ==
           model.CustomerName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = context.Customers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.CustomerName = model.CustomerName;
            element.CustomerLogin = model.CustomerLogin;
            context.SaveChanges();
        }
    }
}
