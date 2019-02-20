using odec.Framework.Generic.WithUnifiedId;

namespace odec.Server.Model.Store.Abst.Models.Generic.Glossaries
{
    public class GoodImageGeneric<T, TGoodId, TGood> : Image<T> where T : struct
    {
        public TGood Good { get; set; }

        public TGoodId GoodId { get; set; }
        public bool IsMain { get; set; }
    }
}
