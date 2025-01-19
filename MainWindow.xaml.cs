using System.Windows;
using System.Windows.Controls;

namespace BingoGame;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private List<int> bingoNumbers;
    private Random random;
    private const int NUMBER = 35;
    
    public MainWindow()
    {
        InitializeComponent();
        
        // fill 0 - 34 to list - bingo item lists
        IEnumerable<int> range = Enumerable.Range(0, NUMBER);
        bingoNumbers = range.ToList();
        
        // Init random class
        random = new Random();
    }

    private void bingo_turn(object sender, RoutedEventArgs e)
    {
        
        if (bingoNumbers.Count < 2)
        {
            // less than 2, quit
            // avoid out of range
            MessageBox.Show("No Enough Value!");
            return;
        }
        
        // 1. generate random number - index
        int currentIndex = random.Next(0, bingoNumbers.Count);
        // 2. get the actual value
        int currentValue = bingoNumbers[currentIndex];
        
        // 3. display current Bingo item
        /* display the image displaying current item
         */

        BingoDialog bingoDialog = new BingoDialog(currentValue);
        
        System.Media.SystemSounds.Hand.Play();
        bingoDialog.ShowDialog();
        
        /* add stroke/ mask on image
         * set the opacity to semi-transparent to identify be used
         */
        String canvasName = $"Pic{currentValue}";
        Canvas canvas = (Canvas) this.FindName(canvasName);

        if (canvas != null)
        {
            // set the opacity mask to identify the selected item
            canvas.Opacity = 0.2;
        }
        
        // 4. delete the number in Array
        bingoNumbers.RemoveAt(currentIndex);
        
        // display item index and remain size for debug - Debug only
        // MessageBox.Show($"Now we are at {currentValue}\nand index is {currentIndex}\nand we have {bingoNumbers.Count} left");
        
    }

    ~MainWindow()
    {
    }
}
