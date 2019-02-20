using System;
using System.Collections.Generic;
using odec.Entity.DAL.Interop;
using odec.Server.Model.Store.Helpers.Enums;

namespace odec.Store.DAL.Interop
{
    public interface ILocationStoreRepository<in TKey, TContext, TStore,TLocation, TGood, TStoreFilter, TStoreGoodsFilter>
    {
        TGood GetGoodBySerialNumber(TKey userId, TKey storeId, string serialNumber, GoodInitOptions options = GoodInitOptions.Default);
        TGood GetGoodByArticul(TKey userId, TKey storeId, string articul, GoodInitOptions options = GoodInitOptions.Default);
        IEnumerable<TStore> Get(TStoreFilter filter);
        IEnumerable<TGood> GetGoods(TStoreGoodsFilter filter);
        TGood GetGoodById(TLocation location, TStore store, TKey goodId, GoodInitOptions options = GoodInitOptions.Default);
        void AddStore(TStore store, TLocation location, IEnumerable<Tuple<TGood, int>> goods = null);
        void RemoveStore(TStore store, TLocation location);
        void ActivateStore(TStore store, TLocation location);
        void DeactivateStore(TStore store, TLocation location);
        int GetStoreGoodCount(TStore store, TLocation location);
        void SetStoreGoodCount(TStore store, TGood good, TLocation location, int count);
        void AddGood(TStore store, TGood good, TLocation location, int count = 1);
        void RemoveGood(TStore store, TGood good, TLocation location);
        int ReserveGood(TStore store, TGood good, TLocation location, int count = 1);
        int RemoveGoodReservation(TStore store, TGood good, TLocation location, int count = 1);
        void IncreaseGoodQuantity(TStore store, TGood good, TLocation location, int count = 1);
        void DecreaseGoodQuantity(TStore store, TGood good, TLocation location, int count = 1);
    }
}