using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadWpfBlendBlackJack.Models
{
    public class Card
    {
        public enum cardSuit { HEARTS, CLUBS, SPADES, DIAMONDS };

        public enum cardRank { ACE = 1, TWO, THREE, FOUR, FIVE, SIX,  SEVEN, EIGHT, NINE,
            TEN , JACK , QUEEN,  KING  }

        public cardSuit suit { get; set; }
        public cardRank rank { get; set; }
        public string image { get; set; }
        public int cardVal { get; set; }  // convert enum val to numeric
    }
}
