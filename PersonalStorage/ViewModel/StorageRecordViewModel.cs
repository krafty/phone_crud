// (c) Rishikesh Parkhe 2016
using Org.RishikeshParkhe.PersonalStorage.DataModel;
using Org.RishikeshParkhe.PersonalStorage.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace Org.RishikeshParkhe.PersonalStorage.ViewModel
{
    public class StorageRecordViewModel : BindableBase
    {
        #region Public Fields

        public EventHandler OperationCompletedEvent = delegate { };

        #endregion Public Fields

        #region Private Fields

        private string _data;
        private Guid _id;
        private string _key;
        private DateTime _modifiedDate;

        #endregion Private Fields

        #region Public Constructors

        public StorageRecordViewModel()
        {
            Initialise();
        }

        public StorageRecordViewModel(StorageRecord data)
        {
            _id = data.Id;
            _key = data.Key;
            _data = data.Data;
            _modifiedDate = data.Modified;

            Initialise();
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

        public DelegateCommand DeleteCommand { get; private set; }

        public Guid Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged(() => Id);
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

        public DateTime ModifiedDate
        {
            get
            {
                return _modifiedDate;
            }

            set
            {
                _modifiedDate = value;
                OnPropertyChanged(() => ModifiedDate);
            }
        }

        public DelegateCommand StoreDataCommand { get; private set; }

        #endregion Public Properties

        #region Private Methods

        private bool CanStoreData()
        {
            return !(string.IsNullOrEmpty(Key) || string.IsNullOrEmpty(Data));
        }

        private async void HandleDeletion()
        {
            if (_id != Guid.Empty)
            {
                Repository repo = new Repository();
                await repo.Delete(_id);
                await repo.SaveChanges();

                OperationCompletedEvent(this, EventArgs.Empty);
            }
        }

        private async void HandleStoreData()
        {
            Repository repository = new Repository();
            await repository.AddRecord(new StorageRecord()
            {
                Id = Guid.NewGuid(),
                Data = Data,
                Key = Key,
                Modified = DateTime.Now
            });

            await repository.SaveChanges();
            OperationCompletedEvent(this, EventArgs.Empty);
        }

        private void Initialise()
        {
            StoreDataCommand = new DelegateCommand(HandleStoreData, CanStoreData);
            DeleteCommand = new DelegateCommand(HandleDeletion);
        }

        #endregion Private Methods
    }
}