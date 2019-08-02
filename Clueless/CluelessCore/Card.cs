using System;
using System.Collections.Generic;

namespace CluelessCore
{
    public class Card
    {
        public CardType type;
        public String name;

        public Card(CardType type, String name)
        {
            this.type = type;
            this.name = name;
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}", type.ToString(), name);
        }

        public static List<Card> generateDefaultCards(){
            List<Card> cards = new List<Card>();
            cards.Add(new Card(CardType.Character, "Red"));
            cards.Add(new Card(CardType.Character, "Blue"));
            cards.Add(new Card(CardType.Character, "Yellow"));
            cards.Add(new Card(CardType.Character, "Purple"));
            cards.Add(new Card(CardType.Character, "Grey"));
            cards.Add(new Card(CardType.Character, "White"));
            
            cards.Add(new Card(CardType.Item, "Knife"));
            cards.Add(new Card(CardType.Item, "Revolver"));
            cards.Add(new Card(CardType.Item, "Sword"));
            cards.Add(new Card(CardType.Item, "Rope"));
            cards.Add(new Card(CardType.Item, "Pipe"));
            cards.Add(new Card(CardType.Item, "Wrench"));
            
            cards.Add(new Card(CardType.Room, "Kitchen"));
            cards.Add(new Card(CardType.Room, "Dining Room"));
            cards.Add(new Card(CardType.Room, "Lounge"));
            cards.Add(new Card(CardType.Room, "Hall"));
            cards.Add(new Card(CardType.Room, "Study"));
            cards.Add(new Card(CardType.Room, "Billiard Room"));
            cards.Add(new Card(CardType.Room, "Conservatory"));
            cards.Add(new Card(CardType.Room, "Ballroom"));

            return cards;
        }
    }

    public enum CardType
    {
        Room,
        Character,
        Item
    }
    
    
}