﻿using Dapper.Contrib.Extensions;

namespace VMsales8.Models
{
    [Table("product")]
    public partial class ProductModel : BaseModel
    {
        private int _product_pk;
        [ExplicitKey]
        public int product_pk
        {
            get => _product_pk;
            set
            {
                if (_product_pk != value)
                    _product_pk = value;
                NotifyOfPropertyChange(nameof(product_pk));
            }
        }
        private string _product_name;
        public string product_name
        {
            get => _product_name;
            set
            {
                if (_product_name != value)
                    _product_name = value;
                NotifyOfPropertyChange(nameof(product_name));
            }
        }
        private string _description;
        public string description
        {
            get => _description;
            set
            {
                if (_description != value)
                    _description = value;
                NotifyOfPropertyChange(nameof(description));
            }

        }
        private int _quantity;
        public int quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                    _quantity = value;
                NotifyOfPropertyChange(nameof(quantity));
            }
        }
        private decimal _cost;
        public decimal cost
        {
            get => _cost;
            set
            {
                if (_cost != value)
                    _cost = value;
                NotifyOfPropertyChange(nameof(cost));
            }

        }
        private string _sku;
        public string sku
        {
            get => _sku;
            set
            {
                if (_sku != value)
                    _sku = value;
                NotifyOfPropertyChange(nameof(sku));
            }
        }
        private decimal _listed_price;
        public decimal listed_price
        {
            get => _listed_price;
            set
            {
                if (_listed_price != value)
                    _listed_price = value;
                NotifyOfPropertyChange(nameof(listed_price));
            }

        }
        private int _instock;
        public int instock
        {
            get => _instock;
            set
            {
                if (_instock != value)
                    _instock = value;
                NotifyOfPropertyChange(nameof(instock));
            }

        }

        private string _condition;
        public string condition
        {
            get => _condition;
            set
            {
                if (_condition != value)
                    _condition = value;
                NotifyOfPropertyChange(nameof(condition));
            }
        }
        private string _listing_url;
        public string listing_url
        {
            get => _listing_url;
            set
            {
                if (_listing_url != value)
                    _listing_url = value;
                NotifyOfPropertyChange(nameof(listing_url));
            }

        }
        private string _listing_number;
        public string listing_number
        {
            get => _listing_number;
            set
            {
                if (_listing_number != value)
                    _listing_number = value;
                NotifyOfPropertyChange(nameof(listing_number));
            }


        }
        private DateTime _listing_date;
        public DateTime listing_date
        {
            get => _listing_date;
            set
            {
                if (_listing_date == value)
                    _listing_date = value;
                NotifyOfPropertyChange(nameof(listing_date));
            }

        }
        private string _brand_name;
        public string brand_name
        {
            get => _brand_name;
            set
            {
                if (_brand_name != value)
                    _brand_name = value;
                NotifyOfPropertyChange(nameof(brand_name));
            }
        }

        private bool _IsSelected;
        public bool IsSelected
        {
            get => _IsSelected;
            set
            {
                _IsSelected = value;
                NotifyOfPropertyChange(nameof(IsSelected));
            }
        }
        private int _product_category_pk;
        [ExplicitKey]
        public int product_category_pk
        {
            get => _product_category_pk;
            set
            {
                if (_product_category_pk != value)
                    _product_category_pk = value;
                NotifyOfPropertyChange(nameof(product_category_pk));
            }
        }

        private int _category_fk { get; set; }
        public int category_fk
        {
            get => _category_fk;
            set
            {
                if (_category_fk != value)
                    _category_fk = value;
                NotifyOfPropertyChange(nameof(category_fk));
            }
        }

        private string _category_name;
        public string category_name
        {
            get => _category_name;
            set
            {
                if (_category_name != value)
                    _category_name = value;
                NotifyOfPropertyChange(nameof(category_name));
            }
        }

        private int _purchase_order_detail_fk;
        public int purchase_order_detail_fk
        {
            get => _purchase_order_detail_fk;
            set
            {
                if (_purchase_order_detail_fk != value)
                    _purchase_order_detail_fk = value;
                NotifyOfPropertyChange(nameof(purchase_order_detail_fk));
            }
        }
    }
}