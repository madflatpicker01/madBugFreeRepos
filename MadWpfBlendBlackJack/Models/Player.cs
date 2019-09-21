using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadWpfBlendBlackJack.Models
{
    public class Player : PlayerStats
    {


        public string name { get; set; }

        public string image { get; set; }

        public int bank { get; set; }

        public int bet { get; set; }

    }
}
