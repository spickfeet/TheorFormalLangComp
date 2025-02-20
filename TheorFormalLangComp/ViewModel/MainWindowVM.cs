using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TheorFormalLangComp.Files;

namespace TheorFormalLangComp.ViewModel
{
    class MainWindowVM : BasicVM
    {
        private FileWorker _fileWorker;
        private string _textInput;

        private bool _fileSaved;
        public string TextInput
        {
            get { return _textInput; }
            set 
            {
                Set(ref _textInput, value);
                _fileSaved = false;
            }
        }

        public Action<string> PathChanged;
        public MainWindowVM()
        {
            _fileWorker = new();
            PathChanged += _fileWorker.OnPathChanged;
        }
        private bool TryChoosePath()
        {
            SaveFileDialog fileDialog = new SaveFileDialog()
            {
                AddExtension = true,
                Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
            };

            if (fileDialog.ShowDialog() == true)
            {
                string pathName = fileDialog.FileName;
                PathChanged?.Invoke(pathName);

                if (!File.Exists(pathName))
                    File.Create(pathName).Close();

                return true;
            }
            else { return false; }
        }

        private bool TryOpen()
        {
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                AddExtension = true,
                Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
            };

            if (fileDialog.ShowDialog() == true)
            {
                string pathName = fileDialog.FileName;
                PathChanged?.Invoke(pathName);

                return true;
            }
            else { return false; }
        }

        private void Open(object sender, EventArgs e)
        {
            if (TryOpen() == false) return;

            TextInput = _fileWorker.GetData();
            _fileSaved = true;
        }

        private void Create()
        {
            if(_fileSaved == false)
            {
                
            }
            TextInput = "";
        }

        private void Save()
        {
            if (string.IsNullOrEmpty(_fileWorker.PathName))
                if (TryChoosePath() == false) return;
            _fileWorker.Save(TextInput);
            _fileSaved = true;
        }

        private void SaveAs()
        {
            if (TryChoosePath() == false) return;
            _fileWorker.Save(TextInput);
            _fileSaved = true;
        }
    }
}
