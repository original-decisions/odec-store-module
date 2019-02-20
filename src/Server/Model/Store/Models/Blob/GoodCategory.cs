using CategoryE = odec.Server.Model.Category.Category;
namespace odec.Server.Model.Store.Blob
{
    /// <summary>
    /// Linked Entity for good and category
    /// </summary>
    public class GoodCategory
    {
        //  [Key, Column(Order = 0)]
        /// <summary>
        /// Good Identity
        /// </summary>
        public int GoodId { get; set; }
        /// <summary>
        /// Good navigation property
        /// </summary>
        public Good Good { get; set; }
        //  [Key, Column(Order = 1)]
        /// <summary>
        /// Category Identity
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// Category navigation property
        /// </summary>
        public CategoryE Category { get; set; }
    }
}