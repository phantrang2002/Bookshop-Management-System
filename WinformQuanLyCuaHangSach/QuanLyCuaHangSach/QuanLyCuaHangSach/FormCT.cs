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
    public partial class FormCT : Form
    {
        string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;
        string maHD = "";
        public FormCT(string maHD)
        {
            InitializeComponent();
            this.maHD = maHD;
            txtMaHD.Text = maHD;

        }
        private void FormCT_Load(object sender, EventArgs e)
        {
            layDSCT(maHD);

            txtMaHD.Enabled = false;
            //  thanhtien(maHD);

        }


        /*

        private void thanhtien(string maHD)
        {
            
            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT SUM((fGiaban*iSoLuong)-fMucgiamgia) FROM tblChiTietHoaDon LEFT JOIN tblHoaDon ON tblHoaDon.sMaHD = tblChiTietHoaDon.sMaHD WHERE tblChiTietHoaDon.sMaHD = '" + maHD + "'", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    object result = cmd.ExecuteScalar();
                    txtThanhTien.Text = Convert.ToString(result);
                }
            }
            


        }*/

        private void layDSCT(string maHD)
        {
            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT tblHoaDon.sMaHD as [Mã hóa đơn], sMaSach as [Mã sách], iSoluong as [Số lượng], fGiaban as [Giá bán], fMucgiamgia as [Mức giảm giá] FROM tblChiTietHoaDon LEFT JOIN tblHoaDon ON tblHoaDon.sMaHD = tblChiTietHoaDon.sMaHD WHERE tblChiTietHoaDon.sMaHD = '" + maHD + "'", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewCT.DataSource = tb;

                    }
                }
            }
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            this.Close();
            FormHD dn = new FormHD();
            dn.ShowDialog();
        }



        private void dataGridViewCT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaHD.Text = dataGridViewCT.CurrentRow.Cells["Mã hóa đơn"].Value.ToString();
            txtMaSach.Text = dataGridViewCT.CurrentRow.Cells["Mã sách"].Value.ToString();
            txtSoLuong.Text = dataGridViewCT.CurrentRow.Cells["Số lượng"].Value.ToString();
            txtGiaBan.Text = dataGridViewCT.CurrentRow.Cells["Giá bán"].Value.ToString();
            txtMucGiamGia.Text = dataGridViewCT.CurrentRow.Cells["Mức giảm giá"].Value.ToString();




            txtMaHD.Enabled = false;

        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;
            DialogResult result = MessageBox.Show("Bạn muốn thêm Chi tiết hóa đơn này ?", "THÔNG BÁO", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        try
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "insertCT";
                            cmd.Parameters.AddWithValue("@mahd", txtMaHD.Text);
                            cmd.Parameters.AddWithValue("@masach", txtMaSach.Text);
                            cmd.Parameters.AddWithValue("@sl", Convert.ToInt32(txtSoLuong.Text));
                            cmd.Parameters.AddWithValue("@gb", float.Parse(txtGiaBan.Text));
                            cmd.Parameters.AddWithValue("@mgg", float.Parse(txtMucGiamGia.Text));


                            cnn.Open();
                            int i = cmd.ExecuteNonQuery();
                            cnn.Close();
                        }

                        catch
                        {
                            MessageBox.Show("Hãy nhập lại", "THÔNG BÁO");

                        }

                    }
                }


                layDSCT(maHD);

            }
            else
            {
                Close();
            }
        }

        //SỬA
        private void btnSua_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;

            DialogResult result = MessageBox.Show("Bạn đã hoàn thành việc sửa CTHĐ này?", "THÔNG BÁO", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "updateCT";

                            cmd.Parameters.AddWithValue("@mahd", txtMaHD.Text);
                            cmd.Parameters.AddWithValue("@masach", txtMaSach.Text);
                            cmd.Parameters.AddWithValue("@sl", Convert.ToInt32(txtSoLuong.Text));
                            cmd.Parameters.AddWithValue("@gb", float.Parse(txtGiaBan.Text));
                            cmd.Parameters.AddWithValue("@mgg", float.Parse(txtMucGiamGia.Text));

                            cnn.Open();
                            int i = cmd.ExecuteNonQuery();
                            cnn.Close();
                        }
                    }
                }
                layDSCT(maHD);
            }
            else
            {
                Close();
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;

            DialogResult result = MessageBox.Show("Bạn muốn xóa CTHĐ này?", "THÔNG BÁO", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    SqlConnection conn = new SqlConnection(constr);
                    conn.Open();
                    int CurrentIndex = dataGridViewCT.CurrentCell.RowIndex;
                    string MaHD = Convert.ToString(dataGridViewCT.Rows[CurrentIndex].Cells[0].Value.ToString());
                    string deletedStr = "DELETE FROM tblChiTietHoaDon where sMaHD ='" + MaHD + "' And sMaSach='" + txtMaSach.Text + "'";



                    SqlCommand deletedCmd = new SqlCommand(deletedStr, conn);
                    deletedCmd.CommandType = CommandType.Text;
                    deletedCmd.ExecuteNonQuery();


                    // da.Update(ds,);
                    layDSCT(maHD);



                    MessageBox.Show("Bạn đã xóa thành công!", "THÔNG BÁO", MessageBoxButtons.OK);
                    conn.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Tồn tại đơn liên quan không thể xóa", "THÔNG BÁO");
                    // MessageBox.Show(ex.Message);
                }
            }
            else { Close(); }
        }


        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {

            //sử dụng thuộc tính RowFilter để tìm kiếm theo tên sTenNXB

            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM v_DSCT", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewCT.DataSource = tb;

                        string rowFilter = string.Format("{0} like '{1}'", "[Mã sách]", "*" + txtTimKiem.Text + "*");
                        (dataGridViewCT.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
                    }
                }
            }
        }

     

    }
}
