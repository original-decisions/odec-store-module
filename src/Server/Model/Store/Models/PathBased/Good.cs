using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using odec.Server.Model.Store.Abst.Models.Generic.Glossaries.UnifiedKey;
using odec.Server.Model.Store.Clothes;

namespace odec.Server.Model.Store.PathBased
{
    /// <summary>
    /// Good
    /// </summary>
    public class Good :GoodGeneric<int,Type,Season>
    {
        /// <summary>
        /// Mark-Up for 
        /// </summary>
        [Required,DefaultValue(0.10)]
        public decimal MarkUp { get; set; }

        public decimal? Height { get; set; }
        public decimal? Width { get; set; }
        public decimal? Depth { get; set; }



    }
}