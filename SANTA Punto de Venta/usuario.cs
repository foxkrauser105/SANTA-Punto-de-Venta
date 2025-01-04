namespace SANTA_Punto_de_Venta
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public usuario()
        {
            clientes = new HashSet<cliente>();
        }

        [Key]
        [StringLength(20)]
        public string usuclave { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        [Required]
        [StringLength(30)]
        public string aPaterno { get; set; }

        [StringLength(30)]
        public string aMaterno { get; set; }

        public byte[] pass { get; set; }

        public DateTime fechaAlta { get; set; }

        public int status { get; set; }

        [StringLength(15)]
        public string telefono { get; set; }

        public DateTime fechaUltAct { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cliente> clientes { get; set; }
    }
}
