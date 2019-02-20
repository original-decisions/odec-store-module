
namespace odec.Server.Model.Store.Blob
{
    /// <summary>
    ///  Linked Entity for good and designer
    /// </summary>
    public class GoodDesigner
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
        /// Designer Identity
        /// </summary>
        public int DesignerId { get; set; }
        /// <summary>
        /// Designer navigation property
        /// </summary>
        public Designer Designer { get; set; }
    }
}