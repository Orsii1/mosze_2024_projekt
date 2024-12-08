using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace ElfeledettVarosokWPF
{
    public class Inventory
    {
        private List<string> items;

        public Inventory()
        {
            items = new List<string>();
        }

        public void AddItem(string item)
        {
            items.Add(item);
        }

        public void RemoveItem(string item)
        {
            items.Remove(item);
        }

        public string DisplayItems()
        {
            if (items.Count == 0)
            {
                return "Az inventory üres.";
            }

            return string.Join(", ", items);
        }
    }
}
