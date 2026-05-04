using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactAndAspApp.Server.Models;
using ReactAndAspApp.Server.Services;

namespace ReactAndAspApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerTypesController : ControllerBase
    {
        private readonly ICustomerTypeService _service;
        public CustomerTypesController(ICustomerTypeService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var ct = await _service.GetByIdAsync(id);
            if (ct == null) return NotFound();
            return Ok(ct);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerType ct)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _service.CreateAsync(ct);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerType ct)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                await _service.UpdateAsync(id, ct);
                return NoContent();
            }
            catch (System.ArgumentException) { return NotFound(); }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
