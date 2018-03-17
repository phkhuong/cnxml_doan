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
        var Danh_sach_Nhom_Laptop = (XmlElement)Cong_ty.GetElementsByTagName("Danh_sach_Nhom_Lap_top")[0];
        // ===================== Bổ sung thông tin   =============================== 
        foreach (XmlElement Laptop in Danh_sach_Laptop.GetElementsByTagName("Laptop"))
        {
            var So_luong_Ton = XL_NGHIEP_VU.Tinh_So_luong_ton_Laptop(Laptop);
            Laptop.SetAttribute("So_luong_Ton", So_luong_Ton.ToString());
            var Doanh_thu = XL_NGHIEP_VU.Tinh_Doanh_thu_Laptop(Laptop);
            Laptop.SetAttribute("Doanh_thu", Doanh_thu.ToString());
        }
        foreach (XmlElement Nhom_Laptop in Danh_sach_Nhom_Laptop.GetElementsByTagName("Nhom_Lap_top"))
        {
            var Danh_sach_Laptop_cua_Nhom_Laptop = XL_NGHIEP_VU.Tao_Danh_sach_Laptop_cua_Nhom_Laptop(Nhom_Laptop, Danh_sach_Laptop);
            var So_luong_Ton = XL_NGHIEP_VU.Tinh_So_luong_Ton_Danh_sach_Laptop(Danh_sach_Laptop_cua_Nhom_Laptop);
            Nhom_Laptop.SetAttribute("So_luong_Ton", So_luong_Ton.ToString());
            var Doanh_thu = XL_NGHIEP_VU.Tinh_Doanh_thu_Danh_sach_Laptop(Danh_sach_Laptop_cua_Nhom_Laptop);
            Nhom_Laptop.SetAttribute("Doanh_thu", Doanh_thu.ToString());
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
        
        var Danh_sach_Laptop = Du_lieu_He_khach.SelectSingleNode("Danh_sach_Laptop");
        Du_lieu_He_khach.RemoveChild(Danh_sach_Laptop);

        var Danh_sach_Nhan_vien_root = (XmlElement)Cong_ty_He_khach.GetElementsByTagName("Danh_sach_Nhan_vien")[0];
        var Danh_sach_Nhan_vien = Danh_sach_Nhan_vien_root.SelectNodes("Nhan_vien");
        foreach(XmlElement Nhan_vien in Danh_sach_Nhan_vien)
        {
            var Ma_Nhom_Nhan_vien = Nhan_vien.FirstChild.Attributes["Ma_so"].Value;
            if(Ma_Nhom_Nhan_vien != "GIAO_HANG")
            {
                Danh_sach_Nhan_vien_root.RemoveChild(Nhan_vien);
            }
        }

        var Danh_sach_Quan_ly = Danh_sach_Nhan_vien_root.SelectNodes("Quan_ly");
        foreach(XmlElement Quan_ly in Danh_sach_Quan_ly)
        {
            Danh_sach_Nhan_vien_root.RemoveChild(Quan_ly);
        }

        var Danh_sach_Tinh_trang_Phieu_dat = (XmlElement)Cong_ty_He_khach.GetElementsByTagName("Danh_sach_Tinh_trang_Phieu_dat")[0];
        var node = Danh_sach_Tinh_trang_Phieu_dat.SelectSingleNode("Tinh_trang[@Ma_so='CHO_PHAN_CONG']");
        Danh_sach_Tinh_trang_Phieu_dat.RemoveChild(node);

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

    public string Ghi_Nhap_hang_Moi(string Ma_so_Laptop, string Chuoi_Xml_Nhap_hang)
    {
        var Laptop = XL_NGHIEP_VU.Tim_Laptop(Ma_so_Laptop, Du_lieu_Dich_vu);
        var Nhap_hang = XL_NGHIEP_VU.Tao_Doi_tuong_Con(Chuoi_Xml_Nhap_hang, Laptop);
        var Chuoi_Kq_Ghi = XL_LUU_TRU.Ghi_Nhap_hang_Moi(Laptop, Nhap_hang);
        if (Chuoi_Kq_Ghi == "OK")
        {
            var So_luong_Ton = XL_NGHIEP_VU.Tinh_So_luong_ton_Laptop(Laptop);
            Laptop.SetAttribute("So_luong_Ton", So_luong_Ton.ToString());
        }
        var Chuoi_Xml_Kq = $"<DU_LIEU Kq='{Chuoi_Kq_Ghi}' />";
        return Chuoi_Xml_Kq;
    }

    public string Ghi_Tinh_trang_Moi(string Ma_so_Phieu_dat, string Tinh_trang_moi)
    {

        var Phieu_dat = XL_NGHIEP_VU.Tim_Phieu_dat(Ma_so_Phieu_dat, Du_lieu_Dich_vu);


        var Hop_le = Phieu_dat != null;
        var Chuoi_Kq_Ghi = "";
        if (Hop_le)
        {
            string chuoi_ngay = DateTime.Now.ToString(CultureInfo.GetCultureInfo("vi-VN"));
            switch (Tinh_trang_moi)
            {
                case "1":
                    // thanh cong
                    Phieu_dat.SetAttribute("Ngay_Thanh_toan", chuoi_ngay);
                    Phieu_dat.SetAttribute("Tinh_trang", "DA_THANH_TOAN");
                    break;
                case "2":
                    // huy
                    Phieu_dat.SetAttribute("Ngay_Huy", chuoi_ngay);
                    Phieu_dat.SetAttribute("Tinh_trang", "HUY");
                    break;
            }
            
            Chuoi_Kq_Ghi = XL_LUU_TRU.Ghi_Phieu_dat(Phieu_dat);
            if (Chuoi_Kq_Ghi == "OK")
            {
                // co the ghi ban hang vao cac laptop
                // cap nhat so luong ton
            }
            

        }
        else
            Chuoi_Kq_Ghi = "Lỗi Hệ thống - Xin Thực hiện lại ";

        var Chuoi_Xml_Kq = $"<DU_LIEU Kq='{Chuoi_Kq_Ghi}' />";
        return Chuoi_Xml_Kq;

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

    public static XmlElement Tim_Phieu_dat(
          string Ma_so, XmlElement Du_lieu)
    {

        var Danh_sach_Phieu_dat = (XmlElement)Du_lieu.GetElementsByTagName("Danh_sach_Phieu_dat")[0];
        var Kq = (XmlElement)null;
        foreach (XmlElement Phieu_dat in Danh_sach_Phieu_dat.GetElementsByTagName("Phieu_dat"))
        {
            if (Ma_so == Phieu_dat.GetAttribute("Ma_so"))
                Kq = Phieu_dat;

        }


        return Kq;
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
    public static long Tinh_Doanh_thu_Danh_sach_Laptop(List<XmlElement> Danh_sach_Laptop)
    {
        var Doanh_thu = Danh_sach_Laptop.Sum(Laptop => Tinh_Doanh_thu_Laptop(Laptop));
        return Doanh_thu;
    }
    public static long Tinh_So_luong_ton_Laptop(XmlElement Laptop)
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
    public static long Tinh_So_luong_Ton_Danh_sach_Laptop(List<XmlElement> Danh_sach_Laptop)
    {
        var So_luong_Ton = Danh_sach_Laptop.Sum(Laptop => Tinh_So_luong_ton_Laptop(Laptop));
        return So_luong_Ton;
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
    public static List<XmlElement> Tao_Danh_sach_Laptop_cua_Nhom_Laptop(
            XmlElement Nhom_Laptop, XmlElement Danh_sach_Tat_ca_Laptop)
    {
        var Danh_sach = new List<XmlElement>();
        var DS_Tat_ca_Laptop = Tao_Danh_sach(Danh_sach_Tat_ca_Laptop, "Laptop");
        Danh_sach = DS_Tat_ca_Laptop.FindAll(
               Laptop => Laptop.SelectSingleNode("Nhom_Lap_top/@Ma_so").Value == Nhom_Laptop.GetAttribute("Ma_so"));
        return Danh_sach;
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

    public static string Ghi_Nhap_hang_Moi(XmlElement Laptop, XmlElement Nhap_hang)
    {
        var Kq = "";

        try
        {

            var Danh_sach_Nhap_hang = Laptop.GetElementsByTagName("Danh_sach_Nhap_hang")[0];
            Danh_sach_Nhap_hang.AppendChild(Nhap_hang);
            var Duong_dan = Thu_muc_Laptop.FullName + $"\\{Laptop.GetAttribute("Ma_so")}.xml";
            var Chuoi_XML = Laptop.OuterXml;
            File.WriteAllText(Duong_dan, Chuoi_XML);
            Kq = "OK";
        }
        catch (Exception Loi)
        {
            Kq = Loi.Message;
        }
        if (Kq != "OK" && Laptop != null && Nhap_hang != null)
        {
            var Danh_sach_Ban_hang = Laptop.GetElementsByTagName("Danh_sach_Nhap_hang")[0];
            Danh_sach_Ban_hang.RemoveChild(Nhap_hang);
        }
        return Kq;

    }

    public static string Ghi_Phieu_dat(XmlElement Phieu_dat)
    {
        var Kq = "";
        string Chuoi_Phieu_dat_cu = "";
        try
        {
            var Duong_dan = Thu_muc_Phieu_dat.FullName + $"\\{Phieu_dat.GetAttribute("Ma_so")}.xml";
            Chuoi_Phieu_dat_cu = File.ReadAllText(Duong_dan);

            var Chuoi_XML = Phieu_dat.OuterXml;
            File.WriteAllText(Duong_dan, Chuoi_XML);
            Kq = "OK";
        }
        catch (Exception Loi)
        {
            Kq = Loi.Message;
        }
        if (Kq != "OK" && Phieu_dat != null && Chuoi_Phieu_dat_cu != "")
        {
            var Duong_dan = Thu_muc_Phieu_dat.FullName + $"\\{Phieu_dat.GetAttribute("Ma_so")}.xml";
            File.WriteAllText(Duong_dan, Chuoi_Phieu_dat_cu);
        }
        return Kq;
    }
}