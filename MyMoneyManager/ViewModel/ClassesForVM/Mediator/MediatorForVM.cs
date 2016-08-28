using MyMoneyManager.Model;
using MyMoneyManager.Model.Expenses.ViewObject;

namespace MyMoneyManager.ViewModel.ClassesForVM.Mediator
{
    public class MediatorForVM : IMediator
    {
        private static MediatorForVM instance;

        public IConnectedViewModel ExpensesController;
        public IConnectedViewModel AddOrEditExpenses;
        public IConnectedViewModel LineChart;
        public IConnectedViewModel PieChart;

        private MediatorForVM() { }

        public static MediatorForVM Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MediatorForVM();
                }
                return instance;
            }
        }

        public void SendExpensesTo(IConnectedViewModel vm, IViewElement exp)
        {
            if (vm != null)
            {
                vm.NotifyAboutExpenses(exp);
            }
        }
    }
}
