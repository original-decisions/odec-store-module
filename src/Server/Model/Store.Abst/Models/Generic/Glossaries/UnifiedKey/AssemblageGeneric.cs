using odec.Framework.Generic;

namespace odec.Server.Model.Store.Abst.Models.Generic.Glossaries.UnifiedKey
{
    /// <summary>
    /// Обобщенный класс - Коллекция
    /// </summary>
    /// <typeparam name="TKey">Тип идентификатора</typeparam>
    /// <typeparam name="TSex">Тип пола</typeparam>
    public class AssemblageGeneric<TKey, TSex> : Glossary<TKey>
    {
        /// <summary>
        /// Идентификатор пола
        /// </summary>
        public TKey SexId { get; set; }
        /// <summary>
        /// Пол
        /// </summary>
        public TSex Sex { get; set; }

    }
}
