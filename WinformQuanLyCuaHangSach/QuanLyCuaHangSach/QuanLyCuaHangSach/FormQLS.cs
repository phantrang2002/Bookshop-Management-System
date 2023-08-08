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
    public partial class FormQLS : Form
    {
        public FormQLS()
        {
            InitializeComponent();
        }

        private void FormQLS_Load(object sender, EventArgs e)
        {
            layDSSACH();
            layNXB();
        }

        //BẮT LỖI VALID
        private void txtMaSach_Validating(object sender, CancelEventArgs e)
        {
            if (txtMaSach.Text == string.Empty)
                errorProvider1.SetError(txtMaSach, "Bạn phải nhập mã sách!");
            else
            {
                errorProvider1.SetError(txtMaSach, ""); 
            }

        }

        private void txtTenSach_Validating(object sender, CancelEventArgs e)
        {
            if (txtTenSach.Text == string.Empty)
                errorProvider1.SetError(txtMaSach, "Bạn phải nhập tên sách!");
            else
            {
                errorProvider1.SetError(txtTenSach, "");
            }

        }
        private void txtNamXB_Validating(object sender, CancelEventArgs e)
        {
            if (txtNamXB.Text == string.Empty)
                errorProvider1.SetError(txtNamXB, "Bạn phải nhập năm XB!");
            else
            {
                try
                {
                    errorProvider1.SetError(txtNamXB, "");
                }
                catch
                {
                    errorProvider1.SetError(txtNamXB, "Năm XB phải là số!");
                }
            }

        }

        private void txtDonGia_Validating(object sender, CancelEventArgs e)
        {
            if (txtDonGia.Text == string.Empty)
                errorProvider1.SetError(txtDonGia, "Bạn phải nhập đơn giá!");
            else
            {
                try
                {
                    errorProvider1.SetError(txtDonGia, "");
                }
                catch
                {
                    errorProvider1.SetError(txtDonGia, "Đơn giá phải là số!");
                }
            }

        }

        private void txtSoLuong_Validating(object sender, CancelEventArgs e)
        {
            if (txtSoLuong.Text == string.Empty)
                errorProvider1.SetError(txtSoLuong, "Bạn phải nhập số lượng!");
            else
            {
                try
                {
                    errorProvider1.SetError(txtSoLuong, "");
                }
                catch
                {
                    errorProvider1.SetError(txtSoLuong, "Số lượng phải là số!");
                }
            }

        }



        private void layDSSACH()
        {
            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM v_DSSACH", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewSach.DataSource = tb;

                    }
                }
            }
        }

        private void layNXB()
        {
            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblNhaXuatBan", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("NXB");
                        ad.Fill(tb);
                        cbNXB.DataSource = tb;
                        cbNXB.DisplayMember = "sMaNXB";
                    }
                }
            }
        }

      
        //THÊM
        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaSach.Enabled = true;
            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;
            DialogResult result = MessageBox.Show("Bạn muốn thêm sách ?", "THÔNG BÁO", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "insertSACH";
                            cmd.Parameters.AddWithValue("@masach", txtMaSach.Text);
                            cmd.Parameters.AddWithValue("@tensach", txtTenSach.Text);
                            cmd.Parameters.AddWithValue("@namxb", Convert.ToInt32(txtNamXB.Text));
                            cmd.Parameters.AddWithValue("@manxb", cbNXB.Text);
                            cmd.Parameters.AddWithValue("@sl", Convert.ToInt32(txtSoLuong.Text));

                            cmd.Parameters.AddWithValue("@dongia", float.Parse(txtDonGia.Text));


                            cnn.Open();
                            int i = cmd.ExecuteNonQuery();
                            cnn.Close();
                        }

                        catch
                        {
                            MessageBox.Show("Trùng mã sách", "THÔNG BÁO");

                        }

                    }
                }

                layDSSACH();
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

            DialogResult result = MessageBox.Show("Bạn đã hoàn thành việc sửa sách này?", "THÔNG BÁO", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "updateSACH";
                            cmd.Parameters.AddWithValue("@masach", txtMaSach.Text);
                            cmd.Parameters.AddWithValue("@tensach", txtTenSach.Text);
                            cmd.Parameters.AddWithValue("@namxb", Convert.ToInt32(txtNamXB.Text));
                            cmd.Parameters.AddWithValue("@manxb", cbNXB.Text);
                            cmd.Parameters.AddWithValue("@sl", Convert.ToInt32(txtSoLuong.Text));

                            cmd.Parameters.AddWithValue("@dongia", float.Parse(txtDonGia.Text));

                            cnn.Open();
                            int i = cmd.ExecuteNonQuery();
                            cnn.Close();
                        }

                    }
                }
                layDSSACH();
            }
            else
            {
                Close();
            }
            txtMaSach.Enabled = true;
        }

        //XÓA
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;

            DialogResult result = MessageBox.Show("Bạn muốn xóa sách này?", "THÔNG BÁO", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    SqlConnection conn = new SqlConnection(constr);
                    conn.Open();
                    int CurrentIndex = dataGridViewSach.CurrentCell.RowIndex;
                    string Masach = Convert.ToString(dataGridViewSach.Rows[CurrentIndex].Cells[0].Value.ToString());
                    string deletedStr = "DELETE FROM tblSach where sMaSach ='" + Masach + "'";



                    SqlCommand deletedCmd = new SqlCommand(deletedStr, conn);
                    deletedCmd.CommandType = CommandType.Text;
                    deletedCmd.ExecuteNonQuery();


                    // da.Update(ds,);
                    layDSSACH();


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

        //TÌM
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {

            //sử dụng thuộc tính RowFilter để tìm kiếm theo tên sTenNXB

            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM v_DSSACH", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewSach.DataSource = tb;

                        string rowFilter = string.Format("{0} like '{1}'", "[Tên sách]", "*" + txtTimKiem.Text + "*");
                        (dataGridViewSach.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
                    }
                }
            }
        }

        //DATA GRID
        private void dataGridViewSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaSach.Text = dataGridViewSach.CurrentRow.Cells["Mã sách"].Value.ToString();
            txtTenSach.Text = dataGridViewSach.CurrentRow.Cells["Tên sách"].Value.ToString();
            txtNamXB.Text = dataGridViewSach.CurrentRow.Cells["Năm XB"].Value.ToString();
            cbNXB.Text = dataGridViewSach.CurrentRow.Cells["Mã NXB"].Value.ToString();
            txtSoLuong.Text = dataGridViewSach.CurrentRow.Cells["Số lượng"].Value.ToString();
            txtDonGia.Text = dataGridViewSach.CurrentRow.Cells["Đơn giá"].Value.ToString();


            txtMaSach.Enabled = false;
        }



    }
}
