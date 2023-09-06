using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                var categories = await context.Categories.ToListAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05X04 - Falha interna no servidor");
            }

        }


        // Recupera uma categoria
        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
        [FromRoute] int id,
        [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context
                .Categories
                .FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound();

                return Ok(category);
            }
            catch (Exception e)
            {
                return StatusCode(500, "05X05 - Falha interna no servidor");
            }

        }


        // Cria uma categoria
        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync(
        [FromBody] Category model,
        [FromServices] BlogDataContext context)
        {
            try
            {
                context.Categories.Add(model);
                context.SaveChangesAsync();

                return Created($"v1/categories/{model.Id}", model);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest("05XE9 - Não foi possível incluir a categoria");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05X10 - Falha interna no servidor");
            }

        }

        //Atualiza uma categoria
        [HttpPut("v1/categories")]
        public async Task<IActionResult> PutAsync(
        [FromRoute] int id,
        [FromBody] Category model,
        [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context
                .Categories
                .FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound();

                category.Name = model.Name;
                category.Slug = model.Slug;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return Created($"v1/categories/{model.Id}", model);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest("05XE8 - Não foi possível atualizar a categoria");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05X11 - Falha interna no servidor");
            }


        }


        //Deleta uma categoria
        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
        [FromRoute] int id,
        [FromBody] Category model,
        [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context
                  .Categories
                  .FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound();

                category.Name = model.Name;
                category.Slug = model.Slug;

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return Ok(category);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest("05XE7 - Não foi possível excluir a categoria");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05X12 - Falha interna no servidor");
            }

        }

    }
}
