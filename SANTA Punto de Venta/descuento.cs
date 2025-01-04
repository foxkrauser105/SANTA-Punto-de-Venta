namespace SANTA_Punto_de_Venta
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("descuentos")]
    public partial class descuento
    {
        [Key]
        [StringLength(20)]
        public string id_producto { get; set; }

        public double cantidadMinima { get; set; }

        public decimal? precioDescuento { get; set; }

        public int status { get; set; }

        public virtual producto producto { get; set; }
    }
}
