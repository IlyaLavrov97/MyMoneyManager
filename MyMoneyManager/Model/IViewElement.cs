using MyMoneyManager.Model.ChartInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyManager.Model
{
    public interface IViewElement : ILineChartPictured
    {
        IBusinessObject ConvertToBO();
        Guid GetId();
        IViewElement Clone();
    }
}
