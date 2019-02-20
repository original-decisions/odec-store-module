using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace odec.Server.Model.Store.Blob
{
    /// <summary>
    /// Link entity between good and good type
    /// </summary>
    public class GoodType
    {

        //   [Key, Column(Order = 0)]
        /// <summary>
        /// Good identity
        /// </summary>
        public int GoodId { get; set; }
        /// <summary>
        /// Good navigation property
        /// </summary>
        public Good Good { get; set; }
        //   [Key, Column(Order = 1)]
        /// <summary>
        /// Good type Identity
        /// </summary>
        public int TypeId { get; set; }
        /// <summary>
        /// Good Type navigtion property
        /// </summary>
        public Type Type { get; set; }
    }
}