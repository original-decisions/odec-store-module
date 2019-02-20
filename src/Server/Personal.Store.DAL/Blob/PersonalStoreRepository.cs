using System;
using System.Collections.Generic;
using System.Linq;
#if net452
using System.Transactions;
#endif
using Microsoft.EntityFrameworkCore;
using odec.Framework.Extensions;
using odec.Framework.Logging;
using odec.Server.Model.Personal.Store;
using odec.Server.Model.Personal.Store.Filters;
using odec.Server.Model.Store.Blob;
using odec.Server.Model.Store.Helpers.Enums;
using odec.Server.Model.User;
using odec.Store.DAL.Blob;
using odec.Store.DAL.Interop;

namespace odec.Personal.Store.DAL.Blob
{
    public class PersonalStoreRepository : IPersonalStoreRepository<int, DbContext, Server.Model.Store.Store, Good, User, UserStoreFilter<int>, UserStoreGoodsFilter<int, int, int>>
    {
        private readonly StoreRepository _repository;
        private readonly GoodRepository _goodRepo;

        public PersonalStoreRepository()
        {
            _repository = new StoreRepository(Db);
            _goodRepo = new GoodRepository(Db);
        }

        public PersonalStoreRepository(DbContext db)
        {
            Db = db;
            _repository = new StoreRepository(Db);
            _goodRepo = new GoodRepository(Db);
        }
        public DbContext Db { get; set; }
        public void SetConnection(string connection)
        {
            throw new NotImplementedException();
        }

        public void SetContext(DbContext db)
        {
            Db = db;
        }

        public Good GetGoodBySerialNumber(int userId, int storeId, string serialNumber,
            GoodInitOptions options = GoodInitOptions.Default)
        {
            try
            {
                var userStore = Db.Set<UserPersonalStore>().SingleOrDefault(it => it.StoreId == storeId && it.UserId == userId);
                return userStore == null ? null : _repository.GetGoodBySerialNumber(userStore.StoreId, serialNumber, options);
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public Good GetGoodByArticul(int userId, int storeId, string articul, GoodInitOptions options = GoodInitOptions.Default)
        {
            try
            {
                var userStore = Db.Set<UserPersonalStore>().SingleOrDefault(it => it.StoreId == storeId && it.UserId == userId);
                return userStore == null ? null : _repository.GetGoodByArticul(userStore.StoreId, articul, options);
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public IEnumerable<Server.Model.Store.Store> Get(UserStoreFilter<int> filter)
        {
            try
            {
                var query =
                    from userPersonalStore in Db.Set<UserPersonalStore>()
                    join store in Db.Set<Server.Model.Store.Store>() on userPersonalStore.StoreId equals store.Id
                    where userPersonalStore.UserId == filter.UserId &&
                          (!filter.IsOnlyActive || (userPersonalStore.IsActive && store.IsActive))
                    select store;


                //  var resultQuery = query.Where(it => !filter.IsOnlyActive || (it.IsActive && it.Store.IsActive)).Select(it => it.Store);
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

        public IEnumerable<Good> GetGoods(UserStoreGoodsFilter<int, int, int> filter)
        {
            try
            {
                var query = (
                    from pStore in Db.Set<UserPersonalStore>()
                    join storeGood in Db.Set<StoreGood>() on pStore.StoreId equals storeGood.StoreId
                    join good1 in Db.Set<Good>() on storeGood.GoodId equals good1.Id
                    where pStore.UserId == filter.UserId && pStore.StoreId == filter.StoreId
                          &&
                          (string.IsNullOrEmpty(filter.Articul) ||
                           (!string.IsNullOrEmpty(filter.Articul) && storeGood.Articul == filter.Articul))
                          && storeGood.StoreId == filter.StoreId
                    select good1);

                if (filter.GoodCategoryIds != null && filter.GoodCategoryIds.Any())
                {
                    query = from good in query
                            join goodCategory in Db.Set<GoodCategory>() on good.Id equals goodCategory.GoodId
                            where filter.GoodCategoryIds.Contains(goodCategory.CategoryId)
                            select good;
                }
                if (!string.IsNullOrEmpty(filter.SerialNumber))
                    query = query.Where(it => it.SerialNumber == filter.SerialNumber);

                if (!string.IsNullOrEmpty(filter.Name) && !string.IsNullOrWhiteSpace(filter.Name))
                    query = query.Where(it => it.Name.Contains(filter.Name));


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
                if (filter.BasePriceInterval != null && (filter.BasePriceInterval.End.HasValue || filter.BasePriceInterval.Start.HasValue))
                {
                    if (filter.BasePriceInterval.Start.HasValue && filter.BasePriceInterval.End.HasValue)
                        query = query.Where(it => it.BasePrice >= filter.BasePriceInterval.Start && it.BasePrice <= filter.BasePriceInterval.End);
                    if (filter.BasePriceInterval.End.HasValue && !filter.BasePriceInterval.Start.HasValue)
                        query = query.Where(it => it.BasePrice >= 0 && it.BasePrice <= filter.BasePriceInterval.End);
                    if (filter.BasePriceInterval.Start.HasValue && !filter.BasePriceInterval.End.HasValue)
                        query = query.Where(it => it.BasePrice >= filter.BasePriceInterval.Start);
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
                if (filter.DateCreatedInterval?.Start != null)
                    query = query.Where(it => it.DateCreated >= filter.DateCreatedInterval.Start);
                if (filter.DateCreatedInterval?.End != null)
                    query = query.Where(it => it.DateCreated <= filter.DateCreatedInterval.End);

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
                {
                    _goodRepo.InitGood(good, filter.GoodInitOptions);

                }
                return query;
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public Good GetGoodById(User user, Server.Model.Store.Store store, int goodId, GoodInitOptions options = GoodInitOptions.Default)
        {
            try
            {
                var userStore = (from usrStore in Db.Set<UserPersonalStore>()
                                 join store1 in Db.Set<Server.Model.Store.Store>() on usrStore.StoreId equals store1.Id
                                 where usrStore.UserId == user.Id && usrStore.StoreId == store.Id
                                 select store1).SingleOrDefault();
                if (userStore == null)
                    return null;
                var storeGood = Db.Set<StoreGood>().SingleOrDefault(it => it.GoodId == goodId && it.StoreId == userStore.Id);
                return storeGood == null ? null : _goodRepo.GetById(storeGood.GoodId, options);
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void AddStore(Server.Model.Store.Store store, User user, IEnumerable<Tuple<Good, int>> goods = null)
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
                var userStore =
                    Db.Set<UserPersonalStore>()
                        .SingleOrDefault(it => it.StoreId == store.Id && it.UserId == user.Id);

                if (userStore != null)
                    return;
                var persStore = new UserPersonalStore
                {
                    UserId = user.Id,
                    IsActive = true
                };
                var existingStore = _repository.GetById(store.Id);
                if (existingStore == null)
                {
                    _repository.Save(store);
                    persStore.Store = store;
                }
                else
                    persStore.StoreId = store.Id;

                Db.Set<UserPersonalStore>().Add(persStore);
                Db.SaveChanges();
                if (goods == null)
                {
#if net452
                    scope.Complete();

#endif
                    return;
                }
                foreach (var good in goods)
                    AddGood(store, good.Item1, user, good.Item2);
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

        public void RemoveStore(Server.Model.Store.Store store, User user)
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
                var userStore =
                        Db.Set<UserPersonalStore>()
                            .SingleOrDefault(it => it.StoreId == store.Id && it.UserId == user.Id);
                if (userStore == null)
                    return;
                var storeVictim = Db.Set<Server.Model.Store.Store>().SingleOrDefault(it => it.Id == store.Id);
                userStore.Store = null;
                Db.Set<UserPersonalStore>().Remove(userStore);
                Db.SaveChanges();
                _repository.Delete(storeVictim);
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

        public void ActivateStore(Server.Model.Store.Store store, User user)
        {
            try
            {
                var userStore = Db.Set<UserPersonalStore>().SingleOrDefault(it => it.StoreId == store.Id && it.UserId == user.Id);

                if (userStore == null || userStore.IsActive)
                    return;
                userStore.IsActive = true;
                Db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void DeactivateStore(Server.Model.Store.Store store, User user)
        {
            try
            {
                var userStore = Db.Set<UserPersonalStore>().SingleOrDefault(it => it.StoreId == store.Id && it.UserId == user.Id);

                if (userStore == null || !userStore.IsActive)
                    return;
                userStore.IsActive = false;
                Db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public int GetStoreGoodCount(Server.Model.Store.Store store, User user, Good good)
        {
            try
            {
                LogEventManager.Logger.Info(
                    "GetStoreGoodCount(Server.Model.Store.Store store, User user, Good good) execution started.");
                var userStore = (from usrStore in Db.Set<UserPersonalStore>()
                                 join store1 in Db.Set<Server.Model.Store.Store>() on usrStore.StoreId equals store1.Id
                                 where usrStore.UserId == user.Id && usrStore.StoreId == store.Id
                                 select store1).SingleOrDefault();
                if (userStore == null)
                    throw new ArgumentException("Store doesn't exist");
                return _repository.GetStoreGoodCount(userStore, good);
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
            finally
            {
                LogEventManager.Logger.Info("GetStoreGoodCount(Server.Model.Store.Store store, User user, Good good) execution ended.");
            }
        }

        public void SetStoreGoodCount(Server.Model.Store.Store store, Good good, User user, int count)
        {
            try
            {
                var userStore = (from usrStore in Db.Set<UserPersonalStore>()
                                 join store1 in Db.Set<Server.Model.Store.Store>() on usrStore.StoreId equals store1.Id
                                 where usrStore.UserId == user.Id && usrStore.StoreId == store.Id
                                 select store1).SingleOrDefault();
                if (userStore == null)
                    throw new ArgumentException("Store doesn't exist");
                _repository.SetStoreGoodCount(userStore, good, count);
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void AddGood(Server.Model.Store.Store store, Good good, User user, int count = 1)
        {
            try
            {
                var userStore = Db.Set<UserPersonalStore>().SingleOrDefault(it => it.StoreId == store.Id && it.UserId == user.Id);

#if net452
                using (var scope = new TransactionScope(
                    TransactionScopeOption.Required, new TransactionOptions
                    {
                        IsolationLevel = IsolationLevel.ReadCommitted,
                        Timeout = TimeSpan.FromMinutes(2)
                    }))
                {

#endif
                var storeExists = store.Id != 0 || _repository.Exists(store, e => e.Id == store.Id);
                if (!storeExists)
                    _repository.Save(store);
                _repository.AddGood(store, good, count);

                if (userStore == null)
                    Db.Set<UserPersonalStore>().Add(new UserPersonalStore
                    {
                        IsActive = true,
                        StoreId = store.Id,
                        UserId = user.Id,
                    });
                Db.SaveChanges();
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

        public void RemoveGood(Server.Model.Store.Store store, Good good, User user)
        {
            try
            {
                var userStore = (from usrStore in Db.Set<UserPersonalStore>()
                                 join store1 in Db.Set<Server.Model.Store.Store>() on usrStore.StoreId equals store1.Id
                                 where usrStore.UserId == user.Id && usrStore.StoreId == store.Id
                                 select store1).SingleOrDefault();
                if (userStore == null) return;
                _repository.RemoveGood(userStore, good);
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public int ReserveGood(Server.Model.Store.Store store, Good good, User user, int count = 1)
        {
            try
            {
                var userStore = (from usrStore in Db.Set<UserPersonalStore>()
                                 join store1 in Db.Set<Server.Model.Store.Store>() on usrStore.StoreId equals store1.Id
                                 where usrStore.UserId == user.Id && usrStore.StoreId == store.Id
                                 select store1).SingleOrDefault();
                if (userStore == null)
                    throw new ArgumentException("Store doesn't exist");
                return _repository.ReserveGood(userStore, good, count);

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public int RemoveGoodReservation(Server.Model.Store.Store store, Good good, User user, int count = 1)
        {
            try
            {
                var userStore = (from usrStore in Db.Set<UserPersonalStore>()
                                 join store1 in Db.Set<Server.Model.Store.Store>() on usrStore.StoreId equals store1.Id
                                 where usrStore.UserId == user.Id && usrStore.StoreId == store.Id
                                 select store1).SingleOrDefault(); if (userStore == null)
                    throw new ArgumentException("Store doesn't exist");
                return _repository.RemoveGoodReservation(userStore, good, count);

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void IncreaseGoodQuantity(Server.Model.Store.Store store, Good good, User user, int count = 1)
        {
            try
            {
                var userStore = (from usrStore in Db.Set<UserPersonalStore>()
                                 join store1 in Db.Set<Server.Model.Store.Store>() on usrStore.StoreId equals store1.Id
                                 where usrStore.UserId == user.Id && usrStore.StoreId == store.Id
                                 select store1).SingleOrDefault(); if (userStore == null)
                    throw new ArgumentException("Store doesn't exist");
                _repository.IncreaseGoodQuantity(userStore, good, count);

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void DecreaseGoodQuantity(Server.Model.Store.Store store, Good good, User user, int count = 1)
        {
            try
            {
                var userStore = (from usrStore in Db.Set<UserPersonalStore>()
                                 join store1 in Db.Set<Server.Model.Store.Store>() on usrStore.StoreId equals store1.Id
                                 where usrStore.UserId == user.Id && usrStore.StoreId == store.Id
                                 select store1).SingleOrDefault(); if (userStore == null)
                    throw new ArgumentException("Store doesn't exist");

                _repository.DecreaseGoodQuantity(userStore, good, count);

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
