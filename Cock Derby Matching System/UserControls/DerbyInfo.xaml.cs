using System;
using System.Windows;
using System.Windows.Controls;
using CockDerbyMatching_BL;
    using CockDerbyMatching_DAL;

namespace CockDerbyMatchingSystem.UserControls
{
    /// <summary>
    /// Interaction logic for DerbyInfo.xaml
    /// </summary>
    public partial class DerbyInfo : UserControl
    {

        #region Properties, Events, Fields

        private Derby _derby;
        private Boolean _inEditMode = false;
        private Int32 _derbyID = 0;

        #endregion

        public DerbyInfo()
        {
            InitializeComponent();

            RecordSelected();
        }

        private void RecordSelected()
        {
            try
            {
                var derbiesDA = new Derbies();

                _derby = derbiesDA.GetDerby() ?? new Derby();

                this.DataContext = _derby;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                var derbiesBL = new Derbies();

                derbiesBL.UpdateDerbyInfo(_derby);
                
                ////EnableFields(false);
                this.btnEdit.Content = "Edit";
                _inEditMode = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            
        }

        private void EnableFields(Boolean enable)
        {
            this.txtDerbyName.IsEnabled = enable;
            this.dtpDerbyDate.IsEnabled = enable;
            this.txtEntriesNo.IsEnabled = enable;

            this.btnCancel.IsEnabled = enable;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            RecordSelected();
        }


        #region Public Properties

        public Int32 DerbyID
        {
            get
            {
                return _derbyID;
            }
        }

        #endregion

        private void btnResetDerby_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("This will reset all derby information and will close the application.\n\nWould you like to continue?",
                    "Reset Derby Info", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var derbiesDA = new Derbies();

                    derbiesDA.ResetDerby();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
                Application.Current.Shutdown();
            }
        }

    }
}
