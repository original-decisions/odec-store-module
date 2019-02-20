using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using odec.Framework.Generic;

namespace odec.CP.Server.Model.Store.Location.Filters
{
    public class LocationStoreFilter<TKey>: FilterBase
    {
        public LocationStoreFilter()
        {
            IsOnlyActive = true;
        }
        public TKey StoreLocationsIds { get; set; }
        public bool IsOnlyActive { get; set; }
    }
}
