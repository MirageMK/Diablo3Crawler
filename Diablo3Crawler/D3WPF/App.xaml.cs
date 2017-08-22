using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;

namespace D3WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            log4net.Config.XmlConfigurator.Configure();
            this.InitializeComponent();
        }
    }
}
