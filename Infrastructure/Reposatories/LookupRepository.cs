using Application.Models;
using Domain.ProductCollectionAggregate;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reposatories
{
    public class LookupRepository : BaseRepository
    {
        public FullCartContext Context { get; }

        public LookupRepository(FullCartContext context) : base(context)
        {
            this.Context = context;
        }


        public List<CategoryDto> GetAllCategories()
        {
            return   Context.Categories.Where( c => !c.IsDeleted).AsNoTracking()
                .Select(c => new CategoryDto
                {
                    ChildrenCat = c.SubCategory.Select(x => new SubCategoryDto 
                    { 
                         NameAr = x.NameAr,
                         Id = x.Uuid,
                         NameEn = x.NameEn,
                    }),
                    Id = c.Uuid,
                    ParentEn = c.NameEn,
                    ParentAr = c.NameAr,
                    ProductType = "s", // for testing
                    Status = "ss" // for test
                })
                .ToList();
        }
    }
}
