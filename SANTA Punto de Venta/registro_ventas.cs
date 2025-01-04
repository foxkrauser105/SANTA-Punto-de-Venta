namespace SANTA_Punto_de_Venta
{
    using System.ComponentModel.DataAnnotations;

    public partial class registro_ventas
    {
        [Key]
        public int id_registro { get; set; }

        public int id_venta { get; set; }

        [Required]
        [StringLength(20)]
        public string id_producto { get; set; }

        public decimal? precio { get; set; }

        public double cantidad { get; set; }

        public int? descuento { get; set; }

        public int? numcliente { get; set; }

        public int? ncfolio { get; set; }

        public virtual producto producto { get; set; }

        public virtual venta venta { get; set; }
    }
}
