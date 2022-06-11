using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
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
        public async Task<IActionResult> PostAsync(RegisterCategoryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response =  await _mediator.Send(new GetCategoryRequest());
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateCategoryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("inactive")]
        public async Task<IActionResult> PutInactive(InactiveCategoryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("active")]
        public async Task<IActionResult> PutActive(ActiveCategoryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("product")]
        public async Task<IActionResult> PostProduct(RegisterProductRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("product")]
        public async Task<IActionResult> PutProduct(UpdateProductRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("product/inactive")]
        public async Task<IActionResult> PutInactive(InactiveProductRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("product/active")]
        public async Task<IActionResult> PutActive(ActiveProductRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("product/stocks")]
        public async Task<IActionResult> PutStocks(UpdateProductStoreRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{idcategory}/product")]
        public async Task<IActionResult> GetProduct(long idcategory)
        {
            var response = await _mediator.Send(new GetProductForCategoryRequest { CategoryId = idcategory});
            return Ok(response);
        }

        [HttpGet("all-products")]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _mediator.Send(new GetAllProductsRequest()));
        }

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetProductById(long productId)
        {
            return Ok(await _mediator.Send(new GetProductByIdRequest(productId)));
        }
    }
}
