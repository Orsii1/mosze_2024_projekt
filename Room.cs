using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ElfeledettVarosokWPF
{
    public class Room
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> ConnectedRooms { get; set; }
        public List<NPC> NPCs { get; set; }

        public Room(string name, string description)
        {
            Name = name;
            Description = description;
            ConnectedRooms = new List<string>();
            NPCs = new List<NPC>();
        }
    }
}
