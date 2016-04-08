// (c) Rishikesh Parkhe 2016
using Newtonsoft.Json;
using Org.RishikeshParkhe.PersonalStorage.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task AddRecord(StorageRecord record)
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
            List<StorageRecord> records = null;
            Uri dataUri = new Uri(_persistenceUri);

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            using (var stream = await file.OpenStreamForReadAsync())
            {
                using (var reader = new JsonTextReader(new StreamReader(stream)))
                {
                    var js = JsonSerializer.Create();
                    _cabinet = js.Deserialize<StorageCabinet>(reader);
                }
            }

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
            using (var stream = await file.OpenStreamForWriteAsync())
            {
                using (var writer = new JsonTextWriter(new StreamWriter(stream)))
                {
                    var js = JsonSerializer.Create();
                    js.Serialize(writer, _cabinet);
                }
            }
        }

        #endregion Public Methods

        #region Internal Methods

        internal async Task Delete(Guid id)
        {
            if (_cabinet == null)
            {
                await RetrieveAllAsync();
            }

            var rec = (from r in _cabinet.Records
                       where r.Id == id
                       select r).FirstOrDefault();

            if (rec != null)
            {
                _cabinet.Records.Remove(rec);
            }
        }

        #endregion Internal Methods
    }
}