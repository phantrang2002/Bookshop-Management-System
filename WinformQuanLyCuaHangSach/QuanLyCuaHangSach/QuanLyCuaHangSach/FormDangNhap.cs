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
    public partial class FormDangNhap : Form
    {
        int dem;
        public FormDangNhap()
        {
            InitializeComponent();
            
        }

        public void SetTimeout(Action action, int timeout)
        {
            var timer = new Timer();
            timer.Interval = timeout;
            timer.Tick += (s, e) =>
            {
                action();
                timer.Stop();
            };
            timer.Start();
        }

        



        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn thoát chương trình không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;
            SqlConnection cnn = new SqlConnection(constr);
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tblNhanVien WHERE sMaNV = N'" + txtTaiKhoan.Text + "' AND sPassWord = N'" + txtMatKhau.Text + "'",cnn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                MessageBox.Show("Đăng nhập thành công", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();

                FormTrangChu f = new FormTrangChu(dt.Rows[0][0].ToString(),dt.Rows[0][1].ToString(), dt.Rows[0][6].ToString(), Convert.ToBoolean(dt.Rows[0][7]));
                // FormQLNV fn = new FormQLNV(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][6].ToString(), Convert.ToBoolean((bool)dt.Rows[0][7] ? 1 : 0));
                FormQLNV fn = new FormQLNV(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][6].ToString(), Convert.ToBoolean(dt.Rows[0][7]));

                f.Show();
            }
            else
            {
               // dem++;
                MessageBox.Show("Vui lòng điền đúng thông tin đăng nhập");
               /* if (dem == 3)
                {
                    MessageBox.Show("Đăng nhập sai quá 3 lần, tài khoản đã bị khóa", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnDangNhap.Enabled = false;
                }*/
            }
        }

        private void FormDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn thoát chương trình không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        /* private void FormDangNhap_Load(object sender, EventArgs e)
         {

                 var action = new Action(() => {

                     if (DialogResult.OK == MessageBox.Show("Hết thời gian 5s", "Thông báo"))
                     {
                         this.Close();

                     }
                     else
                     {
                         // OK wasn't pressed
                     }
                 });
             SetTimeout(action, 5000);



         }*/
    }
}
