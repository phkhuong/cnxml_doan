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
        var Danh_sach_Laptop = (XmlElement)Du_lieu_Dich_vu.GetElementsByTagName("Danh_sach_Laptop")[0];
        var Cong_ty = (XmlElement)Du_lieu_Dich_vu.GetElementsByTagName("Cong_ty")[0];
        // ===================== Bổ sung thông tin   =============================== 
        foreach (XmlElement Laptop in Danh_sach_Laptop.GetElementsByTagName("Laptop"))
        {
            var So_luong_Ton = XL_NGHIEP_VU.Tinh_Ton_Kho_Laptop(Laptop);
            Laptop.SetAttribute("So_luong_Ton", So_luong_Ton.ToString());
            var Doanh_thu = XL_NGHIEP_VU.Tinh_Doanh_thu_Laptop(Laptop);
            Laptop.SetAttribute("Doanh_thu", Doanh_thu.ToString());
        }
    }
    //====== Tạo Dữ liệu cho các Hệ khách ======
    // Tạo Dữ liệu 
    public XmlElement Tao_Du_lieu_cua_Khach_Tham_quan()
    {

        var Chuoi_XML = Du_lieu_Dich_vu.OuterXml;
        var Tai_lieu = new XmlDocument();
        Tai_lieu.LoadXml(Chuoi_XML);
        var Du_lieu_He_khach = Tai_lieu.DocumentElement;
        var Cong_ty_He_khach = (XmlElement)Du_lieu_He_khach.GetElementsByTagName("Cong_ty")[0];
        var Danh_sach_Nhan_vien = Cong_ty_He_khach.GetElementsByTagName("Danh_sach_Nhan_vien")[0];
        Cong_ty_He_khach.RemoveChild(Danh_sach_Nhan_vien);
        var Danh_sach_Laptop = (XmlElement)Du_lieu_He_khach.GetElementsByTagName("Danh_sach_Laptop")[0];

        foreach (XmlElement Laptop_He_khach in Danh_sach_Laptop.SelectNodes("/Laptop"))
        {
            var Danh_sach_Ban_hang = Laptop_He_khach.GetElementsByTagName("Danh_sach_Ban_hang")[0];
            var Danh_sach_Nhap_hang = Laptop_He_khach.GetElementsByTagName("Danh_sach_Nhap_hang")[0];
            Laptop_He_khach.RemoveChild(Danh_sach_Ban_hang);
            Laptop_He_khach.RemoveChild(Danh_sach_Nhap_hang);
        }
        return Du_lieu_He_khach;
    }
    public XmlElement Tao_Du_lieu_cua_Nhan_vien_Nhap_hang()
    {
        var Chuoi_XML = Du_lieu_Dich_vu.OuterXml;
        var Tai_lieu = new XmlDocument();
        Tai_lieu.LoadXml(Chuoi_XML);
        var Du_lieu_He_khach = Tai_lieu.DocumentElement;
        var Cong_ty_He_khach = (XmlElement)Du_lieu_He_khach.GetElementsByTagName("Cong_ty")[0];
        var Danh_sach_Laptop = (XmlElement)Du_lieu_He_khach.GetElementsByTagName("Danh_sach_Laptop")[0];

        foreach (XmlElement Laptop_He_khach in Danh_sach_Laptop.SelectNodes("/Laptop"))
        {
            var Danh_sach_Ban_hang = Laptop_He_khach.GetElementsByTagName("Danh_sach_Ban_hang")[0];
            var Danh_sach_Nhap_hang = Laptop_He_khach.GetElementsByTagName("Danh_sach_Nhap_hang")[0];
            Laptop_He_khach.RemoveChild(Danh_sach_Ban_hang);
            Laptop_He_khach.RemoveChild(Danh_sach_Nhap_hang);
        }

        return Du_lieu_He_khach;
    }
    
    public XmlElement Tao_Du_lieu_cua_Nhan_vien_Giao_hang()
    {
        var Chuoi_XML = Du_lieu_Dich_vu.OuterXml;
        var Tai_lieu = new XmlDocument();
        Tai_lieu.LoadXml(Chuoi_XML);
        var Du_lieu_He_khach = Tai_lieu.DocumentElement;
        var Cong_ty_He_khach = (XmlElement)Du_lieu_He_khach.GetElementsByTagName("Cong_ty")[0];

        // NULL REFERENCE ===> FIXED
        //foreach (XmlElement Laptop_He_khach in Du_lieu_He_khach.SelectNodes("//Laptop"))
        //{
        //    var Danh_sach_Ban_hang = Laptop_He_khach.GetElementsByTagName("Danh_sach_Ban_hang")[0];
        //    var Danh_sach_Nhap_hang = Laptop_He_khach.GetElementsByTagName("Danh_sach_Nhap_hang")[0];
        //    Laptop_He_khach.RemoveChild(Danh_sach_Ban_hang);
        //    Laptop_He_khach.RemoveChild(Danh_sach_Nhap_hang);
        //}

        return Du_lieu_He_khach;
    }
    public XmlElement Tao_Du_lieu_cua_Quan_ly_Giao_hang()
    {
        var Chuoi_XML = Du_lieu_Dich_vu.OuterXml;
        var Tai_lieu = new XmlDocument();
        Tai_lieu.LoadXml(Chuoi_XML);
        var Du_lieu_He_khach = Tai_lieu.DocumentElement;
        //var Cong_ty_He_khach = (XmlElement)Du_lieu_He_khach.GetElementsByTagName("Danh_sach_Phieu_dat")[0];
        //foreach (XmlElement Laptop_He_khach in Du_lieu_He_khach.SelectNodes("//Laptop"))
        //{
        //    var Danh_sach_Ban_hang = Laptop_He_khach.GetElementsByTagName("Danh_sach_Ban_hang")[0];
        //    var Danh_sach_Nhap_hang = Laptop_He_khach.GetElementsByTagName("Danh_sach_Nhap_hang")[0];
        //    Laptop_He_khach.RemoveChild(Danh_sach_Ban_hang);
        //    Laptop_He_khach.RemoveChild(Danh_sach_Nhap_hang);

        //}

        return Du_lieu_He_khach;
    }
}
    

    //************************* Business-Layers BL **********************************
    public partial class XL_NGHIEP_VU
{   // PET : Minh họa Kỹ thuật đơn giản nhất - Không xét tính hiệu quả 
    // ==> Có thể cải tiến 
    public static XmlElement Tim_Laptop(
          string Ma_so, XmlElement Du_lieu)
    {

        var Danh_sach_Laptop =(XmlElement) Du_lieu.GetElementsByTagName("Danh_sach_Laptop")[0];
        var Kq= (XmlElement)null;
        foreach (XmlElement Laptop in Danh_sach_Laptop.GetElementsByTagName("Laptop"))
        {
            if (Ma_so == Laptop.GetAttribute("Ma_so") )
                Kq = Laptop;
             
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
            Doi_tuong_Kq = (XmlElement) Cha.OwnerDocument.ImportNode(Doi_tuong, true);
        }
        catch(Exception Loi)
        {

        }
        return Doi_tuong_Kq;
    }

    

    //==== Tính toán 

    public static long Tinh_Doanh_thu_Laptop(XmlElement Laptop)
    {
        var Doanh_thu = 0L;
        foreach (XmlElement Ban_hang in Laptop.GetElementsByTagName("Ban_hang"))
        {
            Doanh_thu += long.Parse(Ban_hang.GetAttribute("Tien"));
        }
        return Doanh_thu;
    }

    public static long Tinh_Ton_Kho_Laptop(XmlElement Laptop)
    {
        var Ton_Kho = 0L;
        var So_Ban = 0L;
        var So_Nhap = 0L;
        foreach (XmlElement Ban_hang in Laptop.GetElementsByTagName("Ban_hang"))
        {
            So_Ban += long.Parse(Ban_hang.GetAttribute("So_luong"));
        }
        foreach(XmlElement Nhap_hang in Laptop.GetElementsByTagName("Nhap_hang"))
        {
            So_Nhap += long.Parse(Nhap_hang.GetAttribute("So_luong"));
        }
        Ton_Kho = So_Nhap - So_Ban;
        return Ton_Kho;
    }
}



//************************* Data-Layers DL **********************************
public partial class XL_LUU_TRU
{
    static DirectoryInfo Thu_muc_Project = new DirectoryInfo(HostingEnvironment.ApplicationPhysicalPath);
    static DirectoryInfo Thu_muc_Du_lieu = Thu_muc_Project.GetDirectories("2-Du_lieu_Luu_tru")[0];
    static DirectoryInfo Thu_muc_Cong_ty = Thu_muc_Du_lieu.GetDirectories("Cong_ty")[0];
    static DirectoryInfo Thu_muc_Laptop = Thu_muc_Du_lieu.GetDirectories("Laptop")[0];
    static DirectoryInfo Thu_muc_Phieu_dat = Thu_muc_Du_lieu.GetDirectories("Phieu_dat")[0];
    static XmlElement Du_lieu;
    public static XmlElement Doc_Du_lieu()
    {   if (Du_lieu == null)
        {
            var Chuoi_XML = "<Du_lieu />";
            var Tai_lieu = new XmlDocument();
            Tai_lieu.LoadXml(Chuoi_XML);
            Du_lieu = Tai_lieu.DocumentElement;
            var Cong_ty = Doc_Danh_sach_Cong_ty().FirstChild;
            Du_lieu.AppendChild(Tai_lieu.ImportNode(Cong_ty, true));
            var Danh_sach_Laptop = Doc_Danh_sach_Laptop();
            Du_lieu.AppendChild(Tai_lieu.ImportNode(Danh_sach_Laptop, true));
            var Danh_sach_Phieu_dat = Doc_Danh_sach_Phieu_dat();
            Du_lieu.AppendChild(Tai_lieu.ImportNode(Danh_sach_Phieu_dat, true));
        }
        
        return Du_lieu;
    }
    static XmlElement Doc_Danh_sach_Laptop()
    {
        var Chuoi_XML_Danh_sach = "<Danh_sach_Laptop />";
        var Tai_lieu_Danh_sach = new XmlDocument();
        Tai_lieu_Danh_sach.LoadXml(Chuoi_XML_Danh_sach);
        var Danh_sach = Tai_lieu_Danh_sach.DocumentElement;
        Thu_muc_Laptop.GetFiles("*.xml").ToList().ForEach(Tap_tin =>
        {
            var Duong_dan = Tap_tin.FullName;
            var Tai_lieu = new XmlDocument();
            Tai_lieu.Load(Duong_dan);
            var Laptop = Tai_lieu.DocumentElement;
            var Laptop_cua_Danh_sach = Tai_lieu_Danh_sach.ImportNode(Laptop, true);
            Danh_sach.AppendChild(Laptop_cua_Danh_sach);
        });
        return Danh_sach;
    }
    static XmlElement Doc_Danh_sach_Cong_ty()
    {
        var Chuoi_XML_Danh_sach = "<Danh_sach_Cong_ty />";
        var Tai_lieu_Danh_sach = new XmlDocument();
        Tai_lieu_Danh_sach.LoadXml(Chuoi_XML_Danh_sach);
        var Danh_sach = Tai_lieu_Danh_sach.DocumentElement;
        Thu_muc_Cong_ty.GetFiles("*.xml").ToList().ForEach(Tap_tin =>
        {
            var Duong_dan = Tap_tin.FullName;
            var Tai_lieu = new XmlDocument();
            Tai_lieu.Load(Duong_dan);
            var Cong_ty = Tai_lieu.DocumentElement;
            var Cong_ty_cua_Danh_sach = Tai_lieu_Danh_sach.ImportNode(Cong_ty, true);
            Danh_sach.AppendChild(Cong_ty_cua_Danh_sach);
        });
        return Danh_sach;
    }

    static XmlElement Doc_Danh_sach_Phieu_dat()
    {
        var Chuoi_XML_Danh_sach = "<Danh_sach_Phieu_dat />";
        var Tai_lieu_Danh_sach = new XmlDocument();
        Tai_lieu_Danh_sach.LoadXml(Chuoi_XML_Danh_sach);
        var Danh_sach = Tai_lieu_Danh_sach.DocumentElement;
        Thu_muc_Phieu_dat.GetFiles("*.xml").ToList().ForEach(Tap_tin =>
        {
            var Duong_dan = Tap_tin.FullName;
            var Tai_lieu = new XmlDocument();
            Tai_lieu.Load(Duong_dan);
            var Phieu_dat = Tai_lieu.DocumentElement;
            var Phieu_dat_cua_Danh_sach = Tai_lieu_Danh_sach.ImportNode(Phieu_dat, true);
            Danh_sach.AppendChild(Phieu_dat_cua_Danh_sach);
        });
        return Danh_sach;
    }


    public static string Ghi_Dat_hang_Moi(XmlElement Laptop, XmlElement Dat_hang)
    {
        var Kq = "";
        
        try
        {
           
            var Danh_sach_Dat_hang = Laptop.GetElementsByTagName("Danh_sach_Dat_hang")[0];
            Danh_sach_Dat_hang.AppendChild(Dat_hang);
            var Duong_dan = Thu_muc_Laptop.FullName + $"\\{Laptop.GetAttribute("Ma_so")}.xml";
            var Chuoi_XML = Laptop.OuterXml;
            File.WriteAllText(Duong_dan, Chuoi_XML);
            Kq = "OK";
        }
        catch (Exception Loi)
        {
            Kq = Loi.Message;
        }
        if (Kq != "OK" && Laptop != null && Dat_hang != null)
        {
            var Danh_sach_Dat_hang = Laptop.GetElementsByTagName("Danh_sach_Dat_hang")[0];
            Danh_sach_Dat_hang.RemoveChild(Dat_hang);
        }
        return Kq;

    }

    public static string Ghi_Ban_hang_Moi(XmlElement Laptop, XmlElement Ban_hang)
    {
        var Kq = "";

        try
        {

            var Danh_sach_Ban_hang = Laptop.GetElementsByTagName("Danh_sach_Ban_hang")[0];
            Danh_sach_Ban_hang.AppendChild(Ban_hang);
            var Duong_dan = Thu_muc_Laptop.FullName + $"\\{Laptop.GetAttribute("Ma_so")}.xml";
            var Chuoi_XML = Laptop.OuterXml;
            File.WriteAllText(Duong_dan, Chuoi_XML);
            Kq = "OK";
        }
        catch (Exception Loi)
        {
            Kq = Loi.Message;
        }
        if (Kq != "OK" && Laptop != null && Ban_hang != null)
        {
            var Danh_sach_Ban_hang = Laptop.GetElementsByTagName("Danh_sach_Ban_hang")[0];
            Danh_sach_Ban_hang.RemoveChild(Ban_hang);
        }
        return Kq;

    }
}