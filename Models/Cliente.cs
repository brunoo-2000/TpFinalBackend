namespace TpfinalBack.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Cuit {  get; set; }
        public string Telefono { get; set; }

        public virtual ICollection<Direccion> Direcciones { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }

    }
}
