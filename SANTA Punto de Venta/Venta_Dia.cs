namespace SANTA_Punto_de_Venta
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class venta_dia
    {
        [Key]
        public int id_ventadia { get; set; }

        public decimal inicio { get; set; }

        public decimal monedas { get; set; }

        public decimal uso_monedas { get; set; }

        public decimal proveedores { get; set; }

        public decimal gasto { get; set; }

        public decimal quedo { get; set; }

        public decimal saldo_inicial { get; set; }

        public decimal saldo_final { get; set; }

        public decimal caja { get; set; }

        public decimal venta_saldo { get; set; }

        public decimal venta_abarrote { get; set; }

        public decimal total { get; set; }

        public decimal final { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha { get; set; }
    }
}
