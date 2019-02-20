using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace odec.Server.Model.Store.ParamBased
{
    public class StoreGood
    {
        [Key,Column(Order = 0)]
        public int StoreId { get; set; }
        public Store Store { get; set; }
        [Key, Column(Order = 1)]
        public int GoodId { get; set; }
        public Good Good { get; set; }
        //[Key, Column(Order = 2)]
        //public int SizeId { get; set; }
        //public Size Size { get; set; }
        ////TODO:think about rgb palette. I mean 4 included in key values rgba
        //[Key, Column(Order = 3)]
        //public int ColorId { get; set; }
        //[Required, DefaultValue(0)]
        public int ReservedCounter { get; set; }
        [Required,DefaultValue(1)]
        public int StoreQuantity { get; set; }

    }
}