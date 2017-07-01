using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Карты
{
    class Card
    {
        private string buttonName = null;

        public string ButtonName
        {
            get { return buttonName; }
            set { buttonName = value; }
        }
        private int cardValue = 0;

        public int CardValue
        {
            get { return cardValue; }
            set { cardValue = value; }
        }

        private int cardStatus = 0;

        public int CardStatus
        {
            get { return cardStatus; }
            set { cardStatus = value; }
        }
        public Card(string buttonName, int cardValue, int cardStatus)
        {
            this.buttonName= buttonName;
            this.cardValue = cardValue;
            this.cardStatus = cardStatus;
        }
    }
}
