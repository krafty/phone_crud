// (c) Rishikesh Parkhe 2016
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.RishikeshParkhe.PersonalStorage.DataModel
{
    public class StorageCabinet
    {
        #region Public Properties

        public string ProgramId { get; set; }
        public List<StorageRecord> Records { get; set; }

        #endregion Public Properties
    }

    public class StorageRecord
    {
        #region Public Properties

        public string Data { get; set; }
        public Guid Id { get; set; }
        public string Key { get; set; }
        public DateTime Modified { get; set; }

        #endregion Public Properties
    }
}