using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using NUnit.Framework;
using odec.Framework.Logging;
using odec.Personal.Store.DAL.Blob;
using odec.Server.Model.Personal.Store.Contexts;
using odec.Server.Model.Personal.Store.Filters;
using odec.Server.Model.Store.Blob;
using odec.Server.Model.User;
using StoreM = odec.Server.Model.Store.Store;
//using IStoreRepo = odec.Store.DAL.Interop.IPersonalStoreRepository<int, System.Data.Entity.DbContext, odec.Server.Model.Store.Store, odec.Server.Model.Store.Blob.Good, odec.Server.Model.User.User, odec.Server.Model.Personal.Store.Filters.UserStoreFilter<int>, odec.Server.Model.Personal.Store.Filters.UserStoreGoodsFilter<int, int, int>>;

namespace Store.DAL.Tests.Blob
{
    public class PersonalStoreRepositoryTester : Tester<PersonalStoreContext>
    {
        private StoreM GenerateModel()
        {
            return new StoreM
            {
                Name = "My Conversation",
                Code = "Conv1",
                IsActive = true,
                DateCreated = DateTime.Now,
                SortOrder = 0,
            };
        }

        [Test]
        public void GetGoodBySerialNumber_P()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new PersonalStoreContext(options))
                {
                    var repository =
                        new PersonalStoreRepository(db);
                    StoreTestHelper.PopulateDefaultPersonalStoreData(db);
                    var item = db.Set<Good>().First();
                    var store = db.Set<StoreM>().First();
                    var user = db.Set<User>().First(it => it.UserName == "Andrew");
                    Assert.DoesNotThrow(() => item = repository.GetGoodBySerialNumber(user.Id, store.Id, item.SerialNumber));
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void GetGoodByArticul()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new PersonalStoreContext(options))
                {
                    var repository =
                        new PersonalStoreRepository(db);
                    StoreTestHelper.PopulateDefaultPersonalStoreData(db);
                    var item = db.Set<Good>().First();
                    var store = db.Set<StoreM>().First();
                    var user = db.Set<User>().First(it => it.UserName == "Andrew");
                    Assert.DoesNotThrow(() => item = repository.GetGoodByArticul(user.Id, store.Id, item.Articul));
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void Get()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new PersonalStoreContext(options))
                {
                    var repository =
                        new PersonalStoreRepository(db);
                    StoreTestHelper.PopulateDefaultPersonalStoreData(db);
                    IEnumerable<StoreM> result = null;
                    var user = db.Set<User>().First(it => it.UserName == "Andrew");
                    var filter = new UserStoreFilter<int>
                    {
                        UserId = user.Id,
                        IsOnlyActive = false
                    };
                    Assert.DoesNotThrow(() => result = repository.Get(filter));
                    Assert.True(result != null && result.Any() && result.Count() == 1);
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void GetGoods()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new PersonalStoreContext(options))
                {
                    var repository =
                        new PersonalStoreRepository(db);
                    StoreTestHelper.PopulateDefaultPersonalStoreData(db);
                    IEnumerable<Good> result = null;
                    var filter = new UserStoreGoodsFilter<int, int, int>()
                    {
                        UserId = 1,
                        StoreId = 1
                    };
                    Assert.DoesNotThrow(() => result = repository.GetGoods(filter));
                    Assert.True(result != null && result.Any() && result.Count() == 3);
                    filter.GoodCategoryIds =
                        db.Set<GoodCategory>().Select(it => it.CategoryId).Distinct().ToList();
                    Assert.DoesNotThrow(() => result = repository.GetGoods(filter));
                    Assert.True(result != null && result.Any() && result.Count() == 1);
                    filter.GoodCategoryIds = null;
                    filter.GoodTypeIds =
                        db.Set<GoodType>().Select(it => it.TypeId).Distinct().ToList();
                    Assert.DoesNotThrow(() => result = repository.GetGoods(filter));
                    Assert.True(result != null && result.Any() && result.Count() == 1);
                    filter.GoodTypeIds = null;
                    filter.GoodDesignerIds =
                        db.Set<GoodDesigner>().Select(it => it.DesignerId).Distinct().ToList();
                    Assert.DoesNotThrow(() => result = repository.GetGoods(filter));
                    Assert.True(result != null && result.Any() && result.Count() == 1);
                    filter.GoodDesignerIds = null;
                    filter.GoodColorIds =
                        db.Set<GoodColor>().Select(it => it.ColorId).Distinct().ToList();
                    Assert.DoesNotThrow(() => result = repository.GetGoods(filter));
                    Assert.True(result != null && result.Any() && result.Count() == 1);
                    filter.GoodColorIds = null;
                    filter.GoodMaterialsIds =
                        db.Set<GoodMaterial>().Select(it => it.MaterialId).Distinct().ToList();
                    Assert.DoesNotThrow(() => result = repository.GetGoods(filter));
                    Assert.True(result != null && result.Any() && result.Count() == 1);
                    filter.GoodColorIds = null;
                    filter.GoodMaterialsIds = null;
                    filter.GoodDepthInterval.End = 100;
                    Assert.DoesNotThrow(() => result = repository.GetGoods(filter));
                    Assert.True(result != null && result.Any() && result.Count() == 3);
                    filter.GoodDepthInterval.End = null;
                    filter.GoodDepthInterval.Start = 20;
                    Assert.DoesNotThrow(() => result = repository.GetGoods(filter));
                    Assert.True(result != null && result.Any() && result.Count() == 3);
                    filter.GoodDepthInterval.End = 100;
                    Assert.DoesNotThrow(() => result = repository.GetGoods(filter));
                    Assert.True(result != null && result.Any() && result.Count() == 3);
                    filter.GoodDepthInterval.Start = null;
                    filter.GoodDepthInterval.End = null;
                    filter.GoodHeightInterval.End = 100;
                    Assert.DoesNotThrow(() => result = repository.GetGoods(filter));
                    Assert.True(result != null && result.Any() && result.Count() == 3);
                    filter.GoodHeightInterval.End = null;
                    filter.GoodHeightInterval.Start = 20;
                    Assert.DoesNotThrow(() => result = repository.GetGoods(filter));
                    Assert.True(result != null && result.Any() && result.Count() == 3);
                    filter.GoodHeightInterval.End = 100;
                    Assert.DoesNotThrow(() => result = repository.GetGoods(filter));
                    Assert.True(result != null && result.Any() && result.Count() == 3);
                    filter.GoodHeightInterval.End = null;
                    filter.GoodHeightInterval.Start = null;
                    filter.GoodWidthInterval.End = 100;
                    Assert.DoesNotThrow(() => result = repository.GetGoods(filter));
                    Assert.True(result != null && result.Any() && result.Count() == 3);
                    filter.GoodWidthInterval.End = null;
                    filter.GoodWidthInterval.Start = 20;
                    Assert.DoesNotThrow(() => result = repository.GetGoods(filter));
                    Assert.True(result != null && result.Any() && result.Count() == 3);
                    filter.GoodWidthInterval.End = 100;
                    Assert.DoesNotThrow(() => result = repository.GetGoods(filter));
                    Assert.True(result != null && result.Any() && result.Count() == 3);
                    filter.GoodHeightInterval.Start = 20;
                    filter.GoodHeightInterval.End = 100;
                    filter.GoodDepthInterval.Start = 20;
                    filter.GoodDepthInterval.End = 100;
                    Assert.DoesNotThrow(() => result = repository.GetGoods(filter));
                    Assert.True(result != null && result.Any() && result.Count() == 3);
                    filter.GoodCategoryIds =
                        db.Set<GoodCategory>().Select(it => it.CategoryId).Distinct().ToList();
                    filter.GoodTypeIds =
                        db.Set<GoodType>().Select(it => it.TypeId).Distinct().ToList();
                    filter.GoodDesignerIds =
                        db.Set<GoodDesigner>().Select(it => it.DesignerId).Distinct().ToList();
                    filter.GoodColorIds =
                        db.Set<GoodColor>().Select(it => it.ColorId).Distinct().ToList();
                    filter.GoodColorIds = null;
                    filter.GoodMaterialsIds =
                        db.Set<GoodMaterial>().Select(it => it.MaterialId).Distinct().ToList();
                    Assert.DoesNotThrow(() => result = repository.GetGoods(filter));
                    Assert.True(result != null && result.Any() && result.Count() == 1);
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void GetGoodById()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new PersonalStoreContext(options))
                {
                    var repository =
                        new PersonalStoreRepository(db);
                    StoreTestHelper.PopulateDefaultPersonalStoreData(db);
                    var item = db.Set<Good>().First();
                    var store = db.Set<StoreM>().First();
                    var user = db.Set<User>().First(it => it.UserName == "Andrew");
                    Assert.DoesNotThrow(() => item = repository.GetGoodById(user, store, item.Id));
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void AddStore()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new PersonalStoreContext(options))
                {
                    var repository =
                        new PersonalStoreRepository(db);
                    StoreTestHelper.PopulateDefaultPersonalStoreData(db);
                    var user = db.Set<User>().First(it => it.UserName == "Andrew");
                    var item = GenerateModel();
                    var goods = db.Set<Good>().ToList().Select(it => new Tuple<Good, int>(it, 1));
                    Assert.DoesNotThrow(() => repository.AddStore(item, user, goods));
                    var filter = new UserStoreFilter<int>
                    {
                        UserId = user.Id,
                        IsOnlyActive = false
                    };
                    Assert.DoesNotThrow(() => item = repository.Get(filter).FirstOrDefault());
                    Assert.NotNull(item);
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void RemoveStore()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new PersonalStoreContext(options))
                {
                    StoreTestHelper.PopulateDefaultPersonalStoreData(db);
                }
                using (var db = new PersonalStoreContext(options))
                {
                    var repository =
                            new PersonalStoreRepository(db);

                    var user = db.Set<User>().First(it => it.UserName == "Andrew");
                    var item = db.Set<StoreM>().First();
                    Assert.DoesNotThrow(() => repository.RemoveStore(item, user));
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void ActivateStore()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new PersonalStoreContext(options))
                {
                    var repository =
                        new PersonalStoreRepository(db);
                    StoreTestHelper.PopulateDefaultPersonalStoreData(db);
                    var user = db.Set<User>().First(it => it.UserName == "Andrew");
                    var item = db.Set<StoreM>().First();
                    Assert.DoesNotThrow(() => repository.ActivateStore(item, user));
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void DeactivateStore()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new PersonalStoreContext(options))
                {
                    var repository =
                        new PersonalStoreRepository(db);
                    StoreTestHelper.PopulateDefaultPersonalStoreData(db);
                    var user = db.Set<User>().First(it => it.UserName == "Andrew");
                    var item = db.Set<StoreM>().First();
                    Assert.DoesNotThrow(() => repository.DeactivateStore(item, user));
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void AddGood()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new PersonalStoreContext(options))
                {
                    var repository =
                        new PersonalStoreRepository(db);
                    StoreTestHelper.PopulateDefaultPersonalStoreData(db);
                    var user = db.Set<User>().First(it => it.UserName == "Andrew");
                    var item = db.Set<StoreM>().First();
                    var good = StoreTestHelper.GenerateGood("Test", db.Sizes.First());
                    Assert.DoesNotThrow(() => repository.AddGood(item, good, user));
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void RemoveGood()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new PersonalStoreContext(options))
                {
                    var repository =
                        new PersonalStoreRepository(db);
                    StoreTestHelper.PopulateDefaultPersonalStoreData(db);

                    var user = db.Set<User>().First(it => it.UserName == "Andrew");
                    var item = db.Set<StoreM>().First();
                    var good = StoreTestHelper.GenerateGood("Test", db.Sizes.First());
                    Assert.DoesNotThrow(() => repository.AddGood(item, good, user));
                    Assert.DoesNotThrow(() => repository.RemoveGood(item, good, user));
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void ReserveGood()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new PersonalStoreContext(options))
                {
                    var repository =
                        new PersonalStoreRepository(db);
                    StoreTestHelper.PopulateDefaultPersonalStoreData(db);
                    var user = db.Set<User>().First(it => it.UserName == "Andrew");
                    var item = db.Set<StoreM>().First();
                    var good = db.Set<Good>().First();
                    var reserved = 0;
                    Assert.DoesNotThrow(() => reserved = repository.ReserveGood(item, good, user, 10));
                    Assert.True(reserved == 1);
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void RemoveGoodReservation()
        {

            try
            {
                var options = CreateNewContextOptions(); using (var db = new PersonalStoreContext(options))
                {
                    var repository =
                        new PersonalStoreRepository(db);
                    StoreTestHelper.PopulateDefaultPersonalStoreData(db);
                    var user = db.Set<User>().First(it => it.UserName == "Andrew");
                    var item = db.Set<StoreM>().First();
                    var good = db.Set<Good>().First();
                    var reserved = 0;
                    Assert.DoesNotThrow(() => reserved = repository.ReserveGood(item, good, user, 10));
                    Assert.True(reserved == 1);
                    int unreserved = 0;
                    Assert.DoesNotThrow(() => unreserved = repository.RemoveGoodReservation(item, good, user, 10));
                    Assert.True(unreserved == 1);
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void IncreaseGoodQuantity()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new PersonalStoreContext(options))
                {
                    var repository =
                        new PersonalStoreRepository(db);
                    StoreTestHelper.PopulateDefaultPersonalStoreData(db);
                    var user = db.Set<User>().First(it => it.UserName == "Andrew");
                    var item = db.Set<StoreM>().First();
                    var good = db.Set<Good>().First();
                    Assert.DoesNotThrow(() => repository.IncreaseGoodQuantity(item, good, user, 10));
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
        [Test]
        public void SetStoreGoodCount()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new PersonalStoreContext(options))
                {
                    var repository =
                        new PersonalStoreRepository(db);
                    StoreTestHelper.PopulateDefaultPersonalStoreData(db);
                    var user = db.Set<User>().First(it => it.UserName == "Andrew");
                    var item = db.Set<StoreM>().First();
                    var good = db.Set<Good>().First();
                    Assert.DoesNotThrow(() => repository.SetStoreGoodCount(item, good, user, 10));
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
        [Test]
        public void DecreaseGoodQuantity()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new PersonalStoreContext(options))
                {
                    var repository =
                        new PersonalStoreRepository(db);
                    StoreTestHelper.PopulateDefaultPersonalStoreData(db);
                    var user = db.Set<User>().First(it => it.UserName == "Andrew");
                    var item = db.Set<StoreM>().First();
                    var good = db.Set<Good>().First();
                    Assert.DoesNotThrow(() => repository.IncreaseGoodQuantity(item, good, user, 10));
                    Assert.DoesNotThrow(() => repository.DecreaseGoodQuantity(item, good, user, 11));
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
    }
}
