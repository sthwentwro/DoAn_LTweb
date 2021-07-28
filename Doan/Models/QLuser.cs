using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Doan.Models
{
    public class QLuser
    {
        DataShopDataContext db = new DataShopDataContext();
        public int IDUser { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Hoten { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<QLuser> dsUser()
        {
            var model = from a in db.Nguoidungs
                        join b in db.Roles on
                        a.RoleID equals b.RoleID
                        where a.RoleID == b.RoleID
                        where a.RoleID == 2
                        select new QLuser()
                        {
                            IDUser = a.IDnguoidung,
                            Username = a.Username,
                            Password = a.Password,
                            Hoten = a.HoTen,
                            Role = b.RoleName,
                            Phone = a.SDT,
                            Address = a.Diachi
                        };
            return model.ToList();
        }
        public Nguoidung ViewDetail(int IDCode)
        {
            return db.Nguoidungs.Where(a=>a.IDnguoidung==IDCode).FirstOrDefault();
        }
        public bool UpdateUser(Nguoidung entity)
        {
            try
            {
                var user = db.Nguoidungs.Where(a => a.IDnguoidung == entity.IDnguoidung).FirstOrDefault();
                user.SDT = entity.SDT;
                user.Username = entity.Username.Trim();
                user.Diachi = entity.Diachi.Trim();
                user.HoTen = entity.HoTen.Trim();
                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}