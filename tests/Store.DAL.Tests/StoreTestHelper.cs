using System;
using Microsoft.EntityFrameworkCore;
using odec.Server.Model.Attachment;
using odec.Server.Model.Attachment.Extended;
using odec.Server.Model.Category;
using odec.Server.Model.Personal.Store;
using odec.Server.Model.Store;
using odec.Server.Model.Store.Blob;
using odec.Server.Model.Store.Clothes;
using odec.Server.Model.User;
using Type = odec.Server.Model.Store.Type;

namespace Store.DAL.Tests
{
    public static class StoreTestHelper
    {

        internal static Attachment GenerateAttachment()
        {
            return new Attachment
            {
                Name = "Test",
                Code = Guid.NewGuid().ToString(),
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
                Content = new byte[] {1,1,1,1,1,1},
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
        internal static Good GenerateGood(string code,Size size)
        {
            return new Good
            {
                Name = "My Conversation",
                Code = code,
                IsActive = true,
                DateCreated = DateTime.Now,
                Articul = Guid.NewGuid().ToString(),
                BasePrice = 1000,
                MarkUp = 1,
                Height = 50,
                Width = 50,
                Depth = 50,
                SizeId = size.Id,
                SerialNumber = Guid.NewGuid().ToString(),
                ShortDescription = string.Empty,
                Description = string.Empty,
                SortOrder = 0,
            };
        }
        internal static void PopulateDefaultPersonalStoreData(DbContext db)
        {
            try
            {
                PopulateDefaultStoreDataCtx(db);
                db.Set<Role>().Add(new Role
                {
                    Id = 1,
                    Name = "Crafter"

                });
                db.Set<Role>().Add(new Role
                {
                    Id = 2,
                    Name = "User",
                });
              //  db.Set<UserRole>().Add(new UserRole { RoleId = 1, UserId = 1 });
                var andrew = new User
                {
                    Id = 1,
                    UserName = "Andrew",

                };
                db.Set<User>().Add(andrew);
                db.Set<User>().Add(new User
                {
                    Id = 2,
                    UserName = "Alex",
                });
                db.Set<UserPersonalStore>().Add(new UserPersonalStore
                {
                    IsActive = true,
                    UserId = andrew.Id,
                    StoreId = 1
                });

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        internal static void PopulateDefaultStoreDataCtx(DbContext db)
        {
            try
            {
                var store = new odec.Server.Model.Store.Store
                {
                    Code = "Store1",
                    Name = "Store",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                };

                db.Set<odec.Server.Model.Store.Store>().Add(store);
                var noScale = new Scale
                {
                    Code = "NOSCALE",
                    Name = "Not Selected",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                };
                db.Set<Scale>().Add(noScale);
                var noSize = new Size
                {
                    Code = "NOSIZE",
                    Name = "Not Selected",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    Scale = noScale
                };
                db.Set<Size>().Add(noSize);
                var good = GenerateGood("TestGoodPopulated",noSize);
                
                var good2 = GenerateGood("TestGoodPopulated2", noSize);
                var good3 = GenerateGood("TestGoodPopulated3", noSize);
                db.Set<Good>().Add(good);
                db.Set<Good>().Add(good2);
                db.Set<Good>().Add(good3);
                db.SaveChanges();
                db.Set<RelativeGood>().Add(new RelativeGood
                {
                    GoodId = good.Id,
                    RelativeGoodId = good2.Id

                });
                db.Set<RelativeGood>().Add(new RelativeGood
                {
                    GoodId = good.Id,
                    RelativeGoodId = good3.Id

                });
               
                db.Set<StoreGood>().Add(new StoreGood
                {
                    GoodId = good.Id,
                    StoreId = store.Id,
                    Articul = good.Articul,
                    StoreQuantity = 1
                });
                db.Set<StoreGood>().Add(new StoreGood
                {
                    GoodId = good2.Id,
                    StoreId = store.Id,
                    Articul = good2.Articul,
                    StoreQuantity = 1
                });
                db.Set<StoreGood>().Add(new StoreGood
                {
                    GoodId = good3.Id,
                    StoreId = store.Id,
                    Articul = good3.Articul,
                    StoreQuantity = 1
                });
                var designer1 = new Designer
                {
                    Code = "D&G",
                    Name = "Dolche and Gabbana",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                };
                var designer2 = new Designer
                {
                    Code = "SCALA",
                    Name = "Scala",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                };
                db.Set<Designer>().Add(designer1);
                db.Set<GoodDesigner>().Add(new GoodDesigner
                {
                    GoodId = good.Id,
                    DesignerId = designer1.Id
                });
                db.Set<Designer>().Add(designer2);
                db.Set<GoodDesigner>().Add(new GoodDesigner
                {
                    GoodId = good.Id,
                    DesignerId = designer2.Id
                });
                var typeHat = new Type
                {
                    Code = "T1",
                    Name = "Hat",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                };
                db.Set<Type>().Add(typeHat);
                db.Set<GoodType>().Add(new GoodType
                {
                    GoodId = good.Id,
                    TypeId = typeHat.Id
                });
                var typeTrousers = new Type
                {
                    Code = "T2",
                    Name = "trousers",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                };
                db.Set<Type>().Add(typeTrousers);
                db.Set<GoodType>().Add(new GoodType
                {
                    GoodId = good.Id,
                    TypeId = typeTrousers.Id
                });

                var category1 = new Category
                {
                    Code = "C1",
                    Name = "C1",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                };
                db.Set<Category>().Add(category1);
                db.Set<GoodCategory>().Add(new GoodCategory
                {
                    GoodId = good.Id,
                    CategoryId = category1.Id
                });
                var category2 = new Category
                {
                    Code = "C2",
                    Name = "C2",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                };
                db.Set<Category>().Add(category2);
                db.Set<GoodCategory>().Add(new GoodCategory
                {
                    GoodId = good.Id,
                    CategoryId = category2.Id
                });
                var iron = new Material
                {
                    Code = "I",
                    Name = "Iron",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                };
                db.Set<Material>().Add(iron);
                db.Set<GoodMaterial>().Add(new GoodMaterial
                {
                    GoodId = good.Id,
                    MaterialId = iron.Id
                });
                var wood = new Material
                {
                    Code = "W",
                    Name = "Wood",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                };
                db.Set<Material>().Add(iron);
                db.Set<GoodMaterial>().Add(new GoodMaterial
                {
                    GoodId = good.Id,
                    MaterialId = wood.Id
                });
                var img1 = GenerateAttachment();
                db.Set<Attachment>().Add(img1);
                db.Set<GoodImage>().Add(new GoodImage
                {
                    GoodId = good.Id,
                    ImageId = img1.Id,
                    IsMain = true
                });
                var img2 = GenerateAttachment();
                db.Set<GoodImage>().Add(new GoodImage
                {
                    GoodId = good.Id,
                    ImageId = img2.Id,
                    IsMain = false
                });
                db.Set<Attachment>().Add(img2);
                var img3 = GenerateAttachment();
                db.Set<Attachment>().Add(img3);
                db.Set<GoodImage>().Add(new GoodImage
                {
                    GoodId = good.Id,
                    ImageId = img3.Id,
                    IsMain = false
                });

                var red = new Color
                {
                    Code = "RED",
                    Name = "Red",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    A = 1,
                    B = 0,
                    R = 255,
                    G = 0
                };
                db.Set<Color>().Add(red);
                db.Set<GoodColor>().Add(new GoodColor
                {
                    GoodId = good.Id,
                    ColorId = red.Id
                });
                var green = new Color
                {
                    Code = "GREEN",
                    Name = "Green",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    A = 1,
                    B = 0,
                    R = 0,
                    G = 255
                };
                db.Set<Color>().Add(green);
                db.Set<GoodColor>().Add(new GoodColor
                {
                    GoodId = good.Id,
                    ColorId = green.Id
                });
                var blue = new Color
                {
                    Code = "BLUE",
                    Name = "Blue",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    A = 1,
                    B = 255,
                    R = 0,
                    G = 0
                };
                db.Set<Color>().Add(blue);
                db.Set<GoodColor>().Add(new GoodColor
                {
                    GoodId = good.Id,
                    ColorId = blue.Id
                });
                db.SaveChanges();
               
            }
            catch (Exception ex)
            {
                throw;
            }
        }
      
    }
}
