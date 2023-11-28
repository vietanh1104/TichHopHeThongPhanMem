using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BaiTH8
{
    public partial class Form1 : Form
    {
        private DataUtils dataUtils;
        public Form1()
        {
            InitializeComponent();
            dataUtils = new DataUtils();
            LoadFile();
        }
        // load data from file
        private void LoadFile()
        {
            dataUtils.LoadFile(dataGridView1);
            sumLabel.Text = dataUtils.sumOfSalary.ToString();
            quantityLabel.Text = dataUtils.salaryOver1000Quantity.ToString();
        }

        // click add button
        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateInput();

                // create new NhanVien object
                var nv = new NhanVien();
                nv.manv = manvTxtBox.Text;
                nv.hoten = hotenTxtBox.Text;
                nv.tuoi = tuoiTxtBox.Text;
                nv.luong = luongTxtBox.Text;
                nv.xa = xaTxtBox.Text;
                nv.huyen = huyenTxtBox.Text;
                nv.tinh = tinhTxtBox.Text;
                nv.dienthoai = dienThoaiTxtBox.Text;

                // add to file
                dataUtils.Add(nv);
                LoadFile();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        // click update button
        private void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateInput();

                var nhanVien = new NhanVien(manvTxtBox.Text, hotenTxtBox.Text, tinhTxtBox.Text, luongTxtBox.Text,
                    xaTxtBox.Text, huyenTxtBox.Text, tinhTxtBox.Text, dienThoaiTxtBox.Text);

                dataUtils.Update(nhanVien);
                LoadFile();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        // click delete button
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                dataUtils.Delete(manvTxtBox.Text);
                LoadFile();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        // click clear button
        private void btn_Clear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }
        
        // click load file button
        private void btn_LoadFile_Click(object sender, EventArgs e)
        {
            LoadFile();
        }
           
        // find by id
        private void btn_Find_Click(object sender, EventArgs e)
        {
            try
            {
                dataUtils.FindByID(manvTxtBox.Text, dataGridView1);
                ClearInputs();
            }
            catch(Exception ex)
            { 
                MessageBox.Show(ex.Message);
                return;
            }
        }

        // find by "tinh"
        private void btn_FindByProvince_Click(object sender, EventArgs e)
        {
            try
            {
                dataUtils.FindByProvice(tinhTxtBox.Text, dataGridView1);
                ClearInputs();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return;
            }
        }

        // click close button
        private void btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        // load selected cell data to text box
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var selectedRow = dataGridView1.CurrentRow;
            manvTxtBox.Text = ConvertToString(selectedRow.Cells[0].Value);
            hotenTxtBox.Text = ConvertToString(selectedRow.Cells[1].Value);
            tuoiTxtBox.Text = ConvertToString(selectedRow.Cells[2].Value);
            luongTxtBox.Text = ConvertToString(selectedRow.Cells[3].Value);
            xaTxtBox.Text = ConvertToString(selectedRow.Cells[4].Value);
            huyenTxtBox.Text = ConvertToString(selectedRow.Cells[5].Value);
            tinhTxtBox.Text = ConvertToString(selectedRow.Cells[6].Value);
            dienThoaiTxtBox.Text = ConvertToString(selectedRow.Cells[7].Value);
        }

        // convert cell value to string
        private string ConvertToString(object o)
        {
            return Convert.ToString(o);
        }

        // clear input function
        private void ClearInputs()
        {
            manvTxtBox.Clear();
            hotenTxtBox.Clear();
            tuoiTxtBox.Clear();
            luongTxtBox.Clear();
            xaTxtBox.Clear();
            huyenTxtBox.Clear();
            tinhTxtBox.Clear();
            dienThoaiTxtBox.Clear();
            ActiveControl = manvTxtBox;
        }

        // input validating function
        private void ValidateInput()
        {
            string idRegex = @"[a-zA-Z]{2}[0-9]{1,}";
            string nameRegex = @"[a-zA-Z]{1,}";
            string addressRegex = @"[a-zA-Z0-9]{1,}";
            string phoneNumRegex = @"0[0-9]{9}";

            string errorMessage = String.Empty;

            if(!Regex.IsMatch(manvTxtBox.Text, idRegex))
            {
                errorMessage += "Id phải có dạng: aa0\n";
            }

            if (!Regex.IsMatch(hotenTxtBox.Text, nameRegex))
            {
                errorMessage += "Tên phải bao gồm các kí tự chữ\n";
            }

            if (!int.TryParse(tuoiTxtBox.Text, out int age) || age < 18)
            {
                errorMessage += "Tuổi phải lớn hơn 18\n";
            }

            if (!double.TryParse(luongTxtBox.Text, out double luong) || luong < 0)
            {
                errorMessage += "Lương phải lớn hơn hoặc bằng 0\n";
            }

            if (!Regex.IsMatch(xaTxtBox.Text, addressRegex)
                || !Regex.IsMatch(huyenTxtBox.Text, addressRegex)
                || !Regex.IsMatch(tinhTxtBox.Text, addressRegex)
                )
            {
                errorMessage += "Địa chỉ phải được nhập\n";
            }

            if (!Regex.IsMatch(dienThoaiTxtBox.Text, phoneNumRegex))
            {
                errorMessage += "Số điện thoại phải bao gồm 10 số và bắt đầu bằng số 0.\n";
            }

            if (errorMessage.Length > 0)
            {
                throw new Exception(errorMessage);
            }

            return;

        }

    }
}
