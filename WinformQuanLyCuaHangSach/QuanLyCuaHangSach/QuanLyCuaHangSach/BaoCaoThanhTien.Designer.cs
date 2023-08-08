
namespace QuanLyCuaHangSach
{
    partial class BaoCaoThanhTien
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.btnIn = new System.Windows.Forms.Button();
            this.txtMaHD = new System.Windows.Forms.TextBox();
            this.lblTenNXB = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Location = new System.Drawing.Point(30, 70);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(970, 462);
            this.crystalReportViewer1.TabIndex = 0;
            // 
            // btnIn
            // 
            this.btnIn.Location = new System.Drawing.Point(276, 21);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(132, 32);
            this.btnIn.TabIndex = 6;
            this.btnIn.Text = "In thành tiền";
            this.btnIn.UseVisualStyleBackColor = true;
            this.btnIn.Click += new System.EventHandler(this.btnIn_click);
            // 
            // txtMaHD
            // 
            this.txtMaHD.Location = new System.Drawing.Point(105, 26);
            this.txtMaHD.Name = "txtMaHD";
            this.txtMaHD.Size = new System.Drawing.Size(143, 22);
            this.txtMaHD.TabIndex = 5;
            // 
            // lblTenNXB
            // 
            this.lblTenNXB.AutoSize = true;
            this.lblTenNXB.Location = new System.Drawing.Point(34, 29);
            this.lblTenNXB.Name = "lblTenNXB";
            this.lblTenNXB.Size = new System.Drawing.Size(51, 17);
            this.lblTenNXB.TabIndex = 4;
            this.lblTenNXB.Text = "Mã HD";
            // 
            // BaoCaoThanhTien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 554);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.txtMaHD);
            this.Controls.Add(this.lblTenNXB);
            this.Controls.Add(this.crystalReportViewer1);
            this.Name = "BaoCaoThanhTien";
            this.Text = "BaoCaoThanhTien";
            this.Load += new System.EventHandler(this.BaoCaoThanhTien_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.TextBox txtMaHD;
        private System.Windows.Forms.Label lblTenNXB;
    }
}