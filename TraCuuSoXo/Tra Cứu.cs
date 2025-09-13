using System;
using System.Net.Http;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TraCuuSoXo
{
    public partial class TraCuu : Form
    {
        private readonly HttpClient _client = new HttpClient();

        // Custom controls
        private ComboBox regionComboBox;
        private DateTimePicker datePicker;
        private Button fetchButton;
        private TableLayoutPanel resultsTable;
        private RichTextBox statusTextBox;

        public TraCuu()
        {
            InitializeComponent();
        }

        private void btnXemKQ_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            dtpThoiGian.CustomFormat = "dd-MM-yyyy";
            string url = string.Empty;
            if (cboCacMien.Text == "Miền Bắc")
            {
                url = $"https://www.minhngoc.com.vn/ket-qua-xo-so/mien-bac/{dtpThoiGian.Text}.html";
            }
            else if (cboCacMien.Text == "Miền Nam")
            {
                url = $"https://www.minhngoc.com.vn/ket-qua-xo-so/mien-nam/{dtpThoiGian.Text}.html";
            }
            else if (cboCacMien.Text == "Miền Trung")
            {
                url = $"https://www.minhngoc.com.vn/ket-qua-xo-so/mien-trung/{dtpThoiGian.Text}.html";
            }
            var data = new ThongTin().LayKQXS(url);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = data;
        }

        private void TraCuu_Load(object sender, EventArgs e)
        {
            dtpThoiGian.Format = DateTimePickerFormat.Custom;
            dtpThoiGian.CustomFormat = "dd-MM-yyyy";
        }
    }
}
