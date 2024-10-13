﻿using System.Collections.ObjectModel;
using System.Windows;
using VMsales8.Logic;
using VMsales8.Models;

namespace VMsales8.ViewModels
{
    public class CustomerOrderViewModel : BaseModel
    {
        private ObservableCollection<CustomerModel> _ObservableCollectionCustomerModel { get; set; }
        public ObservableCollection<CustomerModel> ObservableCollectionCustomerModel
        {
            get { return _ObservableCollectionCustomerModel; }
            set
            {
                if (_ObservableCollectionCustomerModel == value) return;
                _ObservableCollectionCustomerModel = value;
                //RaisePropertyChanged("ObservableCollectionCustomerModel");
            }
        }

        private ObservableCollection<ProductModel> _ObservableCollectionProductModel { get; set; }
        public ObservableCollection<ProductModel> ObservableCollectionProductModel
        {
            get { return _ObservableCollectionProductModel; }
            set
            {
                if (_ObservableCollectionProductModel == value) return;
                _ObservableCollectionProductModel = value;
                //RaisePropertyChanged("ObservableCollectionProductModel");
            }
        }



        public void initial_load()
        {
            // load customers
            IDatabaseProvider dataBaseProvider;
            try
            {
                ObservableCollectionCustomerModel = new ObservableCollection<CustomerModel>();
                dataBaseProvider = getprovider();
                CustomerRepository CustomerRepo = new CustomerRepository(dataBaseProvider);
                ObservableCollectionCustomerModel = CustomerRepo.GetAll().Result.ToObservable();
                CustomerRepo.Commit();
                CustomerRepo.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show("An error has occured loading customers." + e);
            }

            // load products



        }
        public CustomerOrderViewModel()
        {
            initial_load();
        }
    }
}