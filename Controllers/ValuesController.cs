using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/")]
    //Get api/values
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            _context=context;
        }
        [Authorize]
        [AllowAnonymous]
        [HttpGet]
        public async  Task<IActionResult> GetValues()
        {
            var values= await _context.Values.ToListAsync();
            return Ok(values);
        }
        //Get api/values/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValues(int id)
        {
            var getValue= await _context.Values.Where(x=>x.Id==id).FirstOrDefaultAsync();
            return Ok(getValue);
        }
        //POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

}