using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Objects
{
    public sealed class Card
    {
        public string CardID { get; set; }
        public string CardSuit { get; set; }
        public int CardValue { get; set; }
    }
    public sealed class CardDeck
    {
        private List<Card> Cards = new List<Card>();
        private char TryGetItem(int index, out bool result)
        {
            result = true;
            switch (index)
            {
                case 0: return 'h';
                case 1: return 'd';
                case 2: return 's';
                case 3: return 'c';
                default:
                    result = false;
                    throw new ArgumentException("Given index was not found");
            }
        }
        public List<Card> GenerateDeck()
        {
            var max = 52;
            var suitnumber = 13;
            var current_suit = 0;
            var current_card_number = 1;
            while (true)
            {
                if (current_card_number < 10)
                {
                    if (current_suit > 3)
                        break;
                    Cards.Add(new Card()
                    {
                        CardID = $"c0{current_card_number}{TryGetItem(current_suit, out bool result)}",
                        CardValue = current_card_number,
                        CardSuit = TryGetItem(current_suit, out bool suitresult).ToString()
                    });
                    max--;
                    suitnumber--;
                    current_card_number++;
                }
                else
                {
                    Cards.Add(new Card()
                    {
                        CardID = $"c{current_card_number}{TryGetItem(current_suit, out bool result)}",
                        CardValue = current_card_number,
                        CardSuit = TryGetItem(current_suit, out bool suitresult).ToString()
                    });
                    max--;
                    suitnumber--;
                    current_card_number++;
                }
                if(suitnumber == 0)
                {
                    current_suit += 1;
                    current_card_number = 1;
                    suitnumber = 13;
                }
                if (max == 0) break;
            }
            return Cards;
        }
    }
}
