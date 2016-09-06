using MyMoneyManager.Infrastucture.DataTransferObjects;

namespace MyMoneyManager.Model
{
    public interface ISetting : IBusinessObject
    {
        DtoSetting ConvertToDTO();
    }
}
