using System.ComponentModel.DataAnnotations;

namespace TpfinalBack.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El username es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El username no puede superar los 50 caracteres.")]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required(ErrorMessage = "El rol es obligatorio.")]
        public string Rol { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string NombrePila { get; set; } = string.Empty;

        public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
