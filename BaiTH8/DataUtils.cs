using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace BaiTH8
{
    public class DataUtils
    {
        XmlDocument doc;
        XmlElement root;
        string fileName = "congty.xml";
        public long sumOfSalary; // sum of staff's salary who has salary over 1000;
        public int salaryOver1000Quantity; // amount people that have salary more than 1000; 
        public DataUtils()
        {
            try
            {
                doc = new XmlDocument();
                // if file isnot existed, create new file
                if (!File.Exists(fileName))
                {
                    // create "congty" element, add to doc
                    XmlElement congty = doc.CreateElement("congty");
                    doc.AppendChild(congty);
                    doc.Save(fileName);
                }
                // load to data to doc
                doc.Load(fileName);
                root = doc.DocumentElement; 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // load data from file to DataGridView
        public void LoadFile(DataGridView dg)
        {
            // clear all rows
            dg.Rows.Clear();

            // load data from xml file to datagridview
            int index = 0; // started index
            XmlNodeList xmlNodeList = root.SelectNodes("nhanvien"); // get all "nhanvien" nodes
            salaryOver1000Quantity = 0;
            sumOfSalary = 0;
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                dg.Rows.Add();
                dg.Rows[index].Cells[0].Value = xmlNode.Attributes[0].InnerText;
                dg.Rows[index].Cells[1].Value = xmlNode.SelectSingleNode("hoten").InnerText;
                dg.Rows[index].Cells[2].Value = xmlNode.SelectSingleNode("tuoi").InnerText;
                dg.Rows[index].Cells[3].Value = xmlNode.SelectSingleNode("luong").InnerText;
                dg.Rows[index].Cells[4].Value = xmlNode.SelectSingleNode("diachi/xa").InnerText;
                dg.Rows[index].Cells[5].Value = xmlNode.SelectSingleNode("diachi/huyen").InnerText;
                dg.Rows[index].Cells[6].Value = xmlNode.SelectSingleNode("diachi/tinh").InnerText;
                dg.Rows[index].Cells[7].Value = xmlNode.SelectSingleNode("dienthoai").InnerText;
                index++;

                if(long.TryParse(xmlNode.SelectSingleNode("luong").InnerText, out long luong) && luong > 1000)
                {
                    sumOfSalary += luong;
                    salaryOver1000Quantity++;
                }
            }
        }

        // add nhanvien to file
        public void Add(NhanVien nv)
        {
            // find the existing NhanVien
            var nhanvienCanSua = root.SelectSingleNode("nhanvien[@manv='" + nv.manv + "']");
            if (nhanvienCanSua != null)
            {
                throw new Exception($"Mã nhân viên = \"{nv.manv}\" đã tồn tại.");
            }
            XmlElement nhanvien = doc.CreateElement("nhanvien");
            nhanvien.SetAttribute("manv", nv.manv);

            XmlElement hoten = doc.CreateElement("hoten");
            hoten.InnerText = nv.hoten;

            XmlElement tuoi = doc.CreateElement("tuoi");
            tuoi.InnerText = nv.tuoi;

            XmlElement luong = doc.CreateElement("luong");
            luong.InnerText = nv.luong;

            XmlElement dienthoai = doc.CreateElement("dienthoai");
            dienthoai.InnerText = nv.dienthoai;

            XmlElement diachi = doc.CreateElement("diachi");

            XmlElement xa = doc.CreateElement("xa");
            xa.InnerText = nv.xa;

            XmlElement huyen = doc.CreateElement("huyen");
            huyen.InnerText = nv.huyen;

            XmlElement tinh = doc.CreateElement("tinh");
            tinh.InnerText = nv.tinh;

            // add elements to diachi node
            diachi.AppendChild(xa);
            diachi.AppendChild(huyen);
            diachi.AppendChild(tinh);

            // add elements to nhanvien node
            nhanvien.AppendChild(hoten);
            nhanvien.AppendChild(tuoi);
            nhanvien.AppendChild(luong);
            nhanvien.AppendChild(diachi);
            nhanvien.AppendChild(dienthoai);

            // add elements to root
            root.AppendChild(nhanvien);

            doc.AppendChild(root);
            doc.Save(fileName);
        }

        // update an existing NhanVien
        public void Update(NhanVien nv)
        {
            var nhanvienCanSua = root.SelectSingleNode("nhanvien[@manv='" + nv.manv + "']");
            if (nhanvienCanSua == null)
            {
                throw new Exception($"Không tìm thấy nhân viên với mã = \"{nv.manv}\"!");
            }
            XmlElement nhanvien = doc.CreateElement("nhanvien");
            nhanvien.SetAttribute("manv", nv.manv);

            XmlElement hoten = doc.CreateElement("hoten");
            hoten.InnerText = nv.hoten;

            XmlElement tuoi = doc.CreateElement("tuoi");
            tuoi.InnerText = nv.tuoi;

            XmlElement luong = doc.CreateElement("luong");
            luong.InnerText = nv.luong;

            XmlElement dienthoai = doc.CreateElement("dienthoai");
            dienthoai.InnerText = nv.dienthoai;

            XmlElement diachi = doc.CreateElement("diachi");

            XmlElement xa = doc.CreateElement("xa");
            xa.InnerText = nv.xa;

            XmlElement huyen = doc.CreateElement("huyen");
            huyen.InnerText = nv.huyen;

            XmlElement tinh = doc.CreateElement("tinh");
            tinh.InnerText = nv.tinh;

            nhanvien.AppendChild(hoten);
            nhanvien.AppendChild(tuoi);
            nhanvien.AppendChild(luong);
            nhanvien.AppendChild(diachi);
            nhanvien.AppendChild(dienthoai);

            diachi.AppendChild(xa);
            diachi.AppendChild(huyen);
            diachi.AppendChild(tinh);

            root.ReplaceChild(nhanvien, nhanvienCanSua);
            doc.Save(fileName);

        }

        // delete an existing NhanVien
        public void Delete(string manv)
        {
            var nhanvien = root.SelectSingleNode("nhanvien[@manv='" + manv + "']");
            if (nhanvien == null)
            {
                throw new Exception($"Không tìm thấy nhân viên với mã = \"{manv}\"!");
            }

            root.RemoveChild(nhanvien);
            doc.Save(fileName);
        }

        // find NhanVien by Id and return to DataGridView
        public void FindByID(string manv, DataGridView dg)
        {
            var nhanvien = root.SelectSingleNode("nhanvien[@manv='" + manv + "']");
            if(nhanvien == null)
            {
                throw new Exception($"Không tìm thấy nhân viên với mã = \"{manv}\"!");
            }

            dg.Rows.Clear();
            dg.Rows.Add();
            dg.Rows[0].Cells[0].Value = nhanvien.Attributes[0].InnerText;
            dg.Rows[0].Cells[1].Value = nhanvien.SelectSingleNode("hoten").InnerText;
            dg.Rows[0].Cells[2].Value = nhanvien.SelectSingleNode("tuoi").InnerText;
            dg.Rows[0].Cells[3].Value = nhanvien.SelectSingleNode("luong").InnerText;
            dg.Rows[0].Cells[4].Value = nhanvien.SelectSingleNode("diachi/xa").InnerText;
            dg.Rows[0].Cells[5].Value = nhanvien.SelectSingleNode("diachi/huyen").InnerText;
            dg.Rows[0].Cells[6].Value = nhanvien.SelectSingleNode("diachi/tinh").InnerText;
            dg.Rows[0].Cells[7].Value = nhanvien.SelectSingleNode("dienthoai").InnerText;
        }

        // find NhanVien by "tinh" and return to DataGridView
        public void FindByProvice(string tinh, DataGridView dg)
        {
            var nhanvien = root.SelectSingleNode("nhanvien[diachi/tinh='" + tinh + "']");
            if (nhanvien == null)
            {
                throw new Exception($"Không tìm thấy nhân viên với tỉnh = \"{tinh}\"!");
            }

            dg.Rows.Clear();
            dg.Rows.Add();
            dg.Rows[0].Cells[0].Value = nhanvien.Attributes[0].InnerText;
            dg.Rows[0].Cells[1].Value = nhanvien.SelectSingleNode("hoten").InnerText;
            dg.Rows[0].Cells[2].Value = nhanvien.SelectSingleNode("tuoi").InnerText;
            dg.Rows[0].Cells[3].Value = nhanvien.SelectSingleNode("luong").InnerText;
            dg.Rows[0].Cells[4].Value = nhanvien.SelectSingleNode("diachi/xa").InnerText;
            dg.Rows[0].Cells[5].Value = nhanvien.SelectSingleNode("diachi/huyen").InnerText;
            dg.Rows[0].Cells[6].Value = nhanvien.SelectSingleNode("diachi/tinh").InnerText;
            dg.Rows[0].Cells[7].Value = nhanvien.SelectSingleNode("dienthoai").InnerText;
        }
    }
}
