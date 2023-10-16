using ExpressSupport.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpressSupport.DAL
{
    public class DataBaseContext : DbContext
    {
        //Este constructor crea la referencia de DbContextOptions que sirve para configurar las opciones
        //de la BD, como por ejemplo usar SQL Server y usar la cadena de conexión a la BD
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        public DbSet<CategorySoftware> CategoriesSoftware { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CategorySoftware>().HasIndex(c => c.Name).IsUnique();


            //Con esta línea se controla la duplicidad de la tabla Country
            //modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
        }
        //Creación de la entidad que se va a convertir clase en una tabla de BD.
        //El nombre de la tabla se pondrá en plural
        //public DbSet<Country> Countries { get; set; }
    }
}
