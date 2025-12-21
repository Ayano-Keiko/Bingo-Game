using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BingoGame;

public partial class BingoDialog : Window
{
    private System.Windows.Controls.Canvas? bingoImage;
    public BingoDialog(String imgName)
    {
        InitializeComponent();

        // set ICON
        System.Windows.Media.Imaging.BitmapImage iconImg = new System.Windows.Media.Imaging.BitmapImage(new System.Uri("resources/Image/icon.ico", System.UriKind.Relative));
        this.Icon = iconImg;

        bingoImage = (System.Windows.Controls.Canvas) FindName("BingoImage");
        
        if (bingoImage != null)
        {
            // Set the Image Control
            BitmapImage bitmapImage = new BitmapImage(new Uri(imgName, UriKind.RelativeOrAbsolute));
           
            // bingoImage.Stretch = Stretch.Fill;
            bingoImage.Background = new System.Windows.Media.ImageBrush( bitmapImage );
        }
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}