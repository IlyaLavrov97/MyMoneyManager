using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyManager.Model.ChartInterfaces
{
    public interface ILineChartPictured
    {
        double GetMoneyAmount();
        string GetOperationDate();
    }
}
