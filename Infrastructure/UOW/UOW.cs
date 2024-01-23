using AutoMapper;
using Infrastructure.Data;
using Infrastructure.Reposatories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UOW
{
    public class UOW : IUow
    {
        private FullCartContext _context { get; }
        private IMapper _mapper { get; }


        public OrderRepository  OrderRepo { get; }
        public CollectionRepository CollectionRepo{ get; }
        public LookupRepository LookupRepo{ get; }
        public AccountRepository AccountRepo { get; set; }
        public CustomerRepository CustomerRepo { get; set; }

        public UOW(FullCartContext context, IMapper mapper)
        {
            this._context = context; 
            this._mapper = mapper;
            this.CollectionRepo = new CollectionRepository(_context, _mapper);
            this.AccountRepo = new AccountRepository(_context);
            this.CustomerRepo = new CustomerRepository(_context);
            this.LookupRepo = new LookupRepository(_context);
            this.OrderRepo = new OrderRepository(_context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
