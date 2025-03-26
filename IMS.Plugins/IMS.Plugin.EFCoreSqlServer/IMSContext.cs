using IMS.CoreBusiness;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugin.EFCoreSqlServer
{
    public class IMSContext : DbContext
    {
        // this constructor get the connection string from the options in the dbcontext dependency in program.cs file
        // this passes the options to the base class thereby giving it the connection string to operate with
        public IMSContext(DbContextOptions<IMSContext> options): base(options)
        {
            
        }

        public DbSet<Inventory>? Inventories { get; set; }
        public DbSet<Product>? Products { get; set; }

        // when creatin a relationship between two objects, we can simply use the OModelCreating event to
        // specifiy one to one or many to one relationship by specifying how many each has both ways
        // the inventory and product relationship (many to many) so there has be another entity at the middle to establish a many to many relationship
        // which is the ProductInventories object
        public DbSet<ProductInventory>? ProductInventories { get; set; }
        public DbSet<InventoryTransaction>? InventoryTransactions { get; set; }
        public DbSet<ProductTransaction>? ProductTransactions { get; set; }

        // configuring the relationship
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // this create the primary key as a combination of productId and iventoryId
            modelBuilder.Entity<ProductInventory>().HasKey(pi => new { pi.ProductId, pi.InventoryId });

            // this specifies a one to many relatioship between product and productInventories
            modelBuilder.Entity<ProductInventory>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductInventories)
                .HasForeignKey(pi => pi.ProductId);

            // this specifies a one to many relatioship between inventory and productInventories
            modelBuilder.Entity<ProductInventory>()
                .HasOne(pi => pi.Inventory)
                .WithMany(i => i.ProductInventories)
                .HasForeignKey(pi => pi.InventoryId);

            // with setting one to many rlationships from both sides, we have establised a many to many relationship with the help of the productInvrntory object

            // seeding data. it means inserting initial data into the specified tables in the database
            modelBuilder.Entity<Inventory>().HasData(
                new Inventory { InventoryId = 1, InventoryName = "Bike Seat", Quantity = 10, Price = 2 },
                new Inventory { InventoryId = 2, InventoryName = "Bike Body", Quantity = 10, Price = 15 },
                new Inventory { InventoryId = 3, InventoryName = "Bike Wheel", Quantity = 20, Price = 8 },
                new Inventory { InventoryId = 4, InventoryName = "Bike Pedal", Quantity = 20, Price = 1 }
            );
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductName = "Bike", Quantity = 10, Price = 150 },
                new Product { ProductId = 2, ProductName = "Car", Quantity = 10, Price = 25000 }
            );
            modelBuilder.Entity<ProductInventory>().HasData(
                new ProductInventory { ProductId = 1, InventoryId = 1, InventoryQuantity = 1 },
                new ProductInventory { ProductId = 1, InventoryId = 2, InventoryQuantity = 1 },
                new ProductInventory { ProductId = 1, InventoryId = 3, InventoryQuantity = 1 },
                new ProductInventory { ProductId = 1, InventoryId = 4, InventoryQuantity = 1 }
            );

        }
    }
}
