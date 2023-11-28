namespace BaiTH8
{
    public class NhanVien
    {
        public string manv { get; set; }    
        public string hoten { get; set; }    
        public string tuoi { get; set; }    
        public string luong { get; set; }
        public string xa { get; set; }
        public string huyen { get; set; }
        public string tinh { get; set; }
        public string dienthoai { get; set; }

        // constructor with parameters
        // optional
        public NhanVien(string manv, string hoten, string tuoi, string luong, string xa, string huyen, string tinh, string dienthoai)
        {
            this.manv = manv;
            this.hoten = hoten;
            this.tuoi = tuoi;
            this.luong = luong;
            this.xa = xa;
            this.huyen = huyen;
            this.tinh = tinh;
            this.dienthoai = dienthoai;
        }
        // constructor without parameter
        // required
        public NhanVien()
        {

        }
    }

}
