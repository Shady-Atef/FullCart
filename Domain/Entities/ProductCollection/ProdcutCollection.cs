using Domain.Common;
using Domain.ProductCollection_Agg.Inputs;
using Domain.ProductCollectionAggregate.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductCollectionAggregate
{
    public class ProductCollection : BaseEntity
    {
        public string NameAr { get; private set; }
        public string NameEn { get; private set; }
        public string DescriptionEn { get; private set; }
        public string DescriptionAr { get; private set; }
        public string InformationAr { get; private set; }
        public string InformationEn { get; private set; }
        public int TotalQty { get; private set; }
        public decimal Price { get; private set; }
        public decimal DiscountAmount { get; private set; }
        public decimal DiscountPercent { get; private set; }
        public Status AvailabilityStatus { get; private set; }

        public virtual IReadOnlyList<ProdColl> ProdColl => _ProdColl.Where(s => !s.IsDeleted).ToList();
        private readonly List<ProdColl> _ProdColl = new();

    }
}
