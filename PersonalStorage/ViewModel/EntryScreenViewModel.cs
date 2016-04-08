// (c) Rishikesh Parkhe 2016
using Org.RishikeshParkhe.PersonalStorage.DataModel;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace Org.RishikeshParkhe.PersonalStorage.ViewModel
{
    public class EntryScreenViewModel : BindableBase
    {
        #region Private Fields

        private string _data;
        private string _key;

        #endregion Private Fields

        #region Public Constructors

        public EntryScreenViewModel()
        {
            StoreDataCommand = new DelegateCommand(HandleStoreData, CanStoreData);
        }

        public EntryScreenViewModel(StorageRecord data)
        {
            _key = data.Key;
            _data = data.Data;
        }

        #endregion Public Constructors

        #region Public Properties

        public string Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                OnPropertyChanged(() => Data);
                StoreDataCommand.RaiseCanExecuteChanged();
            }
        }

        public string Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
                OnPropertyChanged(() => Data);
                StoreDataCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand StoreDataCommand { get; private set; }

        #endregion Public Properties

        #region Private Methods

        private bool CanStoreData()
        {
            return !(string.IsNullOrEmpty(Key) || string.IsNullOrEmpty(Data));
        }

        private void HandleStoreData()
        {
            throw new NotImplementedException();
        }

        #endregion Private Methods
    }
}