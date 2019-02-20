using odec.Framework.Generic.WithUnifiedId;

namespace odec.Server.Model.Store.Abst.Models.Generic.Glossaries.UnifiedKey
{
    /// <summary>
    /// Обобщенный класс- изображение товара
    /// </summary>
    /// <typeparam name="TKey">Тип идентификатора</typeparam>
    /// <typeparam name="TGood">Тип Товара</typeparam>
    public class GoodImageGeneric<TKey, TGood> : Image<TKey, TGood> where TKey : struct
    {
        /// <summary>
        /// Флаг- является ли изображение основным
        /// </summary>
        public bool IsMain { get; set; }
    }
}
