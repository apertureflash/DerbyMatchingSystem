using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

using CockDerbyMatching_BL;
using CockDerbyMatching_DAL;

namespace CockDerbyMatchingSystem.UserControls
{
    /// <summary>
    /// Interaction logic for DerbyEntries.xaml
    /// </summary>
    public partial class DerbyEntries : UserControl
    {
        //private Int32 _derbyID = 0;
        private Entry _currentEntry;
        private ObservableCollection<Entry> _entries;
        private Int32 _selectedIndex = 0;

        public DerbyEntries()
        {
            InitializeComponent();

            LoadEntries();
        }

        private void dgDerbyEntries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Entry dbEntry;
            DataGrid grid = sender as DataGrid;

            // pass the customer ID to our Orders datasource via the ObjectDataProvider
            ObjectDataProvider entryRoosters = this.FindResource("EntryRoosters") as ObjectDataProvider;
            ObjectDataProvider entryNoFight = this.FindResource("NoFightEntries") as ObjectDataProvider;

            if (grid.SelectedIndex >= 0)
            {
                _selectedIndex = grid.SelectedIndex;
                _currentEntry = (Entry)grid.SelectedValue;
                entryRoosters.MethodParameters[0] = _currentEntry.EntryID;
                entryNoFight.MethodParameters[0] = _currentEntry.EntryID;
            }
            else
            {
                entryRoosters.MethodParameters[0] = 0;
                entryNoFight.MethodParameters[0] = 0;
            }
        }

        #region Public Properties
        public static readonly DependencyProperty DerbyIDProperty = DependencyProperty.Register("DerbyID",
                                                                                                typeof(Int32),                                                                                                typeof(DerbyEntries));
        public Int32 DerbyID
        {
            set
            {
                //_derbyID = value;
                SetValue(DerbyIDProperty, value);
            }
        }

        #endregion

        private void btnEditEntry_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();

            if (dgDerbyEntries.SelectedIndex >= 0)
            {

                DerbyEntry editEntry = new DerbyEntry();

                editEntry.EntryID = _currentEntry.EntryID;

                Window window = new Window
                {
                    Width = 440,
                    Height = 480,
                    Background = (Brush)bc.ConvertFrom("#FFB3BABA"),
                    Title = "Derby Entry",
                    Content = editEntry,
                    HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                    WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                    WindowStyle = System.Windows.WindowStyle.ToolWindow,
                    ShowInTaskbar = false
                };

                window.ShowDialog();

                LoadEntries();
                ReloadRoosters();
            }

        }

        private void btnNewEntry_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();

            DerbyEntry editEntry = new DerbyEntry();

            editEntry.EntryID = 0;

            Window window = new Window
            {
                Width = 440,
                Height = 480,
                Background = (Brush)bc.ConvertFrom("#FFB3BABA"),
                Title = "Derby Entry",
                Content = editEntry,
                HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                WindowStyle = System.Windows.WindowStyle.ToolWindow,
                ShowInTaskbar = true
            };

            window.ShowDialog();

            LoadEntries();
            ReloadRoosters();
        }

        private void ReloadRoosters()
        {
            //DataGrid grid = sender as DataGrid;
            Entry dbEntry;

            // pass the customer ID to our Orders datasource via the ObjectDataProvider
            ObjectDataProvider entryRoosters = this.FindResource("EntryRoosters") as ObjectDataProvider;

            //.SelectedValue.GetType().ToString() == "CockDerbyMatching_DAL.Entry"
            if (this.dgDerbyEntries.SelectedIndex >= 0)
            {
                dbEntry = (Entry)this.dgDerbyEntries.SelectedValue;
                entryRoosters.MethodParameters[0] = dbEntry.EntryID;
            }
            else
            {
                entryRoosters.MethodParameters[0] = 0;
            }
        }

        private void LoadEntries()
        {
            Entries derbyEntries = new Entries();

            _entries = derbyEntries.GetDerbyEntries(1);

            this.dgDerbyEntries.ItemsSource = _entries;
            this.dgDerbyEntries.SelectedIndex = _selectedIndex;
        }

        private void ReloadNoFights()
        {
            Entry dbEntry;

            ObjectDataProvider entryNoFight = this.FindResource("NoFightEntries") as ObjectDataProvider;

            if (this.dgDerbyEntries.SelectedIndex >= 0)
            {
                dbEntry = (Entry)this.dgDerbyEntries.SelectedValue;
                entryNoFight.MethodParameters[0] = _currentEntry.EntryID;
            }
            else
            {
                entryNoFight.MethodParameters[0] = 0;
            }
        }

        private void btnDeleteEntry_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Delete this Derby Entry?", "Delete Derby Entry", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Entries ent = new Entries();

                    ent.DeleteEntry(_currentEntry.EntryID);

                    if (_selectedIndex != 0)
                    {
                        _selectedIndex = _selectedIndex - 1;
                    }
                    LoadEntries();
                    ReloadRoosters();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNoFight_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();

            EntryNoFights entryNF = new EntryNoFights();

            entryNF.EntryID = _currentEntry.EntryID;

            Window window = new Window
            {
                Width = 750,
                Height = 430,
                Background = (Brush)bc.ConvertFrom("#FFB3BABA"),
                Title = "Derby Entry",
                Content = entryNF,
                HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                WindowStyle = System.Windows.WindowStyle.ToolWindow,
                ShowInTaskbar = true
            };

            window.ShowDialog();

            ReloadNoFights();
        }
    }
}
