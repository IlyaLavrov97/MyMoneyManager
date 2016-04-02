﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MyMoneyManager.View;
using MyMoneyManager.ViewModel;

namespace MyMoneyManager
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var MMMView = new MMMView
            {
                DataContext = new MMMViewModel()
            };
            MMMView.Show();
        }
    }
}
