using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RpgApi.Models
{
    public class Disputa
    {
        public int Id { get; set; }
        public DateTime? DataDisputa { get; set; }
        public int AtacanteId { get; set; }
        public int OponenteId { get; set; }
        public string Narracao { get; set; } = string.Empty;
        [NotMapped]
        public int? HabilidadeId { get; set; }
        [NotMapped]
        public List<int>? ListaIdPersonagem { get; set; }
        [NotMapped]
        public List<string>? Resultados { get; set; }
    }
}