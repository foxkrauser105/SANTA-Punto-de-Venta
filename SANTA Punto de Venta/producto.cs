namespace SANTA_Punto_de_Venta
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("productos")]
    public partial class producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public producto()
        {
            registro_notas_credito = new HashSet<registro_notas_credito>();
            registro_ventas = new HashSet<registro_ventas>();
        }

        [Key]
        [StringLength(20)]
        public string id_producto { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string marca { get; set; }

        [Required]
        [StringLength(50)]
        public string categoria { get; set; }

        public double cantidad { get; set; }

        public decimal precio { get; set; }

        //Para productos activos
        public int status { get; set; } = 1;

        public DateTime fechaultact { get; set; }

        public virtual descuento descuento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<registro_notas_credito> registro_notas_credito { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<registro_ventas> registro_ventas { get; set; }
    }
}
