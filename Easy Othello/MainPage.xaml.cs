using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Easy_Othello
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private Playing playing = new Playing();
        private bool IsCurrentBlack = true;

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await Task.Delay(1000);
            ClearGame();
        }

        private void AddBlack(int index)
        {
            Image image = new Image();
            BitmapImage bm = new BitmapImage();
            bm.UriSource = new Uri("ms-appx:///Images/black.png");
            image.Source = bm;
            image.VerticalAlignment = VerticalAlignment.Stretch;
            image.HorizontalAlignment = HorizontalAlignment.Stretch;
            (board_grid.Children.ElementAt(index) as Grid).Children.Add(image);
        }

        private void AddWhite(int index)
        {
            Image image = new Image();
            BitmapImage bm = new BitmapImage();
            bm.UriSource = new Uri("ms-appx:///Images/white.png");
            image.Source = bm;
            image.VerticalAlignment = VerticalAlignment.Stretch;
            image.HorizontalAlignment = HorizontalAlignment.Stretch;
            (board_grid.Children.ElementAt(index) as Grid).Children.Add(image);
        }

        private void ClearGame()
        {
            playing.ChessBoard = new int[8, 8]{
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,1,-1,0,0,0},
            {0,0,0,-1,1,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0} };
            foreach (Grid grid in board_grid.Children)
            {
                grid.Children.Clear();
            }
            AddBlack(27);
            AddWhite(28);
            AddWhite(35);
            AddBlack(36);

            playing.blackCount = 2;
            playing.whiteCount = 2;
            black_count.Text = "2";
            white_count.Text = "2";
            IsCurrentBlack = true;
        }

        private async void Play(int row, int col)
        {
            if (IsCurrentBlack)
            {
                if (!playing.canSetChess(row, col, 1))
                    return;
                playing.reverse(row, col, 1);
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (playing.ChessBoard[i, j] == 1)
                            AddBlack(i * 8 + j);
                        else if (playing.ChessBoard[i, j] == -1)
                            AddWhite(i * 8 + j);
                    }
                }

                playing.blackCount = playing.countBlack();
                playing.whiteCount = playing.countWhite();
                black_count.Text = playing.blackCount.ToString();
                white_count.Text = playing.whiteCount.ToString();

                bool CanSetBlack = playing.canSetChess(1);
                bool CanSetWhite = playing.canSetChess(-1);
                if (CanSetWhite)
                    IsCurrentBlack = !IsCurrentBlack;
                else
                {
                    if (CanSetBlack)
                        await new MessageDialog("Nowhere to situate White").ShowAsync();
                    else
                    {
                        if (playing.blackCount > playing.whiteCount)
                            await new MessageDialog("Black Win").ShowAsync();
                        else if (playing.blackCount < playing.whiteCount)
                            await new MessageDialog("White Win").ShowAsync();
                        else
                            await new MessageDialog("Draw").ShowAsync();
                        ClearGame();
                    }
                }
            }
            else
            {
                if (!playing.canSetChess(row, col, -1))
                    return;
                playing.reverse(row, col, -1);
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (playing.ChessBoard[i, j] == 1)
                            AddBlack(i * 8 + j);
                        else if (playing.ChessBoard[i, j] == -1)
                            AddWhite(i * 8 + j);
                    }
                }

                playing.blackCount = playing.countBlack();
                playing.whiteCount = playing.countWhite();
                black_count.Text = playing.blackCount.ToString();
                white_count.Text = playing.whiteCount.ToString();

                bool CanSetBlack = playing.canSetChess(1);
                bool CanSetWhite = playing.canSetChess(-1);
                if (CanSetBlack)
                    IsCurrentBlack = !IsCurrentBlack;
                else
                {
                    if (CanSetWhite)
                        await new MessageDialog("Nowhere to situate Black").ShowAsync();
                    else
                    {
                        if (playing.blackCount > playing.whiteCount)
                            await new MessageDialog("Black Win").ShowAsync();
                        else if (playing.blackCount < playing.whiteCount)
                            await new MessageDialog("White Win").ShowAsync();
                        else
                            await new MessageDialog("Draw").ShowAsync();
                        ClearGame();
                    }
                }
            }
        }

        private void board_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            Image image = sender as Image;
            Point point = e.GetCurrentPoint(image).Position;
            int row, col;
            row = (int)Math.Floor(point.Y * 8 / image.ActualHeight);
            col = (int)Math.Floor(point.X * 8 / image.ActualWidth);
            Play(row, col);
        }

        private void Reset_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ClearGame();
        }
    }
}
