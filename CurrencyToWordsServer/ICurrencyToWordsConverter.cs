using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CurrencyToWordsServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICurrencyToWordsConverter" in both code and config file together.
    [ServiceContract]
    public interface ICurrencyToWordsConverter
    {
        [OperationContract]
        CurrencyToWordsResult CurrencyToWords(double amountInDigits);
        //   string NumberToWords(int number);
    }
}
