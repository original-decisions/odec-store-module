using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace odec.Server.Model.Store.ParamBased
{
    public class GoodType
    {

        [Key, Column(Order = 0)]
        public int GoodId { get; set; }
        public Good Good { get; set; }
        [Key, Column(Order = 1)]
        public int TypeId { get; set; }
        public Type Type { get; set; }
    }
}