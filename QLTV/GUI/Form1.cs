using QLTV.DTO;
using QLTV.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLTV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadCBBTheLoai();
            LoadBook();
        }
        void LoadCBBTheLoai()
        {
            cbbTheLoai.Items.Add(new CBBItems
            {
                Name = "Tất cả",
                Value = "0"
            });
            var classes = BLL.BLL.Instance.GetGenreList();
            foreach (var c in classes)
            {
                cbbTheLoai.Items.Add(new CBBItems
                {
                    Name = c.Ten,
                    Value = c.TheLoai_ID
                });
            }
            cbbTheLoai.SelectedIndex = 0;
        }
        void LoadBook()
        {
            string bookName = tbSearch.Text;
            string theloaiID = ((CBBItems)cbbTheLoai.SelectedItem).Value;
            dataGridView1.DataSource = BLL.BLL.Instance.GetBooks(theloaiID, bookName);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbbTheLoai.SelectedIndex = 0;
            tbSearch.Text = "";
            LoadBook();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string input = tbSearch.Text.Trim().ToLower();
            string theloaiID = ((CBBItems)cbbTheLoai.SelectedItem).Value;

            List<BookView> books = new List<BookView>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                BookView book = new BookView
                {
                    MaSach = row.Cells["MaSach"].Value?.ToString(),
                    Ten = row.Cells["Ten"].Value?.ToString(),
                    TheLoai = row.Cells["TheLoai"].Value?.ToString(),
                    NgayXuatBan = row.Cells["NgayXuatBan"].Value != null ? Convert.ToDateTime(row.Cells["NgayXuatBan"].Value) : DateTime.MinValue,
                    TrangThai = row.Cells["Trangthai"].Value != null && Convert.ToBoolean(row.Cells["Trangthai"].Value)
                };
                books.Add(book);
            }

            var filteredBooks = books.Where(b =>
                (b.Ten != null && b.Ten.ToLower().Contains(input)) &&
                (theloaiID == "0" || b.TheLoai == theloaiID)).ToList();

            dataGridView1.DataSource = filteredBooks;
        }


        private void cbbTheLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBook();
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            List<BookView> books = new List<BookView>();

            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                BookView book = new BookView
                {
                    MaSach = dr.Cells["MaSach"].Value.ToString(),
                    Ten = dr.Cells["Ten"].Value.ToString(),
                    TheLoai = dr.Cells["TheLoai"].Value.ToString(),
                    NgayXuatBan = dr.Cells["NgayXuatBan"].Value != null ? Convert.ToDateTime(dr.Cells["NgayXuatBan"].Value) : DateTime.MinValue,
                    TrangThai = Convert.ToBoolean(dr.Cells["Trangthai"].Value)
                };

                books.Add(book);
            }

            dataGridView1.DataSource = BLL.BLL.Instance.Sort(books);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Chưa có dòng nào được chọn");
                return;
            }
            else
            {
                DialogResult d = MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi này?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                switch (d)
                {
                    case DialogResult.Yes:
                        List<string> IDs = new List<string>();
                        foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
                        {
                            IDs.Add(dr.Cells["MaSach"].Value.ToString());
                        }
                        if (BLL.BLL.Instance.DeleteBooks(IDs))
                            MessageBox.Show("Xóa thành công!");
                        else
                            MessageBox.Show("Có lỗi xảy ra, vui lòng thử lại sau!");
                        break;
                    case DialogResult.No:
                        return;
                }
            }

            LoadBook();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.ShowDialog();
            this.Dispose();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string ID = dataGridView1.SelectedRows[0].Cells["MaSach"].Value.ToString();
            this.Hide();
            Form2 f2 = new Form2();
            f2.Sender(ID);
            f2.ShowDialog();
            this.Dispose();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cbbTheLoai.SelectedIndex = 0;
            tbSearch.Text = "";
            LoadBook();
        }
    }
}
