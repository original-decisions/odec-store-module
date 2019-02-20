using System.Collections.Generic;
using odec.Entity.DAL.Interop;
using odec.Server.Model.Store.Filters;
using odec.Server.Model.Store.Helpers.Enums;

namespace odec.Store.DAL.Interop
{
    public interface IGoodRepository<TKey, TContext, TGood, TType, TSize, TColor, TGoodImage, TGoodsFilter> :
        IEntityOperations<TKey, TGood>,
        IContextRepository<TContext>,
        IActivatableEntity<int, TGood>
        where TGoodsFilter : GoodsFilter<int>
        where TKey : struct
        where TGood : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        TGood GetById(TKey id, GoodInitOptions options);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<TGood> Get(TGoodsFilter filter);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="goodId"></param>
        /// <returns></returns>
        IEnumerable<TType> GetGoodTypes(TKey goodId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="good"></param>
        /// <returns></returns>
        IEnumerable<TType> GetGoodTypes(TGood good);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="goodGeneric"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        IEnumerable<TGood> GetRelativeGoods(TGood goodGeneric, GoodInitOptions options = GoodInitOptions.Default);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="goodId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        IEnumerable<TGood> GetRelativeGoods(TKey goodId, GoodInitOptions options = GoodInitOptions.Default);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="good"></param>
        /// <returns></returns>
        IEnumerable<TColor> GetColors(TGood good);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="goodId"></param>
        /// <returns></returns>
        IEnumerable<TColor> GetColors(TKey goodId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="goodGeneric"></param>
        /// <returns></returns>
        IEnumerable<TGoodImage> GetGallery(TGood goodGeneric);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="goodId"></param>
        /// <returns></returns>
        IEnumerable<TGoodImage> GetGallery(TKey goodId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="good"></param>
        /// <returns></returns>
        TGoodImage GetMainImg(TGood good);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="goodId"></param>
        /// <returns></returns>
        TGoodImage GetMainImg(TKey goodId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="goods"></param>
        /// <param name="options"></param>
        /// <param name="force"></param>
        void InitGoodsSet(List<TGood> goods, GoodInitOptions options = GoodInitOptions.Default, bool force = false);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="goodIds"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        IEnumerable<TGood> InitGoodsSet(List<TKey> goodIds, GoodInitOptions options = GoodInitOptions.Default);

        void AddImage(TGood good, TGoodImage image, bool isSaveNow = false);

        void RemoveImage(TGood good, TGoodImage image, bool isSaveNow = false);
        void AddColor(TGood good, TColor color, bool isSaveNow = false);

        void RemoveColor(TGood good, TColor color, bool isSaveNow = false);
        void AddType(TGood good, TType type, bool isSaveNow = false);

    //    void ClearAllDependencies(TGood good,bool isImmediatelySaved=false);

        void RemoveType(TGood good, TType type, bool isSaveNow = false);
    }
}