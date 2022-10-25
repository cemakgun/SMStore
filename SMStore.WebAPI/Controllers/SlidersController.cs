using Microsoft.AspNetCore.Mvc;
using SMStore.Entities;
using SMStore.Service.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SMStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidersController : ControllerBase
    {
        private readonly IRepository<Slider> _repository;

        public SlidersController(IRepository<Slider> repository)
        {
            _repository = repository;
        }

       
        [HttpGet]
        public async Task<IEnumerable<Slider>> GetAsync()
        {
            return await _repository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Slider>> GetAsync(int id) // like findAsync
        {
            var data = await _repository.FindAsync(id);
            if (data is null) return NotFound();
            return data;
        }
      
        [HttpPost]
        public async Task<ActionResult<Slider>> PostAsync([FromBody] Slider entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = entity.Id }, entity); // ekleme işleminden sonra geriye eklenen kaydı döndürür.
        }

      
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Slider entity)
        {
            _repository.Update(entity);
            await _repository.SaveChangesAsync();
            return NoContent(); // Api kullanırken güncelleme işleminden sonra no content dönüş türü kullanılır.

        }

   
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var data = _repository.Find(id);
            _repository.Delete(data);
            await _repository.SaveChangesAsync();
            return Ok(); // Geriye silme başarılı mesajı döndür
        }
    }
}
