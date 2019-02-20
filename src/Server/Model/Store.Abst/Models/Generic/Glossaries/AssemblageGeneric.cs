using odec.Framework.Generic;

namespace odec.Server.Model.Store.Abst.Models.Generic.Glossaries
{
    public class AssemblageGeneric<T,TSex,TSexId> : Glossary<T>
    {
        public TSexId SexId { get; set; }

        public TSex Sex { get; set; }

    }
}
