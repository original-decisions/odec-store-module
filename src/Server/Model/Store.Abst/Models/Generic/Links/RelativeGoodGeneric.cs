using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace odec.Server.Model.Store.Abst.Models.Generic.Links
{
    public class RelativeGoodGeneric<TGood,TGoodId>
    {
        [Key,Column(Order = 0)]
        public TGoodId GoodId { get; set; }

        public TGood Good { get; set; }

        [Key, Column(Order = 1)]
        public TGoodId RelativeGoodId { get; set; }

        [ForeignKey("RelativeGoodId")]
        public TGood Relative { get; set; }

    }
}
