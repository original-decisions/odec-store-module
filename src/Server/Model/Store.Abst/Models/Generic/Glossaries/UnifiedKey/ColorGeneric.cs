using System.ComponentModel.DataAnnotations;
using odec.Framework.Generic;

namespace odec.Server.Model.Store.Abst.Models.Generic.Glossaries.UnifiedKey
{
    /// <summary>
    /// Generic server model - Color
    /// </summary>
    /// <typeparam name="TKey">Indentity Type</typeparam>
    public class ColorGeneric<TKey>:Glossary<TKey>
    {
        /// <summary>
        /// Name
        /// </summary>
        [StringLength(50)]
        public override string Name{ get; set; }

      
      //  public ICollection<BasketGood> BasketGoods { get; set; }
    }
}
