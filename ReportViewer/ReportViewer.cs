using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace ReportViewer
{
    public partial class frmReportViewer : Form
    {
        public frmReportViewer()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();

            // A method to populate data inside the referenced DataSet
            //getDataSet(ref <code>dsHesapPlan);

            // Set the path of the rdlc document
            string reportPath = "Reports/rpt_FightSchedulePerEntry.rdlc";

            // Initiate ReportViewer object
            Microsoft.Reporting.WinForms.ReportViewer rView = new Microsoft.Reporting.WinForms.ReportViewer();
            rView.Dock = DockStyle.Fill;
            this.Controls.Add(rView);

            // Add data source to the local report
            // Note that the name should either be passed as parameter or 
            // parsed from the report document
            //rView.LocalReport.DataSources
            //       .Add(new ReportDataSource("HESAP_PLAN_dtLast", dsHesapPlan.Tables[0]));

            // Set the active report path of the ReportViewer object
            rView.LocalReport.ReportPath = reportPath;
        }
    }
}
