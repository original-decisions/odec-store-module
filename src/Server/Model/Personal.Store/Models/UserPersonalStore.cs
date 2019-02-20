using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace odec.Server.Model.Personal.Store
{
    public class UserPersonalStore
    {
     //   [Key,Column(Order = 1)]
        public int StoreId { get; set; }
        
        public Model.Store.Store Store { get; set; }
       // [Key, Column(Order = 0)]
        public int UserId { get; set; }
        public User.User User { get; set; }
        public bool IsActive { get; set; }
    }
}
