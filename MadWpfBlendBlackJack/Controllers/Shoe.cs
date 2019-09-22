using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MadWpfBlendBlackJack.Models
{

    public class Shoe  
    {
        // the number of decks in the shoe
        private int _numberOfDecks;

        private Random _random = new Random();

        // the number of cards in the cut when time to shuffle
        public int shuffleValue;

        // the current shoe
        public  Queue<Card> currentShoe;
        public List<Card> currentShoeList;

        public int NumberOfDecks
        {
            get { return _numberOfDecks; }
        }

        public int CardsLeft
        {
            get { return currentShoe.Count; }
        }

        public Shoe(int numDecksInShoe )
        {
            // constructor 
            _numberOfDecks = numDecksInShoe;
            currentShoeList = new List<Card>();
            for ( int i = 0; i < _numberOfDecks; i++)
            {
                var newDeck = new Deck();
                newDeck.theDeck = newDeck.Shuffle(2, newDeck.theDeck);  // shuffle twice
                currentShoeList.AddRange(newDeck.theDeck);
            }
            currentShoe = new Queue<Card>(currentShoeList);
            int cc = currentShoe.Count;
            double pVal = (cc / 100) * 5;
            shuffleValue = (int)pVal;
         
        }

        // Shuffle all the cards in the shoe
        public List<Card> ShuffleShoe(int times, List<Card> cards)
        {
            if (cards == null || cards.Count == 0) return cards;

                var theCards = cards;

                for (int k = 0; k < times; k++)
                {
                    for (int i = 0; i < theCards.Count; i++)
                    {
                        int j = _random.Next(theCards.Count - 1);

                        Card t = theCards[i];
                        theCards[i] = theCards[j];
                        theCards[j] = t;
                    }
                }
                return theCards;
        }

        // Pop next card from the queue
        public Card GetNext()
        {
            // get the next card/pop from the list
            if (currentShoe.Count > shuffleValue)
            {
                return currentShoe.Dequeue();
            }
            else
            {
                MessageBox.Show("Time to Shuffle the Cards... one sec", "Shuffling", MessageBoxButton.OK, MessageBoxImage.Information);
                // time to shuffle 
                currentShoeList = ShuffleShoe( NumberOfDecks , currentShoeList);
                currentShoe = new Queue<Card>(currentShoeList);
                return currentShoe.Dequeue();
            }
        }
    }
}
