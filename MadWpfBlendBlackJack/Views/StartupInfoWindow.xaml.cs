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
using System.Windows.Shapes;
using MadWpfBlendBlackJack.Controllers;

namespace MadWpfBlendBlackJack.Views
{
    /// <summary>
    /// Interaction logic for StartupInfoWindow.xaml
    /// </summary>
    public partial class StartupInfoWindow : Window
    {

        private GameController _theGame;


        public StartupInfoWindow(GameController theGame)
        {
            InitializeComponent();
            _theGame = theGame;
        }

        private void txtPlayerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            // update the name
            _theGame.thePlayer.name = txtPlayerName.Text;
            //((MainWindow)Application.Current.MainWindow).txtPlayerCards.Text = txtPlayerName.Text;
        }
        
        // when done
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _theGame.playersSelected = true;
            _theGame.startupCompleted = true;
            _theGame.theDealer.name = "the Amazing Dealerino";
            _theGame.theNumDecks = Convert.ToInt32(txtNumDecks.Text);
            _theGame.thePlayer.name = txtPlayerName.Text;
 
            ((MainWindow)Application.Current.MainWindow).txtDealerScore.Visibility = Visibility.Hidden;
            ((MainWindow)Application.Current.MainWindow).lblPlayerName.Visibility = Visibility.Visible;
            ((MainWindow)Application.Current.MainWindow).lblDealerNameVal.Visibility = Visibility.Visible;
            ((MainWindow)Application.Current.MainWindow).lblNumDecksInShoe.Visibility = Visibility.Visible;
            
            // get startup info  
            _theGame.GetStartupInfo();

            ((MainWindow)Application.Current.MainWindow).lblNumDecksInShoe.Content = "Decks In Shoe: " + _theGame.theNumDecks.ToString();
            ((MainWindow)Application.Current.MainWindow).lblNumCardsRemainingInShoe.Content = "Cards Left: ";
            ((MainWindow)Application.Current.MainWindow).lblNumCardsRemainingInShoe.Visibility = Visibility.Visible;

            ((MainWindow)Application.Current.MainWindow).lblPlayersBankRemaining.Content = "Bank: $" + _theGame.thePlayer.bank;
            ((MainWindow)Application.Current.MainWindow).lblPlayerBetAmount.Content = "Bet: $" + _theGame.thePlayer.bet;

            // exit
            this.Close();
        }

        private void TxtPlayerName_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (_theGame != null)
            {
                _theGame.theDealer.name = "the Amazing Dealerino";
                _theGame.thePlayer.name = txtPlayerName.Text;

                // check for exception
                //((MainWindow)Application.Current.MainWindow).txtPlayerName.Text = txtPlayerName.Text;

                ((MainWindow)Application.Current.MainWindow).lblPlayerName.Content = _theGame.thePlayer.name;
                ((MainWindow)Application.Current.MainWindow).lblDealerNameVal.Content = _theGame.theDealer.name;
            }
        }
    }
}
