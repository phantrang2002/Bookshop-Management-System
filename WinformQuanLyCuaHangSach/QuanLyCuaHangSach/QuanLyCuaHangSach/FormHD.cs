using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace QuanLyCuaHangSach
{
    public partial class FormHD : Form
    {

        public FormHD()
        {
            InitializeComponent();
        }

        private void FormHD_Load(object sender, EventArgs e)
        {
            layDSHD();
        }

        private void layDSHD()
        {
            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM v_DSHD", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewHD.DataSource = tb;

                    }
                }
            }
        }

        private void dataGridViewHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaHD.Text = dataGridViewHD.CurrentRow.Cells["Mã hóa đơn"].Value.ToString();
            txtMaNV.Text = dataGridViewHD.CurrentRow.Cells["Mã nhân viên"].Value.ToString();
            txtMaKH.Text = dataGridViewHD.CurrentRow.Cells["Mã khách hàng"].Value.ToString();

            var date = Convert.ToDateTime(dataGridViewHD.CurrentRow.Cells["Ngày lập"].Value.ToString());
            var shortDate = date.ToShortDateString();
            txtNgayLap.Text = shortDate;

            var date1 = Convert.ToDateTime(dataGridViewHD.CurrentRow.Cells["Ngày thanh toán"].Value.ToString());
            var shortDate1 = date1.ToShortDateString();
            txtNgayThanhToan.Text = shortDate1;


            txtMaHD.Enabled = false;

        }

        //thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaHD.Enabled = true;
            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;
            DialogResult result = MessageBox.Show("Bạn muốn thêm HĐ ?", "THÔNG BÁO", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        try
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "insertHD";
                            cmd.Parameters.AddWithValue("@mahd", txtMaHD.Text);
                            cmd.Parameters.AddWithValue("@manv", txtMaNV.Text);
                            cmd.Parameters.AddWithValue("@makh", txtMaKH.Text);
                            cmd.Parameters.AddWithValue("@ngaylap", Convert.ToString(txtNgayLap.Text));
                            cmd.Parameters.AddWithValue("@ngaythanhtoan", Convert.ToString(txtNgayThanhToan.Text));


                            cnn.Open();
                            int i = cmd.ExecuteNonQuery();
                            cnn.Close();
                        }

                        catch
                        {
                            MessageBox.Show("Trùng mã HĐ", "THÔNG BÁO");

                        }

                    }
                }


                layDSHD();

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

            DialogResult result = MessageBox.Show("Bạn đã hoàn thành việc sửa HĐ này?", "THÔNG BÁO", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "updateHD";
                            cmd.Parameters.AddWithValue("@mahd", txtMaHD.Text);
                            cmd.Parameters.AddWithValue("@manv", txtMaNV.Text);
                            cmd.Parameters.AddWithValue("@makh", txtMaKH.Text);
                            cmd.Parameters.AddWithValue("@ngaylap", Convert.ToDateTime(txtNgayLap.Text).Date);
                            cmd.Parameters.AddWithValue("@ngaythanhtoan", Convert.ToDateTime(txtNgayThanhToan.Text).Date);
            

                            cnn.Open();
                            int i = cmd.ExecuteNonQuery();
                            cnn.Close();
                        }
                    }
                }
                layDSHD();
            }
            else
            {
                Close();
            }
            txtMaHD.Enabled = true;
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;

            DialogResult result = MessageBox.Show("Bạn muốn xóa HĐ này?", "THÔNG BÁO", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    SqlConnection conn = new SqlConnection(constr);
                    conn.Open();
                    int CurrentIndex = dataGridViewHD.CurrentCell.RowIndex;
                    string MaHD = Convert.ToString(dataGridViewHD.Rows[CurrentIndex].Cells[0].Value.ToString());

                    var date1 = Convert.ToDateTime(dataGridViewHD.CurrentRow.Cells["Ngày thanh toán"].Value.ToString());
  
                   if(date1 > DateTime.Now)
                    {
                        MessageBox.Show("Đơn chưa thanh toán không được xóa", "THÔNG BÁO");

                    }
                    else
                    {
                        string deletedStr = "DELETE FROM tblHoaDon where sMaHD ='" + MaHD + "'";



                        SqlCommand deletedCmd = new SqlCommand(deletedStr, conn);
                        deletedCmd.CommandType = CommandType.Text;
                        deletedCmd.ExecuteNonQuery();


                        // da.Update(ds,);
                        layDSHD();


                        MessageBox.Show("Bạn đã xóa thành công!", "THÔNG BÁO", MessageBoxButtons.OK);
                        conn.Close();
                    }


                   
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
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM v_DSHD", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewHD.DataSource = tb;

                        string rowFilter = string.Format("{0} like '{1}'", "[Mã hóa đơn]", "*" + txtTimKiem.Text + "*");
                        (dataGridViewHD.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
                    }
                }
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;
            SqlConnection cnn = new SqlConnection(constr);
            SqlDataAdapter da = new SqlDataAdapter("SELECT tblChiTietHoaDon.sMaHD, sMaSach, iSoluong, fGiaban, fMucgiamgia FROM tblChiTietHoaDon LEFT JOIN tblHoaDon ON tblHoaDon.sMaHD = tblChiTietHoaDon.sMaHD WHERE tblChiTietHoaDon.sMaHD = '" + txtMaHD.Text +"'", cnn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                this.Hide();
                FormCT fn = new FormCT(dt.Rows[0][0].ToString());
                fn.Show();
                
            }
             else
            {
                FormCT fn = new FormCT(txtMaHD.Text.ToString());
                fn.Show();
              //  MessageBox.Show("Vui lòng điền đúng thông tin");
                 
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            BaoCaoThanhTien nx = new BaoCaoThanhTien();
            
            nx.Show();
        }

        /*private void btn_Them_Click(object sender, EventArgs e)
        {
            if (mask_maMH.Text == "" || mask_tenMH.Text == "" || mask_soTC.Text == "")
            {
                MessageBox.Show("Bạn phai nhap du cac truong", "Thông báo");
            }
            else if (int.Parse(mask_soTC.Text) < 0)
            {
                MessageBox.Show("du lieu so phai lon hon 0", "Thông báo");
            }
            else 
            {
                themMH(Convert.ToString(mask_maMH.Text), mask_tenMH.Text.ToString().Trim(), int.Parse(mask_soTC.Text));
                laydataGridMH();
            }

                
           private void tb_Search_TextChanged(object sender, EventArgs e)
        {
            search_textbox(tb_search.Text);
        }


        private void search_textbox(string x)
        {
            string sql = "SELECT sMaSV 'Mã sinh viên' ,sTenSV 'Tên sinh viên' , sGT 'Giới tính' , sQueQuan 'Quê quán' ," +
                    "dNgaySinh AS 'Ngày sinh' , sMaLop AS 'Mã lớp' FROM tblSinhVien WHERE CONCAT(sTenSV , sGT ) LIKE '%" + x + "%'";
            ConnectSQL sQL = new ConnectSQL();
            sQL.ketnoi();
            dtGridSV.DataSource = sQL.getTable(sql);
        }
    }
} 
            

        }*/
    }
}
