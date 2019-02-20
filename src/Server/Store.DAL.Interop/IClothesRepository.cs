using System.Collections.Generic;
using odec.Server.Model.Store.Filters;
using odec.Server.Model.Store.Helpers.Enums;

namespace odec.Store.DAL.Interop
{
    public interface IClothesRepository<TKey, TContext, TGood, TType, TSize, TColor, TGoodImage, TGoodReview, TActions, TSeason, TGoodsFilter> : 
        IGoodRepository<TKey, TContext, TGood, TType, TSize, TColor, TGoodImage, TGoodsFilter>
        where TGoodsFilter : GoodsFilter<int>
        where TKey : struct
        where TGood : class
    {
      
        //TODO:Move to UserStore module
        /// <summary>
        /// 
        /// </summary>
        /// <param name="goodGeneric"></param>
        /// <returns></returns>
        IList<TGoodReview> GetGoodReviews(TGood goodGeneric);
        //TODO:Move to UserStore module
        /// <summary>
        /// 
        /// </summary>
        /// <param name="goodID"></param>
        /// <returns></returns>
        IList<TGoodReview> GetGoodReviews(TKey goodID);

        

        
        //TODO: MOve to auction Module
        /// <summary>
        /// 
        /// </summary>
        /// <param name="goodGeneric"></param>
        /// <returns></returns>
        bool IsGoodDiscounted(TGood goodGeneric);
        //TODO: MOve to auction Module
        bool IsGoodDiscounted(TKey goodId);


        //TODO: MOve to auction Module
        /// <summary>
        /// 
        /// </summary>
        /// <param name="goodGeneric"></param>
        /// <returns></returns>
        IList<TActions> GetGoodActions(TGood goodGeneric);
        //TODO: MOve to auction Module
        /// <summary>
        /// 
        /// </summary>
        /// <param name="goodId"></param>
        /// <returns></returns>
        IList<TActions> GetGoodActions(TKey goodId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="seasonId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        IList<TGood> GetGoodsBySeason(TKey seasonId, GoodInitOptions options = GoodInitOptions.Default);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="goodId"></param>
        /// <returns></returns>
        TSeason GetGoodSeason(TKey goodId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="goodGeneric"></param>
        /// <returns></returns>
        TSeason GetGoodSeason(TGood goodGeneric);
    }
}