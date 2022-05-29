using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using SysVentas.Products.Application.Categorys;
using SysVentas.Products.Application.Categorys;
using SysVentas.Products.Application.Categorys.Products;

namespace SysVentas.Products.WebApi.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController: Controller
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public IActionResult Post(RegisterCategoryRequest request)
        {
            var response =  _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response = _mediator.Send(new GetCategoryRequest());
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put(UpdateCategoryRequest request)
        {
            var response =  _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("inactive")]
        public IActionResult PutInactive(InactiveCategoryRequest request)
        {
            var response = _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("active")]
        public IActionResult PutActive(ActiveCategoryRequest request)
        {
            var response = _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("product")]
        public IActionResult PostProduct(RegisterProductRequest request)
        {
            var response = _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("product")]
        public IActionResult PutProduct(UpdateProductRequest request)
        {
            var response = _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("product/inactive")]
        public IActionResult PutInactive(InactiveProductRequest request)
        {
            var response = _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("product/active")]
        public IActionResult PutActive(ActiveProductRequest request)
        {
            var response = _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("product/stocks")]
        public IActionResult PutStocks(UpdateProductStoreRequest request)
        {
            var response = _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{idcategory}/product")]
        public IActionResult GetProduct(long idcategory)
        {
            var response = _mediator.Send(new GetProductForCategoryRequest { CategoryId = idcategory});
            return Ok(response);
        }
    }
}
