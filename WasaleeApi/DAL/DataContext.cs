using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<StoreTiming> StoreTiming { get; set; }
        public DbSet<Cuisine> Cuisine { get; set; }
        public DbSet<StoreCuisine> StoreCuisine { get; set; }
        public DbSet<StoreRating> StoreRating { get; set; }
        //public DbSet<Location> Location { get; set; }
        public DbSet<RequestItem> RequestItem { get; set; }
        public DbSet<RequestItemImages> RequestItemImages { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Store>().OwnsOne(c => c.Location);

            modelBuilder.Entity<RequestItem>()
            .HasMany(a => a.RequestItemImages)
            .WithOne(e => e.RequestItem)
            .IsRequired()
            .HasForeignKey(x => x.RequestItem_Id)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Store>()
               .HasMany(a => a.StoreRatings)
               .WithOne(e => e.Store)
               .IsRequired()
               .HasForeignKey(x => x.Store_Id)
               .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Store>()
               .HasMany(a => a.StoreCuisines)
               .WithOne(e => e.Store)
               .IsRequired()
               .HasForeignKey(x => x.Store_Id)
               .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Cuisine>()
               .HasMany(a => a.StoreCuisine)
               .WithOne(e => e.Cuisine)
               .IsRequired()
               .HasForeignKey(x => x.Cuisine_Id)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Store>()
                .HasMany(a => a.StoreRatings)
                .WithOne(e => e.Store)
                .IsRequired()
                .HasForeignKey(x => x.Store_Id)
                .OnDelete(DeleteBehavior.Cascade);


        }

    }
}
