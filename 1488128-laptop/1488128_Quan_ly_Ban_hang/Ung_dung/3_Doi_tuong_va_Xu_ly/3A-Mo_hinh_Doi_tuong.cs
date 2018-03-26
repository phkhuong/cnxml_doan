using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
#region "********Nhân viên Nhập hàng ***********"
// Du_lieu:
//    Danh_sach_Dien_thoai: 
//      * Dien_thoai :Ma_so,Ten,Don_gia_Ban,Don_gia_Nhap,So_luong_Ton,Doanh_thu
//          Nhom_Dien_thoai:Ma_so,Ten
//   Cua_hang:Ma_so,Ten
//     Danh_sach_Nhom_Dien_thoai:
//       * Nhom_Dien_thoai:Ma_so,Ten,So_luong_Ton,Doanh_thu
//     Danh_sach_Nguoi_dung:(Chỉ có các Nhân viên Nhập hàng )
//       * Nguoi_dung :Ma_so,Ho_ten,Ten_Dang_nhap,Mat_khau
//          Nhom_Nguoi_dung:Ma_so,Ten
//          Danh_sach_Nhom_Dien_thoai:
//              * Nhom_Dien_thoai:Ma_so,Ten

#endregion

public class XL_NGUOI_DUNG_DANG_NHAP
{
    public string Ho_ten, Ma_so = "";
    public List<XmlElement> Danh_sach_Lap_top = new List<XmlElement>();
    public List<XmlElement> Danh_sach_Nhom_Lap_top = new List<XmlElement>();
    public List<XmlElement> Danh_sach_Nhan_vien_Ban_hang = new List<XmlElement>();
    public string Thong_bao = "";
    public List<XmlElement> Danh_sach_Lap_top_Xem = new List<XmlElement>();

}
