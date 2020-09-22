using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.NETCore_API_REST_Template.Data;
using ASP.NETCore_API_REST_Template.Model;

namespace ASP.NETCore_API_REST_Template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CombinadosController : ControllerBase
    {
        private readonly DataContext _context;

        public CombinadosController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Combinados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Combinado>>> GetCombinados()
        {
            return await _context.Combinados.ToListAsync();
        }

        // GET: api/Combinados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Combinado>> GetCombinado(long id)
        {
            var combinado = await _context.Combinados.FindAsync(id);

            if (combinado == null)
            {
                return NotFound();
            }

            return combinado;
        }

        // PUT: api/Combinados/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCombinado(long id, Combinado combinado)
        {
            if (id != combinado.Id)
            {
                return BadRequest();
            }

            _context.Entry(combinado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CombinadoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Combinados
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Combinado>> PostCombinado()
        {
            Combinado combinado = new Combinado()
            {
                Nombre = "Prueba",
                Users = _context.Users.ToList().ToArray()
            };
            _context.Combinados.Add(combinado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCombinado", new { id = combinado.Id }, combinado);
        }

        // DELETE: api/Combinados/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Combinado>> DeleteCombinado(long id)
        {
            var combinado = await _context.Combinados.FindAsync(id);
            if (combinado == null)
            {
                return NotFound();
            }

            _context.Combinados.Remove(combinado);
            await _context.SaveChangesAsync();

            return combinado;
        }

        private bool CombinadoExists(long id)
        {
            return _context.Combinados.Any(e => e.Id == id);
        }
    }
}
