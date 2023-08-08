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
    public partial class FormDoiMatKhau : Form
    {
        public FormDoiMatKhau()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;

        private void btnLuu_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT count(*) from TblNhanVien where sMaNV = '" + txtTenTK.Text + "' and sPassWord = '" + txtMKC.Text + "'", constr);
            DataTable dt = new DataTable();
            da.Fill(dt);
            errorProvider1.Clear();
            if (dt.Rows[0][0].ToString() == "1")
            {
                if(txtMKM.Text == txtNLMKM.Text)
                {
                    if(txtMKM.Text.Length >= 6)
                    {
                        SqlDataAdapter da1 = new SqlDataAdapter("UPDATE TblNhanVien set sPassWord = '" + txtNLMKM.Text + "' where sMaNV ='" + txtTenTK.Text + "'", constr);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        MessageBox.Show("Đổi mật khẩu thành công", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        errorProvider1.SetError(txtMKM, "Độ dài chưa đủ");
                    }

                }
                else
                {
                   // errorProvider1.SetError(txtMKM, "Bạn chưa điền mật khẩu");
                    errorProvider1.SetError(txtNLMKM, "Mật khẩu nhập lại chưa đúng");

                }
            }
            else
            {
                errorProvider1.SetError(txtTenTK, "Sai mã đăng nhập ");
                errorProvider1.SetError(txtMKC, "Mật khẩu cũ chưa đúng");

            }



        }

    }
}
