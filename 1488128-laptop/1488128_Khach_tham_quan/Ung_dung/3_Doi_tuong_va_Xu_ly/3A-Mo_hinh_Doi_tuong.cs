using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;


#region "********Dữ liệu của Khách Tham quan ***********"
// Du_lieu:
//   Cua_hang:Ma_so,Ten
//   Danh_sach_Dien_thoai: 
//      * Dien_thoai :Ma_so,Ten,Don_gia_Ban,Don_gia_Nhap, So_luong_Ton
#endregion


public class XL_KHACH_THAM_QUAN
{
    public List<XmlElement> Danh_sach_Lap_top= new List<XmlElement>();
    public List<XmlElement> Danh_sach_Nhom_Lap_top = new List<XmlElement>();

    public string Thong_bao = "";
    public List<XmlElement> Danh_sach_Lap_top_Xem = new List<XmlElement>();
    public List<XmlElement> Danh_sach_Lap_top_Chon = new List<XmlElement>();

}
