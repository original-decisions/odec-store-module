using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using odec.Framework.Generic;

namespace odec.Server.Model.Store.Clothes
{
    /// <summary>
    /// серверный объект - шкала
    /// </summary>
    public class Scale :Glossary<int>
    {
        /// <summary>
        /// название
        /// </summary>
        [StringLength(30)]
        public override  string Name { get; set; }

        /// <summary>
        /// размер
        /// </summary>
        public virtual ICollection<Size> Sizes { get; set; }

    }
}
