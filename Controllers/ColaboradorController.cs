using Microsoft.AspNetCore.Mvc;
using ApiWorkshops.Data;
using ApiWorkshops.Models;
using System.Linq;

namespace ApiWorkshops.Controllers
{
    [ApiController]
    [Route("api/colaboradores")]
    public class ColaboradorController : ControllerBase
    {
        [HttpPost]
        public IActionResult CadastrarColaborador([FromBody] Colaborador colaborador)
        {
            colaborador.Id = DataStore.Colaboradores.Count + 1;
            DataStore.Colaboradores.Add(colaborador);
            return CreatedAtAction(nameof(CadastrarColaborador), colaborador);
        }

        [HttpGet]
        public IActionResult ListarColaboradores()
        {
            var resultado = DataStore.Colaboradores
                .OrderBy(c => c.Nome)
                .Select(c => new
                {
                    c.Id,
                    c.Nome,
                    Workshops = DataStore.Atas
                        .Where(a => a.Colaboradores.Contains(c))
                        .Select(a => new { a.Workshop.Id, a.Workshop.Nome, a.Workshop.DataRealizacao })
                });

            return Ok(resultado);
        }
    }
}
