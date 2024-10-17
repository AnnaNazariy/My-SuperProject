using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dapper1.Entities;
using Dapper1.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Web.Http;

namespace Dapper1.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(ILogger<ProductController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Product/GetAllProducts
        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsAsync()
        {
            try
            {
                var products = await _unitOfWork._productRepository.GetAllAsync();
                _unitOfWork.Commit();
                _logger.LogInformation("Returned all products from database.");
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllProductsAsync: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // GET: api/Product/GetById/{id}
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Product>> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _unitOfWork._productRepository.GetAsync(id);
                if (product == null)
                {
                    _logger.LogError($"Product with id: {id} not found.");
                    return NotFound();
                }
                _logger.LogInformation($"Returned product with id: {id}");
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetProductByIdAsync: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // POST: api/Product/CreateProduct
        [HttpPost("CreateProduct")]
        public async Task<ActionResult> CreateProductAsync([FromBody] Product newProduct)
        {
            try
            {
                if (newProduct == null)
                {
                    _logger.LogError("Product object is null.");
                    return BadRequest("Product object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid product object.");
                    return BadRequest("Invalid model object");
                }
                var createdId = await _unitOfWork._productRepository.AddAsync(newProduct);
                var createdProduct = await _unitOfWork._productRepository.GetAsync(createdId);
                _unitOfWork.Commit();
                return CreatedAtAction(nameof(GetProductByIdAsync), new { id = createdId }, createdProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateProductAsync: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // PUT: api/Product/UpdateProduct/{id}
        [HttpPut("UpdateProduct/{id}")]
        public async Task<ActionResult> UpdateProductAsync(int id, [FromBody] Product updatedProduct)
        {
            try
            {
                if (updatedProduct == null)
                {
                    _logger.LogError("Product object is null.");
                    return BadRequest("Product object is null");
                }
                var existingProduct = await _unitOfWork._productRepository.GetAsync(id);
                if (existingProduct == null)
                {
                    _logger.LogError($"Product with id: {id} not found.");
                    return NotFound();
                }
                await _unitOfWork._productRepository.ReplaceAsync(updatedProduct);
                _unitOfWork.Commit();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateProductAsync: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // DELETE: api/Product/DeleteProduct/{id}
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<ActionResult> DeleteProductAsync(int id)
        {
            try
            {
                var existingProduct = await _unitOfWork._productRepository.GetAsync(id);
                if (existingProduct == null)
                {
                    _logger.LogError($"Product with id: {id} not found.");
                    return NotFound();
                }
                await _unitOfWork._productRepository.DeleteAsync(id);
                _unitOfWork.Commit();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteProductAsync: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
