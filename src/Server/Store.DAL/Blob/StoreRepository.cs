using System;
using System.Collections.Generic;
using System.Linq;
#if net452
using System.Transactions;
#endif
using Microsoft.EntityFrameworkCore;
using odec.Entity.DAL;
using odec.Framework.Logging;
using odec.Framework.Extensions;
using odec.Server.Model.Store.Blob;
using odec.Server.Model.Store.Clothes;
using odec.Server.Model.Store.Filters;
using odec.Server.Model.Store.Helpers.Enums;
using odec.Store.DAL.Interop;
namespace odec.Store.DAL.Blob
{
    public class StoreRepository : OrmEntityOperationsRepository<int, Server.Model.Store.Store, DbContext>, IStoreRepository<int, DbContext, Server.Model.Store.Store, Good, StoreFilter, StoreGoodsFilter<int, int>>
    {
        private readonly GoodRepository _goodRepo;

        public StoreRepository()
        {
            _goodRepo = new GoodRepository(Db);
        }

        public StoreRepository(DbContext db)
        {
            Db = db;
            _goodRepo = new GoodRepository(Db);
        }
        public void SetConnection(string connection)
        {
            throw new NotImplementedException();
        }

        public void SetContext(DbContext db)
        {
            Db = db;
        }

        public Good GetGoodByArticul(int storeId, string articul, GoodInitOptions options = GoodInitOptions.Default)
        {
            try
            {
                var query = Db.Set<StoreGood>();

                
                var good = (from storeGood in query
                    join good1 in Db.Set<Good>() on storeGood.GoodId equals good1.Id

                    where storeGood.Articul == articul && storeGood.StoreId == storeId
                    select good1).FirstOrDefault();

                if (good==null) return null;

                if (options.HasFlag(GoodInitOptions.InitSize))
                    good.Size = GetById<int, Size>(good.SizeId);
                _goodRepo.InitGood(good, options);
                return good;
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }

        }

        public Good GetGoodBySerialNumber(int storeId, string serialNumber, GoodInitOptions options = GoodInitOptions.Default)
        {
            try
            {
                var query = Db.Set<StoreGood>();

                var good = (from storeGood in query
                    join good1 in Db.Set<Good>() on storeGood.GoodId equals good1.Id
                    where good1.SerialNumber == serialNumber && storeGood.StoreId == storeId
                    select good1).FirstOrDefault();

                if (good ==null) return null;
                
                if (options.HasFlag(GoodInitOptions.InitSize))
                    good.Size = GetById<int, Size>(good.SizeId);
                _goodRepo.InitGood(good, options);
                return good;
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }

        }

        public IEnumerable<Server.Model.Store.Store> Get(StoreFilter filter)
        {
            try
            {
                var query = Db.Set<Server.Model.Store.Store>().Where(it => !filter.IsOnlyActive || it.IsActive);
                query = filter.Sord.Equals("desc", StringComparison.OrdinalIgnoreCase)
                    ? query.OrderByDescending(filter.Sidx)
                    : query.OrderBy(filter.Sidx);

                return query.Skip(filter.Rows * (filter.Page - 1)).Take(filter.Rows).Distinct();
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public IEnumerable<Good> GetGoods(StoreGoodsFilter<int, int> filter)
        {
            try
            {
                var query = from sGood in Db.Set<StoreGood>()
                    join good1 in Db.Set<Good>() on sGood.GoodId equals good1.Id
                            where sGood.StoreId == filter.StoreId
                            select good1;





                                   
                if (filter.GoodCategoryIds != null && filter.GoodCategoryIds.Any())
                {
                    query = from good in query
                            join goodCategory in Db.Set<GoodCategory>() on good.Id equals goodCategory.GoodId
                            where filter.GoodCategoryIds.Contains(goodCategory.CategoryId)
                            select good;
                }
                if (filter.GoodTypeIds != null && filter.GoodTypeIds.Any())
                {
                    query = from good in query
                            join goodType in Db.Set<GoodType>() on good.Id equals goodType.GoodId
                            where filter.GoodTypeIds.Contains(goodType.TypeId)
                            select good;
                }

                if (filter.GoodColorIds != null && filter.GoodColorIds.Any())
                {
                    query = from good in query
                            join goodColor in Db.Set<GoodColor>() on good.Id equals goodColor.GoodId
                            where filter.GoodColorIds.Contains(goodColor.ColorId)
                            select good;
                }
                if (filter.GoodDesignerIds != null && filter.GoodDesignerIds.Any())
                {
                    query = from good in query
                            join goodDesigner in Db.Set<GoodDesigner>() on good.Id equals goodDesigner.GoodId
                            where filter.GoodDesignerIds.Contains(goodDesigner.DesignerId)
                            select good;
                }
                if (filter.GoodMaterialsIds != null && filter.GoodMaterialsIds.Any())
                {
                    query = from good in query
                            join goodMaterial in Db.Set<GoodMaterial>() on good.Id equals goodMaterial.GoodId
                            where filter.GoodMaterialsIds.Contains(goodMaterial.MaterialId)
                            select good;
                }
                if (filter.GoodSizeIds != null && filter.GoodSizeIds.Any())
                {
                    query = query.Where(it => filter.GoodSizeIds.Contains(it.SizeId));
                }
                if (filter.GoodHeightInterval != null && (filter.GoodHeightInterval.End.HasValue || filter.GoodHeightInterval.Start.HasValue))
                {
                    if (filter.GoodHeightInterval.Start.HasValue && filter.GoodHeightInterval.End.HasValue)
                        query = query.Where(it => it.Height >= filter.GoodHeightInterval.Start && it.Height <= filter.GoodHeightInterval.End);
                    if (filter.GoodHeightInterval.End.HasValue && !filter.GoodHeightInterval.Start.HasValue)
                        query = query.Where(it => it.Height >= 0 && it.Height <= filter.GoodHeightInterval.End);
                    if (filter.GoodHeightInterval.Start.HasValue && !filter.GoodHeightInterval.End.HasValue)
                        query = query.Where(it => it.Height >= filter.GoodHeightInterval.Start);
                }
                if (filter.GoodWidthInterval != null && (filter.GoodWidthInterval.End.HasValue || filter.GoodWidthInterval.Start.HasValue))
                {
                    if (filter.GoodWidthInterval.Start.HasValue && filter.GoodWidthInterval.End.HasValue)
                        query = query.Where(it => it.Width >= filter.GoodWidthInterval.Start && it.Width <= filter.GoodWidthInterval.End);
                    if (filter.GoodWidthInterval.End.HasValue && !filter.GoodWidthInterval.Start.HasValue)
                        query = query.Where(it => it.Width >= 0 && it.Width <= filter.GoodWidthInterval.End);
                    if (filter.GoodWidthInterval.Start.HasValue && !filter.GoodWidthInterval.End.HasValue)
                        query = query.Where(it => it.Width >= filter.GoodWidthInterval.Start);
                }
                if (filter.GoodDepthInterval != null && (filter.GoodDepthInterval.End.HasValue || filter.GoodDepthInterval.Start.HasValue))
                {
                    if (filter.GoodDepthInterval.Start.HasValue && filter.GoodDepthInterval.End.HasValue)
                        query = query.Where(it => it.Depth >= filter.GoodDepthInterval.Start && it.Depth <= filter.GoodDepthInterval.End);
                    if (filter.GoodDepthInterval.End.HasValue && !filter.GoodDepthInterval.Start.HasValue)
                        query = query.Where(it => it.Depth >= 0 && it.Depth <= filter.GoodDepthInterval.End);
                    if (filter.GoodDepthInterval.Start.HasValue && !filter.GoodDepthInterval.End.HasValue)
                        query = query.Where(it => it.Depth >= filter.GoodDepthInterval.Start);
                }

                query = filter.Sord.Equals("desc", StringComparison.OrdinalIgnoreCase)
                    ? query.OrderByDescending(filter.Sidx)
                    : query.OrderBy(filter.Sidx);

                query = query.Skip(filter.Rows * (filter.Page - 1)).Take(filter.Rows).Distinct();
                //if (filter.GoodInitOptions.HasFlag(GoodInitOptions.InitSize))
                //    query.Include(it => it.Size);
                foreach (var good in query)
                    _goodRepo.InitGood(good, filter.GoodInitOptions);
                
                return query;
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void AddGood(Server.Model.Store.Store store, Good good, int count = 1)
        {
            try
            {
                if (store.Id == 0)
                    throw new ArgumentException("store must be created");
                var sg = Db.Set<StoreGood>().SingleOrDefault(it => it.GoodId == good.Id && it.StoreId == store.Id);
                if (sg != null)
                    sg.StoreQuantity += count;
                else
                {
                    var storeGood = new StoreGood
                    {
                        Articul = good.Articul,
                        //SerialNumber = good.SerialNumber,
                        StoreQuantity = count,
                        StoreId = store.Id
                    };
                    var goodExists = GetById<int, Good>(good.Id);
                    if (goodExists == null)
                    {
                        //TODO:move upper good should be updated. Or create a method for update good.
                        _goodRepo.Save(good);
                        storeGood.Good = good;
                    }
                    else
                    {
                        storeGood.GoodId = good.Id;
                    }

                    Db.Set<StoreGood>().Add(storeGood);
                }
                Db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }

        }

        public void RemoveGood(Server.Model.Store.Store store, Good good)
        {
            try
            {
                if (store.Id == 0)
                    throw new ArgumentException("store must be created");
                var sg = Db.Set<StoreGood>().SingleOrDefault(it => it.GoodId == good.Id && it.StoreId == store.Id);
                if (sg == null) return;
#if net452
                using (var scope = new TransactionScope(
                    TransactionScopeOption.Required, new TransactionOptions
                    {
                        IsolationLevel = IsolationLevel.ReadCommitted,
                        Timeout = TimeSpan.FromMinutes(2)
                    }))
                {

#endif
                    Db.Set<StoreGood>().Remove(sg);
                    Db.SaveChanges();
                    if (!Db.Set<StoreGood>().Any(it => it.GoodId == good.Id))
                    {
                        _goodRepo.Delete(good.Id);
                    }
#if net452
                scope.Complete();
                }
                    
#endif
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void SetStoreGoodCount(Server.Model.Store.Store store, Good good, int count)
        {
            try
            {
                var sg = Db.Set<StoreGood>().SingleOrDefault(it => it.GoodId == good.Id && it.StoreId == store.Id);
                if (sg == null)
                    throw new ArgumentException("Store good must be created");
                sg.StoreQuantity = count;
                Db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public int GetStoreGoodCount(Server.Model.Store.Store store, Good good)
        {
            try
            {
                LogEventManager.Logger.Info("GetStoreGoodCount(Server.Model.Store.Store store, Good good) execution started");
                var sg = Db.Set<StoreGood>().SingleOrDefault(it => it.GoodId == good.Id && it.StoreId == store.Id);
                if (sg == null)
                    throw new ArgumentException("store must be created");
                return sg.StoreQuantity;
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
            finally
            {
                LogEventManager.Logger.Info("GetStoreGoodCount(Server.Model.Store.Store store, Good good) execution ended");
            }
        }

        public void IncreaseGoodQuantity(Server.Model.Store.Store store, Good good, int count = 1)
        {
            try
            {

                var sg = Db.Set<StoreGood>().SingleOrDefault(it => it.GoodId == good.Id && it.StoreId == store.Id);

                if (sg == null)
                    throw new ArgumentException("store must be created");
                sg.StoreQuantity += count;
                Db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void DecreaseGoodQuantity(Server.Model.Store.Store store, Good good, int count = 1)
        {
            try
            {

                var sg = Db.Set<StoreGood>().SingleOrDefault(it => it.GoodId == good.Id && it.StoreId == store.Id);

                if (sg == null)
                    throw new ArgumentException("store must be created");
                var reserved = sg.StoreQuantity >= count ? count : sg.StoreQuantity;
                sg.StoreQuantity -= reserved;
                Db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public int ReserveGood(Server.Model.Store.Store store, Good good, int count = 1)
        {
            try
            {

                var sg = Db.Set<StoreGood>().SingleOrDefault(it => it.GoodId == good.Id && it.StoreId == store.Id);

                if (sg == null)
                    throw new ArgumentException("store must be created");
                var reserved = sg.StoreQuantity >= count ? count : sg.StoreQuantity;
                sg.ReservedCounter += reserved;
                sg.StoreQuantity -= reserved;
                Db.SaveChanges();
                return reserved;
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public int RemoveGoodReservation(Server.Model.Store.Store store, Good good, int count = 1)
        {
            try
            {
                var sg = Db.Set<StoreGood>().SingleOrDefault(it => it.GoodId == good.Id && it.StoreId == store.Id);

                if (sg == null)
                {
                    throw new ArgumentException("store must be created");
                }
                var unreserved = sg.ReservedCounter >= count ? count : sg.ReservedCounter;
                sg.ReservedCounter -= unreserved;
                sg.StoreQuantity += unreserved;
                Db.SaveChanges();
                return unreserved;
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public override void Delete(Server.Model.Store.Store entity)
        {
            try
            {
#if net452
                using (var scope = new TransactionScope(
                    TransactionScopeOption.Required, new TransactionOptions
                    {
                        IsolationLevel = IsolationLevel.ReadCommitted,
                        Timeout = TimeSpan.FromMinutes(2)
                    }))
                {
                    
#endif
                var goods = from sGood in Db.Set<StoreGood>()
                    join good in Db.Set<Good>() on sGood.GoodId equals good.Id
                    where sGood.StoreId == entity.Id
                    select good;

                    if (goods.Any())
                        foreach (var storeGood in goods)
                            RemoveGood(entity, storeGood);
                    //userStore.Store = null;
                    //Db.Set<UserPersonalStore>().Remove(userStore);
                    //Db.SaveChanges();
                    Db.SaveChanges();
                    base.Delete(entity);
#if net452
                scope.Complete();
                }
                
#endif
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
            
        }
    }
}
