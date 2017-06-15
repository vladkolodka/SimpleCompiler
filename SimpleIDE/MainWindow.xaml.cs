using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using SimpleIDE.Core;

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
            ((TextBox) ((TabItem) FileTabs.Items[1]).Content).Text = File.ReadAllText("code.new.htcl");
        }


        private void CompileButton_OnClick(object sender, RoutedEventArgs e)
        {
            var fileName = $"temp{Guid.NewGuid()}";
            var text = ((TextBox) FileTabs.SelectedContent).Text;

            try
            {
                if (string.IsNullOrWhiteSpace(text)) throw new Exception("Code is empty.");

                File.WriteAllText(fileName, text);

                new ResultsWindow(new Compiler(new[] {fileName})).ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Compilation error");
            }
            File.Delete(fileName);
        }
    }
}