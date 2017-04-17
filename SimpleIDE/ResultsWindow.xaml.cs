using System.Text;
using System.Windows;
using SimpleIDE.Core;

namespace SimpleIDE
{
    /// <summary>
    ///     Interaction logic for ResultsWindow.xaml
    /// </summary>
    public partial class ResultsWindow
    {
        private readonly Compiler _compiler;

        public ResultsWindow(Compiler compiler)
        {
            _compiler = compiler;
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            _compiler.Compile();
            var result = new StringBuilder();

            foreach (var keyValuePair in _compiler.Tokens)
                result.Append(string.Join(", ", keyValuePair.Value)).Append("\n");

            ErrorsList.ItemsSource = _compiler.Errors;

            TokensText.Text = result.ToString();
        }
    }
}