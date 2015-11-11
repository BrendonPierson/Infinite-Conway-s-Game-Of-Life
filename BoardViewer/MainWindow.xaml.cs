using System.Windows;
using ConwaysGameOfLife;
using System.Windows.Threading;
using System;
using Xceed.Wpf.Toolkit;
using System.Collections.Generic;

namespace BoardViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Board currentBoard;
        private DispatcherTimer dispatcherTimer;

        public MainWindow()
        {
            List<Cell> alive = new List<Cell>();
            // Blinker
            alive.Add(new Cell(0, 0));
            alive.Add(new Cell(0, 1));
            alive.Add(new Cell(0, 2));

            // GLider
            //alive.Add(new Cell(0, 1));
            //alive.Add(new Cell(1, 2));
            //alive.Add(new Cell(2, 0));
            //alive.Add(new Cell(2, 1));
            //alive.Add(new Cell(2, 2));

            //alive.Add(new Cell(2, 3));
            //alive.Add(new Cell(3, 2));
            currentBoard = new GameBoard(alive);
            dispatcherTimer = new DispatcherTimer();

            InitializeComponent();

            TheListView.ItemsSource = currentBoard.ToList();
            dispatcherTimer.Tick += dispatcherTimerClick;
            dispatcherTimer.Interval = TimeSpan.FromSeconds((double)RunSpeed.Value);
        }

        private void dispatcherTimerClick(object sender, EventArgs e)
        {
            InitiateTick();
        }

        private void InitiateTick()
        {
            currentBoard.Tick();
            TheListView.ItemsSource = currentBoard.ToList();
        }

        private void Run_Button_Click(object sender, RoutedEventArgs e)
        {
            InitiateTick(); // To make it clear that clicking the button worked
            dispatcherTimer.Start();
            TickButton.IsEnabled = false;
            RunButton.IsEnabled = false;
            RunSpeed.IsEnabled = true;
            StopButton.IsEnabled = true;
        }

        private void Stop_Button_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            TickButton.IsEnabled = true;
            RunButton.IsEnabled = true;
            RunSpeed.IsEnabled = false;
            StopButton.IsEnabled = false;
        }

        private void Tick_Button_Click(object sender, RoutedEventArgs e)
        {
            InitiateTick();
        }

        private void RunSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            dispatcherTimer.Interval = TimeSpan.FromSeconds((double)e.NewValue);
        }
    }
}
