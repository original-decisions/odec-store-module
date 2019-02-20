using System;
using System.Collections.Generic;
using odec.Entity.DAL.Interop;
using odec.Server.Model.Store.Helpers.Enums;

namespace odec.Store.DAL.Interop
{
    public interface IStoreRepository<in TKey, TContext,TStore, TGood, TStoreFilter, TStoreGoodsFilter> : 
        IEntityOperations<TKey,TStore>,
        IActivatableEntity<TKey,TStore>,
        IContextRepository<TContext> where TStore : class 
        where TKey : struct
    {
        TGood GetGoodBySerialNumber(TKey storeId,string serialNumber, GoodInitOptions options = GoodInitOptions.Default);
        TGood GetGoodByArticul(TKey storeId, string articul, GoodInitOptions options = GoodInitOptions.Default);
        IEnumerable<TStore> Get(TStoreFilter filter);
        IEnumerable<TGood> GetGoods(TStoreGoodsFilter filter);
        void AddGood(TStore store, TGood good,int count=1);
        void RemoveGood(TStore store, TGood good);
        void SetStoreGoodCount(TStore store, TGood good, int count);
        int GetStoreGoodCount(TStore store, TGood good);
        void IncreaseGoodQuantity(TStore store, TGood good, int count = 1);
        void DecreaseGoodQuantity(TStore store, TGood good, int count = 1);
        int ReserveGood(TStore store, TGood good, int count = 1);
        int RemoveGoodReservation(TStore store, TGood good, int count = 1);
    }
}