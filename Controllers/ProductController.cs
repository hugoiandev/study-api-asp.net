using Microsoft.AspNetCore.Mvc;
using study_api.Dtos;
using study_api.Entities;
using study_api.Interfaces;

namespace study_api.Controllers
{
    [ApiController]
    [Route("/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;


        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {

            try
            {
                var products = await _productService.GetAllProducts();

                return Ok(products);
            }
            catch (Exception ex) {

                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(ProductRequestDto productDto)
        {

            try
            {

                var product = await _productService.CreateProduct(new Product
                {
                    Name = productDto.Name,
                    Color = productDto.Color,
                    Price = productDto.Price,
                    Stock = productDto.Stock,
                });

                return Ok(new ProductResponseDto(
                    product.Id,
                    product.Name,
                    product.Color,
                    product.Price,
                    product.Stock
                    ));
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {

            try
            {
                await _productService.DeleteProduct(id);

                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetProductById(int id)
        {

            try
            {
                var product = await _productService.GetProductById(id);

                if ((product is not null))
                {
                    return Ok(new ProductResponseDto(product.Id, product.Name, product.Color, product.Price, product.Stock));
                }
                else
                {
                    return BadRequest("Internal error!");
                }
                                
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProductById(int id, ProductRequestDto productDto)
        {

            try
            {
                var product = await _productService.UpdateProduct(new Product
                {
                    Id = id,
                    Name = productDto.Name,
                    Color = productDto.Color,
                    Price = productDto.Price,
                    Stock = productDto.Stock
                });

                if ((product is not null))
                {
                    return Ok(new ProductResponseDto(product.Id, product.Name, product.Color, product.Price, product.Stock));
                }
                else
                {
                    return BadRequest("Internal error!");
                }

            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
    }
}
