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
    /// Interaction logic for HandPickedFights.xaml
    /// </summary>
    public partial class HandPickedFights : UserControl
    {
        ObservableCollection<HandPickedMatch> _handPickedMatches; // = new ObservableCollection<HandPickedMatch>();

        public HandPickedFights()
        {
            InitializeComponent();

            LoadHandPickedFights();

        }

        private void LoadHandPickedFights()
        {
            Derbies derbiesDA = new Derbies();

            _handPickedMatches = derbiesDA.GetHandPickedMatches(); 
            this.dgHandPicked.ItemsSource = _handPickedMatches; 

        }

        private void dgHandPicked_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                DeleteHandPickedMatch(); //((DataGrid)sender).CurrentItem);
            }
        }

        private void DeleteHandPickedMatch()
        {
            if (dgHandPicked.SelectedIndex >= 0)
            {
                if (MessageBox.Show("Delete the selected matching?",
                                    "Delete Match", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    HandPickedMatch dHandPicked = (HandPickedMatch)dgHandPicked.SelectedItem;
                    _handPickedMatches.Remove(dHandPicked);

                    Derbies derbiesDA = new Derbies();
                    derbiesDA.DeleteHandPickedMatch(dHandPicked.MatchUpID);
                }

            }
        }

        private void dgDerbyEntries1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Entry dbEntry;
            DataGrid grid = sender as DataGrid;

            // pass the customer ID to our Orders datasource via the ObjectDataProvider
            ObjectDataProvider entryRoosters = this.FindResource("EntryRoosters1") as ObjectDataProvider;

            //grid.SelectedValue.GetType().ToString() == "CockDerbyMatching_DAL.Entry"
            if (grid.SelectedIndex >= 0)
            {
                dbEntry = (Entry)grid.SelectedValue;
                entryRoosters.MethodParameters[0] = dbEntry.EntryID;
            }
            else
            {
                entryRoosters.MethodParameters[0] = 0;
            }
        }

        private void dgDerbyEntries2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Entry dbEntry;
            DataGrid grid = sender as DataGrid;

            // pass the customer ID to our Orders datasource via the ObjectDataProvider
            ObjectDataProvider entryRoosters = this.FindResource("EntryRoosters2") as ObjectDataProvider;

            if (grid.SelectedIndex >= 0)
            {
                dbEntry = (Entry)grid.SelectedValue;
                entryRoosters.MethodParameters[0] = dbEntry.EntryID;
            }
            else
            {
                entryRoosters.MethodParameters[0] = 0;
            }
        }

        private void btnAddMatch_Click(object sender, RoutedEventArgs e)
        {
            if ((dgDerbyEntries1.SelectedIndex < 0) || (dgDerbyEntries1.SelectedIndex < 0) || 
                (dgEntryRoosters1.SelectedIndex < 0) || (dgEntryRoosters2.SelectedIndex < 0))
            {
                MessageBox.Show("Please select the matching pair");
            }
            else
            {
                if (MessageBox.Show("Add the selected match?",
                                    "Add Match", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        HandPickedMatch newHandPickedMatch = new HandPickedMatch();

                        newHandPickedMatch.DerbyID = 1;

                        Entry selectedEntry = (Entry)dgDerbyEntries1.SelectedItem;

                        newHandPickedMatch.Rooster1EntryNumber = selectedEntry.EntryNumber;
                        newHandPickedMatch.Rooster1Entry = selectedEntry.EntryName;


                        selectedEntry = (Entry)dgDerbyEntries2.SelectedItem;

                        newHandPickedMatch.Rooster2EntryNumber = selectedEntry.EntryNumber;
                        newHandPickedMatch.Rooster2Entry = selectedEntry.EntryName;


                        Rooster selectedRooster = (Rooster)dgEntryRoosters1.SelectedItem;

                        newHandPickedMatch.Rooster1ID = selectedRooster.RoosterID;
                        newHandPickedMatch.Rooster1LB = selectedRooster.RoosterLegBandNumber;
                        newHandPickedMatch.Rooster1Weight = selectedRooster.RoosterWeight;


                        selectedRooster = (Rooster)dgEntryRoosters2.SelectedItem;

                        newHandPickedMatch.Rooster2ID = selectedRooster.RoosterID;
                        newHandPickedMatch.Rooster2LB = selectedRooster.RoosterLegBandNumber;
                        newHandPickedMatch.Rooster2Weight = selectedRooster.RoosterWeight;
                        newHandPickedMatch.DateCreated = DateTime.Now;
                        newHandPickedMatch.CreateBy = "Administrator";
                        newHandPickedMatch.LastUpdated = null;

                        Derbies derbiesDA = new Derbies();

                        if (_handPickedMatches == null)
                        {
                            _handPickedMatches = new ObservableCollection<HandPickedMatch>();
                        }

                        derbiesDA.AddHandPickedMatch(newHandPickedMatch);

                        _handPickedMatches.Add(newHandPickedMatch);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            }
        }

        private void btnDeleteHandPicked_Click(object sender, RoutedEventArgs e)
        {
            if (dgHandPicked.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a handpicked match to delete");
            }
            else
            {
                if (MessageBox.Show("Delete handpicked match?",
                                    "Delete Match", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Derbies derbiesDA = new Derbies();

                        HandPickedMatch hpmMatch = (HandPickedMatch)dgHandPicked.SelectedItem;

                        derbiesDA.DeleteHandPickedMatch(hpmMatch.MatchUpID);

                        _handPickedMatches.Remove(hpmMatch);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }

        }

    }
}
