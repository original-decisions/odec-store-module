using System.Collections.Generic;
using odec.Framework.Generic;
using odec.Framework.Generic.Utility;
using odec.Server.Model.Store.Helpers.Enums;

namespace odec.Server.Model.Store.Filters
{
    public class GoodsFilter<TArrayKey>:FilterBase
    {

        public GoodsFilter()
        {
            InitOptions = GoodInitOptions.Default;
            DepthInterval = new Interval<decimal?>();
            HeightInterval =new Interval<decimal?>();
            WidthInterval = new Interval<decimal?>();
        }

        public IList<TArrayKey> DesignerIds { get; set; }

        public IList<TArrayKey> ColorIds { get; set; }

        public IList<TArrayKey> SizeIds { get; set; }
        public IList<TArrayKey> CategoryIds { get; set; }
        public IList<TArrayKey> TypeIds { get; set; }
        public IList<TArrayKey> MaterialsIds { get; set; }

        public GoodInitOptions InitOptions { get; set; }

        public Interval<decimal?> HeightInterval { get; set; }
        public Interval<decimal?> WidthInterval { get; set; }
        public Interval<decimal?> DepthInterval { get; set; }

    }
}
