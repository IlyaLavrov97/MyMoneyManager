using MyMoneyManager.Infrastucture.DataTransferObjects;

namespace MyMoneyManager.Model
{
    public interface IMoneyElement : IBusinessObject
    {
        IDtoObject ConvertToDTO(byte currency);
    }
}
