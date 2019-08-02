using System;
using System.Collections.Generic;
using System.Globalization;

namespace CluelessCore
{
    public class Room
    {
        public readonly String name;
        public readonly int id;
        
        public bool hasShortcut => shortCutRoom != null;
        
        private List<Tile> _entrances;
        private Room shortCutRoom;
        
        public Room(String name, int id)
        {
            this.name = name;
            this.id = id;
            
            _entrances = new List<Tile>();
        }

        public Tile[] getEntrance()
        {
            return _entrances.ToArray();
        }
    }
}