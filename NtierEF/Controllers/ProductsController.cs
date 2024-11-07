using AutoMapper;
using NtierEF.BLL.DTO.Requests;
using NtierEF.BLL.DTO.Responses;
using NtierEF.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NtierEF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductResponse>> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductRequest request)
        {
            int productId = await _productService.AddAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = productId }, request);
        }

        // PUT: api/Products/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductRequest request)
        {
            await _productService.UpdateAsync(id, request);
            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return NoContent();
        }
    }
}
