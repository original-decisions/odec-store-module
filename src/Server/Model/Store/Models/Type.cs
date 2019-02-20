using System.ComponentModel.DataAnnotations;
using odec.Framework.Generic;

namespace odec.Server.Model.Store
{
    /// <summary>
    /// Type of the good - server model (hat, underwear etc.)
    /// </summary>
    public class Type :Glossary<int>
    {
        /// <summary>
        /// наименование
        /// </summary>
        [StringLength(200)]
        public override string Name { get; set; }
    }
}