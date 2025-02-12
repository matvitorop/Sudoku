﻿using Rating;
using Solver;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

namespace MySudoku
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public readonly Dictionary<string, SudokuLvl> Levels = new Dictionary<string, SudokuLvl>()
            {
                { "Easy", SudokuLvl.Easy },
                { "Normal", SudokuLvl.Normal },
                { "Hard", SudokuLvl.Hard }
            };
        public UserWindow()
        {
            InitializeComponent();
            lvls.ItemsSource = Levels.Keys;
            lvls.SelectedIndex = 0;
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            if (TryGetSelectedLevel(out SudokuLvl level))
            {
                var game = new Gamexaml(level);
                game.Show();
                Hide();
            }
        }

        private bool TryGetSelectedLevel(out SudokuLvl level)
        {
            level = SudokuLvl.Easy;
            if (lvls.SelectedValue is string choosedlvl && Levels.TryGetValue(choosedlvl, out SudokuLvl lvl))
            {
                level = lvl;
                return true;
            }
            return false;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void lvls_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TryGetSelectedLevel(out SudokuLvl level))
                rating.ItemsSource = new RatingGenerator().GenerateDataTable((string)lvls.SelectedValue).DefaultView;
        }

    }
}
