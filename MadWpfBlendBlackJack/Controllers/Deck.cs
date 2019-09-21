using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;

namespace MadWpfBlendBlackJack.Models
{
    public class Deck : Card
    {

        #region "Constants"
        //private static readonly int SHUFFLE_COUNT = 7;
        public static readonly int FIRST_CARD = 1;
        public static readonly int LAST_CARD = 14;
        public static readonly int NUMBER_OF_SAME_VALUE_CARDS = 4;
        #endregion

        //private int _numberOfDecks = 1;

        private Random _random = new Random();

        public List<Card> theDeck;
        public List<string> theImagePathNames;

      
        // constructor
        public Deck( )
        {
            //_numberOfDecks = 1;
            theImagePathNames = LoadCardImagesDirectory();

            CreateNewDeck();
        }

        private List<string> LoadCardImagesDirectory()
        {
            string cardImagesPath = AppDomain.CurrentDomain.BaseDirectory;
            cardImagesPath  = Path.Combine( cardImagesPath, "Cards");
            List<string> localImagesPathNames = new List<string>();

            string[] cardFileNames;

            try
            {
                cardFileNames = Directory.GetFiles(cardImagesPath, "*.png");

                foreach (string fileName in cardFileNames)
                {
                    try
                    {
                        localImagesPathNames.Add(fileName);
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                var theVal = ex;
            }
            return localImagesPathNames;
        }

        private cardRank ReturnEnumValFromIndex( int index)
        {
            if (index > 14 || index < 1) return cardRank.ACE;
            switch (index)
                {
                case 1:
                    return cardRank.ACE;
                case 2:
                    return cardRank.TWO;
                case 3:
                    return cardRank.THREE;
                case 4:
                    return cardRank.FOUR;
                case 5:
                    return cardRank.FIVE;
                case 6:
                    return cardRank.SIX;
                case 7:
                    return cardRank.SEVEN;
                case 8:
                    return cardRank.EIGHT;
                case 9:
                    return cardRank.NINE;
                case 10:
                    return cardRank.TEN;
                case 11:
                    return cardRank.JACK;
                case 12:
                    return cardRank.QUEEN;
                case 13:
                    return cardRank.KING;
            }
            return cardRank.ACE;
        }

        /// <summary>
        /// Find the appropriate card image based upon the requested suit and rank
        /// </summary>
        /// <param name="theSuit"></param>
        /// <param name="theRankVal"></param>
        /// <returns>the image path</returns>
        private string LoadImageFromSuitAndIndex(cardSuit theSuit, int theRankVal)
        {
            string rankStr = theRankVal.ToString();
            switch (theRankVal)
            {
                case 11: rankStr = "j";
                    break;
                case 12: rankStr = "q";
                    break;
                case 13:  rankStr = "k";
                    break;
                default:  break;
            }

            string retString = "";
            switch (theSuit)
            {
                case cardSuit.CLUBS:
                    retString = "c" + rankStr + ".png";
                    break;
                case cardSuit.DIAMONDS:
                    retString = "d" + rankStr + ".png";
                    break;
                case cardSuit.HEARTS:
                    retString = "h" + rankStr + ".png";
                    break;
                case cardSuit.SPADES:
                    retString = "s" + rankStr + ".png";
                    break;
            }

            return @"/Images/Cards/" + retString;
            //return theImagePathNames.Where(x => x.Contains(retString)).FirstOrDefault();
          
        }

        private BitmapImage LoadBitMapImageFromCard(string imgPath)
        {
            imgPath = "pack://application:,," + imgPath;
            Uri resourceUri = new Uri(imgPath, UriKind.Absolute);
            return new BitmapImage(resourceUri);
        }

        private List<Card> LoadSuit( List<Card> curDeck, cardSuit theSuit)
        {
            for (int rankVal = 1; rankVal < 14; rankVal++)
            {
                var theRank = ReturnEnumValFromIndex(rankVal);
                Card theCard = new Card
                {
                    image = LoadImageFromSuitAndIndex(theSuit, rankVal),
                    rank = theRank,
                    suit = theSuit,
                    cardVal = rankVal <= 10 ? rankVal : 10
                };
                theCard.cardBitMapImage = LoadBitMapImageFromCard(theCard.image);
                curDeck.Add(theCard);
            }
            return curDeck;
        }

        // Create the new deck 
        private List<Card> CreateNewDeck( )
        {
            theDeck = new List<Card>();

            // because the cards are always defined as Hearts,Clubs... this apparent ineffiecency is accepted
            theDeck = LoadSuit(theDeck, cardSuit.CLUBS);
            theDeck = LoadSuit(theDeck, cardSuit.DIAMONDS);
            theDeck = LoadSuit(theDeck, cardSuit.HEARTS);
            theDeck = LoadSuit(theDeck, cardSuit.SPADES);
            //Shuffle(SHUFFLE_COUNT, theDeck);
            return theDeck;
        }

        #region "Cards"

        #endregion
 
        public List<Card> Shuffle(int times, List<Card> cards)
        {
            if (theDeck == null || theDeck.Count == 0) return theDeck;

            var theCards = cards;

            for (int k = 0; k < times; k++)
            {
                for (int i = 0; i < theCards.Count; i++)
                {
                    int j = _random.Next( theCards.Count - 1);

                    Card t = theCards[i];
                    theCards[i] = theCards[j]; 
                    theCards[j] = t;
                }
            }
            return theCards;
        }
    }
}
