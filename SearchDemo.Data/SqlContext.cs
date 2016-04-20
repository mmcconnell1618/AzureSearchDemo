namespace SearchDemo.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class SqlContext : DbContext
    {
        // Your context has been configured to use a 'SqlContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'SearchDemo.Data.SqlContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'SqlContext' 
        // connection string in the application configuration file.
        public SqlContext()
            : base("name=SqlContext")
        {            
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleFeature> VehicleFeatures { get; set; }
        public DbSet<VehiclePackage> VehiclePackages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.VehicleFeatures)
                .WithRequired(e => e.Vehicle)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.VehiclePackages)
                .WithRequired(e => e.Vehicle)
                .WillCascadeOnDelete(true);
                                        
        }
    }

}