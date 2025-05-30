﻿using CWCompil.State;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Input;
using TheorFormalLangComp.Files;
using TheorFormalLangComp.RecursiveDescent;
using TheorFormalLangComp.State;
using TheorFormalLangComp.Tokens;

namespace TheorFormalLangComp.ViewModel
{
    class MainWindowVM : BasicVM
    {
        private FileWorker _fileWorker;
        private string _textInput;
        private string _debugText;

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

        public string DebugText
        {
            get { return _debugText; }
            set
            {
                Set(ref _debugText, value);
            }
        }

        public Action<string> PathChanged;
        public MainWindowVM()
        {
            _fileSaved = true;
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

        public void PromptSave() // Изменить название
        {
            while (_fileSaved == false)
            {
                MessageBoxResult result = MessageBox.Show(
                "У вас есть несохраненные изменения. Хотите сохранить файл перед выходом?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Save();
                }
                else if (result == MessageBoxResult.No)
                {
                    return;
                }
            }
        }

        private void Open()
        {
            PromptSave();
            if (TryOpen() == false) return;
            TextInput = _fileWorker.GetData();
            _fileSaved = true;

        }

        private void Create()
        {
            PromptSave();
            TextInput = "";
            _fileWorker.OnPathChanged(null);
            _fileSaved = true;

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

        public ICommand CreateCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Create();
                });
            }
        }

        public ICommand OpenCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Open();
                });
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Save();
                });
            }
        }

        public ICommand SaveAsCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    SaveAs();
                });
            }
        }
        public ICommand Start
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    //List<TokenData<TokenTypesMath>> tokens = MathTokenBuilder.CreateTokens(TextInput);
                    //RecursiveDescent.RecursiveDescent stateMachine = new(tokens);
                    //while (stateMachine.Index < stateMachine.Tokens.Count)
                    //{
                    //    stateMachine.Start();
                    //    stateMachine.StateHist.Add("\n");
                    //}
                    //DebugText = "Мини-ошибки, не нейтрализация:\n";
                    //if (stateMachine.Errors.Count == 0)
                    //{
                    //    DebugText += "Ошибок нет\n\n";
                    //}
                    //foreach (var item in stateMachine.Errors)
                    //{
                    //    DebugText += item;
                    //}

                    //DebugText += "Состояния\n";
                    //foreach (var item in stateMachine.StateHist)
                    //{
                    //    DebugText += item + "__";
                    //}


                    List<TokenData<TokenEnum>> tokens = TokenConverter.CreateTokens(TextInput);
                    StateMachine stateMachine = new(tokens);
                    stateMachine.Start();
                    DebugText = "";
                    if (stateMachine.ErrorsData.Count == 0)
                    {
                        DebugText = "Ошибок нет";
                    }
                    else
                    {
                        foreach (ErrorData errorData in stateMachine.ErrorsData)
                        {
                            DebugText += $"Строка: {errorData.Line} Глобальный индекс: {errorData.Index} Ошибка {errorData.Text} \n";
                        }
                        DebugText += "\nВалидная строка:\n";
                        DebugText += stateMachine.ValidLine;
                    }




                    //MailFinder mailFinder = new MailFinder();
                    //string formattedText = TextInput.Replace("\r", "");
                    //List<MailPosition> mailPositions = mailFinder.Find(formattedText);
                    //DebugText = "";
                    //foreach (MailPosition mailPosition in mailPositions)
                    //{
                    //    DebugText += $"Найдена почта. <<{mailPosition.mail}>>\nСтрока: {mailPosition.line}\nИндекс начала: {mailPosition.startIndex}\nИндекс конца: {mailPosition.endIndex}\n\n\n";
                    //}
                    //DebugText += "История переходов:\n" ;
                    //foreach (IMailFinderState mailPosition in mailFinder.MailFinderStates)
                    //{
                    //    DebugText += mailPosition.GetNameState + "---";
                    //}
                });
            }
        }
    }
}
