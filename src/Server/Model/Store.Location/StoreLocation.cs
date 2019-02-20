
using System.ComponentModel;
using odec.CP.Server.Model.Location;

namespace odec.CP.Server.Model.Store.Location
{
    public class StoreLocation
    {
        public StoreLocation()
        {
            IsActive = true;
        }
        public int StoreId { get; set; }
        public odec.Server.Model.Store.Store Store { get; set; }
        public int LocationId { get; set; }

        public Address Location { get; set; }
        [DefaultValue(true)]
        public bool IsActive { get; set; }

    }
}
