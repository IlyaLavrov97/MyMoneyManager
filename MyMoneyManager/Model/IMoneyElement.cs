using MyMoneyManager.Infrastucture.DataTransferObjects;

namespace MyMoneyManager.Model
{
    public interface IMoneyElement : IBusinessObject
    {
        DtoObject ConvertToDTO(byte currency);
    }
}
