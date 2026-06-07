namespace TpfinalBack.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime fechaDespacho { get; set; }
        public decimal MontoTotal { get; set; }
        public bool Confirmado { get; set; }
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<DetallePedido> Detalles { get; set; }
    }
}
