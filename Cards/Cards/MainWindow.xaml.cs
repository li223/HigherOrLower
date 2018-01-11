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
using Cards.Objects;
using System.IO;
using System.Windows.Threading;

namespace Cards
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int Score = 0;
        Random rand = new Random();
        List<Card> CardDeck { get; set; }
        Card NextCard { get; set; }
        Card CurrentCard { get; set; }
        DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            if (NextCard.CardValue > CurrentCard.CardValue)
            {
                if (HigherButton.IsChecked.Value)
                {
                    MessageBox.Show("Correct");
                    Score += 1;
                }
            }
            else if (LowerButton.IsChecked.Value)
            {
                MessageBox.Show("Correct");
                Score += 1;
            }
            else
                MessageBox.Show("Incorrect");
            timer.Start();
            await LoadNextCard();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CardDeck = new CardDeck().GenerateDeck();
            NextCard = CardDeck[rand.Next(0, CardDeck.Count)];
            CurrentCard = CardDeck[rand.Next(0, CardDeck.Count)];
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Cards", $"{CurrentCard.CardID}.png");
            CardHolder.Source = new BitmapImage(new Uri(path));
        }
        public async Task LoadNextCard()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Cards", $"{NextCard.CardID}.png");
            CurrentCard = NextCard;
            await Task.Delay(4);
            CardHolder.Source = new BitmapImage(new Uri(path));
            NextCard = CardDeck[rand.Next(0, CardDeck.Count)];
        }
    }
}
