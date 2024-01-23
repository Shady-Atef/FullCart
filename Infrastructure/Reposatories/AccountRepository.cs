using Domain.Entities.UserAggregate;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reposatories
{
    public class AccountRepository : BaseRepository
    {
        public FullCartContext Context { get; }

        public AccountRepository(FullCartContext _context) : base(_context)
        {
            this.Context = _context;
        }
        public ApplicationUser FindAccount(string? userName)
        {
            return Context.Users
                   .FirstOrDefault(s => !s.IsDeleted && s.UserName == userName);
        }

        //public int GetRoleTypeId(long id)
        //{
        //    return Context.Roles.Where(r => r.Id == id).FirstOrDefault();
        //}

        public ApplicationUser GetUserData(long id)
        {
            return Context.Users.Where(u => !u.IsDeleted && u.Id == id).FirstOrDefault();
        }

        public void AddAccount(ApplicationUser account)
        {
            Context.Users.Add(account);
        }

        public bool IsUsernameExist(string username)
        {
            return Context.Users.Any(u => u.UserName == username);
        }
    }
}
