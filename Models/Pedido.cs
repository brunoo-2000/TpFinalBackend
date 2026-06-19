namespace TpfinalBack.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime fechaDespacho { get; set; }
        public decimal MontoTotal { get; set; }
        public bool Confirmado { get; set; }
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; } = null!;
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; } = null!;
        public virtual ICollection<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();
    }
}
