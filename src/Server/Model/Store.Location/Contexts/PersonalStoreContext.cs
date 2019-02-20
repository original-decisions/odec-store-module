﻿using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using odec.CP.Server.Model.Location;
using odec.Server.Model.Location;
using odec.Server.Model.Store;
using odec.Server.Model.Store.Abst.Interfaces;
using odec.Server.Model.Store.Blob;
using odec.Server.Model.Store.Clothes;
using StoreE = odec.Server.Model.Store.Store;
using Categ = odec.Server.Model.Category.Category;
namespace odec.CP.Server.Model.Store.Location.Contexts
{
    public class LocationStoreContext: DbContext, 
        IStoreContext<StoreE, Good, Type, Color, Size, Designer, Material, Categ, RelativeGood, StoreGood, GoodType, GoodColor, GoodMaterial, GoodDesigner, GoodCategory, GoodImage, GoodPriceChange>
    {
        private string LocationScheme = "location";
        private string StoreScheme = "store";
        private string CategoryScheme = "dbo";

        public LocationStoreContext(DbContextOptions<LocationStoreContext> options)
            : base(options)
        {

        }

   
        public DbSet<Good> Goods { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Designer> Designers { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<StoreE> Stores { get; set; }
        public DbSet<Categ> Categories { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<RelativeGood> RelativeGoods { get; set; }
        public DbSet<GoodPriceChange> GoodPriceHistory { get; set; }
        public DbSet<GoodImage> GoodGallery { get; set; }
        public DbSet<GoodColor> GoodColors { get; set; }
        public DbSet<GoodMaterial> GoodMaterials { get; set; }
        public DbSet<StoreGood> StoreGoods { get; set; }
        public DbSet<GoodDesigner> GoodDesigners { get; set; }
        public DbSet<GoodType> GoodTypes { get; set; }
        public DbSet<GoodCategory> GoodCategories { get; set; }
        public DbSet<StoreLocation> StoreLocations { get; set; }
        public DbSet<Scale> Scales { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Categ>()
                .ToTable("Categories", CategoryScheme);
            modelBuilder.Entity<StoreE>()
                .ToTable("Stores", StoreScheme);
            modelBuilder.Entity<Good>()
                .ToTable("Goods", StoreScheme);
            modelBuilder.Entity<GoodPriceChange>()
                .ToTable("GoodPriceHistory", StoreScheme)
                .HasKey(it => new { it.DateChange, it.GoodId });
            modelBuilder.Entity<StoreGood>()
                .ToTable("StoreGoods", StoreScheme)
                .HasKey(it=>new {it.StoreId,it.GoodId});
            modelBuilder.Entity<RelativeGood>()
                .ToTable("RelativeGoods", StoreScheme)
                .HasKey(it => new { it.RelativeGoodId, it.GoodId });
            modelBuilder.Entity<GoodImage>()
                .ToTable("GoodsGallery", StoreScheme)
                .HasKey(it => new { it.ImageId, it.GoodId });
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
            modelBuilder.Entity<StoreLocation>()
                .ToTable("StoreLocations", StoreScheme)
                .HasKey(it => new { it.StoreId, it.LocationId });
            modelBuilder.Entity<Scale>()
                .ToTable("Scales", StoreScheme);
            //Location Scheme
            modelBuilder.Entity<Address>()
               .ToTable("Addresses", LocationScheme);
            modelBuilder.Entity<City>()
                .ToTable("Cities", LocationScheme);
            modelBuilder.Entity<Country>()
                .ToTable("Countries", LocationScheme);
            modelBuilder.Entity<Flat>()
                .ToTable("Flats", LocationScheme);
            modelBuilder.Entity<House>()
                .ToTable("Houses", LocationScheme);
            modelBuilder.Entity<Housing>()
                .ToTable("Housings", LocationScheme);
            modelBuilder.Entity<Street>()
                .ToTable("Streets", LocationScheme);
            modelBuilder.Entity<Subway>()
                .ToTable("Subways", LocationScheme);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            base.OnModelCreating(modelBuilder);
        }

    }
}
