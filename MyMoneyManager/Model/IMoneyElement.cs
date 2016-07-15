

using System;

namespace MyMoneyManager.Model
{
    public interface IMoneyElement
    {
        void ConvertToVO(out IViewElement newVO);
        Guid GetId();
    }
}
