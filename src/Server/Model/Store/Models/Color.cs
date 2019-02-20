using System.ComponentModel.DataAnnotations;
using odec.Server.Model.Store.Abst.Models.Generic.Glossaries.UnifiedKey;

namespace odec.Server.Model.Store
{
    /// <summary>
    /// Server Model - Color
    /// </summary>
    public class Color:ColorGeneric<int>
    {
        /// <summary>
        /// Name
        /// </summary>
        [StringLength(50)]
        public override string Name{ get; set; }
        /// <summary>
        /// Red
        /// </summary>
        public int R { get; set; }
        /// <summary>
        /// Green
        /// </summary>
        public int G { get; set; }
        /// <summary>
        /// Blue
        /// </summary>
        public int B { get; set; }
        /// <summary>
        /// Opacity
        /// </summary>
        public decimal A { get; set; }
    }
}
