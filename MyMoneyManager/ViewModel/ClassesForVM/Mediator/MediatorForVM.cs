using MyMoneyManager.Model.Expenses.ViewObject;

namespace MyMoneyManager.ViewModel.ClassesForVM.Mediator
{
    public class MediatorForVM : IMediator
    {
        private static MediatorForVM instance;

        public IConnectedExpensesViewModel ExpensesController;
        public IConnectedExpensesViewModel AddOrEditExpenses;
        public IConnectedExpensesViewModel LineChart;
        public IConnectedExpensesViewModel PieChart;

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

        public void SendExpensesTo(IConnectedExpensesViewModel vm, ViewExpensesInfo exp)
        {
            if (vm != null)
            {
                vm.NotifyAboutExpenses(exp);
            }
        }
    }
}
