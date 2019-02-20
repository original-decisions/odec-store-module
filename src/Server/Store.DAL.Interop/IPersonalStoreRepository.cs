using System;
using System.Collections.Generic;
using odec.Entity.DAL.Interop;
using odec.Server.Model.Store.Helpers.Enums;

namespace odec.Store.DAL.Interop
{
    public interface IPersonalStoreRepository<in TKey, TContext, TStore, TGood,TUser, TStoreFilter, TStoreGoodsFilter> :
        IContextRepository<TContext> 
        where TStore : class
        where TKey : struct
    {
        TGood GetGoodBySerialNumber(TKey userId,TKey storeId,string serialNumber, GoodInitOptions options = GoodInitOptions.Default);
        TGood GetGoodByArticul(TKey userId, TKey storeId, string articul, GoodInitOptions options = GoodInitOptions.Default);
        IEnumerable<TStore> Get(TStoreFilter filter);
        IEnumerable<TGood> GetGoods(TStoreGoodsFilter filter);
        TGood GetGoodById(TUser user, TStore store, TKey goodId, GoodInitOptions options = GoodInitOptions.Default);
        void AddStore(TStore store, TUser user, IEnumerable<Tuple<TGood,int>> goods = null);
        void RemoveStore(TStore store, TUser user);
        void ActivateStore(TStore store, TUser user);
        void DeactivateStore(TStore store, TUser user);
        int GetStoreGoodCount(TStore store, TUser user, TGood good);
        void SetStoreGoodCount(TStore store, TGood good, TUser user, int count);
        void AddGood(TStore store, TGood good,TUser user,int count=1);
        void RemoveGood(TStore store, TGood good, TUser user);
        int ReserveGood(TStore store, TGood good, TUser user, int count = 1);
        int RemoveGoodReservation(TStore store, TGood good, TUser user, int count = 1);
        void IncreaseGoodQuantity(TStore store, TGood good, TUser user, int count = 1);
        void DecreaseGoodQuantity(TStore store, TGood good, TUser user, int count = 1);
    }
}