using Domain.Common;
using Domain.ProductCollection_Agg;
using Domain.ProductCollection_Agg.Inputs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductCollectionAggregate
{
    public class Specification : BaseEntity
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        [ForeignKey("SpeceficationType")]
        public long ProductSpecsTypeId { get; set; }

        public virtual IReadOnlyList<ProductSpec> ProductSpecs => _ProductSpecs.Where(s => !s.IsDeleted).ToList();
        private readonly List<ProductSpec> _ProductSpecs = new();
        public virtual SpeceficationType SpeceficationType { get; set; }
    }
}
