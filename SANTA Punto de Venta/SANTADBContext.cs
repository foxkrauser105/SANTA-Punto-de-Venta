using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace SANTA_Punto_de_Venta
{
    public partial class SANTADBContext : DbContext
    {
        public SANTADBContext()
            : base("name=SANTADBObject")
        {
        }

        public virtual DbSet<cliente> clientes { get; set; }
        public virtual DbSet<descuento> descuentos { get; set; }
        public virtual DbSet<notas_credito> notas_credito { get; set; }
        public virtual DbSet<pagos_notas_credito> pagos_notas_credito { get; set; }
        public virtual DbSet<producto> productos { get; set; }
        public virtual DbSet<registro_notas_credito> registro_notas_credito { get; set; }
        public virtual DbSet<registro_ventas> registro_ventas { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<usuario> usuarios { get; set; }
        public virtual DbSet<venta> ventas { get; set; }
        public virtual DbSet<venta_dia> venta_dia { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cliente>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.aPaterno)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.aMaterno)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.calle)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.colonia)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.usuclaveUltAct)
                .IsUnicode(false);

            modelBuilder.Entity<descuento>()
                .Property(e => e.id_producto)
                .IsUnicode(false);

            modelBuilder.Entity<descuento>()
                .Property(e => e.precioDescuento)
                .HasPrecision(16, 2);

            modelBuilder.Entity<notas_credito>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<notas_credito>()
                .Property(e => e.monto)
                .HasPrecision(16, 2);

            modelBuilder.Entity<notas_credito>()
                .Property(e => e.montoPagado)
                .HasPrecision(16, 2);

            modelBuilder.Entity<pagos_notas_credito>()
                .Property(e => e.importe)
                .HasPrecision(7, 2);

            modelBuilder.Entity<producto>()
                .Property(e => e.id_producto)
                .IsUnicode(false);

            modelBuilder.Entity<producto>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<producto>()
                .Property(e => e.marca)
                .IsUnicode(false);

            modelBuilder.Entity<producto>()
                .Property(e => e.categoria)
                .IsUnicode(false);

            modelBuilder.Entity<producto>()
                .Property(e => e.precio)
                .HasPrecision(16, 2);

            //Tuve que crear estas propiedades para las columnas que tienen constraint de valor por default al insertar
            modelBuilder.Entity<producto>()
                .Property(e => e.status)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            modelBuilder.Entity<producto>()
                .Property(e => e.fechaultact)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            modelBuilder.Entity<producto>()
                .HasOptional(e => e.descuento)
                .WithRequired(e => e.producto)
                .WillCascadeOnDelete();

            modelBuilder.Entity<registro_notas_credito>()
                .Property(e => e.id_producto)
                .IsUnicode(false);

            modelBuilder.Entity<registro_notas_credito>()
                .Property(e => e.precio)
                .HasPrecision(16, 2);

            modelBuilder.Entity<registro_notas_credito>()
                .Property(e => e.importe)
                .HasPrecision(16, 2);

            modelBuilder.Entity<registro_ventas>()
                .Property(e => e.id_producto)
                .IsUnicode(false);

            modelBuilder.Entity<registro_ventas>()
                .Property(e => e.precio)
                .HasPrecision(16, 2);

            modelBuilder.Entity<usuario>()
                .Property(e => e.usuclave)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.aPaterno)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.aMaterno)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .HasMany(e => e.clientes)
                .WithRequired(e => e.usuario)
                .HasForeignKey(e => e.usuclaveUltAct);

            modelBuilder.Entity<venta>()
                .Property(e => e.venta1)
                .HasPrecision(16, 2);

            modelBuilder.Entity<venta_dia>()
                .Property(e => e.inicio)
                .HasPrecision(16, 2);

            modelBuilder.Entity<venta_dia>()
                .Property(e => e.monedas)
                .HasPrecision(16, 2);

            modelBuilder.Entity<venta_dia>()
                .Property(e => e.uso_monedas)
                .HasPrecision(16, 2);

            modelBuilder.Entity<venta_dia>()
                .Property(e => e.proveedores)
                .HasPrecision(16, 2);

            modelBuilder.Entity<venta_dia>()
                .Property(e => e.gasto)
                .HasPrecision(16, 2);

            modelBuilder.Entity<venta_dia>()
                .Property(e => e.quedo)
                .HasPrecision(16, 2);

            modelBuilder.Entity<venta_dia>()
                .Property(e => e.saldo_inicial)
                .HasPrecision(16, 2);

            modelBuilder.Entity<venta_dia>()
                .Property(e => e.saldo_final)
                .HasPrecision(16, 2);

            modelBuilder.Entity<venta_dia>()
                .Property(e => e.caja)
                .HasPrecision(16, 2);

            modelBuilder.Entity<venta_dia>()
                .Property(e => e.venta_saldo)
                .HasPrecision(16, 2);

            modelBuilder.Entity<venta_dia>()
                .Property(e => e.venta_abarrote)
                .HasPrecision(16, 2);

            modelBuilder.Entity<venta_dia>()
                .Property(e => e.total)
                .HasPrecision(16, 2);

            modelBuilder.Entity<venta_dia>()
                .Property(e => e.final)
                .HasPrecision(16, 2);
        }
    }
}
