using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace odec.Server.Model.Store.PathBased
{
    public class GoodPriceChange
    {
        //    [Key,Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // public int Id { get; set; }
        [Key, Column(Order = 1)]
        public DateTime DateChange { get; set; }
        [Key, Column(Order = 0)]
        public int GoodId { get; set; }
        public Good Good { get; set; }
        public decimal Price { get; set; }
    }
}