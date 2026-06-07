namespace TpfinalBack.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public string Rol {  get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
