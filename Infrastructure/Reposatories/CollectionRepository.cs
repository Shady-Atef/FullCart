using Domain.ProductCollectionAggregate;
using Domain.ProductCollectionAggregate.Views;
using Domain.ProductCollectionAggregate.Inputs;
using AutoMapper;
using Infrastructure.Data;
using Application.Models;
using Application.Enums;
using Application.Filters;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Reposatories
{
    public class CollectionRepository
    {
        public FullCartContext Context { get; }
        private readonly IMapper _mapper;
        

        public CollectionRepository(FullCartContext _context, AutoMapper.IMapper mapper)
        {
            this.Context = _context;
            this._mapper = mapper;
        }

        public void AddCollection(Product product)
        {
            Context.Products.Add(product);
        }

        public void UpdateCollection(Product product)
        {
            Context.Products.Update(product);
        }

        public Product GetCollectionById(long id)
        {
            return Context.Products.Where(o => o.Id == id).FirstOrDefault();
        }

        public ProductCollectionListDto GetProduct(string id)
        {
            return _mapper.Map<ProductCollectionListDto>( Context.Products
                .Where(p => p.Uuid == id).FirstOrDefault());
        }


        private IQueryable<Product> GetProductsQueryable(CollectionFilter filter)
        {
            var query = Context.Products
                .Where(o => !o.IsDeleted);

            if (!string.IsNullOrEmpty( filter.Category))
            {
                query = query.Where(q => q.ProductSpecs.Where(p => p.Product.ProductCategory.Any(x => x.Uuid == filter.Category)).Any());
            }

            if (!string.IsNullOrEmpty(filter.SubCategory))
            {
                query = query.Where(q => q.ProductSpecs.Where(p => p.Product.ProductCategory.Any(x => x.SubCategory.Uuid == filter.SubCategory)).Any());
            }

            if (filter.PriceFrom.HasValue)
            {
                query = query.Where(q => q.Price >= filter.PriceFrom);
            }

            if (filter.PriceTo.HasValue)
            {
                query = query.Where(q => q.Price <= filter.PriceTo);
            }

            if (filter.Status.HasValue)
            {
                query = query.Where(q => q.AvailabilityStatus == (Status)filter.Status);
            }

            
            return query;
        }

        private IQueryable<Specification> GetProductsSpecsQueryable(CollectionFilter filter)
        {
            var query = Context.Specifications
                .Include(c => c.ProductSpecs)
                .ThenInclude(x => x.Product)
                .Where(x => x.IsDeleted == false);

            if (!string.IsNullOrEmpty(filter.Category))
            {
                query = query.Where(q => q.ProductSpecs.Where(p => p.Product.ProductCategory.Any(x => x.Uuid == filter.Category)).Any());
            }

            if (!string.IsNullOrEmpty(filter.SubCategory))
            {
                query = query.Where(q => q.ProductSpecs.Where(p => p.Product.ProductCategory.Any(x => x.SubCategory.Uuid == filter.SubCategory)).Any());
            }

            if (filter.PriceFrom.HasValue)
            {
                query = query.Where(q => q.ProductSpecs.Where(x => x.Product.Price >= filter.PriceFrom).Any() );
            }

            if (filter.PriceTo.HasValue)
            {
                query = query.Where(q => q.ProductSpecs.Where(x => x.Product.Price >= filter.PriceTo).Any());
            }

            if (filter.Status.HasValue)
            {
                query = query.Where(q => q.ProductSpecs.Where(x => x.Product.AvailabilityStatus == (Status)filter.Status).Any());
            }

            return query;
        }

        public object GetProductsList(CollectionFilter filter)
        {
            throw new NotImplementedException();
        }

        public object GetProductsSpecs(CollectionFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
