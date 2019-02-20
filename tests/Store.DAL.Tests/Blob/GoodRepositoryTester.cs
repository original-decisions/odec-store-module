using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using odec.Framework.Logging;
using odec.Server.Model.Attachment;
using odec.Server.Model.Attachment.Extended;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using odec.Framework.Generic.Utility;
using odec.Server.Model.Category;
using odec.Server.Model.Store;
using odec.Server.Model.Store.Blob;
using odec.Server.Model.Store.Clothes;
using odec.Server.Model.Store.Contexts.Blob;
using odec.Server.Model.Store.Filters;
using odec.Server.Model.Store.Helpers.Enums;
using odec.Store.DAL.Blob;
//using IGoodRepo = odec.Store.DAL.Interop.IGoodRepository<int, System.Data.Entity.DbContext, odec.Server.Model.Store.Blob.Good, odec.Server.Model.Store.Type, odec.Server.Model.Store.Clothes.Size, odec.Server.Model.Store.Color, odec.Server.Model.Store.Blob.GoodImage, odec.Server.Model.Store.Filters.GoodsFilter<int>>;
using Type = odec.Server.Model.Store.Type;

namespace Store.DAL.Tests.Blob
{
    public class GoodRepositoryTester : Tester<StoreContext>
    {
        private Good GenerateModel()
        {
            return new Good
            {
                Name = "My Conversation",
                Code = "Conv1",
                IsActive = true,
                DateCreated = DateTime.Now,
                Articul = Guid.NewGuid().ToString(),
                BasePrice = 1000,
                MarkUp = 1,
                Height = 50,
                Width = 50,
                Depth = 50,
                Size = new Size
                {
                    Code = "NOSIZE",
                    Name = "Not Selected",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    Scale = new Scale
                    {
                        Code = "NOSCALE",
                        Name = "Not Selected",
                        DateCreated = DateTime.Now,
                        IsActive = true,
                    }
                },
                SerialNumber = Guid.NewGuid().ToString(),
                ShortDescription = string.Empty,
                Description = string.Empty,
                SortOrder = 0,
            };
        }

        private Attachment GenerateAttachment()
        {
            return new Attachment
            {
                Name = "Test",
                Code = "TEST",
                IsActive = true,
                DateCreated = DateTime.Now,
                SortOrder = 0,
                Extension = new Extension
                {
                    Name = "Test",
                    Code = "TEST",
                    IsActive = true,
                    DateCreated = DateTime.Now,
                    SortOrder = 0
                },
                PublicUri = string.Empty,
                IsShared = false,
                Content = new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                AttachmentType = new AttachmentType
                {
                    Name = "Test",
                    Code = "TEST",
                    IsActive = true,
                    DateCreated = DateTime.Now,
                    SortOrder = 0
                }
            };
        }
        [Test]
        public void Save()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new StoreContext(options))
                {
                    var repository =
                        new GoodRepository(db);
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
        public void SaveAndEdit()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new StoreContext(options))
                {
                    var repository =
                        new GoodRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = GenerateModel();
                    Assert.DoesNotThrow(() => repository.Save(item));
                    item.Name = "Name Changed";
                    item.BasePrice = 2200;
                    item.IsActive = true;
                    item.Width = 300;
                    Assert.DoesNotThrow(() => repository.Save(item));
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
                var options = CreateNewContextOptions(); using (var db = new StoreContext(options))
                {
                    var repository = new GoodRepository(db);
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
                var options = CreateNewContextOptions(); using (var db = new StoreContext(options))
                {
                    var repository = new GoodRepository(db);
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
                var options = CreateNewContextOptions(); using (var db = new StoreContext(options))
                {
                    var repository = new GoodRepository(db);
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
                var options = CreateNewContextOptions(); using (var db = new StoreContext(options))
                {
                    var repository = new GoodRepository(db);
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
                var options = CreateNewContextOptions(); using (var db = new StoreContext(options))
                {
                    var repository = new GoodRepository(db);
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
                var options = CreateNewContextOptions(); using (var db = new StoreContext(options))
                {
                    var repository = new GoodRepository(db);
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
                var options = CreateNewContextOptions(); using (var db = new StoreContext(options))
                {
                    var repository = new GoodRepository(db);
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

        class GetGoodsTestCases : IEnumerable
        {
            //private IList<GoodsFilter<int>> filters = new List<GoodsFilter<int>>
            //{
            //    new GoodsFilter<int>(),
            //    new GoodsFilter<int>
            //    {

            //    }
            //};
            public IEnumerator GetEnumerator()
            {
                IList<int> categoriesIds;
                IList<int> goodTypesIds;
                IList<int> designersIds;
                IList<int> colorsIds;
                IList<int> materialsIds;
                var options = CreateNewContextOptions();
                var filters = new List<Tuple<GoodsFilter<int>, int>>
                {
                    new Tuple<GoodsFilter<int>, int>(new GoodsFilter<int>(),3),
                    new Tuple<GoodsFilter<int>, int>(new GoodsFilter<int>{
                        DepthInterval = new Interval<decimal?>
                        {
                            End = 100
                        }
                    },3),
                    new Tuple<GoodsFilter<int>, int>(new GoodsFilter<int>
                    {
                        DepthInterval = new Interval<decimal?>
                        {
                            Start = 20
                        }
                    },3),
                    new Tuple<GoodsFilter<int>, int>(new GoodsFilter<int>
                    {
                        DepthInterval = new Interval<decimal?>
                        {
                            Start = 20,
                            End = 100
                        }
                    },3),
                    new Tuple<GoodsFilter<int>, int>(new GoodsFilter<int>{
                        HeightInterval = new Interval<decimal?>
                        {
                            End = 100
                        }
                    },3),
                    new Tuple<GoodsFilter<int>, int>(new GoodsFilter<int>
                    {
                        HeightInterval = new Interval<decimal?>
                        {
                            Start = 20
                        }
                    },3),
                    new Tuple<GoodsFilter<int>, int>(new GoodsFilter<int>
                    {
                        HeightInterval = new Interval<decimal?>
                        {
                            Start = 20,
                            End = 100
                        }
                    },3),
                    new Tuple<GoodsFilter<int>, int>(new GoodsFilter<int>
                    {
                        WidthInterval = new Interval<decimal?>
                        {
                            Start = 20
                        }
                    },3),
                    new Tuple<GoodsFilter<int>, int>(new GoodsFilter<int>
                    {
                        WidthInterval = new Interval<decimal?>
                        {
                            End = 100
                        }
                    },3),
                    new Tuple<GoodsFilter<int>, int>(new GoodsFilter<int>
                    {
                        WidthInterval = new Interval<decimal?>
                        {
                            Start = 20,
                            End = 100
                        }
                    },3)


                };
                using (var db = new StoreContext(options))
                {
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    categoriesIds = (from goodCategory in db.Set<GoodCategory>()
                                     select goodCategory.CategoryId).Distinct().ToList();
                    goodTypesIds = db.Set<GoodType>().Select(it => it.TypeId).Distinct().ToList();
                    designersIds = db.Set<GoodDesigner>().Select(it => it.DesignerId).Distinct().ToList();
                    colorsIds = db.Set<GoodColor>().Select(it => it.ColorId).Distinct().ToList();
                    materialsIds = db.Set<GoodMaterial>().Select(it => it.MaterialId).Distinct().ToList();
                    ;
                }
                filters.Add(new Tuple<GoodsFilter<int>, int>(new GoodsFilter<int>
                {
                    CategoryIds = categoriesIds
                }, 1));
                filters.Add(new Tuple<GoodsFilter<int>, int>(new GoodsFilter<int>
                {
                    TypeIds = goodTypesIds
                }, 1));
                filters.Add(new Tuple<GoodsFilter<int>, int>(new GoodsFilter<int>
                {
                    DesignerIds = designersIds
                }, 1));
                filters.Add(new Tuple<GoodsFilter<int>, int>(new GoodsFilter<int>
                {
                    ColorIds = colorsIds
                }, 1));
                filters.Add(new Tuple<GoodsFilter<int>, int>(new GoodsFilter<int>
                {
                    MaterialsIds = materialsIds
                }, 1));
                filters.Add(new Tuple<GoodsFilter<int>, int>(new GoodsFilter<int>
                {

                    CategoryIds = categoriesIds,
                    TypeIds = goodTypesIds,
                    DesignerIds = designersIds,
                    ColorIds = colorsIds,
                    MaterialsIds = materialsIds,
                    HeightInterval = new Interval<decimal?>
                    {
                        Start = 20,
                        End = 100
                    },
                    WidthInterval = new Interval<decimal?>
                    {
                        Start = 20,
                        End = 100
                    },
                    DepthInterval = new Interval<decimal?>
                    {
                        Start = 20,
                        End = 100
                    }
                }, 1));
                //TODO: JsonFormater
                return filters.Select(it => new TestCaseData(options, it.Item1).SetName("GetGood->" + JsonConvert.SerializeObject(it.Item1,new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }).Replace('.',',')).Returns(it.Item2)).GetEnumerator();
            }
        }

        [Test]
        [TestCaseSource(typeof(GetGoodsTestCases))]
        public int Get(DbContextOptions<StoreContext> options, GoodsFilter<int> filter)
        {
            try
            {

                using (var db = new StoreContext(options))
                {
                    var repository = new GoodRepository(db);

                    IEnumerable<Good> result = null;
                    //var filter = new GoodsFilter<int>();

                    Assert.DoesNotThrow(() => result = repository.Get(filter));
                    //Assert.True(result != null && result.Any() && result.Count() == 3);
                    return result.Count();

                    //  filter.CategoryIds = (from goodCategory in db.Set<GoodCategory>()
                    //                          select goodCategory.CategoryId).Distinct().ToList();
                    //Assert.DoesNotThrow(() => result = repository.Get(filter));
                    //Assert.True(result != null && result.Any() && result.Count() == 1);
                    //filter.CategoryIds = null;
                    //    filter.TypeIds = db.Set<GoodType>().Select(it => it.TypeId).Distinct().ToList();
                    //Assert.DoesNotThrow(() => result = repository.Get(filter));
                    //Assert.True(result != null && result.Any() && result.Count() == 1);
                    //filter.TypeIds = null;
                    //    filter.DesignerIds = db.Set<GoodDesigner>().Select(it => it.DesignerId).Distinct().ToList();
                    //Assert.DoesNotThrow(() => result = repository.Get(filter));
                    //Assert.True(result != null && result.Any() && result.Count() == 1);
                    //filter.DesignerIds = null;
                    //     filter.ColorIds = db.Set<GoodColor>().Select(it => it.ColorId).Distinct().ToList();
                    //Assert.DoesNotThrow(() => result = repository.Get(filter));
                    //Assert.True(result != null && result.Any() && result.Count() == 1);
                    //filter.ColorIds = null;
                    //      filter.MaterialsIds = db.Set<GoodMaterial>().Select(it => it.MaterialId).Distinct().ToList();
                    //Assert.DoesNotThrow(() => result = repository.Get(filter));
                    //Assert.True(result != null && result.Any() && result.Count() == 1);
                    //filter.ColorIds = null;
                    //filter.MaterialsIds = null;
                    //filter.DepthInterval.End = 100;
                    //Assert.DoesNotThrow(() => result = repository.Get(filter));
                    //Assert.True(result != null && result.Any() && result.Count() == 3);
                    //filter.DepthInterval.End = null;
                    //filter.DepthInterval.Start = 20;
                    //Assert.DoesNotThrow(() => result = repository.Get(filter));
                    //Assert.True(result != null && result.Any() && result.Count() == 3);
                    //filter.DepthInterval.End = 100;
                    //Assert.DoesNotThrow(() => result = repository.Get(filter));
                    //Assert.True(result != null && result.Any() && result.Count() == 3);
                    //filter.DepthInterval.Start = null;
                    //filter.DepthInterval.End = null;
                    //filter.HeightInterval.End = 100;
                    //Assert.DoesNotThrow(() => result = repository.Get(filter));
                    //Assert.True(result != null && result.Any() && result.Count() == 3);
                    //filter.HeightInterval.End = null;
                    //filter.HeightInterval.Start = 20;
                    //Assert.DoesNotThrow(() => result = repository.Get(filter));
                    //Assert.True(result != null && result.Any() && result.Count() == 3);
                    //filter.HeightInterval.End = 100;
                    //Assert.DoesNotThrow(() => result = repository.Get(filter));
                    //Assert.True(result != null && result.Any() && result.Count() == 3);
                    //filter.HeightInterval.End = null;
                    //filter.HeightInterval.Start = null;
                    //filter.WidthInterval.End = 100;
                    //Assert.DoesNotThrow(() => result = repository.Get(filter));
                    //Assert.True(result != null && result.Any() && result.Count() == 3);
                    //filter.WidthInterval.End = null;
                    //filter.WidthInterval.Start = 20;
                    //Assert.DoesNotThrow(() => result = repository.Get(filter));
                    //Assert.True(result != null && result.Any() && result.Count() == 3);
                    //filter.WidthInterval.End = 100;
                    //Assert.DoesNotThrow(() => result = repository.Get(filter));
                    //Assert.True(result != null && result.Any() && result.Count() == 3);
                    //filter.HeightInterval.Start = 20;
                    //filter.HeightInterval.End = 100;
                    //filter.DepthInterval.Start = 20;
                    //filter.DepthInterval.End = 100;
                    //Assert.DoesNotThrow(() => result = repository.Get(filter));
                    //Assert.True(result != null && result.Any() && result.Count() == 3);
                    //filter.CategoryIds = db.Set<GoodCategory>().Select(it => it.CategoryId).Distinct().ToList();
                    //filter.TypeIds = db.Set<GoodType>().Select(it => it.TypeId).Distinct().ToList();
                    //filter.DesignerIds = db.Set<GoodDesigner>().Select(it => it.DesignerId).Distinct().ToList();
                    //filter.ColorIds = db.Set<GoodColor>().Select(it => it.ColorId).Distinct().ToList();
                    //filter.ColorIds = null;
                    //filter.MaterialsIds = db.Set<GoodMaterial>().Select(it => it.MaterialId).Distinct().ToList();
                    //Assert.DoesNotThrow(() => result = repository.Get(filter));
                    //Assert.True(result != null && result.Any() && result.Count() == 1);
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        // [TestCase()]
        public void GetByIdOptions_1()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new StoreContext(options))
                {
                    var repository =
                        new GoodRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = db.Set<Good>().First(it => it.Code == "TestGoodPopulated");
                    Assert.DoesNotThrow(() => item = repository.GetById(item.Id, GoodInitOptions.InitDesigners));
                    Assert.NotNull(item);
                    Assert.Greater(item.Id, 0);
                    Assert.True((item.Designers != null && item.Designers.Any()) &&
                                (item.Colors == null && item.MainImage == null && item.Gallery == null &&
                                 item.Categories == null && item.Types == null && item.Materials == null));
                    item.Designers = null;
                    Assert.DoesNotThrow(() => item = repository.GetById(item.Id, GoodInitOptions.InitCategories));
                    Assert.True((item.Categories != null && item.Categories.Any()) &&
                                (item.Colors == null && item.MainImage == null && item.Gallery == null &&
                                 item.Designers == null && item.Types == null && item.Materials == null));
                    item.Categories = null;
                    Assert.DoesNotThrow(() => item = repository.GetById(item.Id, GoodInitOptions.InitTypes));
                    Assert.True((item.Types != null && item.Types.Any()) &&
                                (item.Colors == null && item.MainImage == null && item.Gallery == null &&
                                 item.Designers == null && item.Categories == null && item.Materials == null));
                    item.Types = null;
                    Assert.DoesNotThrow(() => item = repository.GetById(item.Id, GoodInitOptions.InitMaterial));
                    Assert.True((item.Materials != null && item.Materials.Any()) &&
                                (item.Colors == null && item.MainImage == null && item.Gallery == null &&
                                 item.Designers == null && item.Categories == null && item.Types == null));
                    item.Materials = null;
                    Assert.DoesNotThrow(() => item = repository.GetById(item.Id, GoodInitOptions.InitSize));
                    Assert.True((item.Size != null) &&
                                (item.Materials == null && item.Colors == null && item.MainImage == null && item.Gallery == null &&
                                 item.Designers == null && item.Categories == null && item.Types == null));
                    Assert.DoesNotThrow(() => item = repository.GetById(item.Id, GoodInitOptions.InitSingleImage));
                    Assert.True((item.MainImage != null) &&
                                (item.Materials == null && item.Colors == null && item.Gallery == null &&
                                 item.Designers == null && item.Categories == null && item.Types == null));
                    item.MainImage = null;
                    Assert.DoesNotThrow(() => item = repository.GetById(item.Id, GoodInitOptions.InitImageGallery));
                    Assert.True((item.Gallery != null && item.Gallery.Any()) &&
                                (item.Colors == null && item.MainImage == null && item.Materials == null &&
                                 item.Designers == null && item.Categories == null && item.Types == null));
                    item.Gallery = null;
                    Assert.DoesNotThrow(() => item = repository.GetById(item.Id, GoodInitOptions.InitColors));
                    Assert.True((item.Colors != null && item.Colors.Any()) &&
                                (item.Gallery == null && item.MainImage == null && item.Materials == null &&
                                 item.Designers == null && item.Categories == null && item.Types == null));
                    item.Colors = null;
                    Assert.DoesNotThrow(() => item = repository.GetById(item.Id, GoodInitOptions.InitColorsAndSizes));
                    Assert.True((item.Colors != null && item.Colors.Any() && item.Size != null) &&
                                (item.Gallery == null && item.MainImage == null && item.Materials == null &&
                                 item.Designers == null && item.Categories == null && item.Types == null));
                    item.Colors = null;
                    Assert.DoesNotThrow(() => item = repository.GetById(item.Id, GoodInitOptions.InitColorsSizesDesigners));
                    Assert.True((item.Colors != null && item.Colors.Any() && item.Size != null &&
                                 item.Designers != null) &&
                                (item.Gallery == null && item.MainImage == null && item.Materials == null && item.Categories == null && item.Types == null));
                    item.Colors = null;
                    item.Designers = null;
                    Assert.DoesNotThrow(() => item = repository.GetById(item.Id, GoodInitOptions.InitAllImages));
                    Assert.True((item.MainImage != null && item.Gallery != null) &&
                                (item.Colors == null && item.Materials == null &&
                                 item.Designers == null && item.Categories == null && item.Types == null));
                    item.MainImage = null;
                    item.Gallery = null;
                    Assert.DoesNotThrow(() => item = repository.GetById(item.Id, GoodInitOptions.Default));
                    Assert.True(item.MainImage != null && item.Gallery != null && item.Size != null && item.Colors != null &&
                                item.Materials != null &&
                                item.Designers != null && item.Types != null && item.Categories == null);
                    item.Colors = null;
                    item.Designers = null;
                    item.MainImage = null;
                    item.Gallery = null;
                    item.Materials = null;
                    item.Types = null;
                    Assert.DoesNotThrow(() => item = repository.GetById(item.Id, GoodInitOptions.InitAll));
                    Assert.True(item.MainImage != null && item.Gallery != null && item.Size != null && item.Colors != null &&
                                item.Materials != null &&
                                item.Designers != null && item.Types != null && item.Categories != null);
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void GetGoodTypes()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new StoreContext(options))
                {
                    var repository =
                        new GoodRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = GenerateModel();
                    IEnumerable<Type> result = null;
                    Assert.DoesNotThrow(() => result = repository.GetGoodTypes(item));
                    Assert.True(result == null || !result.Any());
                    item = db.Set<Good>().First(it => it.Code == "TestGoodPopulated");
                    Assert.DoesNotThrow(() => result = repository.GetGoodTypes(item));
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
        public void GetGoodTypesByGoodId()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new StoreContext(options))
                {
                    var repository =
                        new GoodRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = GenerateModel();
                    IEnumerable<Type> result = null;
                    Assert.DoesNotThrow(() => result = repository.GetGoodTypes(item.Id));
                    Assert.True(result == null || !result.Any());
                    item = db.Set<Good>().First(it => it.Code == "TestGoodPopulated");
                    Assert.DoesNotThrow(() => result = repository.GetGoodTypes(item.Id));
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
        public void GetRelativeGoodsByGoodId()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new StoreContext(options))
                {
                    var repository =
                        new GoodRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = GenerateModel();

                    IEnumerable<Good> result = null;
                    Assert.DoesNotThrow(() => result = repository.GetRelativeGoods(item.Id));
                    Assert.True(result == null || !result.Any());
                    item = db.Set<Good>().First(it => it.Code == "TestGoodPopulated");
                    Assert.DoesNotThrow(() => result = repository.GetRelativeGoods(item.Id));
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
        public void GetRelativeGoods()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new StoreContext(options))
                {
                    var repository =
                        new GoodRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = GenerateModel();

                    IEnumerable<Good> result = null;
                    Assert.DoesNotThrow(() => result = repository.GetRelativeGoods(item));
                    Assert.True(result == null || !result.Any());
                    item = db.Set<Good>().First(it => it.Code == "TestGoodPopulated");
                    Assert.DoesNotThrow(() => result = repository.GetRelativeGoods(item));
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
        public void GetColors()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new StoreContext(options))
                {
                    var repository =
                        new GoodRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = GenerateModel();

                    IEnumerable<Color> result = null;
                    Assert.DoesNotThrow(() => result = repository.GetColors(item));
                    Assert.True(result == null || !result.Any());
                    item = db.Set<Good>().First(it => it.Code == "TestGoodPopulated");
                    Assert.DoesNotThrow(() => result = repository.GetColors(item));
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
        public void GetColorsByGoodId()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new StoreContext(options))
                {
                    var repository =
                        new GoodRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = GenerateModel();

                    IEnumerable<Color> result = null;
                    Assert.DoesNotThrow(() => result = repository.GetColors(item.Id));
                    Assert.True(result == null || !result.Any());
                    item = db.Set<Good>().First(it => it.Code == "TestGoodPopulated");
                    Assert.DoesNotThrow(() => result = repository.GetColors(item.Id));
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
        public void GetGallery()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new StoreContext(options))
                {
                    var repository =
                        new GoodRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = GenerateModel();

                    IEnumerable<GoodImage> result = null;
                    Assert.DoesNotThrow(() => result = repository.GetGallery(item));
                    Assert.True(result == null || !result.Any());
                    item = db.Set<Good>().First(it => it.Code == "TestGoodPopulated");
                    Assert.DoesNotThrow(() => result = repository.GetGallery(item));
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
        public void GetGalleryByGoodId()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new StoreContext(options))
                {
                    var repository =
                        new GoodRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = GenerateModel();

                    IEnumerable<GoodImage> result = null;
                    Assert.DoesNotThrow(() => result = repository.GetGallery(item.Id));
                    Assert.True(result == null || !result.Any());
                    item = db.Set<Good>().First(it => it.Code == "TestGoodPopulated");
                    Assert.DoesNotThrow(() => result = repository.GetGallery(item.Id));
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
        public void GetMainImg()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new StoreContext(options))
                {
                    var repository =
                        new GoodRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = GenerateModel();

                    GoodImage result = null;
                    Assert.DoesNotThrow(() => result = repository.GetMainImg(item));
                    Assert.True(result == null);
                    item = db.Set<Good>().First(it => it.Code == "TestGoodPopulated");
                    Assert.DoesNotThrow(() => result = repository.GetMainImg(item));
                    Assert.True(result != null);
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void GetMainImgByGoodId()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new StoreContext(options))
                {
                    var repository =
                        new GoodRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var item = GenerateModel();

                    GoodImage result = null;
                    Assert.DoesNotThrow(() => result = repository.GetMainImg(item.Id));
                    Assert.True(result == null);
                    item = db.Set<Good>().First(it => it.Code == "TestGoodPopulated");
                    Assert.DoesNotThrow(() => result = repository.GetMainImg(item.Id));
                    Assert.True(result != null);
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void InitGoodsSet()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new StoreContext(options))
                {
                    var repository =
                        new GoodRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var items = db.Set<Good>().ToList();
                    Assert.DoesNotThrow(() => repository.InitGoodsSet(items, GoodInitOptions.InitAll));
                    foreach (var item in items)
                    {
                        Assert.True(item.Size != null);
                    }
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void InitGoodsSetByIdSet()
        {
            try
            {
                var options = CreateNewContextOptions(); using (var db = new StoreContext(options))
                {
                    var repository =
                        new GoodRepository(db);
                    StoreTestHelper.PopulateDefaultStoreDataCtx(db);
                    var itemIds = db.Set<Good>().Select(it => it.Id).ToList();
                    IEnumerable<Good> result = null;
                    Assert.DoesNotThrow(() => result = repository.InitGoodsSet(itemIds));
                    Assert.True(result != null && itemIds.Count == result.Count());
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
