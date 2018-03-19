using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.IO;
using System.Xml;
using System.Globalization;
using System.Net;
public class XL_UNG_DUNG
{
    //==================== Khởi động ==============
    static XL_UNG_DUNG Ung_dung = null;
    public bool Khoi_dong_Co_loi = false;
    XmlElement Du_lieu_Ung_dung;
    XmlElement Cong_ty;
    List<XmlElement> Danh_sach_Nhom_Laptop = new List<XmlElement>();
    List<XmlElement> Danh_sach_Laptop = new List<XmlElement>();
    List<XmlElement> Danh_sach_Nguoi_dung = new List<XmlElement>();

    public static XL_UNG_DUNG Khoi_dong_Ung_dung()
    {
        if (Ung_dung == null)
        {
            Ung_dung = new XL_UNG_DUNG();
            Ung_dung.Du_lieu_Ung_dung = XL_LUU_TRU.Doc_Du_lieu();
            if (Ung_dung.Du_lieu_Ung_dung.GetAttribute("Kq") == "OK")
                Ung_dung.Khoi_dong_Du_lieu_Ung_dung();
            else
                Ung_dung.Khoi_dong_Co_loi = true;
        }
        return Ung_dung;
    }

    void Khoi_dong_Du_lieu_Ung_dung()
    {


        Cong_ty = (XmlElement)Du_lieu_Ung_dung.GetElementsByTagName("Cong_ty")[0];
        var DS_Nhom_Laptop = (XmlElement)Cong_ty.GetElementsByTagName("Danh_sach_Nhom_Lap_top")[0];
        Danh_sach_Nhom_Laptop = XL_NGHIEP_VU.Tao_Danh_sach(DS_Nhom_Laptop, "Nhom_Lap_top");
        var DS_Nguoi_dung = (XmlElement)Cong_ty.GetElementsByTagName("Danh_sach_Nhan_vien")[0];
        Danh_sach_Nguoi_dung = XL_NGHIEP_VU.Tao_Danh_sach(DS_Nguoi_dung, "Nhan_vien");
        var DS_Laptop = (XmlElement)Du_lieu_Ung_dung.GetElementsByTagName("Danh_sach_Laptop")[0];
        Danh_sach_Laptop = XL_NGHIEP_VU.Tao_Danh_sach(DS_Laptop, "Laptop");

    }
    //============= Xử lý Chức năng ========
    public XL_NGUOI_DUNG_DANG_NHAP Dang_nhap(string Ten_Dang_nhap, string Mat_khau)
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_DANG_NHAP)null;

        var Nguoi_dung = Danh_sach_Nguoi_dung.FirstOrDefault(
            x => x.GetAttribute("Ten_Dang_nhap") == Ten_Dang_nhap && x.GetAttribute("Mat_khau") == Mat_khau);
        if (Nguoi_dung != null)
        {
            //var DS_Nhom_Laptop = (XmlElement)Nguoi_dung.GetElementsByTagName("Danh_sach_Nhom_Laptop")[0];
            //var Danh_sach_Nhom_Laptop_cua_Nguoi_dung = XL_NGHIEP_VU.Tao_Danh_sach(DS_Nhom_Laptop, "Nhom_Laptop");
            //var Danh_sach_Laptop_cua_Nguoi_dung = Danh_sach_Laptop.FindAll(
            //    x => Danh_sach_Nhom_Laptop_cua_Nguoi_dung.Any(Nhom => Nhom.GetAttribute("Ma_so") == x.SelectSingleNode("Nhom_Laptop/@Ma_so").Value));

            // Thống tin Online 
            Nguoi_dung_Dang_nhap = new XL_NGUOI_DUNG_DANG_NHAP();

            Nguoi_dung_Dang_nhap.Danh_sach_Laptop = Danh_sach_Laptop;
            Nguoi_dung_Dang_nhap.Danh_sach_Nhom_Laptop = Danh_sach_Nhom_Laptop;
            Nguoi_dung_Dang_nhap.Danh_sach_Laptop_Xem = Nguoi_dung_Dang_nhap.Danh_sach_Laptop;
            HttpContext.Current.Session["Nguoi_dung_Dang_nhap"] = Nguoi_dung_Dang_nhap;
        }

        return Nguoi_dung_Dang_nhap;
    }
    //Chức năng Xem
    public string Khoi_dong()
    {
        var Chuoi_HTML = Tao_Chuoi_HTML_Ket_qua();
        return Chuoi_HTML;
    }
    public string Xem_Danh_sach_Laptop()
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_DANG_NHAP)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        Nguoi_dung_Dang_nhap.Danh_sach_Laptop_Xem = Danh_sach_Laptop;
        var Chuoi_HTML = Tao_Chuoi_HTML_Ket_qua();
        return Chuoi_HTML;
    }
    public string Tra_cuu(string Chuoi_Tra_cuu)
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_DANG_NHAP)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        Nguoi_dung_Dang_nhap.Danh_sach_Laptop_Xem = XL_NGHIEP_VU.Tra_cuu_Laptop(
            Chuoi_Tra_cuu, Nguoi_dung_Dang_nhap.Danh_sach_Laptop);

        var Chuoi_HTML = Tao_Chuoi_HTML_Ket_qua();
        return Chuoi_HTML;
    }
    public string Chon_Nhom_Laptop(string Ma_so_Laptop)
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_DANG_NHAP)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        Nguoi_dung_Dang_nhap.Danh_sach_Laptop_Xem = XL_NGHIEP_VU.Tra_cuu_Laptop(
           Ma_so_Laptop, Nguoi_dung_Dang_nhap.Danh_sach_Laptop);
        var Chuoi_HTML = Tao_Chuoi_HTML_Ket_qua();
        return Chuoi_HTML;
    }
    //Chức năng Ghi
    public string Ghi_Nhap_hang_Moi(string Ma_so_Laptop, int So_luong)
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_DANG_NHAP)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        var Laptop = Danh_sach_Laptop.FirstOrDefault(x => x.GetAttribute("Ma_so") == Ma_so_Laptop);


        var Hop_le = Laptop != null;
        if (Hop_le)
        {
            Nguoi_dung_Dang_nhap.Danh_sach_Laptop_Xem = new List<XmlElement>();
            Nguoi_dung_Dang_nhap.Danh_sach_Laptop_Xem.Add(Laptop);

            var Nhap_hang = Laptop.OwnerDocument.CreateElement("Nhap_hang");
            var Don_gia_Nhap = long.Parse(Laptop.GetAttribute("Don_gia_Nhap"));
            var Tien = So_luong * Don_gia_Nhap;
            Nhap_hang.SetAttribute("Ngay", DateTime.Now.ToString());
            Nhap_hang.SetAttribute("So_luong", So_luong.ToString());
            Nhap_hang.SetAttribute("Don_gia", Don_gia_Nhap.ToString());
            Nhap_hang.SetAttribute("Tien", Tien.ToString());
            var Kq_Ghi = XL_LUU_TRU.Ghi_Nhap_hang_Moi(Laptop, Nhap_hang);
            if (Kq_Ghi == "OK")
                Nguoi_dung_Dang_nhap.Thong_bao = "Tiền " + Tien.ToString();
            else
                Nguoi_dung_Dang_nhap.Thong_bao = "Lỗi Hệ thống - Xin Thực hiện lại  ";

        }
        else
            Nguoi_dung_Dang_nhap.Thong_bao = "Lỗi Hệ thống - Xin Thực hiện lại ";

        var Chuoi_HTML = Tao_Chuoi_HTML_Ket_qua();
        return Chuoi_HTML;

    }


    public string Tao_Chuoi_HTML_Ket_qua()
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_DANG_NHAP)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];

        var Chuoi_HTML = $"<div>" +
                 $"{XL_THE_HIEN.Tao_Chuoi_HTML_Thong_bao(Nguoi_dung_Dang_nhap.Thong_bao)}" +
                 $"{XL_THE_HIEN.Tao_Chuoi_HTML_Danh_sach_Nhom_Laptop_Xem(Nguoi_dung_Dang_nhap.Danh_sach_Nhom_Laptop)}" +
                 $"{XL_THE_HIEN.Tao_Chuoi_HTML_Danh_sach_Laptop_Xem(Nguoi_dung_Dang_nhap.Danh_sach_Laptop_Xem)}" +
             $"</div>";
        return Chuoi_HTML;

    }
}
//************************* View/Presentation -Layers VL/PL **********************************
public partial class XL_THE_HIEN
{
    public static string Dia_chi_Media = $"{XL_LUU_TRU.Dia_chi_Dich_vu}/Media";
    public static CultureInfo Dinh_dang_VN = CultureInfo.GetCultureInfo("vi-VN");

    public static string Tao_Chuoi_HTML_Thong_bao(string Thong_bao)
    {
        var Chuoi_HTML = $"<div class='alert alert-info'>" +
                          $"{Thong_bao} " +
                          $"</div>";
        return Chuoi_HTML;
    }
    public static string Tao_Chuoi_HTML_Danh_sach_Laptop_Xem(List<XmlElement> Danh_sach_Laptop)
    {
        var Chuoi_HTML_Danh_sach = "<div class='row'>";
        foreach(XmlElement Laptop in Danh_sach_Laptop)
        {
            var Ten = Laptop.GetAttribute("Ten");
            var Ma_so = Laptop.GetAttribute("Ma_so");
            var Don_gia_Ban = long.Parse(Laptop.GetAttribute("Don_gia_Ban"));
            var Chuoi_Chuc_nang_Chon = "<form method='post'>" +
                                        "<input name='Th_Ma_so_Chuc_nang' type='hidden' value='GHI_NHAP_HANG_MOI' />" +
                                         $"<input name='Th_Ma_so_Laptop' type='hidden' value='{Ma_so}' />" +
                                         $"<input name='Th_So_luong' required='required' autocomplete='off' " +
                                         $"style='border:none;border-bottom:solid 1px blue'" +
                                        $"type='number' min='1'  max='100' value='10' />  " +
                                       "</form>";
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
                                     $"{Chuoi_Chuc_nang_Chon}"+
                                 $"</div>" +
                             "</div>";
            Chuoi_HTML_Danh_sach += Chuoi_HTML;
        }
        
        Chuoi_HTML_Danh_sach += "</div>";
        return Chuoi_HTML_Danh_sach;
    }

    public static string Tao_Chuoi_HTML_Danh_sach_Nhom_Laptop_Xem(List<XmlElement> Danh_sach)
    {
        var Chuoi_HTML_Danh_sach = "<div class='btn btn-primary' style='margin:10px'>";
        Danh_sach.ForEach(Nhom_Laptop =>
        {
            var Ten = Nhom_Laptop.GetAttribute("Ten");
            var Ma_so = Nhom_Laptop.GetAttribute("Ma_so");

            var So_luong_Ton = int.Parse(Nhom_Laptop.GetAttribute("So_luong_Ton"));
            var Chuoi_Chuc_nang_Chon = $"<form method='post'>" +
                                        "<input name='Th_Ma_so_Chuc_nang' type='hidden' value='CHON_NHOM_LAPTOP' />" +
                                         $"<input name='Th_Ma_so_Nhom_Laptop' type='hidden' value='{Ma_so}' />" +
                                         $"<button type='submit' class ='btn btn-primary'>{Ten} ({So_luong_Ton})</button>" +
                                       "</form>";
            var Chuoi_Hinh = $"";
            var Chuoi_Thong_tin = $"<div class='' style=''> " +
                          $"{Chuoi_Chuc_nang_Chon}" +
                          $"</div>";
            var Chuoi_HTML = $"<div class='btn ' style=' ' >" +
                               $"{Chuoi_Hinh}" + $"{Chuoi_Thong_tin}" +
                             "</div>";
            Chuoi_HTML_Danh_sach += Chuoi_HTML;
        });

        Chuoi_HTML_Danh_sach += "</div>";
        return Chuoi_HTML_Danh_sach;
    }
}
//************************* Business-Layers BL **********************************
public partial class XL_NGHIEP_VU
{
    public static XmlElement Tim_Laptop(string Ma_so, List<XmlElement> Danh_sach_Laptop)
    {
        var Kq = (XmlElement)null;
        foreach (XmlElement Laptop in Danh_sach_Laptop)
        {
            if (Ma_so == Laptop.GetAttribute("Ma_so"))
                Kq = Laptop;

        }
        return Kq;
    }
    public static  List<XmlElement>  Tra_cuu_Laptop(
          string Chuoi_Tra_cuu, List<XmlElement>   Danh_sach_Laptop)
    {
        Chuoi_Tra_cuu = Chuoi_Tra_cuu.ToUpper();
        var Danh_sach_Kq = new List<XmlElement>();
        foreach(XmlElement Laptop in Danh_sach_Laptop)
        {
            var Ten = Laptop.GetAttribute("Ten");
           
            if (Ten.ToUpper().Contains(Chuoi_Tra_cuu ) || Laptop.SelectSingleNode("Nhom_Lap_top/@Ma_so").Value == Chuoi_Tra_cuu)
            {
                Danh_sach_Kq.Add(Laptop);
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
}

//************************* Data-Layers DL **********************************
public partial class XL_LUU_TRU
{
    
    public static string Dia_chi_Dich_vu = "http://localhost:61828";
    static string Dia_chi_Dich_vu_Du_lieu = $"{Dia_chi_Dich_vu}/1-Dich_vu_Giao_tiep/DV_Nhan_vien_Nhap_hang.cshtml";

    public static  XmlElement  Doc_Du_lieu()
    {
        var Chuoi_XML = "<Du_lieu />";
        var Xu_ly = new WebClient();
        Xu_ly.Encoding = System.Text.Encoding.UTF8;
        var Tham_so = "Ma_so_Xu_ly=KHOI_DONG_DU_LIEU_NHAN_VIEN_NHAP_HANG";
        var Dia_chi_Xu_ly = $"{Dia_chi_Dich_vu_Du_lieu}?{Tham_so}";
        try
        {
            var Chuoi_Kq = Xu_ly.DownloadString(Dia_chi_Xu_ly);
            if (Chuoi_Kq.Trim() != "")
                Chuoi_XML = Chuoi_Kq;


        }
        catch (Exception Loi)
        {
            Chuoi_XML = $"<Du_lieu Kq='{Loi.Message}'  />";
        }

        var Tai_lieu = new XmlDocument();
        Tai_lieu.LoadXml(Chuoi_XML);
        var Du_lieu = Tai_lieu.DocumentElement;
        Du_lieu.SetAttribute("Kq", "OK");
        return Du_lieu;
    }

    public static string Ghi_Nhap_hang_Moi(XmlElement Laptop, XmlElement Nhap_hang)
    {
        var Kq = "OK";

        try
        {
            var Xu_ly = new WebClient();
            Xu_ly.Encoding = System.Text.Encoding.UTF8;
            var Tham_so = $"Ma_so_Xu_ly=GHI_NHAP_HANG_MOI&Ma_so_Laptop={Laptop.GetAttribute("Ma_so")}";
            var Dia_chi_Xu_ly = $"{Dia_chi_Dich_vu_Du_lieu}?{Tham_so}";
            var Chuoi_XML_Nhap_hang = Nhap_hang.OuterXml;
            var Chuoi_XML_Kq = Xu_ly.UploadString(Dia_chi_Xu_ly, Chuoi_XML_Nhap_hang).Trim();
            var Tai_lieu = new XmlDocument();
            Tai_lieu.LoadXml(Chuoi_XML_Kq);
            Kq = Tai_lieu.DocumentElement.GetAttribute("Kq");
        }
        catch (Exception Loi)
        {
            Kq = Loi.Message;
        }
        if (Kq == "OK")
        {
            var So_luong_Ton = int.Parse(Laptop.GetAttribute("So_luong_Ton"));
            var So_luong = int.Parse(Nhap_hang.GetAttribute("So_luong"));
            So_luong_Ton += So_luong;
            Laptop.SetAttribute("So_luong_Ton", So_luong_Ton.ToString());

        }
        return Kq;

    }


}