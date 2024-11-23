using System.Collections.Generic;
using ApiWorkshops.Models;

namespace ApiWorkshops.Data
{
    public static class DataStore
    {
        public static List<Colaborador> Colaboradores { get; set; } = new List<Colaborador>();
        public static List<Workshop> Workshops { get; set; } = new List<Workshop>();
        public static List<Ata> Atas { get; set; } = new List<Ata>();
    }
}
