using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Room
    {
        public double? danhGia { get; set; }
        public string hinhAnh { get; set; }

        public string maLoai { get; set; }
        public string tenPhong { get; set; }

        public decimal? giamGia { get; set; }
        public decimal? giaPhong { get; set; }

        public string tenLP { get; set; }

        public DateTime ngayDat { get; set; }
        public DateTime ngayTra { get; set; }

        public bool inStock { get; set; }

        public Room() { }

        public Room(string tenPhong)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var tList = from ph in db.Phongs
                            join lp in db.LoaiPhongs
                            on ph.maLoai equals lp.maLoai
                            select new
                            {
                                danhGia = ph.danhGia,
                                hinhAnh = ph.hinhAnh,
                                maLoai = ph.maLoai,
                                giamGia = ph.giamGia,
                                giaPhong = lp.giaPhong,
                                tenLP = lp.tenLP,
                                tenPhong = ph.tenPhong,
                            };

                var result = tList.FirstOrDefault(item => item.tenPhong == tenPhong);

                this.danhGia = result.danhGia;
                this.hinhAnh = result.hinhAnh;

                this.maLoai = result.maLoai;
                this.giamGia = result.giamGia;

                this.giaPhong = result.giaPhong;
                this.tenLP = result.tenLP;

                this.tenPhong = result.tenPhong;
            }
        }
    }
}