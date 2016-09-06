using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyManager.Infrastucture.DataTransferObjects
{
    [DataContract]
    [KnownType(typeof(DtoItem))]
    [KnownType(typeof(DtoSetting))]
    public abstract class DtoObject
    {
        [DataMember]
        public Guid Id { get; private set; }

        public DtoObject(Guid id)
        {
            Id = id;
        }

        public abstract bool IsEqual(DtoObject dtoObject);
    }
}
