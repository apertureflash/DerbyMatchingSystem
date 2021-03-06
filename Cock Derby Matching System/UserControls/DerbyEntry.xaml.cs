﻿using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using CockDerbyMatching_BL;
using CockDerbyMatching_DAL;

namespace CockDerbyMatchingSystem.UserControls
{
    /// <summary>
    /// Interaction logic for DerbyEntry.xaml
    /// </summary>
    public partial class DerbyEntry : UserControl
    {
        private Entry _currentEntry;
        private Rooster _currentRooster = new Rooster();
        Boolean _editMode = true;
        Boolean _newRoosters = false;
        Int32 _entryID;

        ObservableCollection<Rooster> _entryRoosters = new ObservableCollection<Rooster>();

        public DerbyEntry()
        {
            InitializeComponent();

            //this.txtLBNumber.DataContext = _currentRooster;
            //this.txtWeight.DataContext = _currentRooster;
        }

        public Int32 EntryID
        {
            set
            {

                //existing entry
                if (value > 0)
                {
                    _entryID = value;

                    var ent = new Entries();

                    _currentEntry = ent.GetEntry(_entryID);
                    
                    _editMode = true;
                }
                ////var maxEntryNumber = new MaxEntryNumber();

                ////if (_currentEntry.EntryID > maxEntryNumber.NewEntryNumber) //Validator for Number of Entries
                ////{
                ////    MessageBox.Show("Max Number of Entries Based from Derby Info Reached");
                ////}

                //new entry
                else
                {
                    NewEntry();
                    
                }

                this.DataContext = _currentEntry;
                LoadRoosters();
            }
        }

        public Entry CurrentEntry
        {
            /*
            set
            {
                if (value == null)
                {
                    _currentEntry = new Entry();

                    MaxEntryNumber newEntryNumber = new MaxEntryNumber();

                    _currentEntry.EntryNumber = newEntryNumber.NewEntryNumber; //newEntryNumber[0].NewEntryNumber;

                    this.DataContext = _currentEntry;
                    _editMode = false;
                }
                else
                {
                    _currentEntry = value;
                    this.DataContext = _currentEntry;
                    LoadRoosters();
                }
             
            }
             */ 
            get
            {
                return _currentEntry;
            }
        }

        public ObservableCollection<Rooster> EntryRoosters
        {
            set
            {
                _entryRoosters = value;

                if (_entryRoosters == null)
                {
                    _entryRoosters = new ObservableCollection<Rooster>();
                }

                this.dgRoosters.ItemsSource = _entryRoosters;
                this.dgRoosters.Focus();

                if (_entryRoosters.Count <= 0)
                {
                    _editMode = false;
                }
                else
                {
                    this.dgRoosters.SelectedIndex = 0;
                }
            }
        }
        
        private void LoadRoosters()
        {
            var entryRoosters = new Roosters();

            _entryRoosters = entryRoosters.GetEntryRoosters(_currentEntry.EntryID);

            if (_entryRoosters == null)
            {
                _entryRoosters = new ObservableCollection<Rooster>();
            }

            this.dgRoosters.ItemsSource = _entryRoosters;
            this.dgRoosters.Focus();

            if (_entryRoosters.Count <= 0)
            {
                _newRoosters = true;
            }
            else
            {
                this.dgRoosters.SelectedIndex = 0;
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            NewRooster();
        }

        private void dgRoosters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var grid = sender as DataGrid;

            if (grid.SelectedIndex >= 0)
            {
                _currentRooster = (Rooster)grid.SelectedValue;
            }

            this.txtLBNumber.Text = _currentRooster.RoosterLegBandNumber;
            this.txtWeight.Text = _currentRooster.RoosterWeight.ToString();

            //set to edit mode
            _newRoosters = false;
        }

        private void btnAddRooster_Click(object sender, RoutedEventArgs e)
        {
            if (_newRoosters)
            {
                #region Add Mode
                try
                {
                    _currentRooster = new Rooster
                    {
                        EntryID = _currentEntry.EntryID,
                        RoosterLegBandNumber = txtLBNumber.Text,
                        RoosterWeight = Decimal.Parse(txtWeight.Text),
                        CreatedBy = "Administrator",
                        RoosterName = _currentEntry.EntryName
                    };

                    _entryRoosters.Add(_currentRooster);
                    
                   // NewRooster();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                #endregion
            }
            else
            {
                #region Edit Mode
                try
                {
                    _currentRooster.RoosterLegBandNumber = this.txtLBNumber.Text;
                    _currentRooster.RoosterWeight = Decimal.Parse(this.txtWeight.Text);
                    _currentRooster.UpdatedBy = "Administrator";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                #endregion
            }
        }

        private void NewRooster()
        {
            _newRoosters = true;
            this.txtLBNumber.Text = NewLegBandNumber();
            this.txtWeight.Text = "";
            _currentRooster = new Rooster();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var currentRoosters = new Roosters();
            var currentEntry = new Entries();

            try
            {
                if (!_editMode)
                {
                    if (MessageBox.Show("Add the Entry?", "Add Derby Entry", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Int32 newID;

                        _currentEntry.DerbyID = 1;
                        _currentEntry.CreatedBy = "Administrator";
                        newID = currentEntry.AddDerbyEntry(_currentEntry);

                        currentRoosters.AddRoosters(_entryRoosters, newID);
                    }

                    NewEntry();
                    LoadRoosters();

                }
                else
                {
                    if (MessageBox.Show("Update the Entry?", "Update Derby Entry", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        _currentEntry.UpdatedBy = "Administrator";
                        currentEntry.UpdateEntryInfo(_currentEntry);

                        if (_newRoosters)
                        {
                            currentRoosters.AddRoosters(_entryRoosters, _currentEntry.EntryID);
                        }
                        else
                        {
                            currentRoosters.EditRoosters(_entryRoosters);
                        }
                    }

                    var myParent = (Window)this.Parent;
                    myParent.Close();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewEntry()
        {
            _currentEntry = new Entry();

            var newEntryNumber = new MaxEntryNumber();

            _currentEntry.EntryNumber = newEntryNumber.NewEntryNumber; //newEntryNumber[0].NewEntryNumber;

            _editMode = false;

            DataContext = _currentEntry;

            txtLBNumber.Text = NewLegBandNumber();


            //var maxEntryNumber = new Derby();

            //if (_currentEntry.EntryID > maxEntryNumber.NumberOfEntries)
            //{
            //    MessageBox.Show("Max Number of Entries Based from Derby Info Reached");
            //}

        }

        private String NewLegBandNumber()
        {
            var newGameFowlNumber = new MaxGameFowlNumber();

            return _currentRooster.RoosterLegBandNumber = newGameFowlNumber.NewLegBandNumber;
        }
        /*
        private void dgRoosters_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IInputElement element = e.MouseDevice.DirectlyOver;

            if (element != null && element is FrameworkElement)
            {
                if (((FrameworkElement)element).Parent is DataGridCell)
                {
                    var grid = sender as DataGrid;
                    if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                    {
                        var rowView = grid.SelectedItem as Rooster;

                        if (rowView != null)
                        {
                            _currentRooster = rowView;
                        }
                    }
                }
            }
        }
        */
    }
}
