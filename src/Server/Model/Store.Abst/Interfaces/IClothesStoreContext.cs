using Microsoft.EntityFrameworkCore;

namespace odec.Server.Model.Store.Abst.Interfaces
{
    public interface IClothesStoreContext<TGood, TType, TRelativeGood, TGoodImage, TColor, TSize, TDesigner, TMaterial,TScale, TAssemblage, TSeason, TCategory>
        //:IStoreContext<TGood, TGoodType, TRelativeGood, TGoodImage, TColor, TSize, TDesigner, TMaterial,TCategory> 
        where TGood : class 
        where TType : class 
        where TRelativeGood : class 
        where TGoodImage : class 
        where TColor : class 
        where TSize : class 
        where TDesigner : class 
        where TMaterial : class where TScale : class 
        where TAssemblage : class 
        where TSeason : class 
        where TCategory : class
    {
        DbSet<TGood> Goods { get; set; }
        DbSet<TType> Types { get; set; }
        DbSet<TColor> Colors { get; set; }
        DbSet<TDesigner> Designers { get; set; }
        DbSet<TMaterial> Materials { get; set; }
        DbSet<TCategory> Categories { get; set; }
        DbSet<TSize> Sizes { get; set; }

        DbSet<TRelativeGood> RelativeGoods { get; set; }
        DbSet<TGoodImage> GoodGallery { get; set; }
        DbSet<TScale> Scales { get; set; }

        DbSet<TAssemblage> Assemblages { get; set; }

        DbSet<TSeason> Seasons { get; set; }

    }

    //public interface IClothesStoreContext<TStore, TGood, TType, TColor, TSize, TDesigner, TMaterial, TCategory, TRelativeGood, TStoreGood, TGoodType, TGoodColor, TGoodMaterial, TGoodDesigner, TGoodCategory, TGoodImage, TGoodPriceChange, TScale, TAssemblage, TSeason>
    //    : IStoreContext<TStore, TGood, TType, TColor, TSize, TDesigner, TMaterial, TCategory, TRelativeGood, TStoreGood, TGoodType, TGoodColor, TGoodMaterial, TGoodDesigner, TGoodCategory, TGoodImage, TGoodPriceChange>
    //    where TGood : class
    //    where TGoodType : class
    //    where TRelativeGood : class
    //    where TGoodImage : class
    //    where TColor : class
    //    where TSize : class
    //    where TDesigner : class
    //    where TMaterial : class where TScale : class
    //    where TAssemblage : class
    //    where TSeason : class
    //    where TCategory : class where TStore : class where TType : class where TStoreGood : class where TGoodColor : class where TGoodMaterial : class where TGoodDesigner : class where TGoodCategory : class where TGoodPriceChange : class
    //{
    //    DbSet<TScale> Scales { get; set; }

    //    DbSet<TAssemblage> Assemblages { get; set; }

    //    DbSet<TSeason> Seasons { get; set; }

    //}
}