namespace SANTA_Punto_de_Venta
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class notas_credito
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int numcliente { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ncfolio { get; set; }

        [Required]
        [StringLength(2)]
        public string status { get; set; }

        public DateTime fechaAlta { get; set; }

        [Column(TypeName = "date")]
        public DateTime fechaCompromiso { get; set; }

        public decimal monto { get; set; }

        public decimal montoPagado { get; set; }
    }
}
