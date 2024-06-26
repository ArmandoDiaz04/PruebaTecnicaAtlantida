using System.ComponentModel.DataAnnotations;

namespace API.Core.Domain.Generic
{
    public abstract class BaseDomain
    {
        public int id { get; set; }
        public DateTime fechaCreacion { get; set; } = DateTime.Now;
        [MaxLength(128)]
        public string creadoPor { get; set; }
        public DateTime? fechaModificacion { get; set; }
        [MaxLength(128)]
        public string? modificadoPor { get; set; }
        public string status { get; set; } = "1";
    }
}
