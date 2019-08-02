using System;

namespace CluelessCore
{
    public class TurnResult
    {
        public bool accepted = false;
        public int nextplayer = -1;
        public int nextphase = -1;

        public bool accusationSuccessful = false;
        public suggestionResult suggestion = null;

    }

    public class suggestionResult
    {
        public int playersAsked = 0;
        public Card revealedCard = null;
    }
}