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
    /// Interaction logic for EntryNoFights.xaml
    /// </summary>
    public partial class EntryNoFights : UserControl
    {
        private ObservableCollection<Entry> _availableFights;
        private ObservableCollection<Entry> _noFights;
        private Int32 _entryID;

        public EntryNoFights()
        {
            InitializeComponent();
        }

        public Int32 EntryID
        {
            set
            {
                _entryID = value;

                LoadAvailableFights();
                LoadNoFight();
            }
        }

        private void LoadAvailableFights()
        {
            Entries ent = new Entries();

            _availableFights = ent.GetAvailableFights(_entryID);

            this.lstEntries.ItemsSource = _availableFights;
        }

        private void LoadNoFight()
        {
            Entries ent = new Entries();

            _noFights = ent.GetNoFightEntries(_entryID);

            this.lstNoFight.ItemsSource = _noFights;
        }

        private void lstNoFight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lstEntries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (this.lstEntries.SelectedIndex >= 0)
            {
                _noFights.Add((Entry)this.lstEntries.SelectedItem);
                _availableFights.Remove((Entry)this.lstEntries.SelectedItem);
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (this.lstNoFight.SelectedIndex >= 0)
            {
                _availableFights.Add((Entry)this.lstNoFight.SelectedItem);
                _noFights.Remove((Entry)this.lstNoFight.SelectedItem);
            }
        }

        private void btnSaveNoFightEntries_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Update the No-Fight Settings?", "No-Fight", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    Entries ent = new Entries();

                    ent.SetNoFightEntries(_noFights, _entryID);

                    Window myParent = (Window)this.Parent;

                    myParent.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
