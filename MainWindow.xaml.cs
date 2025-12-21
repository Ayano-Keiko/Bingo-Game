using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace BingoGame;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private List<String> fileList;  // To store all file names
    private String[] files;
    private Random random;  // to generate random number
    private System.Windows.Controls.Grid iconArea;  // the container for M * N grid area

    public MainWindow()
    {
        InitializeComponent();

        // set ICON
        System.Windows.Media.Imaging.BitmapImage iconImg = new System.Windows.Media.Imaging.BitmapImage(new System.Uri("resources/Image/icon.ico", System.UriKind.Relative));
        this.Icon = iconImg;

        // associated with xaml
        iconArea = (System.Windows.Controls.Grid) this.FindName("IconArea");
        // System.Diagnostics.Trace.WriteLine($"width: {iconArea.Width} height: {iconArea.Height}");

        // fill 0 - 34 to list - bingo item lists -- get all image files name from resources/Image/Bingo
        files = Directory.GetFiles("resources/Image/Bingo");
        // get the grid size (row, col)
        (int row, int col) gridSize = GetDivisor.getDivisor(files.Length);
        // System.Diagnostics.Trace.WriteLine($"size: [ {gridSize.row}, {gridSize.col} ]");
        System.Windows.Thickness thickness = new System.Windows.Thickness(20);

        for (int r = 0; r < gridSize.row; r++)
        {
            System.Windows.Controls.RowDefinition rowDef = new System.Windows.Controls.RowDefinition();
            iconArea.RowDefinitions.Add(rowDef);
        }

        for (int c = 0; c < gridSize.col; c++)
        {
            System.Windows.Controls.ColumnDefinition colDef = new System.Windows.Controls.ColumnDefinition();
            iconArea.ColumnDefinitions.Add(colDef);
        }

        for (int r = 0; r < gridSize.row; r ++)
        {
            for (int c = 0; c < gridSize.col; c ++)
            {
                int indice = r * gridSize.col + c;
                
                // mtach the number in file name
                String pattern = @"(\d+)";
                MatchCollection matches;
                Regex selectNumber = new Regex(pattern, RegexOptions.None);
                matches = selectNumber.Matches(files[indice]);

                // load image
                System.Uri imgPath = new System.Uri(files[indice], System.UriKind.RelativeOrAbsolute);
                System.Windows.Media.Imaging.BitmapImage img = new System.Windows.Media.Imaging.BitmapImage(imgPath);

                System.Windows.Controls.Canvas canvas = new System.Windows.Controls.Canvas();
                canvas.Width = iconArea.Width / gridSize.col;
                canvas.Height = iconArea.Height / gridSize.row;
                canvas.Margin = thickness;
                canvas.Name = $"Pic{matches[0]}";
                this.RegisterName($"Pic{matches[0]}", canvas);  // register the name in XAML
                canvas.Background = new System.Windows.Media.ImageBrush(img);
                canvas.Opacity = 1.0;

                Grid.SetRow(canvas, r);
                Grid.SetColumn(canvas, c);
                iconArea.Children.Add(canvas);

            }
        }

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
            
            canvas.Opacity = 0.4;
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