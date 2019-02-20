using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using odec.CP.Server.Model.Location;
using odec.Entity.DAL.Interop;
using odec.Server.Model.Store.Blob;
using odec.Server.Model.Store.Filters;
using odec.Server.Model.Store.Helpers.Enums;
using odec.Store.DAL.Interop;

namespace odec.CP.Store.Location.DAL
{
    public class LocationBasedStore: 
        ILocationStoreRepository<int,DbContext,odec.Server.Model.Store.Store,Address,Good,StoreFilter,StoreGoodsFilter<int,int>>,
        IContextRepository<DbContext>
    {
        public Good GetGoodBySerialNumber(int userId, int storeId, string serialNumber,
            GoodInitOptions options = GoodInitOptions.Default)
        {
            throw new NotImplementedException();
        }

        public Good GetGoodByArticul(int userId, int storeId, string articul, GoodInitOptions options = GoodInitOptions.Default)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<odec.Server.Model.Store.Store> Get(StoreFilter filter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Good> GetGoods(StoreGoodsFilter<int, int> filter)
        {
            throw new NotImplementedException();
        }

        public Good GetGoodById(Address location, odec.Server.Model.Store.Store store, int goodId, GoodInitOptions options = GoodInitOptions.Default)
        {
            throw new NotImplementedException();
        }

        public void AddStore(odec.Server.Model.Store.Store store, Address location, IEnumerable<Tuple<Good, int>> goods = null)
        {
            throw new NotImplementedException();
        }

        public void RemoveStore(odec.Server.Model.Store.Store store, Address location)
        {
            throw new NotImplementedException();
        }

        public void ActivateStore(odec.Server.Model.Store.Store store, Address location)
        {
            throw new NotImplementedException();
        }

        public void DeactivateStore(odec.Server.Model.Store.Store store, Address location)
        {
            throw new NotImplementedException();
        }

        public int GetStoreGoodCount(odec.Server.Model.Store.Store store, Address location)
        {
            throw new NotImplementedException();
        }

        public void SetStoreGoodCount(odec.Server.Model.Store.Store store, Good good, Address location, int count)
        {
            throw new NotImplementedException();
        }

        public void AddGood(odec.Server.Model.Store.Store store, Good good, Address location, int count = 1)
        {
            throw new NotImplementedException();
        }

        public void RemoveGood(odec.Server.Model.Store.Store store, Good good, Address location)
        {
            throw new NotImplementedException();
        }

        public int ReserveGood(odec.Server.Model.Store.Store store, Good good, Address location, int count = 1)
        {
            throw new NotImplementedException();
        }

        public int RemoveGoodReservation(odec.Server.Model.Store.Store store, Good good, Address location, int count = 1)
        {
            throw new NotImplementedException();
        }

        public void IncreaseGoodQuantity(odec.Server.Model.Store.Store store, Good good, Address location, int count = 1)
        {
            throw new NotImplementedException();
        }

        public void DecreaseGoodQuantity(odec.Server.Model.Store.Store store, Good good, Address location, int count = 1)
        {
            throw new NotImplementedException();
        }

        public DbContext Db { get; set; }
        public void SetConnection(string connection)
        {
            throw new NotImplementedException();
        }

        public void SetContext(DbContext db)
        {
            throw new NotImplementedException();
        }
    }
}
