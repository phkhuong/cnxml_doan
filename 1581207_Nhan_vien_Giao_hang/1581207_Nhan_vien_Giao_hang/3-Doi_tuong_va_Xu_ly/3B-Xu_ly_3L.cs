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


    public static string Tao_Chuoi_HTML_Danh_sach_Laptop(XmlElement Danh_sach_Laptop)
    {
        var Chuoi_HTML_Danh_sach = "<div class='row'>";
        foreach(XmlElement Dien_thoai in Danh_sach_Laptop.GetElementsByTagName("Laptop"))
        {
            var Ten = Dien_thoai.GetAttribute("Ten");
            var Ma_so = Dien_thoai.GetAttribute("Ma_so");
            var Don_gia_Ban = long.Parse(Dien_thoai.GetAttribute("Don_gia_Ban"));

            var Chuoi_Hinh = $"<div class='KHUNG_HINH mx-auto'>" +
                                $"<img src='{Dia_chi_Media}/{Ma_so}.png' class='img-thumbnail HINH'/>" +
                             "</div>";
            var Chuoi_Thong_tin = $"<div>" +
                                      $"<strong>{Ten}</strong>" +
                                      $"<br />Đơn giá: { Don_gia_Ban.ToString("c0", Dinh_dang_VN) }" +
                                  $"</div>";
            var Chuoi_HTML = $"<div class='KHUNG col-6 col-sm-4 col-md-3 col-lg-2'>" +
                                 $"<div class='THONG_TIN'>" +
                                     $"{Chuoi_Hinh}" +
                                     $"{Chuoi_Thong_tin}" +
                                     $"<button type='button' class='btn btn-primary hidden-sm-down btn-block ADD_BUTTON'>THÊM VÀO GIỎ HÀNG <img class='CART_ICON' src='https://phongvu.vn/skin/frontend/default/tek_v2/images/icon_ShoppingCart2.svg' /></button>" +
                                 $"</div>" +
                             "</div>";
            Chuoi_HTML_Danh_sach += Chuoi_HTML;
        }
        
        Chuoi_HTML_Danh_sach += "</div>";
        return Chuoi_HTML_Danh_sach;
    }

    public static string Tao_Chuoi_HTML_Danh_sach_Phieu_dat(XmlElement Danh_sach_Phieu_dat)
    {
        var Chuoi_HTML_Danh_sach = "<div class='row'>";
        foreach (XmlElement Phieu_dat in Danh_sach_Phieu_dat.GetElementsByTagName("Phieu_dat"))
        {
            var Ngay = Phieu_dat.GetAttribute("Ngay");
            var Ma_so = Phieu_dat.GetAttribute("Ma_so");
            var Tien = long.Parse(Phieu_dat.GetAttribute("Tien"));
            var Tinh_trang = Phieu_dat.GetAttribute("Tinh_trang");

            //var Chuoi_Hinh = $"<div class='KHUNG_HINH mx-auto'>" +
            //                    $"<img src='{Dia_chi_Media}/{Ma_so}.png' class='img-thumbnail HINH'/>" +
            //                 "</div>";
            var Chuoi_Thong_tin = $"<div>" +
                                      $"<strong>{Ma_so}</strong>" +
                                      $"<br />Ngày đặt: {Ngay}" +
                                      $"<br />Đơn giá: { Tien.ToString("c0", Dinh_dang_VN) }" +
                                      $"<br />Tình trạng: { Tinh_trang }" +
                                  $"</div>";
            var Chuoi_HTML = $"<div class='KHUNG col-6 col-sm-4 col-md-3 col-lg-2'>" +
                                 $"<div class='THONG_TIN'>" +
                                     
                                     $"{Chuoi_Thong_tin}" +
                                     $"<button type='button' class='btn btn-primary hidden-sm-down btn-block ADD_BUTTON'>CHI TIẾT</button>" +
                                 $"</div>" +
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
    
}

//************************* Data-Layers DL **********************************
public partial class XL_LUU_TRU
{
    
    public static string Dia_chi_Dich_vu = "http://localhost:61828";
    static string Dia_chi_Dich_vu_Du_lieu = $"{Dia_chi_Dich_vu}/1-Dich_vu_Giao_tiep/DV_Nhan_vien_Giao_hang.cshtml";

    public static  XmlElement  Doc_Du_lieu()
    {
        var Chuoi_XML = "<Du_lieu />";
        var Xu_ly = new WebClient();
        Xu_ly.Encoding = System.Text.Encoding.UTF8;
        var Tham_so = "Ma_so_Xu_ly=KHOI_DONG_DU_LIEU_NHAN_VIEN_GIAO_HANG";
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