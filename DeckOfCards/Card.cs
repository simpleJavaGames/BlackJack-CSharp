using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This is a converted copy of the Java Card class that I made here: https://github.com/SCHOOLACCOUNT32/Deck-and-Card-template/blob/main/src/Card.java
namespace DeckOfCards{
    public class Card {
        private readonly byte cardSuit;
        private readonly byte cardRankByte;
        private String cardRankFinal;

        public Card(byte cardSuit,byte cardRankbyte){
            this.cardSuit = cardSuit; //0 = Spades, 1 = Hearts, 2 = Diamonds , 3 = Clubs
            this.cardRankByte = cardRankbyte;
            switch (cardRankbyte){
                case 1: cardRankFinal = "Ace";
                    break;
                case 11: cardRankFinal = "J";
                    break;// Assign suit and rank to each card
                case 12: cardRankFinal = "Q";
                    break;
                case 13:
                    cardRankFinal = "K";
                    break;
                default: cardRankFinal = ""+cardRankbyte;
                    break;
            }
        }

        public String ToCardString(){
            switch(cardSuit){
                case 0:
                    return cardRankFinal + " of Spades";                 // return each card as a string
                case 1:
                    return cardRankFinal + " of Hearts";
                case 2:
                    return cardRankFinal + " of Diamonds";
                case 3:
                    return cardRankFinal + " of Clubs";
            }
            return null;
        }
        public byte GetCardSuit(){
            return cardSuit;
        }
        public byte GetCardRank(){
            return cardRankByte;
        }
    }
}
