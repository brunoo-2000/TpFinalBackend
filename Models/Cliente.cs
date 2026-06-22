using System.ComponentModel.DataAnnotations;

namespace TpfinalBack.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(150, ErrorMessage = "El nombre no puede superar los 150 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El CUIT es obligatorio.")]
        [MaxLength(13, ErrorMessage = "El CUIT no puede superar los 13 caracteres.")]
        [RegularExpression(@"^\d{2}-\d{8}-\d{1}$", ErrorMessage = "El CUIT debe tener el formato 20-12345678-9.")]
        public string Cuit { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [MaxLength(20, ErrorMessage = "El teléfono no puede superar los 20 caracteres.")]
        public string Telefono { get; set; } = string.Empty;

        public virtual ICollection<Direccion> Direcciones { get; set; } = new List<Direccion>();
        public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
