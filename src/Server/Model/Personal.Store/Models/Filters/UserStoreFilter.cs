using odec.Framework.Generic;
using odec.Server.Model.Store.Filters;

namespace odec.Server.Model.Personal.Store.Filters
{
    public class UserStoreFilter<TUserKey>:FilterBase
    {
        public UserStoreFilter()
        {
            IsOnlyActive = true;
        }
        public TUserKey UserId { get; set; }
        public bool IsOnlyActive { get; set; }

    }
}
