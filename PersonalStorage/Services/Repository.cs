// (c) Rishikesh Parkhe 2016
using Newtonsoft.Json;
using Org.RishikeshParkhe.PersonalStorage.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Org.RishikeshParkhe.PersonalStorage.Services
{
    public class Repository
    {
        #region Private Fields

        private const string _persistenceUri = "ms-appx:///DataModel/Cabinet.json";
        private StorageCabinet _cabinet;

        #endregion Private Fields

        #region Public Methods

        public async void AddRecord(StorageRecord record)
        {
            if (_cabinet == null)
            {
                await RetrieveAllAsync();
            }

            _cabinet.Records.Add(record);
        }

        public async Task<string> LoadRawData(Guid id)
        {
            var records = await RetrieveAllAsync();
            var data = from r in records
                       where r.Id == id
                       select r.Data;

            return data.FirstOrDefault();
        }

        public async Task<List<StorageRecord>> RetrieveAllAsync()
        {
            Uri dataUri = new Uri(_persistenceUri);
            List<StorageRecord> records = null;

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            string jsonText = await FileIO.ReadTextAsync(file);
            _cabinet = JsonConvert.DeserializeObject<StorageCabinet>(jsonText);
            if (_cabinet != null)
            {
                records = _cabinet.Records;
            }

            return records;
        }

        public async Task SaveChanges()
        {
            Uri dataUri = new Uri(_persistenceUri);
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            var jsonText = JsonConvert.SerializeObject(_cabinet);
            await FileIO.WriteTextAsync(file, jsonText);
        }

        #endregion Public Methods
    }
}