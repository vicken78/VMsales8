using Dapper.Contrib.Extensions;

namespace VMsales8.Models
{
    [Table("product_photo")]
    public class ProductPhotoModel : BaseModel
    {
        [ExplicitKey]
        public int product_photo_pk { get; set; }
        public int product_fk { get; set; }
        public int photo_fk { get; set; }
    }
}
