using odec.Framework.Generic;

namespace odec.Server.Model.Store.Filters
{
    public class StoreFilter: FilterBase
    {
        public StoreFilter()
        {
            IsOnlyActive = true;
        }
        public bool IsOnlyActive { get; set; }
    }
}
