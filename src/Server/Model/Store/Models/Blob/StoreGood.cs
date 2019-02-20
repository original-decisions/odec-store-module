using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace odec.Server.Model.Store.Blob
{
    /// <summary>
    /// Link entity between good and Store
    /// </summary>
    public class StoreGood
    {
        //    [Key, Column(Order = 0)]
        // [Index("ix_Articul_StoreId", IsUnique = true, Order = 0)]
        /// <summary>
        /// Store Indentity 
        /// </summary>
        public int StoreId { get; set; }
        /// <summary>
        /// Store navigation property
        /// </summary>
        public Store Store { get; set; }
        //  [Key, Column(Order = 1)]
        /// <summary>
        /// Good Identity
        /// </summary>
        public int GoodId { get; set; }
        /// <summary>
        /// Good navigation property
        /// </summary>
        public Good Good { get; set; }
        /// <summary>
        /// Number of reserved goods on the particular store
        /// </summary>
        public int ReservedCounter { get; set; }
        /// <summary>
        /// Number of available goods on the particular store
        /// </summary>
        [Required, DefaultValue(1)]
        public int StoreQuantity { get; set; }
        /// <summary>
        /// Good Articul
        /// </summary>
        [Required]
        //[Index("ix_Articul_StoreId", IsUnique = true, Order = 1)]
        [StringLength(255)]
        public string Articul { get; set; }
        //[Required]
        //[Index(IsUnique = true)]
        //public string SerialNumber { get; set; }

    }
}