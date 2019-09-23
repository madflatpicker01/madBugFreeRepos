using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MadWpfBlendBlackJack.Models;
 
using System.Text;
using System.Collections.Generic;

namespace BlackJackUtilitiesTests
{
    [TestClass]
    public class BlackJackTests
    {


    

        private Shoe _currentShoe;

        private Player _currentPlayer;

        private Player _currentDealer;

        private Deck _currentDeck;

        [TestInitialize]
        public void DoInit()
        {
         
            _currentDealer = new Player();
            _currentPlayer = new Player();
        
        }

        private void DumpDeckToTrace( List<Card> theDeck)
        {
            string theResult = string.Empty;
            StringBuilder outStrg = new StringBuilder();

            int cardNum = 0;
            foreach (var c in theDeck )
            {
                outStrg.AppendLine(string.Format("CardNum:  {0} CardVal: {1} CardSuit: {2} CardRank: {3} CardImage: {4} ",
                    cardNum.ToString(), c.cardVal.ToString(), c.suit.ToString(), c.rank.ToString(), c.image));
                outStrg.Append(System.Environment.NewLine);

                cardNum++;
            }
            theResult = outStrg.ToString();
            System.Diagnostics.Trace.WriteLine(theResult);

        }
        
      
        public void CreateNewDeckWorksAsExpected()
        {
            _currentDeck = new Deck();

            DumpDeckToTrace(_currentDeck.theDeck);
            Assert.IsNotNull(_currentDeck);
        }
 
        public void ShuffleDeckWorksAsExpected()
        {
            Deck newDeck = new Deck();
            newDeck.theDeck =  newDeck.Shuffle( 1, newDeck.theDeck);
             DumpDeckToTrace(newDeck.theDeck);
            Assert.IsNotNull(newDeck);
        }


    
        public void ShuffleNewShoeWorksAsExpected()
        {
            _currentShoe = new Shoe(6);
            _currentShoe.currentShoe = new Queue<Card>( _currentShoe.ShuffleShoe(4, _currentShoe.currentShoeList)); // shuffle all the cards
            DumpDeckToTrace(_currentShoe.currentShoeList);
            Assert.IsNotNull(_currentShoe);
        }

        [TestMethod]
        public void DealWorksAsExpected()
        {



        }

     
        public void GetNextCardWorksAsExpected()
        {
            _currentShoe = new Shoe(2);
            Card nextCard = _currentShoe.GetNext();
            Assert.IsNotNull(nextCard);
        }

        [TestMethod]
        public void CalculateHandValueWorksAsExpected()
        {
        }
    }
}
