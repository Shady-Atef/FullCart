using Domain.Common;
using Domain.ProductCollection_Agg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductCollectionAggregate
{
    public class Category : BaseEntity
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }

        public virtual IReadOnlyList<Product> Products => _Products.Where(s => !s.IsDeleted).ToList();
        private readonly List<Product> _Products = new();

        public virtual IReadOnlyList<SubCategory> SubCategory => _SubCategory.Where(s => !s.IsDeleted).ToList();
        private readonly List<SubCategory> _SubCategory = new();
    }
}
