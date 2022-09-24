using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CurrencyToWordsClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void convert_Click(object sender, RoutedEventArgs e)
        {

            double amountInDigits = Convert.ToDouble(moneyInNumbers.Text.Replace(",", "."));
            if (moneyInNumbers.Text.Contains("."))
            {
                MessageBox.Show("Please change the sperator to a comma.", null, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                CurrencyToWordsConverterClient client = new CurrencyToWordsConverterClient();
                var amountInwords = client.CurrencyToWords(amountInDigits);
                if (amountInwords.errorCode != "")
                {
                    MessageBox.Show(amountInwords.errorCode, null, MessageBoxButton.OK, MessageBoxImage.Error);
                    moneyInWords.Text = amountInwords.resultInWords;
                }
                else
                {
                    moneyInWords.Text = amountInwords.resultInWords;
                }
                client.Close();
            }

        }
    }
}
