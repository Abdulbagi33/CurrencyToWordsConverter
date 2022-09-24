using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CurrencyToWordsServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CurencyToWordsConverter" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CurencyToWordsConverter.svc or CurencyToWordsConverter.svc.cs at the Solution Explorer and start debugging.
    public class CurrencyToWordsConverter : ICurrencyToWordsConverter
    {
        readonly double MAX_AMOUNT_DOLLARS = 999999999.99;
        readonly double MAX_AMOUNT_CENTS = 99;
        public CurrencyToWordsResult CurrencyToWords(double amoutInDigits)
        {
            CurrencyToWordsResult result = new CurrencyToWordsResult();

            if (amoutInDigits > MAX_AMOUNT_DOLLARS)
            {
                result.resultInWords = "";
                result.errorCode = "The maximum amount of money is 999 999 999.99.";
                return result;
            }
            int dollarValue = 0;
            int centValue = 0;
            string doller = string.Empty;
            string cents = string.Empty;
            if (amoutInDigits.ToString().Contains("."))
            {
                dollarValue = int.Parse(amoutInDigits.ToString().Split('.')[0]);
                centValue = int.Parse(amoutInDigits.ToString().Split('.')[1]);

                if (centValue > MAX_AMOUNT_CENTS)
                {
                    result.resultInWords = "";
                    result.errorCode = "The maximum amount of cents is 99.";
                    return result;
                }
                else
                {
                    doller = NumberToWords(dollarValue);
                    cents = NumberToWords(centValue);
                }
            }
            else
            {
                doller = NumberToWords(Convert.ToInt32(amoutInDigits));
            }

            result.resultInWords = !string.IsNullOrEmpty(cents) ? (centValue == 1 ?
                    string.Format("{0} Doller and {1} Cent", doller, cents) :
                    string.Format("{0} Doller and {1} Cents", doller, cents)) :
                    string.Format("{0} Doller", doller);
            result.errorCode = "";

            return result;
        }

        public string NumberToWords(int number)
        {
            if (number == 0)
                return "Zero";

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine",
                    "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[(number) / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[(number) % 10];
                }
            }
            return words;
        }
    }
}
