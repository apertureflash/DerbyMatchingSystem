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
using System.Windows.Shapes;

using System.Windows.Navigation;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using CockDerbyMatchingSystem;


using System.Data.SqlClient;
using System.Xml;
using System.Xml.Serialization;
using System.Data.OleDb;
using System.IO;

using System.Configuration;

namespace CockDerbyMatchingSystem.Windows
{
    /// <summary>
    /// Interaction logic for ReportViewer.xaml
    /// </summary>
    public partial class ReportViewer : Window
    {
        public ReportViewer()
        {
            InitializeComponent();
        }

        private void btnEntryFights_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection SqlCon = new SqlConnection("Data Source=WGITPI5-TDIAZ;Initial Catalog=DerbyMatching;User ID=sa;Password=P@ssw0rd"); //Integrated Security=True");

            SqlCon.Open();

            DataSet ds = new DataSet("ReportDataSet");

            // Create a Command  
            SqlCommand SqlComm = new SqlCommand();
            SqlComm.Connection = SqlCon;
            SqlComm.CommandType = CommandType.StoredProcedure;
            SqlComm.CommandText = "usp_GetFightSchedulePerEntry";

            // Set a Data Commands  
            SqlDataAdapter SqlDa = new SqlDataAdapter(SqlComm);

            SqlDa.Fill(ds);

            this._reportViewer.ProcessingMode = ProcessingMode.Local;
            this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("FightMatchesData", ds.Tables[0]));


            DataSet ds2 = new DataSet("ReportDataSet");

            SqlComm.CommandType = CommandType.Text;
            SqlComm.CommandText = "SELECT TOP 1 * FROM Derbies";

            // Set a Data Commands  
            SqlDa = new SqlDataAdapter(SqlComm);

            SqlDa.Fill(ds2);


            this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DerbyData", ds2.Tables[0]));


            // Bind it with Report DataSource
            this._reportViewer.LocalReport.ReportPath = @"C:\Projects\Sagupaan\Cock Derby Matching System\Cock Derby Matching System\Reports\FightMatchesPerEntry.rdlc";
            // Show report
            this._reportViewer.RefreshReport();
        }


        private void btnFightSchedule_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection SqlCon = new SqlConnection("Data Source=.;Initial Catalog=DerbyMatching;Integrated Security=True");

            //"Data Source=.\\sqlexpress;Initial Catalog=DerbyMatching;Integrated Security=True"
            SqlCon.Open();

            DataSet ds = new DataSet("ReportDataSet");

            // Create a Command  
            SqlCommand SqlComm = new SqlCommand();
            SqlComm.Connection = SqlCon;
            SqlComm.CommandType = CommandType.Text;
            
            SqlComm.CommandText = "SELECT * FROM FightSequences ORDER BY FightNumber";

            // Set a Data Commands  
            SqlDataAdapter SqlDa = new SqlDataAdapter(SqlComm);

            SqlDa.Fill(ds);

            this._reportViewer.ProcessingMode = ProcessingMode.Local;
            this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DerbyFightScheduleData", ds.Tables[0]));



            DataSet ds2 = new DataSet("ReportDataSet");

            SqlComm.CommandType = CommandType.Text;
            SqlComm.CommandText = "SELECT TOP 1 * FROM Derbies";

            // Set a Data Commands  
            SqlDa = new SqlDataAdapter(SqlComm);

            SqlDa.Fill(ds2);


            this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DerbyData", ds2.Tables[0]));


            // Bind it with Report DataSource
            this._reportViewer.LocalReport.ReportPath = @"C:\Projects\Sagupaan\Cock Derby Matching System\Cock Derby Matching System\Reports\DerbyFightSchedule.rdlc";
            // Show report
            this._reportViewer.RefreshReport();
        }
    }
}
