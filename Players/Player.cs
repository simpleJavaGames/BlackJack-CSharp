using System;
using System.Collections.Generic;
using System.Text;
using DeckOfCards;

namespace Players
{
    class Player
    {
        private int balance;
        private String name;
        private List<Card> playerHand = new List<Card>();
        private bool isBusted { get; set; } = false;
        private int playerBetAmount = 0;

        public Player()
        {
            balance = 500;
            name = "Jared";
        }
        public Player(int balance, String name)
        {
            this.balance = balance;
            this.name = name;
        }

        public bool IsBusted()
        {
            return isBusted;
        }

        public int GetBalance()
        {
            return balance;
        }

        public void WinAddMoney()
        {
            balance += playerBetAmount;
        }

        public void WinAddMoneyBlackJack()
        {
            balance += (int)(playerBetAmount * 1.5);
        }

        public void LoseReduceMoney()
        {
            balance -= playerBetAmount;
        }

        public String GetName()
        {
            return name;
        }

        public void AddChosenCard(Card chosenCard)
        {
            playerHand.Add(chosenCard);
        }

        public String PlayerHandToString()
        {
            String returnedString = "";
            for (int i = 0; i < playerHand.Count; i++)
            {
                returnedString += playerHand[i].ToCardString();
                if (i != playerHand.Count - 1)
                {
                    returnedString += " & ";
                }
            }
            return returnedString;
        }

        public void Hit(Card chosenCard)
        {
            playerHand.Add(chosenCard);
        }

        public byte PlayerHandValue()
        {
            byte handValue = 0;
            byte numAces = 0;
            for (int i = 0; i < playerHand.Count; i++)
            {
                switch (playerHand[i].GetCardRank())
                {
                    case 1:
                        if (handValue + 11 > 21)
                        {
                            handValue += 1;
                        }
                        else
                        {
                            handValue += 11;
                            numAces++;
                        }
                        break;
                    case 11:
                    case 12:
                    case 13:
                        handValue += 10;
                        break;
                    default:
                        handValue += (byte)playerHand[i].GetCardRank();
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

        
        public bool HasBlackJack()
        {
            bool hasAce = false;
            bool hasFaceCard = false;

            for (int i = 0; i < 2; i++)
            {
                switch (playerHand[i].GetCardRank())
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
            playerHand.Clear();
            isBusted = false;
        }
        public int GetPlayerBetAmount()
        {
            return playerBetAmount;
        }
        public void SetPlayerBetAmount(int playerBetAmount)
        {
            this.playerBetAmount = playerBetAmount;
        }
    }
}
