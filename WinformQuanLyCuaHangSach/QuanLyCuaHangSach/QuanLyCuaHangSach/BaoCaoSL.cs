using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public partial class BaoCaoSL : Form
    {
        public BaoCaoSL()
        {
            InitializeComponent();
        }

        private void BaoCaoSL_Load(object sender, EventArgs e)
        {
            ReportDocument cry = new ReportDocument();
            cry.Load(@"D:\WinformQuanLyCuaHangSach\QuanLyCuaHangSach\QuanLyCuaHangSach\CrystalReportThongKeSach.rpt");
            crystalReportViewer1.ReportSource = cry;
            crystalReportViewer1.Refresh();
        }

         
    }
}
