using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BingoGame;

public partial class BingoDialog : Window
{
    private Image? bingoImage;
    public BingoDialog(String imgName)
    {
        InitializeComponent();

        bingoImage = (Image) FindName("BingoImage");

        if (bingoImage != null)
        {
            // Set the Image Control
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(imgName, UriKind.Relative);
            bitmapImage.EndInit();
            bingoImage.Stretch = Stretch.Fill;
            bingoImage.Source = bitmapImage;
        }
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}