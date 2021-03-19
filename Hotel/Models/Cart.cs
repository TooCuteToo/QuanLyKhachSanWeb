using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Cart
    {
        public List<Room> items;

        public Cart()
        {
            items = new List<Room>();
        }

        public void add(Room item)
        {
            Room phong = items.FirstOrDefault(room => room.tenPhong == item.tenPhong);

            if (phong == null)
            {
                items.Add(item);
                return;
            }

            return;

        }

        public void delete(Room item)
        {
            Room phong = items.FirstOrDefault(room => room.tenPhong == item.tenPhong);

            items.Remove(phong);
        }

        public int count()
        {
            if (items == null)
            {
                return 0;
            }

            return items.Count;
        }

        public decimal? sumMoney()
        {
            if (items == null)
            {
                return 0;
            }

            return items.Sum(item =>
            {
                decimal? sum = 0;
                var totalDays = item.ngayTra.Subtract(item.ngayDat).Days;

                if (item.giamGia == null)
                {
                    sum += totalDays * item.giaPhong;
                }
                else
                {
                    sum += totalDays * item.giamGia;
                }

                return sum;
            });
        }

        public decimal? sumDiscountMoney()
        {
            if (items == null)
            {
                return 0;
            }

            return items.Sum(item => {
                decimal? sum = 0;

                if (item.giamGia != null)
                {
                    var totalDays = item.ngayTra.Subtract(item.ngayDat).Days;
                    sum += (item.giaPhong - item.giamGia) * totalDays;
                }

                return sum;
            });
        }

    }
}