using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CategoryE = odec.Server.Model.Category.Category;

namespace odec.Server.Model.Store.PathBased
{
    public class GoodCategory
    {
        [Key, Column(Order = 0)]
        public int GoodId { get; set; }
        public Good Good { get; set; }
        [Key, Column(Order = 1)]
        public int CategoryId { get; set; }
        public CategoryE Category { get; set; }
    }
}