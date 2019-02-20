using odec.Server.Model.Store.Abst.Models.Generic.Links.UnifiedKey;

namespace odec.Server.Model.Store.Blob
{
    /// <summary>
    /// Link entity between goods (Many-to-Many Rel)
    /// </summary>
    public class RelativeGood:RelativeGoodGeneric<int, Good>
    {
    }
}
