using System.Collections.Generic;

namespace ApiWorkshops.Models
{
    public class Ata
    {
        public int Id { get; set; }
        public Workshop Workshop { get; set; }
        public List<Colaborador> Colaboradores { get; set; } = new List<Colaborador>();
    }
}
