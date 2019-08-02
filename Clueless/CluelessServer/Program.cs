using System;
using CluelessCore;

namespace CluelessServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World, I'm clueless!");
            Console.WriteLine("How many players are playing?");

            string input = Console.ReadLine();
            try
            {
                int currentplayer = Int32.Parse(input);
                Game game = new Game();
                TurnResult res = game.StartGame(currentplayer);

                currentplayer = res.nextplayer;

                while (!res.accusationSuccessful)
                {
                    res = PlayRound(game, res);
                }

                if (res.accusationSuccessful)
                {
                    Console.WriteLine("Player {0} solved the riddle!", res.nextplayer);
                }

            }
            catch (FormatException fex)
            {
                Console.WriteLine("That's not a number.");
            }
        }

        static TurnResult PlayRound(Game game, TurnResult res)
        {
            Console.WriteLine("Player{0}s Turn.", res.nextplayer);
            
            if (res.nextphase == 0)
            {
                Console.WriteLine("what room do you want to move?");
                string room = Console.ReadLine();
                res = game.Play(FirstAction.Move, room);

                if (res.accepted)
                {
                    Console.WriteLine("You moved to the {0}.", room);
                }


            } else if (res.nextphase == 1)
            {
                Console.WriteLine("Make a suggestion. Person:");
                string person = Console.ReadLine();
                Console.WriteLine("Item: ");
                string item = Console.ReadLine();

                res = game.Play(SecondAction.Suggest, person, item, "");
                if (res.accepted && res.suggestion != null)
                {
                    if (res.suggestion.revealedCard != null)
                    {
                        Console.WriteLine("The {0} was revealed to you by Player {1}", res.suggestion.revealedCard.name,
                            res.nextplayer + res.suggestion.playersAsked);
                    }
                    else
                    {
                        Console.WriteLine("Nobody was able to help you.");
                    }
                }

            } else if (res.nextphase == 2)
            {
                Console.WriteLine("Want to accuse Someone? (y/n)");
                string accuse = Console.ReadLine();
                if (accuse.Contains("y"))
                {
                    Console.WriteLine("Make a accusation. Person:");
                    string person = Console.ReadLine();
                    Console.WriteLine("Item: ");
                    string item = Console.ReadLine();
                    Console.WriteLine("Room: ");
                    string room = Console.ReadLine();

                    res = game.Play(SecondAction.Accuse, person, item, room);
                }
                else
                {
                    res = game.Play(SecondAction.Pass, "", "", "");
                }
            }

            return res;
        }
    }
}