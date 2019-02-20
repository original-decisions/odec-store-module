using System;
using System.Collections.Generic;
using odec.Framework.Generic;
using odec.Framework.Generic.Utility;
using odec.Server.Model.Store.Helpers.Enums;

namespace odec.Server.Model.Personal.Store.Filters
{
    public class UserStoreGoodsFilter<TStoreKey, TArrayKey,TUserKey>:FilterBase
    {
        public UserStoreGoodsFilter()
        {

            GoodInitOptions = GoodInitOptions.Default;
            GoodDepthInterval = new Interval<decimal?>();
            GoodWidthInterval = new Interval<decimal?>();
            GoodHeightInterval = new Interval<decimal?>();
        }

        public TUserKey UserId { get; set; }
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
        public Interval<decimal?> BasePriceInterval { get; set; }

        public Interval<DateTime?> DateCreatedInterval { get; set; }
        public string Articul { get; set; }
        public string SerialNumber { get; set; }
        public string Name { get; set; }
    }
}
