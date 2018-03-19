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
//      * Laptop :Ma_so,Ten,Don_gia_Ban,CPU,Loai_Ram,Card_Man_hinh_Onboard,He_dieu_hanh,Trong_luong,Bao_hanh
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


public class XL_NGUOI_DUNG_DANG_NHAP
{
    public string Ho_ten, Ma_so = "";
    public List<XmlElement> Danh_sach_Laptop = new List<XmlElement>();
    public List<XmlElement> Danh_sach_Nhom_Laptop = new List<XmlElement>();

    public string Thong_bao = "";
    public List<XmlElement> Danh_sach_Laptop_Xem = new List<XmlElement>();

}