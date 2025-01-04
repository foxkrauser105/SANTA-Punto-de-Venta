using System.Data.Entity;

namespace SANTA_Punto_de_Venta
{
    public class ProductContext : DbContext
    {
        public DbSet<producto> Products { get; set; }
    }
}
