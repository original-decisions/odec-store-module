using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace odec.Server.Model.Store.Abst.Models.Generic.Links.UnifiedKey
{
    /// <summary>
    /// Generic server models - link entity between goods (Many-to-Many Rel)
    /// </summary>
    /// <typeparam name="TKey">Identitity Type</typeparam>
    /// <typeparam name="TGood">Good Type</typeparam>
    public class RelativeGoodGeneric<TKey,TGood>
    {
        /// <summary>
        /// Good identity
        /// </summary>
        //[Key,Column(Order = 0)]
        public TKey GoodId { get; set; }
        /// <summary>
        /// Good navigation property
        /// </summary>
        public TGood Good { get; set; }
        /// <summary>
        /// Relative good Identity
        /// </summary>
        //[Key, Column(Order = 1)]
        public TKey RelativeGoodId { get; set; }
        /// <summary>
        /// Related good navigation property
        /// </summary>
        [ForeignKey("RelativeGoodId")]
        public TGood Relative { get; set; }

    }
}
