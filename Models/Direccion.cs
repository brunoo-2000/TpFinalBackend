using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace TpfinalBack.Models
{
    public class Direccion
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Calle { get; set; }
        [Required]
        [MaxLength(10)]
        public string Numero { get; set; }
        [Required]
        [MaxLength(100)]
        public string Ciudad { get; set; }
        [Required]
        public int ClienteId { get; set; }
        [ValidateNever]
        public virtual Cliente? Cliente { get; set; }
    }
}
