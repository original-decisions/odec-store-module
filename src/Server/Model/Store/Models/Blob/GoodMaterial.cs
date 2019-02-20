using System.ComponentModel.DataAnnotations;

namespace odec.Server.Model.Store.Blob
{
    /// <summary>
    /// Link entity between good and material
    /// </summary>
    public class GoodMaterial
    {
        //     [Key,Column(Order = 0)]
        /// <summary>
        /// Good Identity
        /// </summary>
        public int GoodId { get; set; }
        /// <summary>
        /// Good navigation property
        /// </summary>
        public Good Good { get; set; }
        //     [Key,Column(Order = 1)]
        /// <summary>
        /// Material Identity
        /// </summary>
        public int MaterialId { get; set; }
        /// <summary>
        /// Material navigation property
        /// </summary>
        public Material Material { get; set; }
        /// <summary>
        /// Percent value of material in good
        /// </summary>
        [Range(0, 100)]
        public int? Value { get; set; }
    }
}