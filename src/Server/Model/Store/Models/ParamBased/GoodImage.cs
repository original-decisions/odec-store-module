
namespace odec.Server.Model.Store.ParamBased
{
    /// <summary>
    /// серверный объект - изображение товара
    /// </summary>
    public class GoodImage
    {
        public int ImageId { get; set; }
        public Attachment.Attachment Image { get; set; }
        public bool IsMain { get; set; }
        public int GoodId { get; set; }
        public Good Good { get; set; }
    }
}
