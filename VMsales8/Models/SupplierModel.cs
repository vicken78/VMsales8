using Dapper.Contrib.Extensions;

namespace VMsales8.Models
{
    [Table("supplier")]
    public class SupplierModel : BaseModel
    {
        private int _supplier_pk { get; set; }
        [ExplicitKey]
        public int supplier_pk
        {
            get => _supplier_pk;
            set
            {
                if (_supplier_pk != value)
                    _supplier_pk = value;
                NotifyOfPropertyChange(() => supplier_pk);
            }
        }

        private string _supplier_name { get; set; }
        public string supplier_name
        {
            get => _supplier_name;
            set
            {
                if (_supplier_name != value)
                    _supplier_name = value;
                NotifyOfPropertyChange(() => supplier_name);
            }
        }

        private string _address { get; set; }
        public string address
        {
            get => _address;
            set
            {
                if (_address != value)
                    _address = value;
                NotifyOfPropertyChange(() => address);
            }
        }

        private string _city { get; set; }
        public string city
        {
            get => _city;
            set
            {
                if (_city != value)
                    _city = value;
                NotifyOfPropertyChange(() => city);
            }
        }

        private string _state { get; set; }
        public string state
        {
            get => _state;
            set
            {
                if (_state != value)
                    _state = value;
                NotifyOfPropertyChange(() => state);
            }
        }

        private string _country { get; set; }
        public string country
        {
            get => _country;
            set
            {
                if (_country != value)
                    _country = value;
                NotifyOfPropertyChange(() => country);
            }
        }

        private string _zip { get; set; }
        public string zip
        {
            get => _zip;
            set
            {
                if (_zip != value)
                    _zip = value;
                NotifyOfPropertyChange(() => zip);
            }
        }


        private string _phone { get; set; }
        public string phone
        {
            get => _phone;
            set
            {
                if (_phone != value)
                    _phone = value;
                NotifyOfPropertyChange(() => phone);
            }
        }

        private string _email { get; set; }
        public string email
        {
            get => _email;
            set
            {
                if (_email != value)
                    _email = value;
                NotifyOfPropertyChange(() => email);
            }
        }
    }
}