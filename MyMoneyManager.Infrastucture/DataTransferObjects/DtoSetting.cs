using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyManager.Infrastucture.DataTransferObjects
{
    [DataContract]
    public abstract class DtoSetting : DtoObject
    {
        [DataMember]
        public string JsonSetting { get; private set; }

        public DtoSetting(Guid id, string jsonSetting) : base(id)
        {
            JsonSetting = jsonSetting;
        }
    }
}
