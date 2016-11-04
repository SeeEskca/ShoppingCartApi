using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountteWebAPIServices.Models
{
    public class PaymentDetail : IEntity
    {
        public string cardType { get; set; }
        public string cardNo { get; set; }
        public string cardexpiry { get; set; }
        public string cardsecurity { get; set; }
        public string cardOwnerName { get; set; }

        public string userId { get; set; }
        public string paymentId { get; set; }

        public void setFields(DataRow dr)
        {
            this.cardType = dr["CardType"].ToString();

            this.cardNo = getCardTerminalDigits(dr["CardNumber"].ToString());
            this.cardexpiry = dr["CardExpiry"].ToString();
            this.cardsecurity = dr["CardSecurity"].ToString();
            this.userId = dr["UserId"].ToString();
            this.cardOwnerName = dr["CardOwnerName"].ToString();
            this.paymentId = dr["PaymentId"].ToString();
        }

        string getCardTerminalDigits(string cardNumber)
        {

            string tDigits = cardNumber.Substring(cardNumber.Length - 4);

            return tDigits;
        }

    }
}
