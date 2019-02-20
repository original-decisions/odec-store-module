using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace odec.Server.Model.Store.ParamBased
{
    //TODO:Study this approach
    public class GoodParameter
    {
        [Key,Column(Order = 1)]
        public int ParameterNameId { get; set; }
        public ParameterName Name { get; set; }
        [Key, Column(Order = 0)]
        public int GoodId { get; set; }
        public Good Good { get; set; }
        public string Value { get; set; }
        public string DataType { get; set; }
    }
}
