using System;
using System.Threading;
using DeckOfCards;
using Players;

namespace Blackjack_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool playing = true;
            Deck Deck1 = new Deck();
            Player Player1 = new Player();
            Dealer Dealer = new Dealer();
            Console.WriteLine("Let's play blackjack!");

            do
            {
                int playerBetAmount = 0;
                //Player bet amount
                Console.WriteLine(Player1.GetName() + "'s balance is " + Player1.GetBalance());
                Console.Write("How much would you like to bet? (Max bet 100): ");
                playerBetAmount = int.Parse(Console.ReadLine());
                if (playerBetAmount > 100 || playerBetAmount <= 0 || playerBetAmount > Player1.GetBalance())
                {
                    Console.WriteLine("Please enter a valid bet.");
                    playerBetAmount = int.Parse(Console.ReadLine());
                }
                else
                {
                    Player1.SetPlayerBetAmount(playerBetAmount);
                }

                Console.WriteLine();

                bool allPlayersStand = false;
                bool allPlayersBusted = false;

                // Deal the Dealer's hand
                Dealer.AddChosenCard(Deck1.DealOneCard());
                Deck1.RemoveTopCard();
                Dealer.AddChosenCard(Deck1.DealOneCard());
                Deck1.RemoveTopCard();
                Console.WriteLine(Dealer.DealerHandToStringInital());

                Console.WriteLine(); // indent a line

                // Deal the player's hand, check for blackjack and then show value.
                Player1.AddChosenCard(Deck1.DealOneCard());
                Deck1.RemoveTopCard();
                Player1.AddChosenCard(Deck1.DealOneCard());
                Deck1.RemoveTopCard();
                Console.WriteLine(Player1.GetName() + "'s hand is: " + Player1.PlayerHandToString());
                if (Player1.HasBlackJack())
                {
                    Console.WriteLine(Player1.GetName() + " has a Blackjack!\n");
                    allPlayersStand = true;
                }
                else
                {
                    Console.Write(Player1.GetName() + "'s hand is worth: (" + Player1.PlayerHandValue() + ")\n");
                    Console.WriteLine();
                }

                byte playerChoice = 0;

                //Player decisions

                while (allPlayersStand == false && allPlayersBusted == false)
                {
                    Console.Write(Player1.GetName() + " would you like to 1) Hit 2) Stand? ");
                    playerChoice = byte.Parse(Console.ReadLine());
                    switch (playerChoice)
                    {
                        case 1:
                            Player1.Hit(Deck1.DealOneCard());
                            Deck1.RemoveTopCard();
                            Console.WriteLine("\n" + Player1.GetName() + "'s hand is: " + Player1.PlayerHandToString());
                            Console.Write(Player1.GetName() + "'s hand is worth: (" + Player1.PlayerHandValue() +
                                          ")\n");
                            if (Player1.IsBusted() == true)
                            {
                                Console.WriteLine(Player1.GetName() + " has busted!\n");
                                allPlayersBusted = true;
                            }

                            break;
                        case 2:
                            Console.WriteLine();
                            allPlayersStand = true;
                            break;
                        default:
                            while (playerChoice != 1 && playerChoice != 2)
                            {
                                Console.WriteLine("Please enter a valid choice.");
                                playerChoice = byte.Parse(Console.ReadLine());
                            }

                            break;
                    }
                }

                //Check if Player/Dealer has blackjack
                Console.WriteLine("The Dealer's hand is: " + Dealer.DealerHandToString());
                if (Player1.HasBlackJack() == true && Dealer.HasBlackJack() == true)
                {
                    wait2secs();
                    Console.WriteLine("Push! All bets are refunded.");
                    Console.WriteLine(Player1.GetName() + "'s balance is now " + Player1.GetBalance());
                }
                else if (Dealer.HasBlackJack() == true)
                {
                    wait2secs();
                    dealerWins(Player1);
                }
                else if (Player1.HasBlackJack() == true)
                {
                    wait2secs();
                    playerWinsBlackJack(Player1);
                }


                //Dealer decisions
                if (Player1.HasBlackJack() == false && Dealer.HasBlackJack() == false)
                {
                    Console.Write("Dealer's hand is worth: (" + Dealer.DealerHandValue() + ")\n");
                    wait2secs();
                    while (Dealer.DealerHandValue() < 16)
                    {
                        Dealer.Hit(Deck1.DealOneCard());
                        Deck1.RemoveTopCard();
                        Console.WriteLine("\nDealer's hand is: " + Dealer.DealerHandToString());
                        Console.Write("Dealer's hand is worth: (" + Dealer.DealerHandValue() + ")\n");
                        if (Dealer.IsBusted() == true)
                        {
                            Console.WriteLine("Dealer has busted!");
                        }

                        wait2secs();
                    }

                    while (Dealer.DealerHandValue() < Player1.PlayerHandValue() && Player1.IsBusted() == false)
                    {
                        Dealer.Hit(Deck1.DealOneCard());
                        Deck1.RemoveTopCard();
                        Console.WriteLine("\nDealer's hand is: " + Dealer.DealerHandToString());
                        Console.Write("Dealer's hand is worth: (" + Dealer.DealerHandValue() + ")\n");
                    }

                    if (Dealer.IsBusted() == true && Player1.IsBusted() == false)
                    {
                        playerWins(Player1);
                    }
                    else if (Dealer.IsBusted() == false && Player1.IsBusted() == true)
                    {
                        dealerWins(Player1);
                    }
                    else if (Dealer.IsBusted() == true && Player1.IsBusted() == true)
                    {
                        Console.WriteLine("Push! All bets are refunded.");
                        Console.WriteLine(Player1.GetName() + "'s balance is now " + Player1.GetBalance());
                    }
                    else if (Dealer.DealerHandValue() > Player1.PlayerHandValue())
                    {
                        dealerWins(Player1);
                    }
                    else if (Player1.PlayerHandValue() == Dealer.DealerHandValue())
                    {
                        Console.WriteLine("Push! All bets are refunded.");
                        Console.WriteLine(Player1.GetName() + "'s balance is now " + Player1.GetBalance());
                    }
                    else
                    {
                        playerWins(Player1);
                    }
                }

                //Reset the game
                resetGame(Dealer, Player1, Deck1);

                //Check if player still has money to play.
                if (Player1.GetBalance() <= 0)
                {
                    playing = false;
                    Console.WriteLine(Player1.GetName() + " is out of money.");
                }

                Console.WriteLine("Would you like to quit? (1 = Play again, 2 = Quit)");
                if (byte.Parse(Console.ReadLine()) == 2)
                {
                    playing = false;
                }

                Console.WriteLine("\n----------------------------------------------\n");
                wait2secs();
            } while (playing);

            //Console.WriteLine(Player1.GetName() + "'s balance is " + Player1.GetBalance()); // get Balance for player
        }

        public static void wait2secs()
        {
            try
            {
                Thread.Sleep(1000);
            }
            catch (ThreadInterruptedException ex)
            {
                Console.WriteLine("Thread was interrupted");
            }
        }

        public static void playerWins(Player Player1)
        {
            Console.WriteLine(Player1.GetName() + " wins!\n");
            Player1.WinAddMoney();
            Console.WriteLine(Player1.GetName() + "'s balance is now " + Player1.GetBalance());
        }

        public static void dealerWins(Player Player1)
        {
            Console.WriteLine("Dealer wins!\n");
            Player1.LoseReduceMoney();
            Console.WriteLine(Player1.GetName() + "'s balance is now " + Player1.GetBalance());
        }

        public static void playerWinsBlackJack(Player Player1)
        {
            Console.WriteLine(Player1.GetName() + " wins!\n");
            Player1.WinAddMoneyBlackJack();
            Console.WriteLine(Player1.GetName() + "'s balance is now " + Player1.GetBalance());
        }

        public static void resetGame(Dealer Dealer, Player Player1, Deck Deck1)
        {
            Deck1.ResetDeck();
            Player1.ClearHand();
            Dealer.ClearHand();
        }
    }
}
