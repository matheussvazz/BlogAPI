using Blog.Data;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        //Recupera as categorias
        [HttpGet("v1/categories")] // localhost:PORT/v1/categories
        public async Task<IActionResult> GetAsync(
        [FromServices] BlogDataContext context)
        {
            var categories = await context.Categories.ToListAsync();
            return Ok(categories);
        }

       // Recupera uma categoria
        [HttpGet("v1/categories/{id:int}")] 
        public async Task<IActionResult> GetByIdAsync(
        [FromRoute] int id, 
        [FromServices] BlogDataContext context)
        {
            var category = await context
                .Categories
                .FirstOrDefaultAsync(x=> x.Id == id);
           
           if (category == null)
             return NotFound();

          return Ok(categories);
        }

       // Cria uma categoria
        [HttpPost("v1/categories")] 
        public async Task<IActionResult> PostAsync(
        [FromBody] Category model, 
        [FromServices] BlogDataContext context)
        {
            context.Categories.Add(model);
            context.SaveChangesAsync();

            return Created($"v1/categories/{model.Id}", model);

        }

        //Atualiza uma categoria
        [HttpPut("v1/categories")] 
        public async Task<IActionResult> PutAsync(
        [FromRoute] Category model, 
        [FromServices] BlogDataContext context)
        {
            var category = await context
                .Categories
                .FirstOrDefaultAsync(x=> x.Id == id);
           
            if (category == null)
              return NotFound();

            category.Name = model.Name;
            category.Slug = model.Slug;
             
            context.Categories.Update(category);
            await context.SaveChangesAsync(category);

            return Created($"v1/categories/{model.Id}", model);
        }


         //Delte uma categoria
        [HttpDelete("v1/categories/{id:int}")] 
        public async Task<IActionResult> DeleteAsync(
        [FromRoute] Category model, 
        [FromServices] BlogDataContext context)
        {
            var category = await context
                .Categories
                .FirstOrDefaultAsync(x=> x.Id == id);
           
            if (category == null)
              return NotFound();

            category.Name = model.Name;
            category.Slug = model.Slug;

            context.Categories.Remove(category);
            await context.SaveChangesAsync(category);

            return Ok(category);
        }
    }
}