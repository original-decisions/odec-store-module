using System.ComponentModel.DataAnnotations;
using odec.Framework.Generic;

namespace odec.Server.Model.Store.Abst.Models.Generic.Glossaries
{
    public class SizeGeneric<T, TScaleId, TScale> : Glossary<T>
    {
        [StringLength(20)]
        public override string Name { get; set; }

        [Required]
        public TScaleId ScaleId { get; set; }

        public TScale Scale { get; set; }
    }
}
