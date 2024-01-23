using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.ProductCollection_Agg;
using Domain.ProductCollection_Agg.Inputs;
using Domain.ProductCollectionAggregate.Inputs;

namespace Domain.ProductCollectionAggregate
{
    public class Product : BaseEntity
    {
        public string NameAr { get; private set; }
        public string NameEn { get; private set; }
        public string DescriptionEn { get; private set; }
        public string DescriptionAr { get; private set; }
        public string InformationAr { get; private set; }
        public string InformationEn { get; private set; }
        public int TotalQty { get; private set; }
        public decimal Price { get; private set; }
        public decimal DiscountAmount { get;private set; }
        public decimal DiscountPercent { get;private set; }
        public Status AvailabilityStatus { get; private set; }

        public event EventHandler OnEvent;

        [ForeignKey("Producer")]
        public long ProducerId { get; private set; }

        public virtual IReadOnlyList<Attachment> Attachments => _Attachments.Where(s => !s.IsDeleted).ToList();
        private readonly List<Attachment> _Attachments = new();
        public virtual IReadOnlyList<ProductStore> ProductStores => _ProductStores.Where(s => !s.IsDeleted).ToList();
        private readonly List<ProductStore> _ProductStores = new();

        public virtual IReadOnlyList<ProductSpec> ProductSpecs => _ProductSpecs.Where(s => !s.IsDeleted).ToList();
        private readonly List<ProductSpec> _ProductSpecs = new();

        public virtual IReadOnlyList<ProdColl> ProdColl => _ProdColl.Where(s => !s.IsDeleted).ToList();
        private readonly List<ProdColl> _ProdColl = new();

        public virtual IReadOnlyList<ProductCategory> ProductCategory => _ProductCategory.Where(s => !s.IsDeleted).ToList();
        private readonly List<ProductCategory> _ProductCategory = new();


        public void AddNewProduct(ProductInput product, long createdBy)
        {
            this.Id = product.ProductId.Value;
            this.NameAr = product.NameAr;
            this.NameEn = product.NameEn;
            this.Price = product.Price;
            this.DescriptionAr = product.DescriptionAr;
            this.DescriptionEn = product.DescriptionEn;
            this.DiscountAmount = product.DiscountAmount;
            this.DiscountPercent = product.DiscountPercent;
            this.CreationTime = DateTime.Now;
            this.Version++;
            this.CreatedBy = createdBy;
            this.AvailabilityStatus = product.AvailabilityStatus;
            this.InformationAr = product.InformationAr;
            this.InformationEn = product.InformationEn;
            this.ProducerId = product.ProducerId;
            this.TotalQty = product.TotalQty;

            product.Attachments.ForEach(a => _Attachments.Add(new Attachment(a.Path, a.DisplayOrder, createdBy, this.Id)));
            _ProductStores.Add(new ProductStore().CreateNewProduct(product.TotalQty, product.ExpirationDate, product.OriginalPrice));
            product.SubCategories.ForEach(x => _ProductCategory.Add(new ProductCategory().CreateNewProductCategory(product.ProductId,x)));
        }

        

        public void UpdateCollection(ProductInput product, long updatedBy)
        {
            this.NameAr = product.NameAr;
            this.NameEn = product.NameEn;
            this.Price = product.Price;
            this.DescriptionEn = product.DescriptionAr;
            this.DescriptionEn = product.DescriptionEn;
            this.CreationTime = DateTime.Now;
            this.Version++;
            this.DiscountAmount = product.DiscountAmount;
            this.DiscountPercent = product.DiscountPercent;
            this.CreatedBy = updatedBy;
            this.AvailabilityStatus = product.AvailabilityStatus;
            this.InformationEn = product.InformationEn;
            this.ProducerId = product.ProducerId;
            this.TotalQty = product.TotalQty;

            _Attachments.ForEach(a => a.Delete(updatedBy));
            product.Attachments.ForEach(a => _Attachments.Add(new Attachment(a.Path, a.DisplayOrder, updatedBy, this.Id)));

            _ProductStores.Add(new ProductStore().CreateNewProduct(product.TotalQty, product.ExpirationDate, product.OriginalPrice));
            product.SubCategories.ForEach(x => _ProductCategory.Add(new ProductCategory().CreateNewProductCategory(product.ProductId, x)));
        }


        public void DeleteCollection(long deletedBy) 
        {
            this.DeletedBy = deletedBy;
            this.IsDeleted = true;
            this.Version++;
            this.DeletedTime = DateTime.Now;
            this._Attachments.ForEach(r => r.Delete(deletedBy));
            this._ProductStores.ForEach(r => r.Delete(deletedBy));
        }
    }
}
