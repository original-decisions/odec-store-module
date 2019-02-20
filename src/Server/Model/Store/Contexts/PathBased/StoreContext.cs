using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using odec.Server.Model.Store.Blob;
using odec.Server.Model.Store.Clothes;
using CategoryE = odec.Server.Model.Category.Category;
using Good = odec.Server.Model.Store.PathBased.Good;
using GoodCategory = odec.Server.Model.Store.PathBased.GoodCategory;
using GoodColor = odec.Server.Model.Store.PathBased.GoodColor;
using GoodDesigner = odec.Server.Model.Store.PathBased.GoodDesigner;
using GoodImage = odec.Server.Model.Store.PathBased.GoodImage;
using GoodPriceChange = odec.Server.Model.Store.PathBased.GoodPriceChange;
using GoodType = odec.Server.Model.Store.PathBased.GoodType;
using RelativeGood = odec.Server.Model.Store.PathBased.RelativeGood;
using StoreGood = odec.Server.Model.Store.PathBased.StoreGood;

namespace odec.Server.Model.Store.Contexts.PathBased
{
    public class StoreContext:DbContext//:IStoreContext<Good,GoodType,RelativeGood,GoodImage,Color,Size,Designer,Material, Category.Category, GoodPriceChange>
    {
        private string StoreScheme = "store";
        private string CategoryScheme = "dbo";
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {

        }
        public DbSet<Good> Goods { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<RelativeGood> RelativeGoods { get; set; }
        public DbSet<GoodPriceChange> GoodPriceHistory { get; set; }
        public DbSet<GoodImage> GoodGallery { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Designer> Designers { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<CategoryE> Categories { get; set; }
        public DbSet<StoreGood> StoreGoods { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<GoodDesigner> GoodDesigners { get; set; }
        public DbSet<GoodType> GoodTypes { get; set; }
        public DbSet<GoodCategory> GoodCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: add get from configuration from context
            modelBuilder.Entity<CategoryE>()
                .ToTable("Categories", CategoryScheme);
            modelBuilder.Entity<Store>()
                .ToTable("Stores", StoreScheme);
            modelBuilder.Entity<Good>()
                .ToTable("Goods", StoreScheme);
            modelBuilder.Entity<GoodPriceChange>()
                .ToTable("GoodPriceHistory", StoreScheme)
                .HasKey(it => new { it.DateChange, it.GoodId });
            modelBuilder.Entity<StoreGood>()
                .ToTable("StoreGoods", StoreScheme)
                .HasKey(it => new { it.StoreId, it.GoodId });
            modelBuilder.Entity<RelativeGood>()
                .ToTable("RelativeGoods", StoreScheme)
                .HasKey(it => new { it.RelativeGoodId, it.GoodId });
            modelBuilder.Entity<GoodImage>()
                .ToTable("GoodsGallery", StoreScheme);
            modelBuilder.Entity<Designer>()
                .ToTable("Designers", StoreScheme);
            modelBuilder.Entity<GoodCategory>()
                .ToTable("GoodCategories", StoreScheme)
                .HasKey(it => new { it.CategoryId, it.GoodId });
            modelBuilder.Entity<GoodMaterial>()
                .ToTable("GoodMaterials", StoreScheme)
                .HasKey(it => new { it.MaterialId, it.GoodId });
            modelBuilder.Entity<GoodType>()
                .ToTable("GoodTypes", StoreScheme)
                .HasKey(it => new { it.TypeId, it.GoodId });
            modelBuilder.Entity<GoodDesigner>()
                .ToTable("GoodDesigners", StoreScheme)
                .HasKey(it => new { it.DesignerId, it.GoodId });
            modelBuilder.Entity<GoodColor>()
                .ToTable("GoodColors", StoreScheme)
                .HasKey(it => new { it.ColorId, it.GoodId });
            modelBuilder.Entity<Color>()
                .ToTable("Colors", StoreScheme);
            modelBuilder.Entity<Size>()
                .ToTable("Sizes", StoreScheme);
            modelBuilder.Entity<Material>()
                .ToTable("Materials", StoreScheme);
            modelBuilder.Entity<Type>()
                .ToTable("Types", StoreScheme);
            modelBuilder.Entity<Scale>()
                .ToTable("Scales", StoreScheme);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            base.OnModelCreating(modelBuilder);
        }
    }
}