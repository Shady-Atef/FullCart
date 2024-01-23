using Domain.CustomerAggregate;
using Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Enums;
using Application.Filters;
using Infrastructure.Data;

namespace Infrastructure.Reposatories
{
    public class CustomerRepository : BaseRepository
    {
        private FullCartContext Context { get; }

        public CustomerRepository(FullCartContext _context): base(_context) 
        {
            this.Context = _context;
        }

        public void AddCustomer(Customer product)
        {
            Context.Customers.Add(product);
        }

        public void UpdateCustomer(Customer product)
        {
            Context.Customers.Update(product);
        }

        public Customer GetCustomerById(int id)
        {
            return Context.Customers.Where(o => o.Id == id).FirstOrDefault();
        }

        public Customer GetCustomerData(long id)
        {
           return Context.Customers.Where(c=>c.UserId == id).FirstOrDefault();
        }

       

        //public List<Vw_CustomerList> GetCustomerList(CustomerFilter filter)
        //{
        //    var query =  Context.Vw_CustomerList
        //        .Where(o => !o.IsDeleted);

        //    if (filter.CategoryId.HasValue)
        //    {
        //        query = query.Where(q => q.CategoryId == filter.CategoryId);
        //    }

        //    if (filter.PriceFrom.HasValue)
        //    {
        //        query.Where(q => q.Price >= filter.PriceFrom);
        //    }

        //    if (filter.PriceTo.HasValue)
        //    {
        //         query.Where(q => q.Price <= filter.PriceTo);
        //    }

        //    if (filter.AvailabilityStatus.HasValue)
        //    {
        //        query.Where(q => q.AvailabilityStatus == (AvailabilityStatusEnum)filter.AvailabilityStatus);
        //    }
        //    return query.OrderByDescending(q=>q.NameAr).AsNoTracking().ToList();

        //}

    }
}
