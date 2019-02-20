using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using odec.Entity.DAL;
using odec.Framework.Logging;
using odec.Server.Model.Store;
using odec.Server.Model.Store.Blob;
using odec.Server.Model.Store.Clothes;
using odec.Server.Model.Store.Filters;
using odec.Server.Model.Store.Helpers.Enums;
using odec.Store.DAL.Interop;
using odec.Framework.Extensions;
using odec.Server.Model.Attachment;
using odec.Server.Model.Category;
using TypeT = odec.Server.Model.Store.Type;

namespace odec.Store.DAL.Blob
{
    public class GoodRepository : OrmEntityOperationsRepository<int, Good, DbContext>, IGoodRepository<int, DbContext, Good, TypeT, Size, Color, GoodImage, GoodsFilter<int>>
    {
        public GoodRepository()
        {
        }
        public GoodRepository(DbContext db)
        {
            Db = db;
        }


        public Good GetGoodByArticul(string articul, GoodInitOptions options = GoodInitOptions.Default)
        {
            try
            {
                var query = Db.Set<Good>();

              
                var good = query.First(it => it.Articul == articul);
                if (options.HasFlag(GoodInitOptions.InitSize))
                    good.Size = GetById<int, Size>(good.SizeId);
                InitGood(good, options);
                return good;
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }

        }

        public Good GetGoodByName(string name, GoodInitOptions options = GoodInitOptions.Default)
        {
            try
            {
                var query = Db.Set<Good>();

                
                var good = query.First(it => it.Name == name);
                if (options.HasFlag(GoodInitOptions.InitSize))
                    good.Size = GetById<int, Size>(good.SizeId);
                InitGood(good,options);
                return good;
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }

        }

        public IEnumerable<Good> Get(GoodsFilter<int> filter)
        {
            try
            {
                var query = Db.Set<Good>().AsQueryable();

                if (filter.CategoryIds != null && filter.CategoryIds.Any())
                {
                    query = from good in query
                        join goodCategory in Db.Set<GoodCategory>() on good.Id equals goodCategory.GoodId
                        where filter.CategoryIds.Contains(goodCategory.CategoryId)
                        select good;
                }
                if (filter.TypeIds!= null && filter.TypeIds.Any())
                {
                    query = from good in query
                        join goodType in Db.Set<GoodType>() on good.Id equals goodType.GoodId
                            where filter.TypeIds.Contains(goodType.TypeId)
                            select good;
                }

                if (filter.ColorIds!= null && filter.ColorIds.Any())
                {
                    query = from good in query
                            join goodColor in Db.Set<GoodColor>() on good.Id equals goodColor.GoodId
                            where filter.ColorIds.Contains(goodColor.ColorId)
                            select good;
                }
                if (filter.DesignerIds!=null && filter.DesignerIds.Any())
                {
                    query = from good in query
                            join goodDesigner in Db.Set<GoodDesigner>() on good.Id equals goodDesigner.GoodId
                            where filter.DesignerIds.Contains(goodDesigner.DesignerId)
                            select good;
                }
                if (filter.MaterialsIds!= null && filter.MaterialsIds.Any())
                {
                    query = from good in query
                            join goodMaterial in Db.Set<GoodMaterial>() on good.Id equals goodMaterial.GoodId
                            where filter.MaterialsIds.Contains(goodMaterial.MaterialId)
                            select good;
                }
                if (filter.SizeIds != null && filter.SizeIds.Any())
                {
                    query = query.Where(it => filter.SizeIds.Contains(it.SizeId));
                }
                if (filter.HeightInterval != null && (filter.HeightInterval.End.HasValue || filter.HeightInterval.Start.HasValue))
                {
                    if (filter.HeightInterval.Start.HasValue && filter.HeightInterval.End.HasValue)
                        query = query.Where(it => it.Height >= filter.HeightInterval.Start && it.Height <= filter.HeightInterval.End);
                    if (filter.HeightInterval.End.HasValue && !filter.HeightInterval.Start.HasValue)
                        query = query.Where(it => it.Height >= 0 && it.Height <= filter.HeightInterval.End);
                    if (filter.HeightInterval.Start.HasValue && !filter.HeightInterval.End.HasValue)
                        query = query.Where(it => it.Height >= filter.HeightInterval.Start);
                }

                if (filter.WidthInterval!= null && (filter.WidthInterval.End.HasValue || filter.WidthInterval.Start.HasValue))
                {
                    if (filter.WidthInterval.Start.HasValue && filter.WidthInterval.End.HasValue)
                        query = query.Where(it => it.Width >= filter.WidthInterval.Start && it.Width <= filter.WidthInterval.End);
                    if (filter.WidthInterval.End.HasValue && !filter.WidthInterval.Start.HasValue)
                        query = query.Where(it => it.Width >= 0 && it.Width <= filter.WidthInterval.End);
                    if (filter.WidthInterval.Start.HasValue && !filter.WidthInterval.End.HasValue)
                        query = query.Where(it => it.Width >= filter.WidthInterval.Start);
                }
                if (filter.DepthInterval != null && (filter.DepthInterval.End.HasValue || filter.DepthInterval.Start.HasValue))
                {
                    if (filter.DepthInterval.Start.HasValue && filter.DepthInterval.End.HasValue)
                        query = query.Where(it => it.Depth >= filter.DepthInterval.Start && it.Depth <= filter.DepthInterval.End);
                    if (filter.DepthInterval.End.HasValue && !filter.DepthInterval.Start.HasValue)
                        query = query.Where(it => it.Depth >= 0 && it.Depth <= filter.DepthInterval.End);
                    if (filter.DepthInterval.Start.HasValue && !filter.DepthInterval.End.HasValue)
                        query = query.Where(it => it.Depth >= filter.DepthInterval.Start);
                }

                query = filter.Sord.Equals("desc", StringComparison.OrdinalIgnoreCase)
                    ? query.OrderByDescending(filter.Sidx)
                    : query.OrderBy(filter.Sidx);

                query =query.Skip(filter.Rows * (filter.Page - 1)).Take(filter.Rows).Distinct();

                foreach (var good in query)
                {
                    //TODO: move to init
                    if (filter.InitOptions.HasFlag(GoodInitOptions.InitSize))
                        good.Size = GetById<int, Size>(good.SizeId);
                    InitGood(good, filter.InitOptions);
                    



                }
                return query;
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }


        public Good GetById(int id, GoodInitOptions options = GoodInitOptions.Default)
        {
            var query = Db.Set<Good>();
            var good = query.Single(it => it.Id == id);
            //TODO: move to init
            if (options.HasFlag(GoodInitOptions.InitSize))
                good.Size = GetById<int, Size>(good.SizeId);
            InitGood(good, options);
            return good;
        }



        public IEnumerable<TypeT> GetGoodTypes(int goodID)
        {
            try
            {
                return (from goodType in Db.Set<TypeT>()
                        join good in Db.Set<Good>() on goodType.Id equals good.Id
                        where goodID == good.Id
                        select goodType).ToList();
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }

        }

        public IEnumerable<TypeT> GetGoodTypes(Good good)
        {
            return GetGoodTypes(good.Id);
        }


        public IEnumerable<Good> GetRelativeGoods(Good good, GoodInitOptions options = GoodInitOptions.Default)
        {
            return GetRelativeGoods(good.Id, options);
        }

        public IEnumerable<Good> GetRelativeGoods(int goodId, GoodInitOptions options = GoodInitOptions.Default)
        {
            try
            {
                var goods = (from relativeGood in Db.Set<RelativeGood>()
                join good in Db.Set<Good>() on relativeGood.RelativeGoodId equals good.Id
                where relativeGood.GoodId == goodId
                select good).ToList();





                InitGoodsSet(goods, options);
                return goods;
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }

        }



        public IEnumerable<Color> GetColors(Good good)
        {
            try
            {
                return GetColors(good.Id);
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public IEnumerable<Color> GetColors(int goodId)
        {
            try
            {
                return (from goodColor in Db.Set<GoodColor>()
                    join color in Db.Set<Color>() on goodColor.ColorId equals color.Id
                    where goodColor.GoodId == goodId
                    select color);
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }
        public IEnumerable<GoodImage> GetGallery(Good good)
        {
            return GetGallery(good.Id);
        }

        public IEnumerable<GoodImage> GetGallery(int goodId)
        {
            try
            {
                return (from goodImg in Db.Set<GoodImage>()
                    join attachment in Db.Set<Attachment>() on goodImg.ImageId equals attachment.Id
                        where goodImg.GoodId == goodId
                        select new GoodImage
                        {
                            ImageId = goodImg.ImageId,
                            IsMain = goodImg.IsMain,
                            Good = goodImg.Good,
                            GoodId = goodImg.GoodId,
                            Image = attachment
                        }).ToList();
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }

        }

        public GoodImage GetMainImg(Good good)
        {
            return GetMainImg(good.Id);
        }

        public GoodImage GetMainImg(int goodId)
        {
            try
            {
                var img = (from goodImg in Db.Set<GoodImage>()
                    join attachment in Db.Set<Attachment>() on goodImg.ImageId equals attachment.Id
                
                
                    where goodImg.GoodId == goodId && goodImg.IsMain
                    select new GoodImage
                    {
                        ImageId = goodImg.ImageId,
                        GoodId = goodImg.GoodId,
                        IsMain = goodImg.IsMain,
                        Good = goodImg.Good,
                        Image = attachment
                    }).SingleOrDefault();
                return img;
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }

        }

        public void InitGoodsSet(List<Good> goods, GoodInitOptions options = GoodInitOptions.Default, bool force = false)
        {
            try
            {
                foreach (var good in goods)
                {
                    if (options.HasFlag(GoodInitOptions.InitSize) && good.Size ==null)
                        good.Size =GetById<int,Size>(good.SizeId);
                    InitGood(good, options);
                }
                
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public IEnumerable<Good> InitGoodsSet(List<int> goodIds, GoodInitOptions options = GoodInitOptions.Default)
        {
            try
            {
                return goodIds.Select(goodId => GetById(goodId, options));
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void AddImage(Good good, GoodImage image, bool isSaveNow = false)
        {
            try
            {
                if (good == null || good.Id == 0)
                    throw new ArgumentException("No good provided.");
                if (image.Image == null )
                    throw new ArgumentException("No image provided.");

                if (image.Image.Id == 0)
                {
                    Add(image.Image);
                }
                image.ImageId = image.Image.Id;
                image.Image = null;
                image.GoodId = good.Id;
                Db.Set<GoodImage>().Add(image);
                if (isSaveNow)
                    Db.SaveChanges();

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void RemoveImage(Good good, GoodImage image, bool isSaveNow = false)
        {
            try
            {
                if (good == null || good.Id == 0)
                    return;
                if (image.Image == null || image.Image.Id == 0)
                    return;
                var dbImage =
                    Db.Set<GoodImage>().Single(it => it.ImageId == image.Image.Id && it.GoodId == good.Id);
                if (dbImage.IsMain)
                {
                   var newMain =  Db.Set<GoodImage>().FirstOrDefault(it =>it.GoodId == good.Id);
                    if (newMain != null)
                        newMain.IsMain = true;
                }
                
                Db.Set<GoodImage>().Remove(dbImage);
                //todo: think about save delete of image entity.
                if (isSaveNow)
                    Db.SaveChanges();

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void AddColor(Good good, Color color, bool isSaveNow = false)
        {
            try
            {
                if (good == null || good.Id == 0)
                    throw new ArgumentException("No good provided.");
                if (color == null || color.Id == 0)
                    throw new ArgumentException("No color provided.");

                Db.Set<GoodColor>().Add(new GoodColor
                {
                    ColorId = color.Id,
                    GoodId = good.Id
                });
                if (isSaveNow)
                    Db.SaveChanges();

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void RemoveColor(Good good, Color color, bool isSaveNow = false)
        {
            try
            {
                if (good == null || good.Id == 0)
                    return;
                if (color == null || color.Id == 0)
                    return;
                var dbColor =
                    Db.Set<GoodColor>().Single(it => it.ColorId == color.Id && it.GoodId == good.Id);
                Db.Set<GoodColor>().Remove(dbColor);
                if (isSaveNow)
                    Db.SaveChanges();

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void AddType(Good good, TypeT type, bool isSaveNow = false)
        {
            try
            {
                if (good == null || good.Id == 0)
                    throw new ArgumentException("No good provided.");
                if (type == null || type.Id == 0)
                    throw new ArgumentException("No type provided.");

                Db.Set<GoodType>().Add(new GoodType
                {
                    TypeId = type.Id,
                    GoodId = good.Id
                });
                if (isSaveNow)
                    Db.SaveChanges();

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void ClearAllDependencies(Good good, bool isImmediatelySaved = false)
        {
            try
            {
                foreach (var oTypes in Db.Set<GoodType>().Where(it => it.GoodId == good.Id))
                    Db.Set<GoodType>().Remove(oTypes);

                foreach (var oCategory in Db.Set<GoodCategory>().Where(it => it.GoodId == good.Id))
                    Db.Set<GoodCategory>().Remove(oCategory);

                foreach (var oDesigner in Db.Set<GoodDesigner>().Where(it => it.GoodId == good.Id))
                    Db.Set<GoodDesigner>().Remove(oDesigner);

                foreach (var oColor in Db.Set<GoodColor>().Where(it => it.GoodId == good.Id))
                    Db.Set<GoodColor>().Remove(oColor);
                foreach (var gMat in Db.Set<GoodMaterial>().Where(it => it.GoodId == good.Id))
                    Db.Set<GoodMaterial>().Remove(gMat);
                foreach (var gImage in Db.Set<GoodImage>().Where(it => it.GoodId == good.Id && !it.IsMain))
                    Db.Set<GoodImage>().Remove(gImage);

                if (isImmediatelySaved)
                    Db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
            
        }

        public void RemoveType(Good good, TypeT type, bool isSaveNow = false)
        {
            try
            {
                if (good == null || good.Id == 0)
                    return;
                if (type == null || type.Id == 0)
                    return;
                var dbtype =
                    Db.Set<GoodType>().Single(it => it.TypeId == type.Id && it.GoodId == good.Id);
                Db.Set<GoodType>().Remove(dbtype);
                if (isSaveNow)
                    Db.SaveChanges();

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }
        public void AddMaterial(Good good, Material material,int? value=null, bool isSaveNow = false)
        {
            try
            {
                if (good == null || good.Id == 0)
                    throw new ArgumentException("No good provided.");
                if (material == null || material.Id == 0)
                    throw new ArgumentException("No material provided.");

                Db.Set<GoodMaterial>().Add(new GoodMaterial
                {
                    MaterialId = material.Id,
                    GoodId = good.Id,
                    Value = value
                });
                if (isSaveNow)
                    Db.SaveChanges();

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void RemoveMaterial(Good good, Material material, bool isSaveNow = false)
        {
            try
            {
                if (good == null || good.Id == 0)
                    return;
                if (material == null || material.Id == 0)
                    return;
                var dbMaterial =
                    Db.Set<GoodMaterial>().Single(it => it.MaterialId == material.Id && it.GoodId == good.Id);
                Db.Set<GoodMaterial>().Remove(dbMaterial);
                if (isSaveNow)
                    Db.SaveChanges();

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void AddCategory(Good good, Category category, bool isSaveNow = false)
        {
            try
            {
                if (good == null || good.Id == 0)
                    throw new ArgumentException("No good provided.");
                if (category == null || category.Id == 0)
                    throw new ArgumentException("No category provided.");

                Db.Set<GoodCategory>().Add(new GoodCategory
                {
                    CategoryId = category.Id,
                    GoodId = good.Id
                });
                if (isSaveNow)
                    Db.SaveChanges();

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void RemoveCategory(Good good, Category category, bool isSaveNow = false)
        {
            try
            {
                if (good == null || good.Id == 0)
                    return;
                if (category == null || category.Id == 0)
                    return;
                var dbCategory =
                    Db.Set<GoodCategory>().Single(it => it.CategoryId == category.Id && it.GoodId == good.Id);
                Db.Set<GoodCategory>().Remove(dbCategory);
                if (isSaveNow)
                    Db.SaveChanges();

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void AddDesigner(Good good,Designer designer, bool isSaveNow = false)
        {
            try
            {
                if (good == null || good.Id == 0)
                    throw new ArgumentException("No good provided.");
                if (designer == null || designer.Id == 0)
                    throw new ArgumentException("No designer provided.");

                Db.Set<GoodDesigner>().Add(new GoodDesigner
                {
                    DesignerId = designer.Id,
                    GoodId = good.Id
                });
                if (isSaveNow)
                    Db.SaveChanges();

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }
        public void RemoveDesigner(Good good, Designer designer,bool isSaveNow =false)
        {
            try
            {
                if (good == null || good.Id == 0)
                   return;
                if (designer == null || designer.Id == 0)
                    return;
                var dbDesigner =
                    Db.Set<GoodDesigner>().Single(it => it.DesignerId == designer.Id && it.GoodId == good.Id);
                Db.Set<GoodDesigner>().Remove(dbDesigner);
                if (isSaveNow)
                    Db.SaveChanges();
                
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }
        public void SetConnection(string connection)
        {
           throw new NotImplementedException();
        }

        public void SetContext(DbContext db)
        {
            Db = db;
        }

        protected void MarkGoodRelationsDeleted(int goodId)
        {
            var gallery = GetGallery(goodId).Where(it=>!it.IsMain);
            var mainImg = GetMainImg(goodId);
            if (gallery!= null && gallery.Any())
                Db.Set<GoodImage>().RemoveRange(gallery);
            if (mainImg!= null)
                Db.Set<GoodImage>().Remove(mainImg);
            var materials = Db.Set<GoodMaterial>().Where(it => it.GoodId == goodId);
            if (materials.Any())
                Db.Set<GoodMaterial>().RemoveRange(materials);
            var relativeGoods = Db.Set<RelativeGood>().Where(it => it.GoodId == goodId || it.RelativeGoodId ==goodId).Distinct();
            if (relativeGoods.Any())
                Db.Set<RelativeGood>().RemoveRange(relativeGoods);
            var  colors = Db.Set<GoodColor>().Where(it => it.GoodId == goodId );

            if (materials.Any())
                Db.Set<GoodColor>().RemoveRange(colors);
            var categories = Db.Set<GoodCategory>().Where(it => it.GoodId == goodId);

            if (categories.Any())
                Db.Set<GoodCategory>().RemoveRange(categories);
            var types = Db.Set<GoodType>().Where(it => it.GoodId == goodId);

            if (types.Any())
                Db.Set<GoodType>().RemoveRange(types);
            var designers = Db.Set<GoodDesigner>().Where(it => it.GoodId == goodId);

            if (designers.Any())
                Db.Set<GoodDesigner>().RemoveRange(designers);

        }

        public void InitGood(Good good,GoodInitOptions options =GoodInitOptions.Default)
        {
            if (options.HasFlag(GoodInitOptions.InitColors))
            {
                good.Colors = (from gColor in Db.Set<GoodColor>()
                    join color in Db.Set<Color>() on gColor.ColorId equals color.Id
                    where gColor.GoodId == good.Id
                    select color).ToList();
            }
            if (options.HasFlag(GoodInitOptions.InitMaterial))
            {
                good.Materials =
                    (from gMaterial in Db.Set<GoodMaterial>()
                     join material in Db.Set<Material>() on gMaterial.MaterialId equals material.Id
                        where gMaterial.GoodId == good.Id
                        select new GoodMaterial
                        {
                            Value = gMaterial.Value,
                            GoodId = gMaterial.GoodId,
                            Good = good,
                            Material = material,
                            MaterialId = gMaterial.MaterialId
                        }).ToList();


            }
            if (options.HasFlag(GoodInitOptions.InitSingleImage))
            {
                var img = GetMainImg(good.Id);
                good.MainImage = img?.Image;
            }
            if (options.HasFlag(GoodInitOptions.InitImageGallery))
            {
                var query2 = from goodImage in Db.Set<GoodImage>()
                    join attachment in Db.Set<Attachment>() on goodImage.ImageId equals attachment.Id
                    where goodImage.GoodId == good.Id
                    select new GoodImage
                    {
                        GoodId = goodImage.GoodId,
                        Good = good,
                        ImageId = goodImage.ImageId,
                        Image = attachment,
                        IsMain = goodImage.IsMain
                    }
            
            ;


                good.Gallery = query2.Where(it => !it.IsMain).Select(it => it.Image).ToList();
            }

            if (options.HasFlag(GoodInitOptions.InitDesigners))
                good.Designers = 
                    (
                    from goodDesigner in Db.Set<GoodDesigner>()
                    join designer in Db.Set<Designer>() on goodDesigner.DesignerId equals designer.Id
                    where goodDesigner.GoodId == good.Id
                    select designer).ToList();
            if (options.HasFlag(GoodInitOptions.InitCategories))
                good.Categories =
                    (from goodCategory in Db.Set<GoodCategory>()
                    join category in Db.Set<Category>() on goodCategory.CategoryId equals category.Id
                    where goodCategory.GoodId == good.Id
                    select category).ToList();

            if (options.HasFlag(GoodInitOptions.InitTypes))
                good.Types =
                    (from goodType in Db.Set<GoodType>()
                     join type in Db.Set<odec.Server.Model.Store.Type>() on goodType.TypeId equals type.Id
                     where goodType.GoodId == good.Id
                     select type).ToList();

        }


        public override void Save(Good entity)
        {
            try
            {
                LogEventManager.Logger.Info("Start Save(Good entity)");
                base.Save(entity);
                LogEventManager.Logger.Info("Start was Passed");
                if (entity.Designers != null && entity.Designers.Any())
                    foreach (var designer in entity.Designers)
                        AddDesigner(entity, designer);
                LogEventManager.Logger.Info("Designers Added to context");
                if (entity.Categories != null && entity.Categories.Any())
                    foreach (var category in entity.Categories)
                        AddCategory(entity, category);
                LogEventManager.Logger.Info("Categories Added to context");
                if (entity.Types != null && entity.Types.Any())
                    foreach (var type in entity.Types)
                        AddType(entity, type);
                LogEventManager.Logger.Info("Types Added to context");
                if (entity.Colors != null && entity.Colors.Any())
                    foreach (var color in entity.Colors)
                        AddColor(entity, color);
                LogEventManager.Logger.Info("colors Added to context");
                if (entity.Materials != null && entity.Materials.Any())
                    foreach (var material in entity.Materials)
                        AddMaterial(entity, material.Material, material.Value);
                LogEventManager.Logger.Info("Materials Added to context");
                if (entity.MainImage != null)
                    AddImage(entity, new GoodImage
                    {
                        GoodId = entity.Id,
                        Image = entity.MainImage,
                        IsMain = true
                    });
                LogEventManager.Logger.Info("Image Added to context");
                if (entity.Gallery == null || !entity.Gallery.Any()) return;
                foreach (var image in entity.Gallery)
                    AddImage(entity, new GoodImage
                    {
                        GoodId = entity.Id,
                        Image = image,
                        IsMain = true
                    });
                LogEventManager.Logger.Info("Gallery Added to context");
                Db.SaveChanges();
                LogEventManager.Logger.Info("Context Saved Added to context");
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public override void Delete(Good entity)
        {
            try
            {
                if (entity.Id == 0)
                    return;
                MarkGoodRelationsDeleted(entity.Id);
                Db.SaveChanges();
                base.Delete(entity);
            }
            catch (Exception ex )
            {
                LogEventManager.Logger.Error(ex.Message,ex);
                throw;
            }
            
        }
    }
}
