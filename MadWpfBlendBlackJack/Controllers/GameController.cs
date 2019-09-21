using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadWpfBlendBlackJack.Models;
using MadWpfBlendBlackJack.Views;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MadWpfBlendBlackJack.Controllers
{
    // access to the cards and players
    public class GameController
    {
        // Game control info
        public bool playersSelected = false;
        public bool startupCompleted = false;
        public bool dealAllowed = false;
        public bool handCompleted = false;
        public bool standSelected = false;
        public bool playerWon = false;
        public bool wasPlayerBlackjack = false;
        

        public Player thePlayer;
        public Player theDealer;
        public int theNumDecks = 2;

        public  List<Card> dealerHand;
        public  List<Card> playersHand;
        private Shoe currentShoe;

        private Deck theDeck; 
        public BitmapImage theComputerCard; 

        public GameController()
        {
            playersSelected = false;
            startupCompleted = false;
            thePlayer = new Player();
            playersHand = new List<Card>();
            theDealer = new Player();
            dealerHand = new List<Card>();
            theDeck = new Deck();
            theComputerCard = theDeck.computerCard;
        }

  

    /// <summary>
    /// get the required information to begin play
    /// </summary>
        public void GetStartupInfo()
        {
            StartupInfoWindow startupInfo = new StartupInfoWindow( this );
            startupInfo.Show();

            currentShoe = new Shoe(theNumDecks);
            thePlayer.startingBank = 1000;
            thePlayer.bank = thePlayer.startingBank;
            thePlayer.handsWon = 0;
            thePlayer.handsPlayed = 0;
            thePlayer.endingBank = 0;
            thePlayer.bet = 10; // default bet

            theDealer.handsPlayed = 0;
            theDealer.handsWon = 0;
            theDealer.startingBank = 5000; // possible to break the dealer
            theDealer.bank = 5000;
            theDealer.bet = 0;
            theDealer.image = "Ugly";

            // Game control info
            playersSelected = true;
            startupCompleted = true;
            dealAllowed = true;
            handCompleted = false;
            standSelected = false;
    }
        
        /// <summary>
        /// get a new shoe - shuffled - with the number of decks specified
        /// </summary>
        public void GetNewShoe()
        {
            // get the new shoe based upon the number of desired decks required
            currentShoe = new Shoe(theNumDecks);
            dealAllowed = true;
        }

        /// <summary>
        /// allow the player to update the bet from the default  of 10
        /// </summary>
        public void GetPlayerBet()
        {
            // get the bet for this hand
            // determine if the player bank has enough money
            // 
            thePlayer.bet = 10;
            thePlayer.bank -= thePlayer.bet;
        }

        /// <summary>
        /// retrieve the next card from the shoe on behalf of the player 
        /// </summary>
        /// <param name="theHand"></param>
        public void GetNextCard( ref List<Card> theHand )
        {
            theHand.Add( currentShoe.GetNext());
        }

        /// <summary>
        /// finish playing the hand on behalf of the dealer since no one is victorious as yet
        /// </summary>
        public string CompleteHand()
        {
            // the player hit stand so calculate the results
            string resultStr = string.Empty;
            int dealerScore = GetScore(dealerHand);
            if ( dealerScore < 17)
            {
                // finish the game
                do
                {
                    GetNextCard(ref dealerHand);
                    ((MainWindow)Application.Current.MainWindow).txtDealerCards.Text = DisplayCardsToUser(dealerHand);
                    ((MainWindow)Application.Current.MainWindow).icDealerImageItems.ItemsSource = BuildListOfImagesForItemsControl(dealerHand, false);
                    ((MainWindow)Application.Current.MainWindow).txtDealerScore.Text = DisplayScoresToUser(dealerHand);
                    ((MainWindow)Application.Current.MainWindow).txtDealerScore.Visibility = Visibility.Visible;


                } while (GetScore(dealerHand) < 17);
            }
          
                resultStr = CheckScores();
         
            ((MainWindow)Application.Current.MainWindow).txtDealerCards.Text = DisplayCardsToUser(dealerHand);
            ((MainWindow)Application.Current.MainWindow).icDealerImageItems.ItemsSource = BuildListOfImagesForItemsControl(dealerHand, false);
            ((MainWindow)Application.Current.MainWindow).txtDealerScore.Text = DisplayScoresToUser(dealerHand);
            ((MainWindow)Application.Current.MainWindow).txtDealerScore.Visibility = Visibility.Visible;

            handCompleted = true;
            dealAllowed = true; 
            return resultStr;
        }


        /// <summary>
        /// Deal the cards for the initial round
        /// </summary>
        public void DealTheCards()
        {
            dealAllowed = false; // don't allow another deal until this hand is done
            handCompleted = false;  // the hand is not done yet
            standSelected = false;  // reset
                                        
            thePlayer.handsPlayed++;
            theDealer.handsPlayed++;
            dealerHand.Clear();
            playersHand.Clear();

            // deal two cards
            // deal the cards - one to player - one to dealer ; repeat
            GetNextCard(ref playersHand);
            GetNextCard(ref dealerHand);
            GetNextCard(ref playersHand);
            GetNextCard(ref dealerHand);
        }


        /// <summary>
        /// Check to see if there is a bust or winner yet
        /// </summary>
        /// <returns></returns>
        public string CheckScores()
        {
            int dealersScore = GetScore(dealerHand);

            // check for dealer blackjack on initial deal
            if (( dealersScore == 21) && ( dealerHand.Count == 2))
            {
                // DEALER HAS BLACKJACK 
                playerWon = false;
                handCompleted = true;
                dealAllowed = true;
                CalculateStatistics();

                ((MainWindow)Application.Current.MainWindow).txtDealerCards.Text = DisplayCardsToUser(dealerHand);
                ((MainWindow)Application.Current.MainWindow).icDealerImageItems.ItemsSource = BuildListOfImagesForItemsControl(dealerHand, false);
                ((MainWindow)Application.Current.MainWindow).txtDealerScore.Text = DisplayScoresToUser(dealerHand);
                ((MainWindow)Application.Current.MainWindow).txtDealerScore.Visibility = Visibility.Visible;

                handCompleted = true;
                dealAllowed = true;
                return "DEALER HAS BLACKJACK AND WINS!";
            }

            int playerScore = GetScore(playersHand);

            // check for player blackjack on initial deal
            if (( playerScore == 21) && (playersHand.Count == 2))
            {
                // DEALER HAS BLACKJACK 
                playerWon = true;
                handCompleted = true;
                dealAllowed = true;
                CalculateStatistics();
                return "PLAYER HAS BLACKJACK AND WINS!";
            }

            if (playerScore > 21)
            {
                playerWon = false;
                handCompleted = true;
                dealAllowed = true;
                CalculateStatistics();
                return "PLAYER BUSTED!";
            }

            if (dealersScore > 21)  // dealer busted so that's a win
            {
                playerWon = true;
                handCompleted = true;
                dealAllowed = true;
                CalculateStatistics();
                return "DEALER BUSTED!  PLAYER WINS!";
            }

            if ((standSelected) && ((dealersScore > playerScore) || (dealersScore == 21)))
            {
                playerWon = false;
                handCompleted = true;
                dealAllowed = true;
                CalculateStatistics();
                return "DEALER WINS!";
            }

            if ((standSelected) && (dealersScore < playerScore))
            {
                playerWon = true;
                handCompleted = true;
                dealAllowed = true;
                CalculateStatistics();
                return "PLAYER WINS!";
            }

            if ((standSelected) && (dealersScore == playerScore))
            {
                playerWon = false;
                handCompleted = true;
                dealAllowed = true;
                return "SCORE TIED.  THE HAND IS A PUSH.";
            }


            return string.Empty;
        }

        // Calculate the value of the cards in the hand
        private int GetScore(List<Card> theHand)
        {
            int score = 0;
            bool acePresent = false;
            int numAces = 0;

            foreach (var c in theHand)
            {
                if (c.rank == Card.cardRank.ACE) // ALLOW ACE TO BE 1 OR 11
                {
                    acePresent = true;
                    numAces++;
                    score += 11;
                }
                else
                {
                    score += c.cardVal;
                }
            }

            if (acePresent)
            {
                do
                {
                    if (score > 21)
                    {
                        if (numAces > 0) score -= 10;
                    }// if using an ACE as 11 would bust the reset it to 1
                    numAces--;
                } while (numAces > 0);
            }
            return score;
        }

        /// <summary>
        /// get the currect card total values for the cards displayed
        /// </summary>
        /// <param name="theHand"></param>
        /// <returns></returns>
        public string DisplayScoresToUser(List<Card> theHand)
        {
            return GetScore(theHand).ToString();
        }

        /// <summary>
        /// Update the statistics for the players
        /// </summary>
        public void CalculateStatistics()
        {
            if ( handCompleted)
            {
                if (playerWon)
                {
                    thePlayer.handsWon++;

                    if ( wasPlayerBlackjack)
                    {
                        thePlayer.bank += thePlayer.bet * 3 / 2;
                        theDealer.bank -= thePlayer.bet * 3 / 2; 
                    }
                    else
                    {
                        thePlayer.bank += thePlayer.bet;
                        theDealer.bank -= thePlayer.bet; 
                    }
                }
                else
                {
                    theDealer.handsWon++;
                    thePlayer.bank -= thePlayer.bet;
                    theDealer.bank += thePlayer.bet;
                }
            }
        }

  
        /// <summary>
        /// return a string that shows the card name and rank in the hand
        /// </summary>
        /// <param name="theHand"></param>
        /// <returns>the string with the values</returns>
        public string DisplayCardsToUser(List<Card> theHand)
        {
            StringBuilder theCards = new StringBuilder();
            foreach (var c in theHand)
            {
                theCards.Append(string.Format("{0}-{1},", c.rank.ToString(), c.suit.ToString()));
            }
            return theCards.ToString();
        }

        /// <summary>
        /// Used to load the items control with the images from the hands in play
        /// </summary>
        /// <param name="theHand"></param>
        /// <returns>the list of image names </returns>
        public List<BitmapImage> BuildListOfImagesForItemsControl(List<Card>  theHand, bool IsInitial)
        {
            // modify to use the bitmap
            //List<string> items = new List<string>();
            List<BitmapImage> items = new List<BitmapImage>();
            foreach (var cc in theHand)
            {
                //items.Add(cc.image);
                items.Add(cc.cardBitMapImage);
            }
            if (IsInitial) items[1] = theComputerCard;
            return items;
        }
    }
}
