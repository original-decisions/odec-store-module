using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using odec.Framework.Generic;
using odec.Server.Model.Store.Clothes;
using Categ = odec.Server.Model.Category.Category;
namespace odec.Server.Model.Store.Blob
{
    /// <summary>s
    /// Good
    /// </summary>
    public class Good :Glossary<int>
    {
        /// <summary>
        /// Articul
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Articul { get; set; }
        /// <summary>
        /// Serial Number
        /// </summary>
        [Required]
        [StringLength(200)]
        public string SerialNumber { get; set; }
        /// <summary>
        /// Good friendly Name
        /// </summary>
        [Required]
        [StringLength(200)]
        public override string Name { get; set; }
        [Required]
        public decimal BasePrice { get; set; }
        /// <summary>
        /// Mark-Up for base price
        /// </summary>
        [Required,DefaultValue(0.10)]
        public decimal MarkUp { get; set; }
        /// <summary>
        /// Good Description
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        [DefaultValue("")]
        public string Description { get; set; }
        /// <summary>
        /// Short Description
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        [DefaultValue("")]
        [MaxLength(400)]
        public string ShortDescription { get; set; }
        /// <summary>
        /// Size Indentifier
        /// </summary>
        public int SizeId { get; set; }
        /// <summary>
        /// Navigation property Size
        /// </summary>
        public Size Size { get; set; }
        /// <summary>
        /// [Not Mapped] - Collection of Colors available for good. 
        /// </summary>
        [NotMapped]
        public IEnumerable<Color> Colors { get; set; }
        /// <summary>
        ///  [Not Mapped] - Collection of Materials available for good. 
        /// </summary>
        [NotMapped]
        public IEnumerable<GoodMaterial> Materials { get; set; }
        /// <summary>
        /// [Not Mapped] - Main image of the good
        /// </summary>
        [NotMapped]
        public Attachment.Attachment MainImage { get; set; }
        /// <summary>
        /// [Not Mapped] - Full Gallery of good images
        /// </summary>
        [NotMapped]
        public IEnumerable<Attachment.Attachment> Gallery { get; set; }
        /// <summary>
        ///  [Not Mapped] - Collection of good designers
        /// </summary>
        [NotMapped]
        public IEnumerable<Designer> Designers { get; set; }
        /// <summary>
        /// [Not Mapped] - Collection of linked to good good types
        /// </summary>
        [NotMapped]
        public IEnumerable<Type> Types { get; set; }
        /// <summary>
        /// [Not Mapped] - Collection of good categories
        /// </summary>
        [NotMapped]
        public IEnumerable<Categ> Categories { get; set; }
        /// <summary>
        /// Height
        /// </summary>
        public decimal? Height { get; set; }
        /// <summary>
        /// Width
        /// </summary>
        public decimal? Width { get; set; }
        /// <summary>
        /// Depth
        /// </summary>
        public decimal? Depth { get; set; }
        
    }
}