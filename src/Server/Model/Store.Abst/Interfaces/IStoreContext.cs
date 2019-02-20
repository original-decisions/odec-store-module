using Microsoft.EntityFrameworkCore;

namespace odec.Server.Model.Store.Abst.Interfaces
{
    //, TManufacturer, TSeason, TAssemblage
    public interface IStoreContext<TStore, TGood, TType, TColor, TSize, TDesigner, TMaterial, TCategory,  TRelativeGood, TStoreGood,TGoodType, TGoodColor, TGoodMaterial, TGoodDesigner, TGoodCategory, TGoodImage, TGoodPriceChange>
        where TGood : class
        where TGoodType : class
        where TRelativeGood : class
        where TGoodImage : class
        where TColor : class
        where TSize : class
        where TDesigner : class
        where TMaterial : class
        where TCategory : class
        where TGoodPriceChange : class
        //    where TManufacturer : class 
        //    where TAssemblage : class 
        //    where TSeason : class 
        where TStore : class 
        where TType : class 
        where TStoreGood : class 
        where TGoodColor : class 
        where TGoodMaterial : class 
        where TGoodCategory : class 
        where TGoodDesigner : class
    {


        DbSet<TGood> Goods { get; set; }
        DbSet<TType> Types { get; set; }
        DbSet<TColor> Colors { get; set; }
        DbSet<TDesigner> Designers { get; set; }
        DbSet<TMaterial> Materials { get; set; }
        DbSet<TStore> Stores { get; set; }
        DbSet<TCategory> Categories { get; set; }
        DbSet<TSize> Sizes { get; set; }
        DbSet<TRelativeGood> RelativeGoods { get; set; }
        DbSet<TGoodPriceChange> GoodPriceHistory { get; set; }
        DbSet<TGoodImage> GoodGallery { get; set; }
        DbSet<TGoodColor> GoodColors { get; set; }
        DbSet<TGoodMaterial> GoodMaterials { get; set; }
        DbSet<TStoreGood> StoreGoods { get; set; }
        DbSet<TGoodDesigner> GoodDesigners { get; set; }
        DbSet<TGoodType> GoodTypes { get; set; }
        DbSet<TGoodCategory> GoodCategories { get; set; }
        //    DbSet<TSeason> Seasons { get; set; } 
        //    DbSet<TManufacturer> Manufacturers { get; set; } 
        //    DbSet<TAssemblage> Assemblages { get; set; }


    }
}