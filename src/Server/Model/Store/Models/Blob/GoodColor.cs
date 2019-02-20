
namespace odec.Server.Model.Store.Blob
{
    /// <summary>
    /// Linked Entity for good and color
    /// </summary>
    public class GoodColor
    {
        //    [Key,Column(Order = 0)]
        /// <summary>
        /// Good Identity
        /// </summary>
        public int GoodId { get; set; }
        /// <summary>
        /// Good navigation property
        /// </summary>
        public Good Good { get; set; }
        //    [Key, Column(Order = 1)]
        /// <summary>
        /// Category Identity
        /// </summary>
        public int ColorId { get; set; }
        /// <summary>
        /// Category navigation property
        /// </summary>
        public Color Color { get; set; }

    }
}