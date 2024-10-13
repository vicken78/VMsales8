using Dapper.Contrib.Extensions;

namespace VMsales8.Models
{
    [Table("photo")]
    public partial class PhotoModel : BaseModel
    {
        [ExplicitKey]
        public int photo_pk { get; set; }
        public int photo_order_number { get; set; }
        public string? photo_path { get; set; }

    }
}
