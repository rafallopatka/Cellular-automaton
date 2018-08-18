using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.ComponentModel;
using System.Threading;
using System.IO;
using Microsoft.Win32;

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Border[,] gameBoardElements;
        private Board board;
        private Automat automat;
        private BackgroundWorker bWorker;
        private string methodName;

        double speed = 0;

        #region initialization
        public MainWindow()
        {
            InitializeComponent();

            bWorker = new BackgroundWorker();
            bWorker.WorkerReportsProgress = true;
            bWorker.WorkerSupportsCancellation = true;
            bWorker.DoWork += new DoWorkEventHandler(bWorker_DoWork);
            bWorker.ProgressChanged += new ProgressChangedEventHandler(bWorker_ProgressChanged);
            bWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bWorker_RunWorkerCompleted);

            PrepareBoard();
            RegisterAlgorithms();
        }

        void RegisterAlgorithms()
        {
            foreach (var item in automat.MethodList)
            {
                cmbAlgorithms.Items.Add(item);
            }

            cmbAlgorithms.SelectedIndex = 0;
        }

        private void PrepareBoard()
        {
            automat = new Automat();
            board = new Board();
            gameBoardElements = new Border[50, 50];

            for (int y = 0; y < gameBoardElements.GetLength(0); y++)
            {
                for (int x = 0; x < gameBoardElements.GetLength(1); x++)
                {
                    gameBoardElements[y, x] = new Border()
                    {
                        Background = Brushes.Lavender,
                        BorderBrush = Brushes.DarkGray,
                        BorderThickness = new Thickness(0.5)
                    };

                    gameBoard.Children.Add(gameBoardElements[y, x]);
                    gameBoardElements[y, x].SetValue(Grid.RowProperty, y);
                    gameBoardElements[y, x].SetValue(Grid.ColumnProperty, x);
                    gameBoardElements[y, x].MouseLeftButtonDown += new MouseButtonEventHandler(MainWindow_MouseLeftButtonDown);
                    gameBoardElements[y, x].MouseRightButtonDown += new MouseButtonEventHandler(MainWindow_MouseRightButtonDown);
                    gameBoardElements[y, x].MouseEnter += new MouseEventHandler(MainWindow_MouseEnter);
                    gameBoardElements[y, x].MouseLeave += new MouseEventHandler(MainWindow_MouseLeave);
                }
            }
        }

        private void LoadDescriptionDocument(string method)
        {   
            TextRange textRange;
            FileStream fileStream;
            string filename = string.Format("documents/{0}.rtf", method);
            if (System.IO.File.Exists(filename))
            {
                textRange = new TextRange(rtbDescription.Document.ContentStart, rtbDescription.Document.ContentEnd);
                using (fileStream = new System.IO.FileStream(filename, System.IO.FileMode.OpenOrCreate))
                {
                    textRange.Load(fileStream, System.Windows.DataFormats.Rtf);
                }
            }   
        }

        private void LoadMap(string filename)
        {
            if (board.Load(filename) == false)
            {
                MessageBox.Show("Nie udało się załadować pliku.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            for (int y = 0; y < 50; y++)
            {
                for (int x = 0; x < 50; x++)
                {
                    var list = automat.GetStateList(cmbAlgorithms.SelectedValue.ToString());
                    State? item = list.Find(n => n.Value == board[y, x]);

                    if (item == null)
                        gameBoardElements[y, x].Background = Brushes.LightGray;
                    else
                        gameBoardElements[y, x].Background = item.Value.Color;
                }
            }
            return;
        }
        #endregion

        #region execution
        void bWorker_DoWork(object sender, DoWorkEventArgs e)
        {            
            var worker = (sender as BackgroundWorker);
            while (worker.CancellationPending == false)
            {
                if (automat.Execute(methodName, ref board) == false)
                    worker.CancelAsync();

                (sender as BackgroundWorker).ReportProgress(1, board);
                Thread.Sleep((int)(speed * 1000));
            }
        }

        void bWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Board board = (e.UserState as Board);
            for (int y = 0; y < 50; y++)
            {
                for (int x = 0; x < 50; x++)
                {
                    var list = automat.GetStateList(cmbAlgorithms.SelectedValue.ToString());
                    State? item = list.Find(n => n.Value == board[y, x]);

                    if (item == null)
                        gameBoardElements[y, x].Background = Brushes.LightGray;
                    else
                        gameBoardElements[y, x].Background = item.Value.Color;
                }
            }
        }

        void bWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnClear.IsEnabled = true;
            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;

            btnSample.IsEnabled = true;
            btnSave.IsEnabled = true;
            btnOpen.IsEnabled = true;
        }    
        #endregion

        #region interaction
        void MainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as Border);
            item.Background = ((State)lbxStates.SelectedValue).Color;

            int y = Grid.GetRow(item);
            int x = Grid.GetColumn(item);

            board.SetCell(x, y, ((State)lbxStates.SelectedValue).Value);
        }

        void MainWindow_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as Border);
            item.Background = Brushes.Lavender;

            int y = Grid.GetRow(item);
            int x = Grid.GetColumn(item);

            board.SetCell(x, y, 0); 
        }

        void MainWindow_MouseEnter(object sender, MouseEventArgs e)
        {
            var item = (sender as Border);
            item.BorderBrush = Brushes.Black;
            item.BorderThickness = new Thickness(1);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                item.Background = ((State)lbxStates.SelectedValue).Color;

                int y = Grid.GetRow(item);
                int x = Grid.GetColumn(item);

                board.SetCell(x, y, ((State)lbxStates.SelectedValue).Value);
            }
        }

        void MainWindow_MouseLeave(object sender, MouseEventArgs e)
        {
            var item = (sender as Border);
            item.BorderBrush = Brushes.DarkGray;
            item.BorderThickness = new Thickness(0.5);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            btnClear.IsEnabled = false;
            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;
            btnSample.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnOpen.IsEnabled = false;

            bWorker.RunWorkerAsync();
        }
        
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            board.Clear();
            foreach (var item in gameBoardElements)
                item.Background = Brushes.Lavender;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            bWorker.CancelAsync();
        }

        private void cmbAlgorithms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            methodName = cmbAlgorithms.SelectedValue.ToString();
            this.LoadDescriptionDocument(methodName);

            this.lbxStates.Items.Clear();

            foreach (var item in automat.GetStateList(methodName))
            {
                this.lbxStates.Items.Add(item);
            }

            this.lbxStates.SelectedIndex = 1;
        }
        
        private void sldSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.speed = sldSpeed.Value;
        }
      
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Zapisz plik";
            sfd.FileName = "Document";
            sfd.DefaultExt = ".am";
            sfd.Filter = "map file (.am)|*.am";

            bool? result = sfd.ShowDialog();
            if (result == true)
            {
                if (board.Save(sfd.FileName) == false)
                    MessageBox.Show("Zapisywanie zakończone niepowodzeniem", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog sfd = new OpenFileDialog();
            sfd.Title = "Załaduj plik";
            sfd.DefaultExt = ".am";
            sfd.Filter = "map file (.am)|*.am";

            bool? result = sfd.ShowDialog();
            if (result == true)
            {
                LoadMap(sfd.FileName);
                return;
            }
        }

        private void btnSample_Click(object sender, RoutedEventArgs e)
        {
            string methodName = cmbAlgorithms.SelectedValue.ToString();
            LoadMap(string.Format("samples/{0}.am", methodName));
        }
        #endregion 





    }
}
