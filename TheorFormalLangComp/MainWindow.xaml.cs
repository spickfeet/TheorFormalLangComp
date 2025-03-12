﻿using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
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
using System.Windows.Shell;
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
                vm.PromptSave();
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

        private void Copy(object sender, RoutedEventArgs e)
        {
            textBox1.Copy();
        }

        private void Cut(object sender, RoutedEventArgs e)
        {
            textBox1.Cut();
        }

        private void Paste(object sender, RoutedEventArgs e)
        {
            textBox1.Paste();
        }
        private void Clear(object sender, RoutedEventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void Help(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            string currentDirectory = Directory.GetCurrentDirectory();
            string fullPath = System.IO.Path.Combine(currentDirectory, "resource", "Справка", "new_project.chm");
            p.StartInfo = new ProcessStartInfo(fullPath)
            {
                UseShellExecute = true
            };
            p.Start();
        }
    }
}