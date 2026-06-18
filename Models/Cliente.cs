using System.ComponentModel.DataAnnotations;
namespace TpfinalBack.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        public string Cuit { get; set; } = string.Empty;
        [Required]
        public string Telefono { get; set; } = string.Empty;

        public virtual ICollection<Direccion> Direcciones { get; set; } = new List<Direccion>();
        public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    }
}
