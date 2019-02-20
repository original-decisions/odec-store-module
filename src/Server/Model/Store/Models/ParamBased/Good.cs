using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using odec.Framework.Generic;

namespace odec.Server.Model.Store.ParamBased
{
    /// <summary>
    /// Good
    /// </summary>
    public class Good :Glossary<int>
    {
        /// <summary>
        /// Артикул
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Articul { get; set; }
        /// <summary>
        /// Серийный номер
        /// </summary>
        [Required]
        [StringLength(20)]
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
        [Required, DefaultValue(0.10)]
        public decimal MarkUp { get; set; }
        /// <summary>
        /// Описание товара
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        [DefaultValue("")]
        public string Description { get; set; }
        /// <summary>
        /// Короткое описание
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        [DefaultValue("")]
        [MaxLength(400)]
        public string ShortDescription { get; set; }
    }
}