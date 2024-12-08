using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ElfeledettVarosokWPF
{
    public class Player
    {
        public string Name { get; set; }
        public List<Item> Inventory { get; set; }

        public Player(string name)
        {
            Name = name;
            Inventory = new List<Item>();
        }
    }

    public class Item
    {
        public string Name { get; set; }

        public Item(string name)
        {
            Name = name;
        }
    }
}
