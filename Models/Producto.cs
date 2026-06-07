namespace TpfinalBack.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal CostoUnitario { get; set; }

        public int stock { get; set; }
    }
}
