using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ElfeledettVarosokWPF
{
    public class Dialogue
    {
        public string Question { get; set; }
        public List<string> Options { get; set; } = new List<string>();
        public List<string> Responses { get; set; } = new List<string>();
    }
}