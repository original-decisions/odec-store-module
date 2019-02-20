using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using odec.Framework.Logging;
using odec.Server.Model.Store.Blob;
using odec.Server.Model.Store.Contexts.Blob;
using odec.Server.Model.Store.Filters;
using odec.Store.DAL.Blob;
using StoreM = odec.Server.Model.Store.Store;
//using IStoreRepo = odec.Store.DAL.Interop.IStoreRepository<int, System.Data.Entity.DbContext, odec.Server.Model.Store.Store, odec.Server.Model.Store.Blob.Good, odec.Server.Model.Store.Filters.StoreFilter, odec.Server.Model.Store.Filters.StoreGoodsFilter<int, int>>;
namespace Store.DAL.Tests.Blob
{
    public class StoreRepositoryTester : Tester<StoreContext>
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
        public void Save()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new StoreContext(options))
                {
                    var repository =
                        new StoreRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = GenerateModel();
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    Assert.Greater(item.Id, 0);
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void Delete()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new StoreContext(options))
                {
                    var repository = new StoreRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = GenerateModel();
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => repository.Delete(item));
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
        [Test]
        public void DeleteById()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new StoreContext(options))
                {
                    var repository = new StoreRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = GenerateModel();
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => repository.Delete(item.Id));
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void Deactivate()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new StoreContext(options))
                {
                    var repository = new StoreRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = GenerateModel();
                    item.IsActive = true;
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => repository.Deactivate(item));
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    Assert.IsFalse(item.IsActive);
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void DeactivateById()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new StoreContext(options))
                {
                    var repository = new StoreRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = GenerateModel();
                    item.IsActive = true;
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => item = repository.Deactivate(item.Id));
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    Assert.IsFalse(item.IsActive);
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void Activate()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new StoreContext(options))
                {
                    var repository = new StoreRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = GenerateModel();
                    item.IsActive = false;
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => repository.Activate(item));
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    Assert.IsTrue(item.IsActive);
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void ActivateById()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new StoreContext(options))
                {
                    var repository = new StoreRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = GenerateModel();
                    item.IsActive = false;
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => item = repository.Activate(item.Id));
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    Assert.IsTrue(item.IsActive);
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void GetById()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new StoreContext(options))
                {
                    var repository = new StoreRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = GenerateModel();
                    Assert.DoesNotThrow(() => repository.Save(item));

                    Assert.DoesNotThrow(() => item = repository.GetById(item.Id));
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    Assert.NotNull(item);
                    Assert.Greater(item.Id, 0);
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
                var options = CreateNewContextOptions();using (var db = new StoreContext(options))
                {
                    var repository = new StoreRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = GenerateModel();
                    IEnumerable<StoreM> result = null;
                    Assert.DoesNotThrow(() => repository.Save(item));

                    var filter = new StoreFilter();
                    Assert.DoesNotThrow(() => result = repository.Get(filter));
                    Assert.True(result != null && result.Any());

                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void GetGoodBySerialNumber()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new StoreContext(options))
                {
                    var repository =
                        new StoreRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = db.Set<Good>().First();
                    
                    Assert.DoesNotThrow(() => item = repository.GetGoodBySerialNumber(1,item.SerialNumber));
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
                var options = CreateNewContextOptions();using (var db = new StoreContext(options))
                {
                    var repository =
                        new StoreRepository(db);

                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = db.Set<Good>().First();
                    Assert.DoesNotThrow(()=> item= repository.GetGoodByArticul(1,item.Articul));
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
                var options = CreateNewContextOptions();using (var db = new StoreContext(options))
                {
                    var repository =
                        new StoreRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    IEnumerable<Good> result = null;
                    var filter = new StoreGoodsFilter<int,int>()
                    {
                        StoreId = 1
                    };
                    Assert.DoesNotThrow(() => result = repository.GetGoods(filter));
                    Assert.True(result != null && result.Any() && result.Count() == 3);
                    filter.GoodCategoryIds = (from entity in db.Set<GoodCategory>()
                        select entity.CategoryId).Distinct().ToList();

                        //db.Set<GoodCategory>().Include(it => it.Category).Select(it => it.Category.Id).Distinct().ToList();
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
        public void AddGood()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new StoreContext(options))
                {
                    var repository =
                        new StoreRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = db.Set<StoreM>().First();
                    var good = StoreTestHelper.GenerateGood("Test", db.Sizes.First());
                    Assert.DoesNotThrow(()=> repository.AddGood(item,good));
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
                var options = CreateNewContextOptions();using (var db = new StoreContext(options))
                {
                    var repository =
                        new StoreRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = db.Set<StoreM>().First();
                    var good = StoreTestHelper.GenerateGood("Test", db.Sizes.First());
                    Assert.DoesNotThrow(() => repository.AddGood(item, good));
                    Assert.DoesNotThrow(() => repository.RemoveGood(item, good));
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
                var options = CreateNewContextOptions();using (var db = new StoreContext(options))
                {
                    var repository =
                        new StoreRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = db.Set<StoreM>().First();
                    var good = db.Set<Good>().First();
                    Assert.DoesNotThrow(() => repository.IncreaseGoodQuantity(item, good, 10));
                 
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
                var options = CreateNewContextOptions();using (var db = new StoreContext(options))
                {
                    var repository =
                        new StoreRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = db.Set<StoreM>().First();
                    var good = db.Set<Good>().First();
                    Assert.DoesNotThrow(() => repository.IncreaseGoodQuantity(item, good, 10));
                    Assert.DoesNotThrow(() => repository.DecreaseGoodQuantity(item, good, 12));

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
                var options = CreateNewContextOptions();using (var db = new StoreContext(options))
                {
                    var repository =
                        new StoreRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = db.Set<StoreM>().First();
                    var good = db.Set<Good>().First();
                    Assert.DoesNotThrow(() => repository.SetStoreGoodCount(item, good, 10));
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
                var options = CreateNewContextOptions();using (var db = new StoreContext(options))
                {
                    var repository =
                        new StoreRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = db.Set<StoreM>().First();
                    var good = db.Set<Good>().First();
                    Assert.DoesNotThrow(() => repository.ReserveGood(item, good));
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
                var options = CreateNewContextOptions();using (var db = new StoreContext(options))
                {
                    var repository =
                        new StoreRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = db.Set<StoreM>().First();
                    var good = db.Set<Good>().First();
                    var reserved = 0;
                    var unresurved = 0;
                    Assert.DoesNotThrow(() => reserved = repository.ReserveGood(item, good, 10));
                    Assert.True(reserved == 1);
                    Assert.DoesNotThrow(() => unresurved = repository.RemoveGoodReservation(item, good, 10));
                    Assert.True(unresurved == 1);
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
