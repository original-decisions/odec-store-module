using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace odec.Server.Model.Store.Blob
{
    /// <summary>
    /// Linked Entity for good and its image
    /// </summary>
    public class GoodImage
    {
        // [Key, Column(Order = 1)]
        //[Key,Column(Order = 1), Index("ix_goodId_IsMain_ImageId", IsUnique = true, Order = 2)]
        /// <summary>
        /// Image Identity
        /// </summary>
        public int ImageId { get; set; }
        /// <summary>
        /// Image navigation property
        /// </summary>
        public Attachment.Attachment Image { get; set; }
        // [Index("ix_goodId_IsMain_ImageId", IsUnique = true,Order = 1)]
        /// <summary>
        /// Is it main image of the good
        /// </summary>
        public bool IsMain { get; set; }
        //   [Key, Column(Order = 0)]
        //[Key, Column(Order = 0), Index("ix_goodId_IsMain_ImageId", IsUnique = true,Order = 0)]
        /// <summary>
        /// Good Identity
        /// </summary>
        public int GoodId { get; set; }
        /// <summary>
        ///   Good navigation property
        /// </summary>
        public Good Good { get; set; }
    }
}
