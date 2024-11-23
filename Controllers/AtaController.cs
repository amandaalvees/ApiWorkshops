using Microsoft.AspNetCore.Mvc;
using ApiWorkshops.Data;
using ApiWorkshops.Models;
using System.Linq;

namespace ApiWorkshops.Controllers
{
    [ApiController]
    [Route("api/atas")]
    public class AtaController : ControllerBase
    {
        [HttpPost]
        public IActionResult CriarAta([FromBody] int workshopId)
        {
            var workshop = DataStore.Workshops.FirstOrDefault(w => w.Id == workshopId);
            if (workshop == null)
                return NotFound("Workshop não encontrado.");

            var ata = new Ata { Id = DataStore.Atas.Count + 1, Workshop = workshop };
            DataStore.Atas.Add(ata);
            return CreatedAtAction(nameof(CriarAta), ata);
        }

        [HttpPut("{ataId}/colaboradores/{colaboradorId}")]
        public IActionResult AdicionarColaborador(int ataId, int colaboradorId)
        {
            var ata = DataStore.Atas.FirstOrDefault(a => a.Id == ataId);
            var colaborador = DataStore.Colaboradores.FirstOrDefault(c => c.Id == colaboradorId);

            if (ata == null || colaborador == null)
                return NotFound("Ata ou Colaborador não encontrado.");

            if (!ata.Colaboradores.Contains(colaborador))
                ata.Colaboradores.Add(colaborador);

            return NoContent();
        }

        [HttpDelete("{ataId}/colaboradores/{colaboradorId}")]
        public IActionResult RemoverColaborador(int ataId, int colaboradorId)
        {
            var ata = DataStore.Atas.FirstOrDefault(a => a.Id == ataId);
            var colaborador = DataStore.Colaboradores.FirstOrDefault(c => c.Id == colaboradorId);

            if (ata == null || colaborador == null)
                return NotFound("Ata ou Colaborador não encontrado.");

            ata.Colaboradores.Remove(colaborador);
            return NoContent();
        }

        [HttpGet]
        public IActionResult FiltrarAtas([FromQuery] string workshopNome, [FromQuery] DateTime? data)
        {
            var atas = DataStore.Atas.AsQueryable();

            if (!string.IsNullOrEmpty(workshopNome))
            {
                atas = atas.Where(a => a.Workshop.Nome.Contains(workshopNome, StringComparison.OrdinalIgnoreCase));
            }

            if (data.HasValue)
            {
                atas = atas.Where(a => a.Workshop.DataRealizacao.Date == data.Value.Date);
            }

            var resultado = atas.Select(a => new
            {
                a.Id,
                a.Workshop.Nome,
                a.Workshop.DataRealizacao,
                Colaboradores = a.Colaboradores.Select(c => new { c.Id, c.Nome })
            });

            return Ok(resultado);
        }
    }
}
