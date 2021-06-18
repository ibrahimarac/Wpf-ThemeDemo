﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Ibrahim.Scheduler.ViewModels;
using Ibrahim.Wpf.CustomControls.Controls;

namespace Ibrahim.UI.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : CustomBaseForm
    {
        public Main()
        {
            InitializeComponent();
            DataContext = new MainVM();
        }
    }
}
