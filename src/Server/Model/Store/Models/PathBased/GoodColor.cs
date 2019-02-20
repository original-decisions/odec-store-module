using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace odec.Server.Model.Store.PathBased
{
    public class GoodColor
    {
        [Key,Column(Order = 0)]
        public int GoodId { get; set; }
        public Good Good { get; set; }
        [Key, Column(Order = 1)]
        public int ColorId { get; set; }
        public Color Color { get; set; }

    }
}