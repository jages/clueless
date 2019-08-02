using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CluelessCore
{
    public class Player
    {
        public readonly int id;
        
        private int posX;
        private int posY;

        private Room _room;

        private List<Card> _cards;

        public Player(int id)
        {
            this.id = id;
            _cards = new List<Card>();
        }

        public override string ToString()
        {
            return getCards();
        }

        public string getCards()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var card in _cards)
            {
                sb.Append(String.Format("{0}, ", card));
            }

            return sb.ToString();
        }

        public void addCard(Card card)
        {
            _cards.Add(card);
        }

        public bool hasCard(CardType type, String name)
        {
            return _cards.Any(item => item.type == type && item.name == name);
        }

        public void Move(int x, int y)
        {
            posX = x;
            posY = y;
        }

        public bool Move(Room room)
        {

            if(_room == room)
                return false;

            _room = room;
            return true;
        }

    }
}