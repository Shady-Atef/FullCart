using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductCollectionAggregate
{
    public class SpeceficationType : BaseEntity
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public virtual IReadOnlyList<Specification> Specification => _Specification.Where(s => !s.IsDeleted).ToList();
        private readonly List<Specification> _Specification = new();
    }
}
