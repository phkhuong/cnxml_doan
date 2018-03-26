using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.IO;
using System.Xml;
using System.Globalization;

public partial class XL_DICH_VU
{
    static XL_DICH_VU Dich_vu = null;

    XmlElement Du_lieu_Dich_vu = null;
    //========= Khởi động ======
    public static XL_DICH_VU Khoi_dong_Dich_vu()
    {
        if (Dich_vu == null)
        {
            Dich_vu = new XL_DICH_VU();
            Dich_vu.Khoi_dong_Du_lieu_cua_Dich_vu();
        }

        return Dich_vu;
    }
    void Khoi_dong_Du_lieu_cua_Dich_vu()
    {
        var Du_lieu_Luu_tru = XL_LUU_TRU.Doc_Du_lieu();
        var Chuoi_XML = Du_lieu_Luu_tru.OuterXml;
        var Tai_lieu = new XmlDocument();
        Tai_lieu.LoadXml(Chuoi_XML);
        Du_lieu_Dich_vu = Tai_lieu.DocumentElement;
        var Danh_sach_Lap_top = (XmlElement)Du_lieu_Dich_vu.GetElementsByTagName("Danh_sach_Lap_top")[0];
        var Cua_hang = (XmlElement)Du_lieu_Dich_vu.GetElementsByTagName("Cong_ty")[0];
        var Danh_sach_Nhom_Lap_top = (XmlElement)Cua_hang.GetElementsByTagName("Danh_sach_Nhom_Lap_top")[0];
        var Danh_sach_Nguoi_dung = (XmlElement)Cua_hang.GetElementsByTagName("Danh_sach_Nguoi_dung")[0];
        // ===================== Bổ sung thông tin   =============================== 
        foreach (XmlElement Lap_top in Danh_sach_Lap_top.GetElementsByTagName("Laptop"))
        {
            var So_luong_Ton = XL_NGHIEP_VU.Tinh_So_luong_Ton_Lap_top(Lap_top);
            Lap_top.SetAttribute("So_luong_Ton", So_luong_Ton.ToString());
            var Doanh_thu = XL_NGHIEP_VU.Tinh_Doanh_thu_Lap_top(Lap_top, DateTime.Today);
            Lap_top.SetAttribute("Doanh_thu", Doanh_thu.ToString());
        }

        foreach (XmlElement Nhom_Lap_top in Danh_sach_Nhom_Lap_top.GetElementsByTagName("Nhom_Lap_top"))
        {
            var Danh_sach_Lap_top_cua_Nhom_Lap_top = XL_NGHIEP_VU.Tao_Danh_sach_Lap_top_cua_Nhom_Lap_top(Nhom_Lap_top, Danh_sach_Lap_top);
            var So_luong_Ton = XL_NGHIEP_VU.Tinh_So_luong_Ton_Danh_sach_Lap_top(Danh_sach_Lap_top_cua_Nhom_Lap_top);
            Nhom_Lap_top.SetAttribute("So_luong_Ton", So_luong_Ton.ToString());
            var Doanh_thu = XL_NGHIEP_VU.Tinh_Doanh_thu_Danh_sach_Lap_top(Danh_sach_Lap_top_cua_Nhom_Lap_top, DateTime.Today);
            Nhom_Lap_top.SetAttribute("Doanh_thu", Doanh_thu.ToString());
        }
        foreach (XmlElement Nguoi_dung in Danh_sach_Nguoi_dung.GetElementsByTagName("Nguoi_dung"))
        {
            foreach (XmlElement Nhom_Lap_top in Nguoi_dung.SelectNodes("Danh_sach_Nhom_Lap_top/Nhom_Lap_top"))
            {
                var Danh_sach_Lap_top_cua_Nhom_Lap_top = XL_NGHIEP_VU.Tao_Danh_sach_Lap_top_cua_Nhom_Lap_top(Nhom_Lap_top, Danh_sach_Lap_top);
                var So_luong_Ton = XL_NGHIEP_VU.Tinh_So_luong_Ton_Danh_sach_Lap_top(Danh_sach_Lap_top_cua_Nhom_Lap_top);
                Nhom_Lap_top.SetAttribute("So_luong_Ton", So_luong_Ton.ToString());
                var Doanh_thu = XL_NGHIEP_VU.Tinh_Doanh_thu_Danh_sach_Lap_top(Danh_sach_Lap_top_cua_Nhom_Lap_top, DateTime.Today);
                Nhom_Lap_top.SetAttribute("Doanh_thu", Doanh_thu.ToString());
            }
        }
    }
    //====== Tạo Dữ liệu cho các Hệ khách ======
    public XmlElement Tao_Du_lieu_cua_Ung_dung_Quan_ly_Nguoi_dung()
    {
        var Chuoi_XML = Du_lieu_Dich_vu.OuterXml;
        var Tai_lieu = new XmlDocument();
        Tai_lieu.LoadXml(Chuoi_XML);
        var Du_lieu = Tai_lieu.DocumentElement;
        var Cua_hang = (XmlElement)Du_lieu.GetElementsByTagName("Cong_ty")[0];
        //var Danh_sach_Nguoi_dung = (XmlElement)Cua_hang.GetElementsByTagName("Danh_sach_Nguoi_dung")[0];
        //var DS_Nguoi_dung = XL_NGHIEP_VU.Tao_Danh_sach(Danh_sach_Nguoi_dung, "Nguoi_dung");
        //foreach (XmlElement Nguoi_dung in DS_Nguoi_dung)
        //    if (Nguoi_dung.SelectSingleNode("Nhom_Nguoi_dung/@Ma_so").Value != "NHAP_HANG")
        //        Danh_sach_Nguoi_dung.RemoveChild(Nguoi_dung);// Xóa Các Người dùng không thuộc Nhóm tương ứng  
        foreach (XmlElement Lap_top in Du_lieu.GetElementsByTagName("Laptop"))
        {
            var Danh_sach_Ban_hang = (XmlElement)Lap_top.GetElementsByTagName("Danh_sach_Ban_hang")[0];
            Lap_top.RemoveChild(Danh_sach_Ban_hang);
            var Danh_sach_Nhap_hang = (XmlElement)Lap_top.GetElementsByTagName("Danh_sach_Dat_hang")[0];
            Lap_top.RemoveChild(Danh_sach_Nhap_hang);
        }// Xóa Tất các  Nhập hàng, Bán hàng 
        return Du_lieu;
    }
    public XmlElement Tao_Du_lieu_cua_Ung_dung_Khach_Tham_quan()
    {

        var Chuoi_XML = Du_lieu_Dich_vu.OuterXml;
        var Tai_lieu = new XmlDocument();
        Tai_lieu.LoadXml(Chuoi_XML);
        var Du_lieu = Tai_lieu.DocumentElement;
        var Cua_hang = (XmlElement)Du_lieu.GetElementsByTagName("Cong_ty")[0];
        //var Danh_sach_Nguoi_dung = (XmlElement)Cua_hang.GetElementsByTagName("Danh_sach_Nguoi_dung")[0];
        //Cua_hang.RemoveChild(Danh_sach_Nguoi_dung);

        foreach (XmlElement Lap_top in Du_lieu.GetElementsByTagName("Laptop"))
        {
            var Danh_sach_Ban_hang = (XmlElement)Lap_top.GetElementsByTagName("Danh_sach_Ban_hang")[0];
            Lap_top.RemoveChild(Danh_sach_Ban_hang);
            var Danh_sach_Nhap_hang = (XmlElement)Lap_top.GetElementsByTagName("Danh_sach_Dat_hang")[0];
            Lap_top.RemoveChild(Danh_sach_Nhap_hang);
        }// Xóa Tất các  Nhập hàng, Bán hàng 
        return Du_lieu;
    }
    public XmlElement Tao_Du_lieu_cua_Ung_dung_Nhan_vien_Nhap_hang()
    {
        var Chuoi_XML = Du_lieu_Dich_vu.OuterXml;
        var Tai_lieu = new XmlDocument();
        Tai_lieu.LoadXml(Chuoi_XML);
        var Du_lieu = Tai_lieu.DocumentElement;
        var Cua_hang = (XmlElement)Du_lieu.GetElementsByTagName("Cua_hang")[0];
        var Danh_sach_Nguoi_dung = (XmlElement)Cua_hang.GetElementsByTagName("Danh_sach_Nguoi_dung")[0];
        var DS_Nguoi_dung = XL_NGHIEP_VU.Tao_Danh_sach(Danh_sach_Nguoi_dung, "Nguoi_dung");
        foreach (XmlElement Nguoi_dung in DS_Nguoi_dung)
            if (Nguoi_dung.SelectSingleNode("Nhom_Nguoi_dung/@Ma_so").Value != "NHAP_HANG")
                Danh_sach_Nguoi_dung.RemoveChild(Nguoi_dung);// Xóa Các Người dùng không thuộc Nhóm tương ứng  
        foreach (XmlElement Lap_top in Du_lieu.GetElementsByTagName("Laptop"))
        {
            var Danh_sach_Ban_hang = (XmlElement)Lap_top.GetElementsByTagName("Danh_sach_Ban_hang")[0];
            Lap_top.RemoveChild(Danh_sach_Ban_hang);
            var Danh_sach_Nhap_hang = (XmlElement)Lap_top.GetElementsByTagName("Danh_sach_Dat_hang")[0];
            Lap_top.RemoveChild(Danh_sach_Nhap_hang);
        }// Xóa Tất các  Nhập hàng, Bán hàng 


        return Du_lieu;

    }
    public XmlElement Tao_Du_lieu_cua_Ung_dung_Nhan_vien_Ban_hang()
    {
        var Chuoi_XML = Du_lieu_Dich_vu.OuterXml;
        var Tai_lieu = new XmlDocument();
        Tai_lieu.LoadXml(Chuoi_XML);
        var Du_lieu = Tai_lieu.DocumentElement;
        var Cua_hang = (XmlElement)Du_lieu.GetElementsByTagName("Cong_ty")[0];
        var Danh_sach_Nguoi_dung = (XmlElement)Cua_hang.GetElementsByTagName("Danh_sach_Nguoi_dung")[0];
        var DS_Nguoi_dung = XL_NGHIEP_VU.Tao_Danh_sach(Danh_sach_Nguoi_dung, "Nguoi_dung");
        foreach (XmlElement Nguoi_dung in DS_Nguoi_dung)
            if (Nguoi_dung.SelectSingleNode("Nhom_Nguoi_dung/@Ma_so").Value != "BAN_HANG")
                Danh_sach_Nguoi_dung.RemoveChild(Nguoi_dung);// Xóa Các Người dùng không thuộc Nhóm tương ứng  
        foreach (XmlElement Lap_top in Du_lieu.GetElementsByTagName("Laptop"))
        {
            var Danh_sach_Ban_hang = (XmlElement)Lap_top.GetElementsByTagName("Danh_sach_Ban_hang")[0];
            Lap_top.RemoveChild(Danh_sach_Ban_hang);
            var Danh_sach_Nhap_hang = (XmlElement)Lap_top.GetElementsByTagName("Danh_sach_Dat_hang")[0];
            Lap_top.RemoveChild(Danh_sach_Nhap_hang);
        }// Xóa Tất các  Nhập hàng, Bán hàng 
        return Du_lieu;
    }
    public XmlElement Tao_Du_lieu_cua_Ung_dung_Quan_ly_Nhap_hang()
    {
        var Chuoi_XML = Du_lieu_Dich_vu.OuterXml;
        var Tai_lieu = new XmlDocument();
        Tai_lieu.LoadXml(Chuoi_XML);
        var Du_lieu = Tai_lieu.DocumentElement;
        var Cua_hang = (XmlElement)Du_lieu.GetElementsByTagName("Cong_ty")[0];
        var Danh_sach_Nguoi_dung = (XmlElement)Cua_hang.GetElementsByTagName("Danh_sach_Nguoi_dung")[0];
        var DS_Nguoi_dung = XL_NGHIEP_VU.Tao_Danh_sach(Danh_sach_Nguoi_dung, "Nguoi_dung");
        foreach (XmlElement Nguoi_dung in DS_Nguoi_dung)
            if (Nguoi_dung.SelectSingleNode("Nhom_Nguoi_dung/@Ma_so").Value != "QUAN_LY_NHAP_HANG")
                Danh_sach_Nguoi_dung.RemoveChild(Nguoi_dung);// Xóa Các Người dùng không thuộc Nhóm tương ứng  
        foreach (XmlElement Lap_top in Du_lieu.GetElementsByTagName("Laptop"))
        {
            var Danh_sach_Ban_hang = (XmlElement)Lap_top.GetElementsByTagName("Danh_sach_Ban_hang")[0];
            Lap_top.RemoveChild(Danh_sach_Ban_hang);
            var Danh_sach_Nhap_hang = (XmlElement)Lap_top.GetElementsByTagName("Danh_sach_Dat_hang")[0];
            Lap_top.RemoveChild(Danh_sach_Nhap_hang);
        }// Xóa Tất các  Nhập hàng, Bán hàng 
        return Du_lieu;
    }
    public XmlElement Tao_Du_lieu_cua_Ung_dung_Quan_ly_Ban_hang()
    {
        var Chuoi_XML = Du_lieu_Dich_vu.OuterXml;
        var Tai_lieu = new XmlDocument();
        Tai_lieu.LoadXml(Chuoi_XML);
        var Du_lieu = Tai_lieu.DocumentElement;
        var Cua_hang = (XmlElement)Du_lieu.GetElementsByTagName("Cong_ty")[0];
        var Danh_sach_Nguoi_dung = (XmlElement)Cua_hang.GetElementsByTagName("Danh_sach_Nguoi_dung")[0];
        var DS_Nguoi_dung = XL_NGHIEP_VU.Tao_Danh_sach(Danh_sach_Nguoi_dung, "Nguoi_dung");
        foreach (XmlElement Nguoi_dung in DS_Nguoi_dung)
            if (Nguoi_dung.SelectSingleNode("Nhom_Nguoi_dung/@Ma_so").Value != "QUAN_LY_BAN_HANG"
                && Nguoi_dung.SelectSingleNode("Nhom_Nguoi_dung/@Ma_so").Value != "BAN_HANG")
                Danh_sach_Nguoi_dung.RemoveChild(Nguoi_dung);// Xóa Các Người dùng không thuộc Nhóm tương ứng  
        foreach (XmlElement Lap_top in Du_lieu.GetElementsByTagName("Laptop"))
        {
            var Danh_sach_Ban_hang = (XmlElement)Lap_top.GetElementsByTagName("Danh_sach_Ban_hang")[0];
            Lap_top.RemoveChild(Danh_sach_Ban_hang);
            var Danh_sach_Nhap_hang = (XmlElement)Lap_top.GetElementsByTagName("Danh_sach_Dat_hang")[0];
            Lap_top.RemoveChild(Danh_sach_Nhap_hang);
        }// Xóa Tất các  Nhập hàng, Bán hàng 
        return Du_lieu;
    }
    public string Ghi_Nhap_hang_Moi(string Ma_so_Lap_top, string Chuoi_Xml_Nhap_hang)
    {
        var Lap_top = XL_NGHIEP_VU.Tim_Lap_top(Ma_so_Lap_top, Du_lieu_Dich_vu);
        var Nhap_hang = XL_NGHIEP_VU.Tao_Doi_tuong_Con(Chuoi_Xml_Nhap_hang, Lap_top);
        var Chuoi_Kq_Ghi = XL_LUU_TRU.Ghi_Nhap_hang_Moi(Lap_top, Nhap_hang);
        if (Chuoi_Kq_Ghi == "OK")
        {
            var So_luong_Ton = XL_NGHIEP_VU.Tinh_So_luong_Ton_Lap_top(Lap_top);
            Lap_top.SetAttribute("So_luong_Ton", So_luong_Ton.ToString());
        }
        var Chuoi_Xml_Kq = $"<DU_LIEU Kq='{Chuoi_Kq_Ghi}' />";
        return Chuoi_Xml_Kq;
    }
    public string Ghi_Ban_hang_Moi(string Ma_so_Lap_top, string Chuoi_Xml_Ban_hang)
    {
        var Lap_top = XL_NGHIEP_VU.Tim_Lap_top(Ma_so_Lap_top, Du_lieu_Dich_vu);
        var Ban_hang = XL_NGHIEP_VU.Tao_Doi_tuong_Con(Chuoi_Xml_Ban_hang, Lap_top);
        var Chuoi_Kq_Ghi = XL_LUU_TRU.Ghi_Ban_hang_Moi(Lap_top, Ban_hang);
        if (Chuoi_Kq_Ghi == "OK")
        {
            var So_luong_Ton = XL_NGHIEP_VU.Tinh_So_luong_Ton_Lap_top(Lap_top);
            Lap_top.SetAttribute("So_luong_Ton", So_luong_Ton.ToString());
        }
        var Chuoi_Xml_Kq = $"<DU_LIEU Kq='{Chuoi_Kq_Ghi}' />";
        return Chuoi_Xml_Kq;
    }
    public string Cap_nhat_don_gia_nhap(string Ma_so_Lap_top, string Don_gia_Nhap)
    {
        var Lap_top = XL_NGHIEP_VU.Tim_Lap_top(Ma_so_Lap_top, Du_lieu_Dich_vu);
        var Chuoi_Kq_Ghi = XL_LUU_TRU.Cap_nhat_Don_gia_nhap(Lap_top, Don_gia_Nhap);
        if (Chuoi_Kq_Ghi == "OK")
        {
            Lap_top.SetAttribute("Don_gia_Nhap", Don_gia_Nhap);
        }
        var Chuoi_Xml_Kq = $"<DU_LIEU Kq='{Chuoi_Kq_Ghi}' />";
        return Chuoi_Xml_Kq;
    }
    public string Cap_nhat_don_gia_ban(string Ma_so_Lap_top, string Don_gia_ban)
    {
        var Lap_top = XL_NGHIEP_VU.Tim_Lap_top(Ma_so_Lap_top, Du_lieu_Dich_vu);
        var Chuoi_Kq_Ghi = XL_LUU_TRU.Cap_nhat_Don_gia_ban(Lap_top, Don_gia_ban);
        if (Chuoi_Kq_Ghi == "OK")
        {
            Lap_top.SetAttribute("Don_gia_ban", Don_gia_ban);
        }
        var Chuoi_Xml_Kq = $"<DU_LIEU Kq='{Chuoi_Kq_Ghi}' />";
        return Chuoi_Xml_Kq;
    }
    public string Ghi_Phieu_Dat_hang(string Chuoi_Xml_Ban_hang)
    {

        var Tai_lieu = new XmlDocument();
        Tai_lieu.LoadXml(Chuoi_Xml_Ban_hang);
        var Phieu_dat = Tai_lieu.DocumentElement;
        var Chuoi_Kq_Ghi = XL_LUU_TRU.Ghi_Phieu_Dat_hang_moi(Phieu_dat);
        var Chuoi_Xml_Kq = $"<DU_LIEU Kq='{Chuoi_Kq_Ghi}' />";
        return Chuoi_Xml_Kq;
    }
}
// 

//************************* Business-Layers BL **********************************
public partial class XL_NGHIEP_VU
{
    public static XmlElement Tim_Lap_top(
         string Ma_so, XmlElement Du_lieu)
    {

        var Danh_sach_Lap_top = (XmlElement)Du_lieu.GetElementsByTagName("Danh_sach_Lap_top")[0];
        var Kq = (XmlElement)null;
        foreach (XmlElement Lap_top in Danh_sach_Lap_top.GetElementsByTagName("Laptop"))
        {
            if (Ma_so == Lap_top.GetAttribute("Ma_so"))
                Kq = Lap_top;

        }
        return Kq;
    }
    public static XmlElement Tao_Doi_tuong_Con(string Chuoi_XML, XmlElement Cha)
    {
        var Doi_tuong_Kq = (XmlElement)null;
        try
        {

            var Tai_lieu = new XmlDocument();
            Tai_lieu.LoadXml(Chuoi_XML);
            var Doi_tuong = Tai_lieu.DocumentElement;
            Doi_tuong_Kq = (XmlElement)Cha.OwnerDocument.ImportNode(Doi_tuong, true);
        }
        catch (Exception Loi)
        {

        }
        return Doi_tuong_Kq;
    }
    //==== Tính toán ======
    public static long Tinh_So_luong_Ton_Lap_top(XmlElement Lap_top)
    {
        var Tong_Nhap_hang = 0L;
        foreach (XmlElement Nhap_hang in Lap_top.GetElementsByTagName("Nhap_hang"))
            Tong_Nhap_hang += long.Parse(Nhap_hang.GetAttribute("So_luong"));
        var Tong_Ban_hang = 0L;
        foreach (XmlElement Ban_hang in Lap_top.GetElementsByTagName("Ban_hang"))
            Tong_Ban_hang += long.Parse(Ban_hang.GetAttribute("So_luong"));

        var So_luong_Ton = Tong_Nhap_hang - Tong_Ban_hang;
        return So_luong_Ton;
    }
    public static long Tinh_So_luong_Ton_Danh_sach_Lap_top(List<XmlElement> Danh_sach_Lap_top)
    {
        var So_luong_Ton = Danh_sach_Lap_top.Sum(Lap_top => Tinh_So_luong_Ton_Lap_top(Lap_top));
        return So_luong_Ton;
    }
    public static long Tinh_Doanh_thu_Lap_top(XmlElement Lap_top, DateTime Ngay)
    {
        var Doanh_thu = 0L;
        foreach (XmlElement Ban_hang in Lap_top.GetElementsByTagName("Ban_hang"))
        {
            var Ngay_Ban = DateTime.Parse(Ban_hang.GetAttribute("Ngay"));
            if (Ngay.Day == Ngay_Ban.Day && Ngay.Month == Ngay_Ban.Month && Ngay.Year == Ngay_Ban.Year)
                Doanh_thu += long.Parse(Ban_hang.GetAttribute("Tien"));
        }
        return Doanh_thu;
    }
    public static long Tinh_Doanh_thu_Danh_sach_Lap_top(List<XmlElement> Danh_sach_Lap_top, DateTime Ngay)
    {
        var Doanh_thu = Danh_sach_Lap_top.Sum(Dien_thoai => Tinh_Doanh_thu_Lap_top(Dien_thoai, Ngay));

        return Doanh_thu;
    }

    // Tạo Danh sách ======
    public static List<XmlElement> Tao_Danh_sach(XmlElement Danh_sach_Nguon, string Loai_Doi_tuong)
    {
        var Danh_sach = new List<XmlElement>();
        foreach (XmlElement Doi_tuong in Danh_sach_Nguon.GetElementsByTagName(Loai_Doi_tuong))
        {
            Danh_sach.Add(Doi_tuong);
        }
        return Danh_sach;
    }
    public static List<XmlElement> Tao_Danh_sach_Lap_top_cua_Nhom_Lap_top(
            XmlElement Nhom_Lap_top, XmlElement Danh_sach_Tat_ca_Lap_top)
    {
        var Danh_sach = new List<XmlElement>();
        var DS_Tat_ca_Lap_top = Tao_Danh_sach(Danh_sach_Tat_ca_Lap_top, "Laptop");
        Danh_sach = DS_Tat_ca_Lap_top.FindAll(
               Lap_top => Lap_top.SelectSingleNode("Nhom_Lap_top/@Ma_so").Value == Nhom_Lap_top.GetAttribute("Ma_so"));
        return Danh_sach;
    }
}

//************************* Data-Layers DL **********************************
public partial class XL_LUU_TRU
{
    static DirectoryInfo Thu_muc_Project = new DirectoryInfo(HostingEnvironment.ApplicationPhysicalPath);
    static DirectoryInfo Thu_muc_Du_lieu = Thu_muc_Project.GetDirectories("2-Du_lieu_Luu_tru")[0];
    static DirectoryInfo Thu_muc_Cua_hang = Thu_muc_Du_lieu.GetDirectories("Cong_ty")[0];
    static DirectoryInfo Thu_muc_Lap_top = Thu_muc_Du_lieu.GetDirectories("Lap_top")[0];
    static DirectoryInfo Thu_muc_Phieu_dat = Thu_muc_Du_lieu.GetDirectories("Phieu_dat")[0];
    static XmlElement Du_lieu;
    public static XmlElement Doc_Du_lieu()
    {
        if (Du_lieu == null)
        {
            var Chuoi_XML = "<Du_lieu />";
            var Tai_lieu = new XmlDocument();
            Tai_lieu.LoadXml(Chuoi_XML);
            Du_lieu = Tai_lieu.DocumentElement;
            var Cua_hang = Doc_Cua_hang();
            Du_lieu.AppendChild(Tai_lieu.ImportNode(Cua_hang, true));
            var Danh_sach_San_pham = Doc_Danh_sach_Lap_top();
            Du_lieu.AppendChild(Tai_lieu.ImportNode(Danh_sach_San_pham, true));
        }
        return Du_lieu;
    }
    static XmlElement Doc_Danh_sach_Lap_top()
    {
        var Chuoi_XML_Danh_sach = "<Danh_sach_Lap_top />";
        var Tai_lieu_Danh_sach = new XmlDocument();
        Tai_lieu_Danh_sach.LoadXml(Chuoi_XML_Danh_sach);
        var Danh_sach = Tai_lieu_Danh_sach.DocumentElement;
        Thu_muc_Lap_top.GetFiles("*.xml").ToList().ForEach(Tap_tin =>
        {
            var Duong_dan = Tap_tin.FullName;
            var Tai_lieu = new XmlDocument();
            Tai_lieu.Load(Duong_dan);
            var San_pham = Tai_lieu.DocumentElement;
            var San_pham_cua_Danh_sach = Tai_lieu_Danh_sach.ImportNode(San_pham, true);
            Danh_sach.AppendChild(San_pham_cua_Danh_sach);
        });
        return Danh_sach;
    }
    static XmlElement Doc_Cua_hang()
    {
        var Duong_dan = Thu_muc_Cua_hang.FullName + "\\Cong_ty.xml";
        var Tai_lieu = new XmlDocument();
        Tai_lieu.Load(Duong_dan);
        var Cua_hang = Tai_lieu.DocumentElement;
        return Cua_hang;
    }
    public static string Ghi_Nhap_hang_Moi(XmlElement Lap_top, XmlElement Nhap_hang)
    {
        var Kq = "";

        try
        {
            var Danh_sach_Nhap_hang = Lap_top.GetElementsByTagName("Danh_sach_Nhap_hang")[0];
            Danh_sach_Nhap_hang.AppendChild(Nhap_hang);
            var Duong_dan = Thu_muc_Lap_top.FullName + $"\\{Lap_top.GetAttribute("Ma_so")}.xml";
            var Chuoi_XML = Lap_top.OuterXml;
            File.WriteAllText(Duong_dan, Chuoi_XML);
            Kq = "OK";
        }
        catch (Exception Loi)
        {
            Kq = Loi.Message;
        }
        if (Kq != "OK" && Lap_top != null && Nhap_hang != null)
        {
            var Danh_sach_Nhap_hang = Lap_top.GetElementsByTagName("Danh_sach_Nhap_hang")[0];
            Danh_sach_Nhap_hang.RemoveChild(Nhap_hang);
        }
        return Kq;
    }
    public static string Ghi_Ban_hang_Moi(XmlElement Lap_top, XmlElement Ban_hang)
    {
        var Kq = "";

        try
        {

            var Danh_sach_Ban_hang = Lap_top.GetElementsByTagName("Danh_sach_Ban_hang")[0];
            Danh_sach_Ban_hang.AppendChild(Ban_hang);
            var Duong_dan = Thu_muc_Lap_top.FullName + $"\\{Lap_top.GetAttribute("Ma_so")}.xml";
            var Chuoi_XML = Lap_top.OuterXml;
            File.WriteAllText(Duong_dan, Chuoi_XML);
            Kq = "OK";
        }
        catch (Exception Loi)
        {
            Kq = Loi.Message;
        }
        if (Kq != "OK" && Lap_top != null && Ban_hang != null)
        {
            var Danh_sach_Ban_hang = Lap_top.GetElementsByTagName("Danh_sach_Ban_hang")[0];
            Danh_sach_Ban_hang.RemoveChild(Ban_hang);
        }
        return Kq;

    }
    public static string Ghi_Phieu_Dat_hang_moi(XmlElement Phieu_dat)
    {
        var Kq = "";
        var Ma_so = Thu_muc_Phieu_dat.GetFiles("*.xml").ToList().Count + 1;
        try
        {
            Phieu_dat.SetAttribute("Ma_so", Ma_so.ToString());
            var Duong_dan = Thu_muc_Phieu_dat.FullName + $"\\{Phieu_dat.GetAttribute("Ma_so")}.xml";
            var Chuoi_XML = Phieu_dat.OuterXml;
            File.WriteAllText(Duong_dan, Chuoi_XML);
            Kq = "OK";
        }
        catch (Exception Loi)
        {
            Kq = Loi.Message;
        }
        if (Kq != "OK" && Phieu_dat != null)
        {

        }
        return Kq;
    }
    public static string Cap_nhat_Don_gia_nhap(XmlElement Lap_top, string Tien)
    {
        var Kq = "";
        var Don_gia_nhap_cu = Lap_top.GetAttribute("Don_gia_Nhap");
        try
        {

            Lap_top.SetAttribute("Don_gia_Nhap", Tien);
            var Duong_dan = Thu_muc_Lap_top.FullName + $"\\{Lap_top.GetAttribute("Ma_so")}.xml";
            var Chuoi_XML = Lap_top.OuterXml;
            File.WriteAllText(Duong_dan, Chuoi_XML);
            Kq = "OK";
        }
        catch (Exception Loi)
        {
            Kq = Loi.Message;
        }
        if (Kq != "OK" && Lap_top != null && Tien != null)
        {
            Lap_top.SetAttribute("Don_gia_Nhap", Don_gia_nhap_cu);
        }
        return Kq;

    }
    public static string Cap_nhat_Don_gia_ban(XmlElement Lap_top, string Tien)
    {
        var Kq = "";
        var Don_gia_ban_cu = Lap_top.GetAttribute("Don_gia_Ban");
        try
        {

            Lap_top.SetAttribute("Don_gia_Ban", Tien);
            var Duong_dan = Thu_muc_Lap_top.FullName + $"\\{Lap_top.GetAttribute("Ma_so")}.xml";
            var Chuoi_XML = Lap_top.OuterXml;
            File.WriteAllText(Duong_dan, Chuoi_XML);
            Kq = "OK";
        }
        catch (Exception Loi)
        {
            Kq = Loi.Message;
        }
        if (Kq != "OK" && Lap_top != null && Tien != null)
        {
            Lap_top.SetAttribute("Don_gia_Ban", Don_gia_ban_cu);
        }
        return Kq;

    }
}