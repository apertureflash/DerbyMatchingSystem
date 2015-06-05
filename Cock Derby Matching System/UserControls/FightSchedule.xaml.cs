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
using Cock_Derby_Matching_System;

namespace CockDerbyMatchingSystem.UserControls
{
    /// <summary>
    /// Interaction logic for FightSchedule.xaml
    /// </summary>
    public partial class FightSchedule : UserControl
    {
        public FightSchedule()
        {
            InitializeComponent();

            Derbies derbiesDA = new Derbies();
            this.dgFightSchedule.ItemsSource = derbiesDA.GetExistingFightSequence();
            LoadUnMatchedEntries();
        }

        private void btnFightSequence_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Generating a new fight sequence will overwrite the existing fight schedule.\n\nWould you like to continue?",
                "Generate Fight Sequence", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                LoadNewFightSequence();
                LoadUnMatchedEntries();
            }
            
        }

        private void LoadNewFightSequence()
        {
            Derbies derbiesDA = new Derbies();
            this.dgFightSchedule.ItemsSource = derbiesDA.GenerateFightSequence();
        }

        private void LoadUnMatchedEntries()
        {
            Roosters rOosters = new Roosters();

            this.dgUnMatched.ItemsSource = rOosters.GetUnMatchedRoosterEntries();
        }

        private void btnHandPicked_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();

            Window window = new Window
            {
                WindowStyle = System.Windows.WindowStyle.ToolWindow,
                Width = 830,
                Height = 660,
                Background = (Brush)bc.ConvertFrom("#FFB3BABA"),
                Title = "Hand Picked Matches",
                Content = new HandPickedFights(),
                HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                ShowInTaskbar = true
            };

            window.ShowDialog();

        }

        private void btnPrintSequence_Click(object sender, RoutedEventArgs e)
        {
            Windows.ReportViewer rptViewer = new Windows.ReportViewer()
            {
                Title = "Reports",
                ShowInTaskbar = false
            };

            rptViewer.Show();
        }
    }
}
