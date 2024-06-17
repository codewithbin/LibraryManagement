using QLTV.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace QLTV.UI
{
    public partial class Form2 : Form
    {
        Thread thread;
        public delegate void Send(string ID);
        public Send Sender;
        string ID;
        void GetID(string ID)
        {
            this.ID = ID;
        }
        public Form2()
        {
            Sender = new Send(GetID);
            InitializeComponent();
            LoadCBBTheLoai();
        }
        void LoadCBBTheLoai()
        {
            cbbTheLoai.Items.Add(new CBBItems
            {
                Name = "Chọn thể loại",
                Value = "0"
            });
            var classes1 = BLL.BLL.Instance.GetGenreList();
            foreach (var c in classes1)
            {
                cbbTheLoai.Items.Add(new CBBItems
                {
                    Name = c.Ten,
                    Value = c.TheLoai_ID
                });
            }
            cbbTheLoai.SelectedIndex = 0;
        }
        void RunMainForm(object sender)
        {
            Application.Run(new Form1());
        }
        void GoToMainForm()
        {
            this.Dispose();
            thread = new Thread(RunMainForm);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            GoToMainForm();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!IsValid())
            {
                return;
            }

            if (ID == null)
            {
                if (!BLL.BLL.Instance.IsExist(tbID.Text))
                {
                    MessageBox.Show("ID đã tồn tại!");
                    return;
                }
                if (BLL.BLL.Instance.AddBook(GetBook()))
                    MessageBox.Show("Thêm sách thành công!");
                else
                    MessageBox.Show("Có lỗi xảy ra, vui lòng thử lại sau!");
            }
            else
            {
                if (BLL.BLL.Instance.UpdateBook(GetBook()))
                    MessageBox.Show("Cập nhập sách thành công!");
                else
                    MessageBox.Show("Có lỗi xảy ra, vui lòng thử lại sau!");
            }

            //this.Close();
            GoToMainForm();
        }
        bool IsValid()
        {
            if (tbID.Text == "")
            {
                MessageBox.Show("Vui lòng nhập ID sách!");
                return false;
            }
            if (tbTen.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên sách!");
                return false;
            }
            if (cbbTheLoai.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn thể loại!");
                return false;
            }
            if (rbtnConsach.Checked == false && rbtnHetsach.Checked == false)
            {
                MessageBox.Show("Vui lòng chọn trạng thái!");
                return false;
            }
            return true;
        }
        Book GetBook()
        {
            Book book = new Book();
            book.Sach_ID = tbID.Text;
            book.Ten = tbTen.Text;
            book.TheLoai_ID = ((CBBItems)cbbTheLoai.SelectedItem).Value;
            book.NgayXuatBan = dateTimePicker1.Value;
            if (rbtnConsach.Checked)
                book.TrangThai = true;
            else
                book.TrangThai = false;
            return book;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (ID == null)
                return;
            FillInformation();
        }
        void FillInformation()
        {
            tbID.Enabled = false;
            Book book = BLL.BLL.Instance.GetBookByID(ID);
            tbID.Text = book.Sach_ID;
            tbTen.Text = book.Ten;
            cbbTheLoai.SelectedIndex = cbbTheLoai.FindStringExact(book.Genre.Ten);
            dateTimePicker1.Value = book.NgayXuatBan;
            if (book.TrangThai)
                rbtnConsach.Checked = true;
            else
                rbtnHetsach.Checked = true;
        }
    }
}
