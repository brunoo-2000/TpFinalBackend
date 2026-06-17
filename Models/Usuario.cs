using System.ComponentModel.DataAnnotations;
namespace TpfinalBack.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Rol {  get; set; } = string.Empty;
        public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    }
}
