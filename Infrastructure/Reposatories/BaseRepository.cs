using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reposatories
{
    public class BaseRepository
    {
        private FullCartContext _context;

        public BaseRepository(FullCartContext context)
        {
            _context = context;
        }
        
    }
}
