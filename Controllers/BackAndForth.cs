using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;

namespace OuroborosearchAPI
{
    [Route("api/[controller]")] 
    [ApiController]
    public class SearchController : Controller
    {
        private readonly SearchContext _context;

        public SearchController(SearchContext context)
        {
            _context = context;

            if (_context.SearchSet.Count() == 0)
            {
                _context.SearchSet.Add(new Search { SearchString = "Are there any searches here?" });
                _context.SaveChanges();
            }
        }

        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Search>>> GetSearchItem()
        {
            return await _context.SearchSet.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Search>>> GetSearchItem(string id)
        {
            
            var search = await _context.SearchSet.Where(s => s.SearchString.Contains(id)).ToListAsync();

            if (search == null)
            {
                return NotFound();
            }

            //string jsonSearch = System.Text.Json.JsonSerializer.Serialize(search);

            return search;
        }

        // PUT: api/Todo/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSearchItem(long id, Search search)
        {
            if (id != search.SearchID)
            {
                return BadRequest();
            }

            _context.Entry(search).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SearchItemExists(id))
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

        // POST: api/Todo
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Search>> PostSearchItem(Search search)
        {
            _context.SearchSet.Add(search);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSearchItem", new { id = search.SearchID }, search);
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Search>> DeleteSearchItem(long id)
        {
            var search = await _context.SearchSet.FindAsync(id);
            if (search == null)
            {
                return NotFound();
            }

            _context.SearchSet.Remove(search);
            await _context.SaveChangesAsync();

            return search;
        }

        private bool SearchItemExists(long id)
        {
            return _context.SearchSet.Any(e => e.SearchID == id);
        }
    }
}