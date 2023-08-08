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
   // DateTime.CustomFormat = "dd-MM-yyyy"
    public partial class FormKH : Form
    {
        public FormKH()
        {
            InitializeComponent();
        }


        private void FormKH_Load(object sender, EventArgs e)
        {
            layDSKH();
            
             
        }
 
         
        public bool KTThongTin()
        {
             /*

            if (txtMaKH.Text == "")
            {
                MessageBox.Show("Vui lòng nhập MaKH", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKH.Focus();
                return false;
            }
            if (txtTenKH.Text == "")
            {
                MessageBox.Show("Vui lòng nhập txtTenKH", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenKH.Focus();
                return false;
            }
            if (cbGT.Text == "")
            {
                MessageBox.Show("Vui lòng nhập cbGT ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbGT.Focus();
                return false;
            }

            if (txtNgaySinh.Text == ""   )
            {
                MessageBox.Show("Vui lòng nhập txtNgaySinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNgaySinh.Focus();
                return false;
            }

            if (txtSDT.Text == "")
            {
                MessageBox.Show("Vui lòng nhập txtSDT", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSDT.Focus();
                return false;
            }

            //if (txtSDT.Text != "0123" && txtSDT.Text != "01234")
          //  {
            //    MessageBox.Show("Vui lòng nhập đúng dạng txtSDT", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
             //   txtSDT.Focus();
             //   return false;
          //  }

            //txtSDT.Text.Length < 3

            if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Vui lòng nhập txtDiaChi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return false;
            }
             
            */
            return true;
        }

        /*
        private void textBoxes_TextChanged(object sender, EventArgs e)
        {
            EnableButton();
        }


        private void EnableButton()
        {
            btnThem.Enabled = !Controls.OfType<TextBox>().Any(x => string.IsNullOrEmpty(x.Text));
        }*/

        private void layDSKH()
        {
            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM v_DSKH", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewKH.DataSource = tb;

                    }
                }
            }
        }

        private void dataGridViewKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                txtMaKH.Text = dataGridViewKH.CurrentRow.Cells["Mã KH"].Value.ToString();
                txtTenKH.Text = dataGridViewKH.CurrentRow.Cells["Tên KH"].Value.ToString();
                cbGT.Text = dataGridViewKH.CurrentRow.Cells["Giới tính"].Value.ToString();
             

                var date = Convert.ToDateTime(dataGridViewKH.CurrentRow.Cells["Ngày sinh"].Value.ToString());
                var shortDate = date.ToShortDateString();

                txtNgaySinh.Text = shortDate; 
                txtSDT.Text = dataGridViewKH.CurrentRow.Cells["SĐT"].Value.ToString();
                txtDiaChi.Text = dataGridViewKH.CurrentRow.Cells["Địa chỉ"].Value.ToString();

                txtMaKH.Enabled = false;
        }
        /*
        void txtNgaySinh_Validating(object sender, CancelEventArgs e)

        {

            DateTime rs;

            CultureInfo ci = new CultureInfo("en-IE");

            if (!DateTime.TryParseExact(this.txtNgaySinh.Text, "dd/MM/yyyy", ci, DateTimeStyles.None, out rs))

            {
                e.Cancel = true;
            }

        }*/
        //thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaKH.Enabled = true;
            
            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;

            DialogResult result = MessageBox.Show("Bạn muốn thêm KH ?", "THÔNG BÁO", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                if (KTThongTin())
                {
                     
                    using (SqlConnection cnn = new SqlConnection(constr))
                    {
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            try
                            {

                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "insertKH";
                                cmd.Parameters.AddWithValue("@makh", txtMaKH.Text);
                                cmd.Parameters.AddWithValue("@tenkh", txtTenKH.Text);
                                cmd.Parameters.AddWithValue("@gt", cbGT.Text);
                                cmd.Parameters.AddWithValue("@ngaysinh", Convert.ToString(txtNgaySinh.Text));
                                cmd.Parameters.AddWithValue("@sdt", txtSDT.Text);
                                cmd.Parameters.AddWithValue("@dc", txtDiaChi.Text);

                                cnn.Open();
                                int i = cmd.ExecuteNonQuery();
                                cnn.Close();
                            }

                            catch
                            {
                                MessageBox.Show("Trùng mã kh", "THÔNG BÁO");

                            }

                        }
                    }


                    layDSKH();

                }
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

            DialogResult result = MessageBox.Show("Bạn đã hoàn thành việc sửa KH này?", "THÔNG BÁO", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "updateKH";
                            cmd.Parameters.AddWithValue("@makh", txtMaKH.Text);
                            cmd.Parameters.AddWithValue("@tenkh", txtTenKH.Text);
                            cmd.Parameters.AddWithValue("@gt", cbGT.Text);


                            cmd.Parameters.AddWithValue("@ngaysinh", Convert.ToDateTime(txtNgaySinh.Text).Date);
                            cmd.Parameters.AddWithValue("@sdt", txtSDT.Text);

                            cmd.Parameters.AddWithValue("@dc", txtDiaChi.Text);
                           

                            cnn.Open();
                            int i = cmd.ExecuteNonQuery();
                            cnn.Close();
                        }
                    }
                }
                layDSKH();
            }
            else
            {
                Close();
            }
            txtMaKH.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;

            DialogResult result = MessageBox.Show("Bạn muốn xóa KH này?", "THÔNG BÁO", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    SqlConnection conn = new SqlConnection(constr);
                    conn.Open();
                    int CurrentIndex = dataGridViewKH.CurrentCell.RowIndex;
                    string MaKH = Convert.ToString(dataGridViewKH.Rows[CurrentIndex].Cells[0].Value.ToString());
                    string deletedStr = "DELETE FROM tblKhachHang where sMaKH ='" + MaKH + "'";



                    SqlCommand deletedCmd = new SqlCommand(deletedStr, conn);
                    deletedCmd.CommandType = CommandType.Text;
                    deletedCmd.ExecuteNonQuery();


                    // da.Update(ds,);
                    layDSKH();


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
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM v_DSKH", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewKH.DataSource = tb;

                        string rowFilter = string.Format("{0} like '{1}'", "[Tên KH]", "*" + txtTimKiem.Text + "*");
                        (dataGridViewKH.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
                    }
                }
            }
        }

       


    }
}