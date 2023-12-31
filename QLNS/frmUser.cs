﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNS
{
    public partial class frmUser : Form
    {
        public frmUser()
        {
            InitializeComponent();
        }
        public Boolean KTThongTin()
        {
            if(txtTaiKhoan.Text=="")
            {
                MessageBox.Show("Bạn chưa nhập tên đăng nhập", "THÔNG BÁO");
                txtTaiKhoan.Focus();
                return false;
            }
            else if (txtMatKhau.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu", "THÔNG BÁO");
                txtMatKhau.Focus();
                return false;
            }
            else if(cmbLoaiTaiKhoan.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn loại tài khoản", "THÔNG BÁO");
                return false;
            }
            return true;
        }

        public void loadDataGirdView()
        {
            string query = "select TenDangNhap, MatKhau, PhanQuyen from TaiKhoan";
            dtgvDSTK.DataSource = DataProvider.Instance.ExcuteQuery(query);
        }

        void SetHeaderText()
        {
            //đặt tên cột
            dtgvDSTK.Columns["TenDangNhap"].HeaderText = "Tên đăng nhập";
            dtgvDSTK.Columns["MatKhau"].HeaderText = "Mật khẩu";
            dtgvDSTK.Columns["PhanQuyen"].HeaderText = "Quyền";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string insert = "insert into TaiKhoan values(N'" + txtTaiKhoan.Text + "',N'" + txtMatKhau.Text + "')";
            if (DataProvider.Instance.ExcuteQuery("select TenDangNhap from TaiKhoan").ToString() != txtTaiKhoan.Text)
            {
                if(KTThongTin())
                {
                    DataProvider.Instance.ExcuteNonQuery(insert);
                    dtgvDSTK.Refresh();
                    loadDataGirdView();
                    MessageBox.Show("Thêm thành công");
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string update = "update TaiKhoan set TenDangNhap = N'" + txtTaiKhoan.Text + "',MatKhau=N'" + txtMatKhau.Text + "',PhanQuyen = N'" + cmbLoaiTaiKhoan.Text + "'";
            if (DataProvider.Instance.ExcuteQuery("select TenDangNhap from TaiKhoan").ToString() == txtTaiKhoan.Text)
            {
                if (KTThongTin())
                {
                    DataProvider.Instance.ExcuteNonQuery(update);
                    dtgvDSTK.Refresh();
                    loadDataGirdView();
                    MessageBox.Show("Sửa thành công");
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string delete = "delete from TaiKhoan where TenDangNhap = '" + txtTaiKhoan.Text + "'";
            if (MessageBox.Show("Bạn có muốn xóa không", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                DataProvider.Instance.ExcuteNonQuery(delete);
                dtgvDSTK.Refresh();
                loadDataGirdView();
                MessageBox.Show("Đã xóa dữ liệu");
            }
            if (DataProvider.Instance.ExcuteQuery("select TenDangNhap from TaiKhoan").ToString() != txtTaiKhoan.Text)
                MessageBox.Show("Tài Khoản không tồn tại, không thể xóa");
        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            loadDataGirdView();
            SetHeaderText();
        }
    }
}
