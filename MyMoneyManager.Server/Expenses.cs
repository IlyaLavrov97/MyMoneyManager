//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyMoneyManager.Server
{
    using System;
    using System.Collections.Generic;
    
    public partial class Expenses
    {
        public System.Guid ExpensesId { get; set; }
        public double Expenditure { get; set; }
        public byte CurrencyType { get; set; }
        public string Comment { get; set; }
        public System.DateTime СostsDate { get; set; }
        public byte ExpensesType { get; set; }
        public System.Guid UserId { get; set; }
    
        public virtual ManagerUser ManagerUser { get; set; }
    }
}
