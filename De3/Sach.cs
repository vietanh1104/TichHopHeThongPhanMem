namespace De3
{
    public class Sach
    {
        public string masach { get; set; }
        public string tensach { get; set; }
        public int sotrang { get; set; }
        public string hoten { get; set; }
        public string diachi { get; set; }
        public Sach()
        {

        }
        public Sach(string masach, string tensach, int sotrang, string hotenTg, string diaChiTg)
        {
            this.masach = masach;
            this.tensach = tensach;
            this.sotrang = sotrang;
            this.hoten = hotenTg;
            this.diachi = diaChiTg;
        }
    }
}
