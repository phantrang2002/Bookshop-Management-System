using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public partial class BaoCaoThanhTien : Form
    {
        public BaoCaoThanhTien()
        {
            InitializeComponent();
        }
         


        private void BaoCaoThanhTien_Load(object sender, EventArgs e)
        {
            ReportDocument cry = new ReportDocument();
            cry.Load(@"D:\WinformQuanLyCuaHangSach\QuanLyCuaHangSach\QuanLyCuaHangSach\thanhTienChiTiet.rpt");
            crystalReportViewer1.ReportSource = cry;
            crystalReportViewer1.Refresh();
        }

        private void btnIn_click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.CommandText = "sp_thanhTienChiTiet";
                    cmd.Parameters.AddWithValue("@SMaHD", txtMaHD.Text);
                    using (SqlDataAdapter ad = new SqlDataAdapter())
                    {
                        ad.SelectCommand = cmd;
                        DataTable tb = new System.Data.DataTable();
                        ad.Fill(tb);
                        thanhTienChiTiet rpt = new thanhTienChiTiet();
                        rpt.SetDataSource(tb);
                        crystalReportViewer1.ReportSource = rpt;
                        crystalReportViewer1.Refresh();


                    }

                }
            }
        }
    }
}
