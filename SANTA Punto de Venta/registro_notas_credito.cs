namespace SANTA_Punto_de_Venta
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class registro_notas_credito
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int numcliente { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ncfolio { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int detalle { get; set; }

        [Required]
        [StringLength(20)]
        public string id_producto { get; set; }

        public double cantidad { get; set; }

        public decimal precio { get; set; }

        public decimal importe { get; set; }

        public DateTime fechaSurtido { get; set; }

        public int descuento { get; set; }

        public virtual producto producto { get; set; }
    }
}
