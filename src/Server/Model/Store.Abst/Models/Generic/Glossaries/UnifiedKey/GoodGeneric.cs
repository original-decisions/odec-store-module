using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using odec.Framework.Generic;

namespace odec.Server.Model.Store.Abst.Models.Generic.Glossaries.UnifiedKey
{
    //TODO:It needs refactoring
    /// <summary>
    /// Обобщенный класс - товар
    /// </summary>
    /// <typeparam name="TKey">Тип идентификатора</typeparam>
    /// <typeparam name="TGoodType">Типа Типа товара</typeparam>
    /// <typeparam name="TSeason">ТИп Сезона</typeparam>
    public class GoodGeneric<TKey, TGoodType, TSeason> : Glossary<TKey>
    {
        /// <summary>
        /// Кол-во на складе
        /// </summary>
        [Required]
        public int StoreQuantity { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        [StringLength(200)]
        public override string Name { get; set; }
        /// <summary>
        /// СТарая цена
        /// </summary>
        public decimal OldCost { get; set; }
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
        /// Сезон
        /// </summary>
        public TSeason Season { get; set; }
        /// <summary>
        /// Идентификатор сезона
        /// </summary>
        [Required]
        public TKey SeasonId { get; set; }
        /// <summary>
        /// Идентификатор Типа товара
        /// </summary>
        [Obsolete("We left it for compatability with earlier dbs will be removed in future releases")]
        //[Required]
        public TKey GoodTypeId { get; set; }
        /// <summary>
        /// Тип товара
        /// </summary>
        public virtual TGoodType  GoodType { get; set; }
        /// <summary>
        /// Цена
        /// </summary>
        [Required]
        public decimal Cost { get; set; }
        /// <summary>
        /// Описание товара
        /// </summary>
        [Required]
        [DefaultValue("")]
        [MaxLength(1536)]
        public string Description { get; set; }
        /// <summary>
        /// Короткое описание
        /// </summary>
        [Required]
        [DefaultValue("")]
        [MaxLength(400)]
        public string ShortDescription { get; set; }
        /// <summary>
        /// Путь к изображению товара
        /// </summary>
        [Required]
        public string ImagePath { get; set; }
    }
}