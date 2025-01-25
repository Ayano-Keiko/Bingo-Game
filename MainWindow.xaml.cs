using System.IO;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace BingoGame;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private List<String> fileList;  // To store all file names
    private Random random;  // to generate random number
    
    public MainWindow()
    {
        InitializeComponent();
        
        // fill 0 - 34 to list - bingo item lists -- get all image files name from resources/Image/Bingo
        String[] files = Directory.GetFiles("resources/Image/Bingo");
        fileList = files.ToList();
        
        // Init random class
        random = new Random();
    }

    private void bingo_turn(object sender, RoutedEventArgs e)
    {
        
        if (fileList.Count < 2)
        {
            // less than 2, quit
            // avoid out of range
            MessageBox.Show("No Enough Value!");
            return;
        }
        
        // 1. generate random number - index
        int currentIndex = random.Next(0, fileList.Count);
        // 2. get the actual value
        String currentValue = fileList[currentIndex];
        
        // 3. display current Bingo item
        /* display the image displaying current item
         */

        BingoDialog bingoDialog = new BingoDialog(currentValue);
        
        System.Media.SystemSounds.Hand.Play();
        bingoDialog.ShowDialog();
        
        /* add stroke/ mask on image
         * set the opacity to semi-transparent to identify be used
         */
        
        // use regular expression to match patern
        String pattern = @"(\d+)";
        MatchCollection matches;
        Regex selectNumber = new Regex(pattern, RegexOptions.None);
        matches = selectNumber.Matches(currentValue);
        String canvasName = $"Pic{matches[0]}";
        
        // Set current canvas to semi-transparent
        Canvas canvas = (Canvas) this.FindName(canvasName);

        if (canvas != null)
        {
            // set the opacity mask to identify the selected item
            canvas.Opacity = 0.6;
        }
        
        // 4. delete the number in Array
        fileList.RemoveAt(currentIndex);
        
        // display item index and remain size for debug - Debug only
        // MessageBox.Show($"Now we are at {currentValue}\nand index is {currentIndex}\nand we have {bingoNumbers.Count} left");
        
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            bingo_turn(null, null);
        }
        else if (e.Key == Key.Escape)
        {
             this.Close();
        }
    }

    ~MainWindow()
    {
    }
}