using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public partial class FormTrangChu : Form
    {
        string constr = ConfigurationManager.ConnectionStrings["QuanLyCuaHangSach"].ConnectionString;
        string maNV = "", tenNV = "", passWord = "";
        bool maQuyen = false;
       // Timer timer1;


        private void quảnLýSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormQLS fs = new FormQLS();
            fs.MdiParent = this;
            fs.Show();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn thoát chương trình không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
                FormDangNhap dn = new FormDangNhap();
                dn.ShowDialog();
            }
        }

        private void quảnLýNXBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNXB fx = new FormNXB();
            fx.MdiParent = this;
            fx.Show();
        }

        private void quảnLýKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormKH fk = new FormKH();
            fk.MdiParent = this;
            fk.Show();

        }

        private void FormTrangChu_Load(object sender, EventArgs e)
        {

        }

        private void quảnLýHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHD fn = new FormHD();
            fn.MdiParent = this;
            fn.Show();
        }

        private void báoCáoDSSáchTheoNXBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaoCaoNXB nx = new BaoCaoNXB();
            nx.MdiParent = this;
            nx.Show();
        }

        private void thốngKêSốLượngSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaoCaoSL nx = new BaoCaoSL();
            nx.MdiParent = this;
            nx.Show();
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
             
            FormDoiMatKhau mk = new FormDoiMatKhau();
            mk.MdiParent = this;
            mk.Show();
        }

        /*
        private void thànhTiềnTheoMãToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaoCaoThanhTien nx = new BaoCaoThanhTien();
            nx.MdiParent = this;
            nx.Show();
        }*/

        private void quảnLýNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormQLNV fn = new FormQLNV(maNV, tenNV, passWord,  maQuyen);
            fn.MdiParent = this;
            fn.Show();
        }

        public FormTrangChu(string maNV, string tenNV, string passWord, bool maQuyen)
        {
            InitializeComponent();
            this.maNV = maNV;
            this.tenNV = tenNV;
            this.passWord = passWord;
            this.maQuyen = maQuyen;

            if (maQuyen == false)
            {
                /*quảnLýNhânViênToolStripMenuItem.Enabled = false;*/
            }

           /* timer1 = new Timer();
            timer1.Tick += timer1_Tick;
            timer1.Interval = 1000;
            timer1.Enabled = !timer1.Enabled;*/
        }

      //  int i = 0;
       /* private void timer1_Tick(object sender, EventArgs e)
        {
            i++;
            label1.Text = i.ToString();
        }*/


    }
}
