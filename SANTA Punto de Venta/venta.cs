namespace SANTA_Punto_de_Venta
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("venta")]
    public partial class venta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public venta()
        {
            registro_ventas = new HashSet<registro_ventas>();
        }

        [Key]
        public int id_venta { get; set; }

        [Column("venta")]
        public decimal venta1 { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<registro_ventas> registro_ventas { get; set; }
    }
}
