using System;
using System.Collections.Generic;
using System.Linq;

namespace CluelessCore
{
    public class Game
    {
        private List<Player> _players;
        private List<Room> _rooms;
        private List<Card> _items;
        private List<Card> _persons;
        private Card person;
        private Card room;
        private Card weapon;

        private int currentPlayer;
        private int phase;

        public Game()
        {
            _players = new List<Player>();
            _rooms = new List<Room>();
            _items= new List<Card>();
            _persons = new List<Card>();
        }

        private void initGame()
        {
            //shuffle cards
            Random rng = new Random();
            List<Card> cards = Card.generateDefaultCards().OrderBy(x => rng.Next()).ToList();
            
            //generate rooms
            int r = 0;
            foreach (var room in cards.Where(x => x.type == CardType.Room))
            {
                _rooms.Add(new Room(room.name, r++));
            }
            
            foreach (var item in cards.Where(x => x.type == CardType.Item))
            {
                _items.Add(item);
            }
            
            foreach (var item in cards.Where(x => x.type == CardType.Character))
            {
                _persons.Add(item);
            }

            //set the target
            room = cards.First(x => x.type == CardType.Room);
            cards.Remove(room);
            
            person = cards.First(x => x.type == CardType.Character);
            cards.Remove(person);
            
            weapon = cards.First(x => x.type == CardType.Item);
            cards.Remove(weapon);

            //deal cards
            for (int i = 0; i < cards.Count; i++)
            {
                _players[i % _players.Count].addCard(cards[i]);
            }
        }

        public TurnResult StartGame(int playernumber)
        {
            TurnResult result = new TurnResult();
            
            if (playernumber > 6)
            {
                return result;
            }
            
            for (int i = 0; i < playernumber; i++)
            {
                _players.Add(new Player(i));
            }
                
            initGame();
            
            Random rng = new Random();
            currentPlayer =  _players[rng.Next(_players.Count - 1)].id;

            phase = 0;

            result.accepted = true;
            result.nextplayer = currentPlayer;
            result.nextphase = phase;
            return result;
        }

        public TurnResult Play(FirstAction act, String roomname)
        {
            TurnResult result = new TurnResult();
            result.nextphase = phase;
            result.nextplayer = currentPlayer;
            
            
            if (_rooms.Any(x => x.name == roomname))
            {
                if (phase == 0)
                {
                    if (act == FirstAction.Move)
                    {
                        result.accepted = _players[currentPlayer].Move(_rooms.First(x => x.name == roomname));
                    }

                    if (result.accepted)
                    {
                        phase = 1;
                        result.nextphase = 1;
                    }
                }
            }
            return result;
        }

        private int getNextPlayer()
        {
            currentPlayer = (currentPlayer + 1) % _players.Count;

            return currentPlayer;
        }

        private suggestionResult AskForResults(String room, String person, String item)
        {
            suggestionResult result = new suggestionResult();
            result.playersAsked = 0;
            for(int i = 1; i < _players.Count -1; i++)
            {
                result.playersAsked = i;
                if (_players[(currentPlayer + i) % _players.Count].hasCard(CardType.Room, room))
                    result.revealedCard = new Card(CardType.Room, room);
                else if(_players[(currentPlayer + i) % _players.Count].hasCard(CardType.Character, person))
                    result.revealedCard = new Card(CardType.Character, person);
                else if(_players[(currentPlayer + i) % _players.Count].hasCard(CardType.Item, item))
                    result.revealedCard = new Card(CardType.Item, item);

                if (result.revealedCard != null)
                    break;
            }

            return result;
        }

        private bool Accusationsuccessfull(string room, string person, string item)
        {
            return (this.person.name == person) && (this.room.name == room) && (this.weapon.name == item);
        }

        public TurnResult Play(SecondAction act, String personName, String itemName, String roomName)
        {
            TurnResult result = new TurnResult();

            if ((phase == 2 && act != SecondAction.Accuse) || act == SecondAction.Pass)
            {
                result.accepted = false;
                result.nextphase = 0;
                result.nextplayer = getNextPlayer();
            }
            else
            {

                if (act == SecondAction.Suggest && _persons.Any(x => x.name == personName) &&
                    _items.Any(x => x.name == itemName))
                {
                    result.accepted = true;
                    result.suggestion = AskForResults(roomName, personName, itemName);
                    result.nextphase = 2;
                    result.nextplayer = currentPlayer;

                }
                else if (act == SecondAction.Accuse && _persons.Any(x => x.name == personName) &&
                         _items.Any(x => x.name == itemName) && _rooms.Any(x => x.name == roomName))
                {
                    result.accepted = true;
                    result.accusationSuccessful = Accusationsuccessfull(roomName, personName, itemName);
                    if (!result.accusationSuccessful)
                    {
                        result.nextplayer = getNextPlayer();
                        result.nextphase = 0;
                    }
                }
            }

            return result;
        }
    }


}