using System;
using System.Collections.Generic;
using DeckOfCards;

namespace Players
{
    class Dealer
    {
        private List<Card> DealerHand = new List<Card>();
        private bool isBusted = false;

        public void AddChosenCard(Card chosenCard)
        {
            DealerHand.Add(chosenCard);

        }

        public String DealerHandToString()
        {
            String returnedString = "";
            for (int i = 0; i < DealerHand.Count; i++)
            {
                returnedString += DealerHand[i].ToCardString();
                if (i != DealerHand.Count- 1)
                {
                    returnedString += " & ";
                }
            }

            return returnedString;
        }

        public String DealerHandToStringInital()
        {
            return "The Dealer hand is " + DealerHand[0].ToCardString() + " & ???";
        }

        public byte DealerHandValue()
        {
            byte handValue = 0;
            byte numAces = 0;
            for (int i = 0; i < DealerHand.Count; i++)
            {
                switch (DealerHand[i].GetCardRank())
                {
                    case 1:
                        numAces++;
                        if (handValue + 11 > 21)
                        {
                            handValue += 1;
                        }
                        else
                        {
                            handValue += 11;
                        }

                        break;
                    case 11:
                    case 12:
                    case 13:
                        handValue += 10;
                        break;
                    default:
                        handValue += (byte)DealerHand[i].GetCardRank();
                        break;
                }

                if (handValue > 21)
                {
                    if (numAces > 0)
                    {
                        numAces--;
                        handValue -= 10;
                    }
                    else
                    {
                        isBusted = true;
                    }
                }
            }

            return handValue;
        }

        public void Hit(Card chosenCard)
        {
            DealerHand.Add(chosenCard);
        }

        public bool IsBusted()
        {
            return isBusted;
        }

        public bool HasBlackJack()
        {
            bool hasAce = false;
            bool hasFaceCard = false;

            for (int i = 0; i < 2; i++)
            {
                switch (DealerHand[i].GetCardRank())
                {
                    case 1:
                        hasAce = true;
                        break;
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                        hasFaceCard = true;
                        break;
                }
            }

            if (hasAce == true & hasFaceCard == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ClearHand()
        {
            DealerHand.Clear();
            isBusted = false;
        }
    }
}
