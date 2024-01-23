using AutoMapper;
using Infrastructure.UOW;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Filters;
using Microsoft.AspNetCore.Authorization;

namespace FullCart.Server.Controllers.Admin.ProducutCollectionCTRL
{
    //[Authorize]
    [Route("api/ProductCollection")]
    [ApiController]
    public class ProductCollectionController : BaseController
    {
        readonly IMapper _mapper;
        UOW _UOW { get; }
        IMediator Mediatr { get; }
        public ProductCollectionController(ILogger<BaseController> logger, UOW uow, IMapper mapper, IMediator Mediatr) : base(logger)
        {
            this._mapper = mapper;
            this._UOW = uow;
            this.Mediatr = Mediatr;
        }

        [HttpPost]
        [Route("GetProductsList")]
        public IActionResult GetProductsList(CollectionFilter filter)
        {
            var collection = _UOW.CollectionRepo.GetProductsList(filter);
            return Ok(collection);
        }

        [HttpPost]
        [Route("GetProductSpecs")]
        public IActionResult GetProductSpecs(CollectionFilter filter)
        {
            var collection = _UOW.CollectionRepo.GetProductsSpecs(filter);
            return Ok(collection);
        }

        [HttpGet]
        [Route("GetProduct")]
        public async Task<IActionResult> GetProduct([FromQuery] string id)
        {
            var collection = _UOW.CollectionRepo.GetProduct(id);
            return Ok(collection);
        }

        private void OnAggregateEvent(object sender, EventArgs e)
        {
            if (Mediatr.Publish(e) is var mediatrTask && mediatrTask.IsFaulted) throw mediatrTask.Exception;
        }
    }
}
