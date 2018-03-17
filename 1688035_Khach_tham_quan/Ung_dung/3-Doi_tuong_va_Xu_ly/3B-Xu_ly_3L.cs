using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.IO;
using System.Xml;
using System.Globalization;
using System.Net;
//************************* View/Presentation -Layers VL/PL **********************************
public partial class XL_THE_HIEN
{
    public static string Dia_chi_Media = $"{XL_LUU_TRU.Dia_chi_Dich_vu}/Media";
    public static CultureInfo Dinh_dang_VN = CultureInfo.GetCultureInfo("vi-VN");


    public static string Tao_Chuoi_HTML_Danh_sach_Laptop_Xem(XmlElement Danh_sach_Laptop)
    {
        var Chuoi_HTML_Danh_sach = "<div class='row'>";
        foreach(XmlElement Laptop in Danh_sach_Laptop.GetElementsByTagName("Laptop"))
        {
            var Ten = Laptop.GetAttribute("Ten");
            var Ma_so = Laptop.GetAttribute("Ma_so");
            var Don_gia_Ban = long.Parse(Laptop.GetAttribute("Don_gia_Ban"));
            var Chuoi_Chuc_nang_Chon = "<form method='post'>" +
                                        "<input name='Th_Ma_so_Chuc_nang' type='hidden' value='CHON' />" +
                                         $"<input name='Th_Ma_so_Laptop' type='hidden' value='{Ma_so}' />" +
                                         $"<button type='submit' class='btn btn-primary hidden-sm-down btn-block ADD_BUTTON'>THÊM VÀO GIỎ HÀNG <img class='CART_ICON' src='{Dia_chi_Media}/CART_ICON.svg' /></button>" +
                                       "</form>";
            var Chuoi_Hinh = $"<div class='KHUNG_HINH mx-auto'>" +
                                $"<a href='MH_Chi_tiet_San_pham.cshtml?Ma_so={Ma_so}'><img src='{Dia_chi_Media}/{Ma_so}.png' class='img-thumbnail HINH'/></a>" +
                             "</div>";
            var Chuoi_Thong_tin = $"<div>" +
                                      $"<strong>{Ten}</strong>" +
                                      $"<br />Đơn giá: { Don_gia_Ban.ToString("c0", Dinh_dang_VN) }" +
                                  $"</div>";
            var Chuoi_HTML = $"<div class='KHUNG col-6 col-sm-4 col-md-3 col-lg-2'>" +
                                 $"<div class='THONG_TIN'>" +
                                     $"{Chuoi_Hinh}" +
                                     $"{Chuoi_Thong_tin}" +
                                     $"{Chuoi_Chuc_nang_Chon}"+
                                 $"</div>" +
                             "</div>";
            Chuoi_HTML_Danh_sach += Chuoi_HTML;
        }
        
        Chuoi_HTML_Danh_sach += "</div>";
        return Chuoi_HTML_Danh_sach;
    }

    public static string Tao_Chuoi_HTML_Danh_sach_Laptop_Chon(XmlElement Danh_sach_Laptop)
    {

        var Chuoi_HTML_Danh_sach = "<div class='row'  >";
        if (Danh_sach_Laptop.ChildNodes.Count > 0)
        {
            Chuoi_HTML_Danh_sach = "<div class='row' style='border:1px solid blue;margin:5px;padding:5px'>";
            var Chuoi_Chuc_nang_Dat_hang = "<form method='post'>" +
                                       "<input name='Th_Ma_so_Chuc_nang' type='hidden' value='DAT_HANG' />" +
                                        "<button type='submit' class='btn btn-danger'>Đặt hàng</button>" +
                                      "</form>";
            Chuoi_HTML_Danh_sach += Chuoi_Chuc_nang_Dat_hang;
        }

        foreach (XmlElement Laptop in Danh_sach_Laptop.GetElementsByTagName("Laptop"))
        {
            var Ten = Laptop.GetAttribute("Ten");
            var Ma_so = Laptop.GetAttribute("Ma_so");
            var Don_gia_Ban = long.Parse(Laptop.GetAttribute("Don_gia_Ban"));
            var Ban_hang =  (XmlElement)Laptop.GetElementsByTagName("Ban_hang")[0];
            var So_luong = Ban_hang.GetAttribute("So_luong");
            var Chuoi_Hinh = $"<img src='{Dia_chi_Media}/{ Ma_so}.png' " +
                             "style='width:90px;height:90px;' />";
            var Chuoi_Chuc_nang_Huy_Chon = "<form method='post'>" +
                                       "<input name='Th_Ma_so_Chuc_nang' type='hidden' value='HUY_CHON' />" +
                                        $"<input name='Th_Ma_so_Laptop' type='hidden' value='{Ma_so}' />" +
                                        "<button type='submit' class='btn btn-danger'>-</button>" +
                                      "</form>";
            var Chuoi_Thong_tin = $"<div class='btn' style='text-align:left'> " +
                          $"<strong>{Ten}</strong>" +
                          $"<br />{ Don_gia_Ban.ToString("c0", Dinh_dang_VN) }" +
                          $"<br />Số lượng: { So_luong}"+
                           $"{Chuoi_Chuc_nang_Huy_Chon}" +
                          $"</div>";
            var Chuoi_HTML = $"<div class='col-md-4' style='margin-bottom:10px' >" +
                               $"{Chuoi_Hinh}" + $"{Chuoi_Thong_tin}" +
                             "</div>";
            Chuoi_HTML_Danh_sach += Chuoi_HTML;
        }
        Chuoi_HTML_Danh_sach += "</div>";

        return Chuoi_HTML_Danh_sach;
    }
}
//************************* Business-Layers BL **********************************
public partial class XL_NGHIEP_VU
{
    public static XmlElement Tim_Laptop(string Ma_so, XmlElement Danh_sach_Laptop)
    {
        var Kq = (XmlElement)null;
        foreach (XmlElement Laptop in Danh_sach_Laptop.GetElementsByTagName("Laptop"))
        {
            if (Ma_so == Laptop.GetAttribute("Ma_so"))
                Kq = Laptop;

        }
        return Kq;
    }
    public static  XmlElement  Tra_cuu_Laptop(
          string Chuoi_Tra_cuu, XmlElement   Danh_sach_Laptop)
    {
        Chuoi_Tra_cuu = Chuoi_Tra_cuu.ToUpper();
        var Chuoi_Danh_sach_Kq = "<Danh_sach_Laptop />";
        var Tai_lieu = new XmlDocument();
        Tai_lieu.LoadXml(Chuoi_Danh_sach_Kq);
        var Danh_sach_Kq = Tai_lieu.DocumentElement;
        foreach(XmlElement Laptop in Danh_sach_Laptop.GetElementsByTagName("Laptop"))
        {
            var Ten = Laptop.GetAttribute("Ten");
           
            if (Ten.ToUpper().Contains(Chuoi_Tra_cuu ) )
            {
                var Laptop_Kq = Tai_lieu.ImportNode(Laptop, true );
                Danh_sach_Kq.AppendChild(Laptop_Kq);
            }
        }
        
                    
        return Danh_sach_Kq;
    }

    public static string Get_Ten_Chi_tiet_Laptop(XmlElement Laptop, string Chi_tiet)
    {
        var Chi_tiet_Element = (XmlElement)Laptop.GetElementsByTagName(Chi_tiet)[0];
        return Chi_tiet_Element.GetAttribute("Ten");
    }
    
    public static long Tinh_Tong_So_Laptop_Chon(XmlElement Danh_sach_Laptop)
    {
        var Kq = 0L;
        foreach(XmlElement Laptop in Danh_sach_Laptop.GetElementsByTagName("Laptop"))
        {
            var Ban_hang = (XmlElement)Laptop.GetElementsByTagName("Ban_hang")[0];
            Kq += long.Parse(Ban_hang.GetAttribute("So_luong"));
        }
        return Kq;
    }
}

//************************* Data-Layers DL **********************************
public partial class XL_LUU_TRU
{
    
    public static string Dia_chi_Dich_vu = "http://localhost:61828";
    static string Dia_chi_Dich_vu_Du_lieu = $"{Dia_chi_Dich_vu}/1-Dich_vu_Giao_tiep/DV_Khach_tham_quan.cshtml";

    public static  XmlElement  Doc_Du_lieu()
    {
        var Chuoi_XML = "<Du_lieu />";
        var Xu_ly = new WebClient();
        Xu_ly.Encoding = System.Text.Encoding.UTF8;
        var Tham_so = "Ma_so_Xu_ly=KHOI_DONG_DU_LIEU_KHACH_THAM_QUAN";
        var Dia_chi_Xu_ly = $"{Dia_chi_Dich_vu_Du_lieu}?{Tham_so}";
        try
        {
            var Chuoi_Kq = Xu_ly.DownloadString(Dia_chi_Xu_ly);
            if (Chuoi_Kq.Trim() != "")
                Chuoi_XML = Chuoi_Kq;


        }
        catch (Exception Loi)
        {
            Chuoi_XML = $"<Du_lieu Loi='{Loi.Message}'  />";
        }

        var Tai_lieu = new XmlDocument();
        Tai_lieu.LoadXml(Chuoi_XML);
        var Du_lieu = Tai_lieu.DocumentElement;
        
        return Du_lieu;
    }
    
     

}