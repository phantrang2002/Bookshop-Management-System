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
    public partial class FormNXB : Form
    {
        public FormNXB()
        {
            InitializeComponent();
        }

        private void FormNXB_Load(object sender, EventArgs e)
        {
            layDSNXB();
             
        }

        private void txtMaNXB_Validating(object sender, CancelEventArgs e)
        {
            if (txtMaNXB.Text == string.Empty)
            {
                errorProvider1.SetError(txtMaNXB, "Bạn phải nhập mã NXB!");
                 
            }

            else
            {
                errorProvider1.SetError(txtMaNXB, "");
            }

        }

        private void txtTenNXB_Validating(object sender, CancelEventArgs e)
        {
            if (txtTenNXB.Text == string.Empty)
                errorProvider1.SetError(txtMaNXB, "Bạn phải nhập tên NXB!");
            else
            {
                errorProvider1.SetError(txtTenNXB, "");
            }

        }
        private void txtDiaChi_Validating(object sender, CancelEventArgs e)
        {
            if (txtDiaChi.Text == string.Empty)
                errorProvider1.SetError(txtDiaChi, "Bạn phải nhập địa chỉ!");
            else
            {
                    errorProvider1.SetError(txtDiaChi, "");             
            }

        }

        private void txtSDT_Validating(object sender, CancelEventArgs e)
        {
            if (txtSDT.Text == string.Empty)
                errorProvider1.SetError(txtDiaChi, "Bạn phải nhập SĐT!");
            else
            {
                try
                {
                    errorProvider1.SetError(txtSDT, "");
                }
                catch
                {
                    errorProvider1.SetError(txtSDT, "SĐT phải là số!");
                }
            }

        }

        private void layDSNXB()
        {
            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM v_DSNXB", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewNXB.DataSource = tb;

                    }
                }
            }
        }

        //THÊM
        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaNXB.Enabled = true;
            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;
            DialogResult result = MessageBox.Show("Bạn muốn thêm NXB ?", "THÔNG BÁO", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "insertNXB";
                            cmd.Parameters.AddWithValue("@manxb", txtMaNXB.Text);
                            cmd.Parameters.AddWithValue("@tennxb", txtTenNXB.Text);
                            cmd.Parameters.AddWithValue("@diachi", txtDiaChi.Text);
                            cmd.Parameters.AddWithValue("@sdt", txtSDT.Text);                           
                            cnn.Open();
                            int i = cmd.ExecuteNonQuery();
                            cnn.Close();
                        }
                        catch
                        {
                            MessageBox.Show("Trùng mã NXB", "THÔNG BÁO");
                        }
                    }
                }

                layDSNXB();
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

            DialogResult result = MessageBox.Show("Bạn đã hoàn thành việc sửa NXB này?", "THÔNG BÁO", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "updateNXB";
                            cmd.Parameters.AddWithValue("@manxb", txtMaNXB.Text);
                            cmd.Parameters.AddWithValue("@tennxb", txtTenNXB.Text);
                            cmd.Parameters.AddWithValue("@diachi", txtDiaChi.Text);
                            cmd.Parameters.AddWithValue("@sdt", txtSDT.Text);

                            cnn.Open();
                            int i = cmd.ExecuteNonQuery();
                            cnn.Close();
                        }
                    }
                }
                layDSNXB();
            }
            else
            {
                Close();
            }
            txtMaNXB.Enabled = true;
        }
        //XÓA
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;

            DialogResult result = MessageBox.Show("Bạn muốn xóa NXB này?", "THÔNG BÁO", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    SqlConnection conn = new SqlConnection(constr);
                    conn.Open();
                    int CurrentIndex = dataGridViewNXB.CurrentCell.RowIndex;
                    string MaNXB = Convert.ToString(dataGridViewNXB.Rows[CurrentIndex].Cells[0].Value.ToString());
                    string deletedStr = "DELETE FROM tblNhaXuatBan where sMaNXB ='" + MaNXB + "'";

                    SqlCommand deletedCmd = new SqlCommand(deletedStr, conn);
                    deletedCmd.CommandType = CommandType.Text;
                    deletedCmd.ExecuteNonQuery();


                    // da.Update(ds,);
                    layDSNXB();


                    MessageBox.Show("Bạn đã xóa thành công!", "THÔNG BÁO", MessageBoxButtons.OK);
                    conn.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Tồn tại sách/đơn liên quan không thể xóa", "THÔNG BÁO");
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
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM v_DSNXB", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewNXB.DataSource = tb;

                        string rowFilter = string.Format("{0} like '{1}'", "[Tên NXB]", "*" + txtTimKiem.Text + "*");
                        (dataGridViewNXB.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
                    }
                }
            }
        }

        //DATA GRID
        private void dataGridViewNXB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNXB.Text = dataGridViewNXB.CurrentRow.Cells["Mã NXB"].Value.ToString();
            txtTenNXB.Text = dataGridViewNXB.CurrentRow.Cells["Tên NXB"].Value.ToString();
            txtDiaChi.Text = dataGridViewNXB.CurrentRow.Cells["Địa chỉ"].Value.ToString();
            txtSDT.Text = dataGridViewNXB.CurrentRow.Cells["SĐT"].Value.ToString();
            txtMaNXB.Enabled = false;
        }

    }
}
