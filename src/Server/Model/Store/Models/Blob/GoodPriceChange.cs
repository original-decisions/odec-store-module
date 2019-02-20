using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace odec.Server.Model.Store.Blob
{
    /// <summary>
    /// Entity that discribes the price change history 
    /// </summary>
    public class GoodPriceChange
    {
        //    [Key,Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // public int Id { get; set; }
        //    [Key, Column(Order = 1)]
        /// <summary>
        /// Date Change
        /// </summary>
        public DateTime DateChange { get; set; }
        /// <summary>
        /// Good identity
        /// </summary>
        //  [Key, Column(Order = 0)]
        public int GoodId { get; set; }
        /// <summary>
        /// Good navigation property
        /// </summary>
        public Good Good { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }
    }
}