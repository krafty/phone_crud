// (c) Rishikesh Parkhe 2016
using Org.RishikeshParkhe.PersonalStorage.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Org.RishikeshParkhe.PersonalStorage.ViewModel
{
    public class AllRecordsViewModel : BindableBase
    {
        #region Private Fields

        private ObservableCollection<StorageRecordViewModel> _records;
        private Repository _repository;

        #endregion Private Fields

        #region Public Constructors

        public AllRecordsViewModel(Repository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            _repository = repository;

            AddNewRecordCommand = new DelegateCommand(HandleAddNewRecord);
        }

        #endregion Public Constructors

        #region Public Properties

        public DelegateCommand AddNewRecordCommand { get; private set; }

        public ObservableCollection<StorageRecordViewModel> Records
        {
            get
            {
                return _records;
            }

            set
            {
                _records = value;
                OnPropertyChanged(() => Records);
            }
        }

        #endregion Public Properties

        #region Public Methods

        public async Task LoadRecords()
        {
            var records = await _repository.RetrieveAllAsync();
            Records = new ObservableCollection<StorageRecordViewModel>();
            foreach (var r in records)
            {
                Records.Add(new StorageRecordViewModel(r));
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void HandleAddNewRecord()
        {
        }

        #endregion Private Methods
    }
}