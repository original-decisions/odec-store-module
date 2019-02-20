using Microsoft.EntityFrameworkCore;

namespace odec.Server.Model.Store.Abst.Interfaces
{
    public interface IPersonalStoreContext<TStore, TGood, TType, TColor, TSize, TDesigner, TMaterial, TCategory, TRelativeGood, TStoreGood, TGoodType, TGoodColor, TGoodMaterial, TGoodDesigner, TGoodCategory, TGoodImage, TGoodPriceChange, TUserStore> : IStoreContext<TStore, TGood, TType, TColor, TSize, TDesigner, TMaterial, TCategory, TRelativeGood, TStoreGood, TGoodType, TGoodColor, TGoodMaterial, TGoodDesigner, TGoodCategory, TGoodImage, TGoodPriceChange>
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
        where TUserStore : class
    {
        DbSet<TUserStore> UserStores { get; set; }
    }
}