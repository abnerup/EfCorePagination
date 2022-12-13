using Microsoft.AspNetCore.Mvc;

namespace EfCoreTutorialDotNet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet("{page}")]
        public async Task<ActionResult<List<Product>>> GetProducts(int page) 
        {
            if (_context.Products == null)
            {
                return NotFound();
            }

            var pageResult = 3f;

            var PageCount = Math.Ceiling(await _context.Products.CountAsync() / pageResult);

            var products = await _context.Products
                .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToListAsync();

            var response = new ProductResponse 
            { 
                Products = products,
                CurrentPage = page,
                Pages = (int)PageCount
            };

            return Ok(products);
        }

    
    }
}
