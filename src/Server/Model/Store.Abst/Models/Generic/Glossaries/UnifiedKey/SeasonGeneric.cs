using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using odec.Framework.Generic;

namespace odec.Server.Model.Store.Abst.Models.Generic.Glossaries.UnifiedKey
{
    /// <summary>
    /// Обобщенный тип - сезон
    /// </summary>
    /// <typeparam name="TKey">Тип идентификатора</typeparam>
    public class SeasonGeneric<TKey> : Glossary<TKey>
    {
        /// <summary>
        /// Имя
        /// </summary>
        [StringLength(255)]
        public override string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        [DefaultValue("")]
        public string Description { get; set; }
    }
}
