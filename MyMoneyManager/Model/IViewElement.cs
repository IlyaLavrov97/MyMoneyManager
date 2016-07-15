using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyManager.Model
{
    public interface IViewElement
    {
        void ConvertToBO(out IMoneyElement newBO);
        Guid GetId();
    }
}
