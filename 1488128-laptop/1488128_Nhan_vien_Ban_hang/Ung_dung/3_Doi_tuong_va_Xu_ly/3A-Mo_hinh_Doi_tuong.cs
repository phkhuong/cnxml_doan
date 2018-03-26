using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;


public class XL_NGUOI_DUNG_DANG_NHAP
{
    public string Ho_ten, Ma_so = "";
    public List<XmlElement> Danh_sach_Lap_top = new List<XmlElement>();
    public List<XmlElement> Danh_sach_Nhom_Lap_top = new List<XmlElement>();

    public string Thong_bao = "";
    public long Doanh_thu = 0L;
    public List<XmlElement> Danh_sach_Lap_top_Xem = new List<XmlElement>();

}