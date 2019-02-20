using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using odec.Entity.DAL;
using odec.Framework.Logging;
using odec.Server.Model.Store;
using odec.Server.Model.Store.Clothes;
using odec.Server.Model.Store.Filters;
using odec.Server.Model.Store.Helpers.Enums;
using odec.Server.Model.Store.PathBased;
using odec.Store.DAL.Interop;
using TypeT = odec.Server.Model.Store.Type;

namespace odec.Store.DAL.PathBased
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
                return Db.Set<Good>().Single(good => good.Articul == articul);
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
                return Db.Set<Good>().Single(good => good.Name == name);
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
                return new List<Good>();
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
            return query.SingleOrDefault(it => it.Id == id);
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
                var goods = Db.Set<RelativeGood>().Include(it => it.Relative).ToList().Select(it => it.Relative).ToList();
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
                return Db.Set<GoodColor>().Include(it => it.Color).Select(it => it.Color);
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
                        where goodImg.RelationId == goodId
                        select goodImg);
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
                return (from goodImg in Db.Set<GoodImage>()
                        where goodImg.RelationId == goodId && goodImg.IsMain
                        select goodImg).Single();
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
                return goodIds.Select(it => GetById(it, options));
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void AddImage(Good good, GoodImage image, bool isSaveNow = false)
        {
            throw new NotImplementedException();
        }

        public void RemoveImage(Good good, GoodImage image, bool isSaveNow = false)
        {
            throw new NotImplementedException();
        }

        public void AddColor(Good good, Color color, bool isSaveNow = false)
        {
            throw new NotImplementedException();
        }

        public void RemoveColor(Good good, Color color, bool isSaveNow = false)
        {
            throw new NotImplementedException();
        }

        public void AddType(Good good, TypeT type, bool isSaveNow = false)
        {
            throw new NotImplementedException();
        }

        public void RemoveType(Good good, TypeT type, bool isSaveNow = false)
        {
            throw new NotImplementedException();
        }


        public void SetConnection(string connection)
        {
            throw new NotImplementedException();
        }

        public void SetContext(DbContext db)
        {
            Db = db;
        }
    }
}
