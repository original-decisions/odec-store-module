using System.ComponentModel.DataAnnotations;
using odec.Framework.Generic;

namespace odec.Server.Model.Store.Abst.Models.Generic.Glossaries.UnifiedKey
{
    /// <summary>
    /// Обобщенный класс - размер 
    /// </summary>
    /// <typeparam name="TKey">Тип идентификатора</typeparam>
    /// <typeparam name="TScale">Тип щкалы</typeparam>
    public class SizeGeneric<TKey, TScale> : Glossary<TKey>
    {
        /// <summary>
        /// Имя
        /// </summary>
        [StringLength(15)]
        new public string Name { get; set; }
        /// <summary>
        /// Идентификатор шкалы
        /// </summary>
        [Required]
        public TKey ScaleId { get; set; }
        /// <summary>
        /// Шкала
        /// </summary>
        public TScale Scale { get; set; }
    }
}
