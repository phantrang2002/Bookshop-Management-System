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
    public partial class FormQLNV : Form
    {
        string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;
        string maNV = "", tenNV = "", passWord = "";
        bool maQuyen = false;

        public FormQLNV(string maNV, string tenNV, string passWord, bool maQuyen)
        {
            InitializeComponent();
            this.maNV = maNV;
            this.tenNV = tenNV;
            this.passWord = passWord;
            this.maQuyen = maQuyen;

            if (maQuyen == false)
            {
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                txtPass.Visible = false;
                lblPass.Visible = false;
                cbQuyen.Enabled = false;

            }
             
        }

        
        private void FormQLNV_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'quanLyCuaHangSachDataSet.tblNhanVien' table. You can move, or remove it, as needed.
            this.tblNhanVienTableAdapter.Fill(this.quanLyCuaHangSachDataSet.tblNhanVien);

            if (maQuyen == true)
            {
                layDSNV_admin();

            }
            else
            {
                layDSNV_user();
            }

        }

        private void layDSNV_admin()
        {
            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM v_DSNV_admin", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewNV.DataSource = tb;

                    }
                }
            }
        }

        private void layDSNV_user()
        {
            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM v_DSNV_user", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewNV.DataSource = tb;

                    }
                }
            }
        }

        //DATA GRID
        private void dataGridViewNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (maQuyen == true)
            {
                txtMaNV.Text = dataGridViewNV.CurrentRow.Cells["Mã NV"].Value.ToString();
                txtTenNV.Text = dataGridViewNV.CurrentRow.Cells["Tên NV"].Value.ToString();
                cbGT.Text = dataGridViewNV.CurrentRow.Cells["Giới tính"].Value.ToString();

               
                var date = Convert.ToDateTime(dataGridViewNV.CurrentRow.Cells["Ngày sinh"].Value.ToString()); 
                var shortDate = date.ToShortDateString();

                txtNgaySinh.Text = shortDate;

               txtDiaChi.Text = dataGridViewNV.CurrentRow.Cells["Địa chỉ"].Value.ToString();
                txtSDT.Text = dataGridViewNV.CurrentRow.Cells["SĐT"].Value.ToString();
                txtPass.Text = dataGridViewNV.CurrentRow.Cells["Password"].Value.ToString();
                cbQuyen.Text = dataGridViewNV.CurrentRow.Cells["Mã quyền truy cập"].Value.ToString();
                

                txtMaNV.Enabled = false;
                 
            }
            else
            {
                txtMaNV.Text = dataGridViewNV.CurrentRow.Cells["Mã NV"].Value.ToString();
                txtTenNV.Text = dataGridViewNV.CurrentRow.Cells["Tên NV"].Value.ToString();
                cbGT.Text = dataGridViewNV.CurrentRow.Cells["Giới tính"].Value.ToString();



                var date = Convert.ToDateTime(dataGridViewNV.CurrentRow.Cells["Ngày sinh"].Value.ToString());
                var shortDate = date.ToShortDateString();

                txtNgaySinh.Text = shortDate;


                txtDiaChi.Text = dataGridViewNV.CurrentRow.Cells["Địa chỉ"].Value.ToString();
                txtSDT.Text = dataGridViewNV.CurrentRow.Cells["SĐT"].Value.ToString();



                txtMaNV.Enabled = false;
            }

           
        }

        //thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaNV.Enabled = true;
            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;
            DialogResult result = MessageBox.Show("Bạn muốn thêm NV ?", "THÔNG BÁO", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        try
                        {
                          
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "insertNV";
                            cmd.Parameters.AddWithValue("@manv", txtMaNV.Text);
                            cmd.Parameters.AddWithValue("@tennv", txtTenNV.Text);
                            cmd.Parameters.AddWithValue("@gt", cbGT.Text);
                            cmd.Parameters.AddWithValue("@ngaysinh", Convert.ToString(txtNgaySinh.Text));
                            cmd.Parameters.AddWithValue("@dc", txtDiaChi.Text);
                            cmd.Parameters.AddWithValue("@sdt", txtSDT.Text);
                            cmd.Parameters.AddWithValue("@pass", txtPass.Text);
                           cmd.Parameters.AddWithValue("@maquyen", Convert.ToBoolean(cbQuyen.Text) ? 1 : 0);
  

                            cnn.Open();
                            int i = cmd.ExecuteNonQuery();
                            cnn.Close();
                        }

                       catch
                        {
                           MessageBox.Show("Trùng mã nv", "THÔNG BÁO");

                        }

                    }
                }

                
                    layDSNV_admin();
 
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

            DialogResult result = MessageBox.Show("Bạn đã hoàn thành việc sửa NV này?", "THÔNG BÁO", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "updateNV";
                            cmd.Parameters.AddWithValue("@manv", txtMaNV.Text);
                            cmd.Parameters.AddWithValue("@tennv", txtTenNV.Text);
                            cmd.Parameters.AddWithValue("@gt", cbGT.Text);

                            /*
                            var value = (object)DBNull.Value;
                            DateTime parsedDate;
                            if (DateTime.TryParseExact(txtNgaySinh.Text, "MM.dd.yyyy", null,
                                                       DateTimeStyles.None, out parsedDate))
                            {
                                value = parsedDate;
                            }
                            cmd.Parameters.AddWithValue("@ngaysinh", value);*/

                           cmd.Parameters.AddWithValue("@ngaysinh", Convert.ToDateTime(txtNgaySinh.Text).Date);
                            cmd.Parameters.AddWithValue("@dc", txtDiaChi.Text);
                            cmd.Parameters.AddWithValue("@sdt", txtSDT.Text);
                            cmd.Parameters.AddWithValue("@pass", txtPass.Text);
                            cmd.Parameters.AddWithValue("@maquyen", Convert.ToBoolean(cbQuyen.Text) ? 1 : 0);

                            cnn.Open();
                            int i = cmd.ExecuteNonQuery();
                            cnn.Close();
                        }

                    }
                }
                
                layDSNV_admin();

                
            }
            else
            {
                Close();
            }
            txtMaNV.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;

            DialogResult result = MessageBox.Show("Bạn muốn xóa NV này?", "THÔNG BÁO", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    SqlConnection conn = new SqlConnection(constr);
                    conn.Open();
                    int CurrentIndex = dataGridViewNV.CurrentCell.RowIndex;
                    string MaNV = Convert.ToString(dataGridViewNV.Rows[CurrentIndex].Cells[0].Value.ToString());
                    string deletedStr = "DELETE FROM tblNhanVien where sMaNV ='" + MaNV + "'";



                    SqlCommand deletedCmd = new SqlCommand(deletedStr, conn);
                    deletedCmd.CommandType = CommandType.Text;
                    deletedCmd.ExecuteNonQuery();


                    // da.Update(ds,);
                    layDSNV_admin();


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
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM v_DSNV_admin", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridViewNV.DataSource = tb;

                        string rowFilter = string.Format("{0} like '{1}'", "[Tên NV]", "*" + txtTimKiem.Text + "*");
                        (dataGridViewNV.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
                    }
                }
            }
        }

    }
}
