using System;
using System.Windows.Input;
using Microsoft.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyManager.ViewModel
{
    public class ExpensesControllerViewModel : ViewModelBase
    {

        public ExpensesControllerViewModel()
        {
            OnFocusCommand = new Command(arg => RemoveText());
            LostFocusCommand = new Command(arg => AddText());
        }

        private bool _isFocused = false;
     public bool IsTextBoxFocused
     {
         get
         {
             return _isFocused;
         }
         set
         {
                _isFocused = value;
                if (_isFocused)
            {
                    RemoveText();
                    OnPropertyChanged("IsTextBoxFocused");
           }
                else
                {
                    AddText();
                    OnPropertyChanged("IsTextBoxFocused");
                }
                
        }
    } 

        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");
            }
        }

        private string _searchText;

        public void RemoveText()
        {
            SearchText = "";
        }

        public void AddText()
        {
            if (string.IsNullOrEmpty(SearchText))
                SearchText = "Enter text here...";
        }

        #region Commands

        /// <summary>
        /// Get or set OnFocusCommand.
        /// </summary>
        public ICommand OnFocusCommand { get; set; }

        /// <summary>
        /// Get or set LostFocusCommand.
        /// </summary>
        public ICommand LostFocusCommand { get; set; }

        #endregion
    }
}
