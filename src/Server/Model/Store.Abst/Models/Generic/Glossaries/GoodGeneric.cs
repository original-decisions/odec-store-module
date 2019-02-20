using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using odec.Framework.Generic;

namespace odec.Server.Model.Store.Abst.Models.Generic.Glossaries
{
    public class GoodGeneric<T,TGoodTypeId, TGoodType,TSeasonId, TSeason, TGoodReview, TWish, TBasket> :Glossary<T>
    {
        [Required]
        public int StoreQuantity { get; set; }

        [Required]
        [StringLength(200)]
        public override string Name { get; set; }

        public decimal OldCost { get; set; }

        [Required]
        [StringLength(200)]
        public string Articul { get; set; }


        [Required]
        [StringLength(20)]
        public string SerialNumber { get; set; }


        public TSeason Season { get; set; }

        [Required]
        public TSeasonId SeasonId { get; set; }

        [Required]
        public TGoodTypeId GoodTypeId { get; set; }

        public virtual TGoodType  GoodType { get; set; }
        
        [Required]
        public decimal Cost { get; set; }

        [Required]
        [DefaultValue("")]
        [MaxLength(1536)]
        public string Description { get; set; }

        [Required]
        [DefaultValue("")]
        [MaxLength(400)]
        
        public string ShortDescription { get; set; }

        [Required]
        public string ImagePath { get; set; }

        public virtual ICollection<TGoodReview> GoodReviews { get; set; }

        public virtual ICollection<TWish> InWishList { get; set; }

        public virtual ICollection<TBasket> InBasket { get; set; }


    }
}