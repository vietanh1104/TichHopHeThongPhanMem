using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace De3
{
    public partial class Form1 : Form
    {
        public DataUtils dataUtils;
        public Form1()
        {
            InitializeComponent();
            dataUtils = new DataUtils();
            dataUtils.LoadFile(dataGridView1);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var sach = new Sach(masachTxtBox.Text, tensachTxtBox.Text, int.Parse(soTrangTxtBox.Text), 
                hoTenTgTxtBox.Text, diaChiTgTxtBox.Text);
            dataUtils.Add(sach);
            dataUtils.LoadFile(dataGridView1);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            dataUtils.Remove(masachTxtBox.Text);
            dataUtils.LoadFile(dataGridView1);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            dataUtils.Find(startTxtBox.Text, finishTxtBox.Text, dataGridView1);
        }
    }
}
