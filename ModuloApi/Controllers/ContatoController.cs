using Microsoft.AspNetCore.Mvc;
using ModuloApi.Context;
using ModuloApi.Entities;

namespace ModuloApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly AgendaContext _context;

        public ContatoController(AgendaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var contatos = _context.Contatos;
            return Ok(contatos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var contato = _context.Contatos.FirstOrDefault(x => x.Id == id);

            if (contato is null)
            {
                return NotFound();
            }

            return Ok(contato);
        }

        [HttpGet("ObterPorNome")]
        public IActionResult GetByName(string name)
        {
            var contato = _context.Contatos.FirstOrDefault(x => x.Nome.Contains(name));

            if (contato is null)
            {
                return NotFound();
            }

            return Ok(contato);
        }

        [HttpPost]
        public IActionResult Post(Contato contato)
        {
            if (contato is null)
            {
                return BadRequest();
            }

            _context.Contatos.Add(contato);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Contato model)
        {
            if (model is null)
            {
                return BadRequest();
            }

            var contato = _context.Contatos.FirstOrDefault(x => x.Id == id);

            if (contato is null)
            {
                return NotFound();
            }

            contato.Nome = model.Nome;
            contato.Telefone = model.Telefone;
            contato.Ativo = model.Ativo;
            
            _context.Update(contato);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var contato = _context.Contatos.FirstOrDefault(x => x.Id == id);

            if (contato is null)
            {
                return NotFound();
            }

            _context.Remove(contato);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
