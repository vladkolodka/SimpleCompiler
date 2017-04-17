using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace SimpleIDE
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ((TextBox) ((TabItem) FileTabs.Items[0]).Content).Text =
                @"yes no as equals loop logical symbol number value collection on off sentence blockPr blockListN blockListM text
& , * / ; : ( ) < > <= >= + - @ # | ?";
        }

        private void CompileButton_OnClick(object sender, RoutedEventArgs e)
        {
            var fileName = $"temp{Guid.NewGuid()}";
            var text = ((TextBox) FileTabs.SelectedContent).Text;

            try
            {
                if (string.IsNullOrWhiteSpace(text)) throw new Exception("Code is empty.");

                File.WriteAllText(fileName, text);

                var process = new Process
                {
                    StartInfo =
                    {
                        FileName = "compiler.exe",
                        CreateNoWindow = true,
                        Arguments = fileName,
                        RedirectStandardOutput = true,
                        UseShellExecute = false
                    }
                };
                process.Start();

                var result = process.StandardOutput.ReadToEnd();

                new ResultsWindow(result).ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Compilation error");
            }
            File.Delete(fileName);
        }
    }
}