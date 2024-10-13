using Dapper.Contrib.Extensions;

namespace VMsales8.Models
{
    [Table("category")]
    public class CategoryModel : BaseModel
    {
        private int _category_pk;
        [ExplicitKey]
        public int category_pk
        {
            get => _category_pk;
            set
            {
                if (_category_pk != value)
                {
                    _category_pk = value;
                    NotifyOfPropertyChange(nameof(category_pk));
                }
            }
        }

        private string? _category_name;
        public string category_name

        {
            get => _category_name!;
            set
            {
                if (_category_name != value)
                {
                    _category_name = value;
                    NotifyOfPropertyChange(() => category_name);
                }
            }
        }
        private string? _description;
        public string description
        {
            get => _description!;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    NotifyOfPropertyChange(() => description);
                }
            }
        }

        private DateTime _creation_date;
        public DateTime creation_date
        {
            get => _creation_date;
            set
            {
                if (_creation_date != value)
                {
                    _creation_date = value;
                    NotifyOfPropertyChange(() => creation_date);
                }
            }
        }
    }
}