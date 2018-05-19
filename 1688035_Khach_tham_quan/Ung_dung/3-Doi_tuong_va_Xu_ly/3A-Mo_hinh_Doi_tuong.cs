using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;



#region "********Dữ liệu của Phân hệ Khách Tham quan ***********"

//###############
// Du_lieu:
//   Cong_ty:Ma_so,Ten
//     Danh_sach_Nhom_Lap_top:
//       * Nhom_Lap_top:Ma_so,Ten
//     Danh_sach_Mau_sac:
//       * Mau_sac:Ma_so,Ten
//     Danh_sach_Tinh_nang:
//       * Tinh_nang:Ma_so,Ten
//     Danh_sach_CPU_Series:
//       * CPU_Series:Ma_so,Ten
//     Danh_sach_Card_Man_hinh_Ngoai:
//       * Card_Man_hinh_Ngoai:Ma_so,Ten
//     Danh_sach_Chuan_Man_hinh:
//       * Chuan_Man_hinh:Ma_so,Ten
//     Danh_sach_Kich_thuoc_Man_hinh:
//       * Kich_thuoc_Man_hinh:Ma_so,Ten
//     Danh_sach_Dung_luong_Ram:
//       * Dung_luong_Ram:Ma_so,Ten
//     Danh_sach_HDD:
//       * HDD:Ma_so,Ten
//     Danh_sach_SDD:
//       * SSD:Ma_so,Ten
//     Danh_sach_Cua_hang:
//       * Cua_hang:Ma_so,Ten, Dia_chi, So_dien_thoai
//   Danh_sach_Laptop: 
//      * Laptop :Ma_so,Ten,Don_gia_Ban,CPU,Loai_Ram,Card_Man_hinh_Onboard,He_dieu_hanh,Trong_luong,Bao_hanh, So_luong_Ton
//          Nhom_Lap_top:Ma_so,Ten
//          Tinh_nang:Ma_so,Ten
//          CPU_Series:Ma_so,Ten
//          Chuan_Man_hinh:Ma_so,Ten
//          Mau_sac:Ma_so,Ten
//          Kich_thuoc_Man_hinh:Ma_so,Ten
//          Dung_luong_Ram:Ma_so,Ten
//          Card_Man_hinh_Ngoai:Ma_so,Ten
//          HDD:Ma_so,Ten
//          SSD:Ma_so,Ten
#endregion

public class XL_KHACH_THAM_QUAN
{
    public List<XmlElement> Danh_sach_Laptop = new List<XmlElement>();
    public List<XmlElement> Danh_sach_Nhom_Laptop = new List<XmlElement>();
    public List<XmlElement> Danh_sach_Mau_sac = new List<XmlElement>();
    public List<XmlElement> Danh_sach_Tinh_nang = new List<XmlElement>();
    public List<XmlElement> Danh_sach_CPU_Series = new List<XmlElement>();
    public List<XmlElement> Danh_sach_Card_Man_hinh_Ngoai = new List<XmlElement>();
    public List<XmlElement> Danh_sach_Chuan_Man_hinh = new List<XmlElement>();
    public List<XmlElement> Danh_sach_Kich_thuoc_Man_hinh = new List<XmlElement>();
    public List<XmlElement> Danh_sach_Dung_luong_Ram = new List<XmlElement>();
    public List<XmlElement> Danh_sach_HDD = new List<XmlElement>();
    public List<XmlElement> Danh_sach_SSD = new List<XmlElement>();
    public string Thong_bao = "";
    public List<XmlElement> Danh_sach_Laptop_Xem = new List<XmlElement>();
    public List<XmlElement> Danh_sach_Laptop_Chon = new List<XmlElement>();
    public string Ma_so_Laptop_chon;
    public string Ho_ten;
    public string Dia_chi;
    public string So_Dien_thoai;


    public long Tinh_tong_tien()
    {
        long kq = 0L;
        Danh_sach_Laptop_Chon.ForEach(Laptop =>
        {
            long Don_gia_Ban = long.Parse(Laptop.GetAttribute("Don_gia_Ban"));
            int So_luong = int.Parse(Laptop.GetAttribute("So_luong"));
            kq += Don_gia_Ban * So_luong;
        });
        return kq;
    }

    public void Update_So_luong_ton()
    {
        Danh_sach_Laptop_Chon.ForEach(Laptop_chon =>
        {
            string Ma_so = Laptop_chon.GetAttribute("Ma_so");
            var Laptop = XL_NGHIEP_VU.Tim_Laptop(Ma_so, Danh_sach_Laptop);
            int So_luong_ton = int.Parse(Laptop.GetAttribute("So_luong_ton"));
            int So_luong = int.Parse(Laptop_chon.GetAttribute("So_luong"));
            So_luong_ton -= So_luong;
            Laptop.SetAttribute("So_luong_ton", So_luong_ton.ToString());
        });
    }

}