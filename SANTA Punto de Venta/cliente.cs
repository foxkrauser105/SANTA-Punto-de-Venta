namespace SANTA_Punto_de_Venta
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int numcliente { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        [Required]
        [StringLength(30)]
        public string aPaterno { get; set; }

        [StringLength(30)]
        public string aMaterno { get; set; }

        [Required]
        [StringLength(100)]
        public string calle { get; set; }

        public int numeroExt { get; set; }

        public int? numeroInt { get; set; }

        [Required]
        [StringLength(100)]
        public string colonia { get; set; }

        [Required]
        [StringLength(15)]
        public string telefono { get; set; }

        [Required]
        [StringLength(20)]
        public string usuclaveUltAct { get; set; }

        public virtual usuario usuario { get; set; }
    }
}
