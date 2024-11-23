using Microsoft.AspNetCore.Mvc;
using ApiWorkshops.Data;
using ApiWorkshops.Models;

namespace ApiWorkshops.Controllers
{
    [ApiController]
    [Route("api/workshops")]
    public class WorkshopController : ControllerBase
    {
        [HttpPost]
        public IActionResult CadastrarWorkshop([FromBody] Workshop workshop)
        {
            workshop.Id = DataStore.Workshops.Count + 1;
            DataStore.Workshops.Add(workshop);
            return CreatedAtAction(nameof(CadastrarWorkshop), workshop);
        }
    }
}
