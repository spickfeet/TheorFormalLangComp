﻿using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheorFormalLangComp.ViewModel;

namespace TheorFormalLangComp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowVM();
        }


        void WindowClosing(object sender, CancelEventArgs e)
        {
            if (DataContext is MainWindowVM vm)
            {
                vm.TrySave();
            }
        }

        private void Undo(object sender, RoutedEventArgs e)
        {
            textBox1.Undo();
        }

        private void Redo(object sender, RoutedEventArgs e)
        {
            textBox1.Redo();
        }
    }
}