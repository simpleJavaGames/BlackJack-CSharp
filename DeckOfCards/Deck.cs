//This is a converted copy of the Java Deck class that I made here: https://github.com/SCHOOLACCOUNT32/Deck-and-Card-template/blob/main/src/Deck.java
//todo finish completing this .

using System;
using System.Collections.Generic;

namespace DeckOfCards {
    public class Deck {
        private static int Counter;
        protected List<Card> DeckOfCards = new List<Card>();
        private List<Card> CardsOut = new List<Card>();


        public Deck() {

            for(byte i=0;i<4;i++){                        // create the deck and fill it with all the cards
                for(byte j=1;j<14;j++){
                    DeckOfCards.Add(new Card(i,j));
                }
            }
            ShuffleDeck();
        }

        public void ShuffleDeck(){
            DeckOfCards = Shuffle(DeckOfCards);
            // shuffle deck
        }
        
        private static List<Card> Shuffle(List<Card> list) {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1) {
                n--;
                int k = rng.Next(n + 1);
                Card value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return new List<Card>(list);
        }

        public Card DealOneCard(){
            int chosenCard = Counter; // Deal one card out
            Counter++;                            // there is a bug where you can get the same card twice. this is because we're removing the card before sending the new one.
            CardsOut.Add(DeckOfCards[chosenCard]);
            return DeckOfCards[chosenCard];
        }
        public void RemoveTopCard(){
            DeckOfCards.RemoveAt(Counter);
        }
        public int DeckOfCardsLength(){
            return DeckOfCards.Count;
        }

        //This will reset the deck by Adding all of the cards out back into the deck.
        public void ResetDeck(){
            Counter =0;
            DeckOfCards.AddRange(CardsOut);
            CardsOut.Clear();
            ShuffleDeck();
        }
    }
}
