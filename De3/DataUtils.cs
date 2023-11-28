using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace De3
{
    public class DataUtils
    {
        string filename = "thuvien.xml";
        XmlDocument doc;
        XmlElement root;
        public DataUtils()
        {
            doc = new XmlDocument();
            if (!File.Exists(filename))
            {
                XmlElement thuvien = doc.CreateElement("thuvien");
                doc.AppendChild(thuvien);
                doc.Save(filename);
            }
            doc.Load(filename);
            root = doc.DocumentElement;
        }
        // load data from file
        public void LoadFile(DataGridView dg)
        {
            var sachs = root.SelectNodes("sach");

            // add data to datagridview
            dg.Rows.Clear();
            int index = 0;
            foreach(XmlNode sach in sachs)
            {
                dg.Rows.Add();
                dg.Rows[index].Cells[0].Value = sach.Attributes[0].Value;
                dg.Rows[index].Cells[1].Value = sach.SelectSingleNode("tensach").InnerText;
                dg.Rows[index].Cells[2].Value = sach.SelectSingleNode("sotrang").InnerText;
                XmlNode tacgia = sach.SelectSingleNode("tacgia");
                dg.Rows[index].Cells[3].Value = tacgia.Attributes[0].Value;
                dg.Rows[index].Cells[4].Value = tacgia.SelectSingleNode("diachi").InnerText;
                index++;
            }
        }

        public void Add(Sach sach)
        {
            var existingSach = root.SelectSingleNode("sach[@masach='" + sach.masach + "']");

            if (existingSach != null)
            {
                MessageBox.Show("Trùng mã sách");
                return;
            }

            var newNode = doc.CreateElement("sach");
            newNode.SetAttribute("masach", sach.masach);

            var tensach = doc.CreateElement("tensach");
            tensach.InnerText = sach.tensach;

            var sotrang = doc.CreateElement("sotrang");
            sotrang.InnerText = sach.sotrang.ToString();

            var tacgia = doc.CreateElement("tacgia");
            tacgia.SetAttribute("hoten", sach.hoten);

            var diachi = doc.CreateElement("diachi");
            diachi.InnerText = sach.diachi;

            tacgia.AppendChild(diachi);

            newNode.AppendChild(tensach);
            newNode.AppendChild(sotrang);
            newNode.AppendChild(tacgia);
            

            root.AppendChild(newNode);
            doc.AppendChild(root);
            doc.Save(filename);
        }
        public void Remove(string masach)
        {
            var existingSach = root.SelectSingleNode("sach[@masach='" + masach + "']");

            if (existingSach == null)
            {
                MessageBox.Show("Không tìm thấy sách");
                return;
            }

            var dialog = MessageBox.Show("Bạn có chắc chắn muốn xóa không?","Xác nhận", MessageBoxButtons.YesNo);

            if(dialog == DialogResult.Yes)
            {
                root.RemoveChild(existingSach);
                doc.AppendChild(root);
                doc.Save(filename);
            }     
        }
        public void Find(string from, string to, DataGridView dg)
        {
            var existingSachs = root.SelectNodes("sach[sotrang >= " + from+ " and sotrang <= " + to+ "]");

            if (existingSachs == null || existingSachs.Count == 0)
            {
                MessageBox.Show("Không tìm thấy sách");
                return;
            }
            // add data to datagridview
            dg.Rows.Clear();
            int index = 0;
            foreach (XmlNode sach in existingSachs)
            {
                dg.Rows.Add();
                dg.Rows[index].Cells[0].Value = sach.Attributes[0].Value;
                dg.Rows[index].Cells[1].Value = sach.SelectSingleNode("tensach").InnerText;
                dg.Rows[index].Cells[2].Value = sach.SelectSingleNode("sotrang").InnerText;
                XmlNode tacgia = sach.SelectSingleNode("tacgia");
                dg.Rows[index].Cells[3].Value = tacgia.Attributes[0].Value;
                dg.Rows[index].Cells[4].Value = tacgia.SelectSingleNode("diachi").InnerText;
                index++;
            }
        }

    }
}
