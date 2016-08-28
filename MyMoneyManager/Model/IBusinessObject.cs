using MyMoneyManager.Infrastucture.DataTransferObjects;
using System;

namespace MyMoneyManager.Model
{
    public interface IBusinessObject
    {
        IViewElement ConvertToVO();
        Guid GetId();
    }
}
