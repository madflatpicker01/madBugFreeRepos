using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MadWpfBlendBlackJack.Views;

using MadWpfBlendBlackJack.Controllers;

namespace MadWpfBlendBlackJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GameController theGame;

        public MainWindow()
        {
            InitializeComponent();
            SetImagesInitial();
            theGame = new GameController();

            txtPlayerCards.Visibility = Visibility.Hidden;           
            txtDealerCards.Visibility = Visibility.Hidden;         
            txtDealerScore.Visibility = Visibility.Hidden;
        }


        private void SetImagesInitial()
        {
            txtDealerCards.Text = "This is the text";
        }

        // Exit the application
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(1);
        }

        // Start a new game
        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            // new game can not be selected when a game is in progress;
            if (theGame.startupCompleted)
            {
                MessageBox.Show("NEW GAME may not be started when a game is in progress. ", "NEW GAME NOT ALLOWED", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // get startup info using dialog 
            theGame.GetStartupInfo();
        }

        private void TxtPlayerCards_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        // player stands so calculate results of the game 
        private void btnStand_Click(object sender, RoutedEventArgs e)
        {
            if (!theGame.startupCompleted)
            {
                MessageBox.Show("Please Select NEW to create a NEW game, then select DEAL", "STAND NOT ALLOWED", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if ( theGame.dealAllowed)
            {
                MessageBox.Show("Please select DEAL", "STAND NOT ALLOWED", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if ( theGame.handCompleted)
            {
                MessageBox.Show("Previous hand was completed. Please select DEAL", "STAND  NOT ALLOWED", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            theGame.standSelected = true;
            txtFinalResults.Text = theGame.CompleteHand();
            MessageBox.Show(txtFinalResults.Text, "HAND COMPLETED", MessageBoxButton.OK, MessageBoxImage.Information);
            txtFinalResults.Visibility = Visibility.Visible;
        }

        //         // allow player to update the bet
        //       theGame.GetPlayerBet();
 
        
        private void BtnDeal_Click_1(object sender, RoutedEventArgs e)
        {
            if (!theGame.startupCompleted)
            {
                MessageBox.Show("Please Select NEW to create a NEW game, then select DEAL", "DEAL NOT ALLOWED", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if ( !theGame.dealAllowed) 
            {
                MessageBox.Show("New Deal Not Allowed. Please complete current hand.", "DEAL NOT ALLOWED", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // clear the board
            icPlayerImageItems.ItemsSource = string.Empty; // clear the items
            icDealerImageItems.ItemsSource = string.Empty; // clear the items

            txtDealerCards.Text = string.Empty;
            txtPlayerCards.Text = string.Empty;

            // deal the initial hand
            theGame.DealTheCards();

            txtPlayerCards.Visibility = Visibility.Hidden;
            txtPlayerCards.Text = theGame.DisplayCardsToUser(theGame.playersHand);
            txtDealerCards.Visibility = Visibility.Hidden;
            txtDealerCards.Text = theGame.DisplayCardsToUser(theGame.dealerHand);

            txtPlayerScore.Text = theGame.DisplayScoresToUser(theGame.playersHand);
            txtDealerScore.Visibility = Visibility.Hidden;
            txtDealerScore.Text = theGame.DisplayScoresToUser(theGame.dealerHand);

            icPlayerImageItems.ItemsSource = theGame.BuildListOfImagesForItemsControl(theGame.playersHand, false);
          
            icDealerImageItems.ItemsSource = theGame.BuildListOfImagesForItemsControl(theGame.dealerHand, true  );
           
            txtFinalResults.Visibility = Visibility.Hidden;
            txtFinalResults.Text = theGame.CheckScores();
            if (!string.IsNullOrEmpty(txtFinalResults.Text))
            {
                txtFinalResults.Visibility = Visibility.Visible;
                MessageBox.Show(txtFinalResults.Text, "HAND COMPLETED", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnHit_Click_1(object sender, RoutedEventArgs e)
        {
            if ( !theGame.startupCompleted )
            {
                MessageBox.Show("Please Select NEW to create a NEW game, then select DEAL", "HIT NOT ALLOWED", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if ( theGame.dealAllowed)
            {
                MessageBox.Show("Please Select DEAL", "HIT NOT ALLOWED", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if ( theGame.handCompleted)
            {
                MessageBox.Show("Previous Hand was completed.  Please Select DEAL", "HIT NOT ALLOWED", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }

            theGame.GetNextCard(ref theGame.playersHand);  // get the next card for the player


            icPlayerImageItems.ItemsSource = theGame.BuildListOfImagesForItemsControl(theGame.playersHand, false);
            txtPlayerCards.Text = theGame.DisplayCardsToUser(theGame.playersHand);
            txtPlayerScore.Text = theGame.DisplayScoresToUser(theGame.playersHand);


            txtFinalResults.Visibility = Visibility.Hidden;
            txtFinalResults.Text = theGame.CheckScores();
            if (!string.IsNullOrEmpty(txtFinalResults.Text))
            {
                txtFinalResults.Visibility = Visibility.Visible;
                MessageBox.Show(txtFinalResults.Text, "HAND COMPLETED", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // initial
        }

        private void btnDisplayStatistics_Click(object sender, RoutedEventArgs e)
        {
            string playerStats = string.Format("Player Stats:  HandsPlayed: {0} HandsWon: {1} StartingBank: {2} CurrentBank: {3} ",
                theGame.thePlayer.handsPlayed,
                theGame.thePlayer.handsWon,
                theGame.thePlayer.startingBank,
                theGame.thePlayer.bank);

            string dealerStats = string.Format("Dealer Stats:  HandsPlayed: {0} HandsWon: {1} StartingBank: {2} CurrentBank: {3} ",
               theGame.theDealer.handsPlayed,
               theGame.theDealer.handsWon,
               theGame.theDealer.startingBank,
               theGame.theDealer.bank);

            MessageBox.Show(playerStats + System.Environment.NewLine + dealerStats, "Stats", MessageBoxButton.OK, MessageBoxImage.Information);

        }
    }
}
