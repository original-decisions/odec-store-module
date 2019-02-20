using System.Collections.Generic;
using odec.Framework.Generic;
using odec.Framework.Generic.Utility;
using odec.Server.Model.Store.Helpers.Enums;

namespace odec.Server.Model.Store.Filters
{
    public class StoreGoodsFilter<TStoreKey,TArrayKey>: FilterBase
    {
        public StoreGoodsFilter()
        {
            GoodInitOptions = GoodInitOptions.Default;
            GoodHeightInterval = new Interval<decimal?>();
            GoodWidthInterval = new Interval<decimal?>();
            GoodDepthInterval = new Interval<decimal?>();
        }
        public TStoreKey StoreId { get; set; }
        public GoodInitOptions GoodInitOptions { get; set; }
        public IList<TArrayKey> GoodCategoryIds { get; set; }
        public IList<TArrayKey> GoodTypeIds { get; set; }
        public IList<TArrayKey> GoodColorIds { get; set; }
        public IList<TArrayKey> GoodDesignerIds { get; set; }
        public IList<TArrayKey> GoodMaterialsIds { get; set; }
        public IList<TArrayKey> GoodSizeIds { get; set; }
        public Interval<decimal?> GoodHeightInterval { get; set; }
        public Interval<decimal?> GoodWidthInterval { get; set; }
        public Interval<decimal?> GoodDepthInterval { get; set; }
    }
}
