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
public class XL_UNG_DUNG
{
    //==================== Khởi động ==============
    static XL_UNG_DUNG Ung_dung = null;
    XmlElement Du_lieu_Ung_dung;
    XmlElement Cua_hang;
    List<XmlElement> Danh_sach_Nhom_Lap_top=new List<XmlElement>();
    List<XmlElement> Danh_sach_Lap_top=new List<XmlElement>();
    List<XmlElement> Danh_sach_Nguoi_dung = new List<XmlElement>();

    public static XL_UNG_DUNG Khoi_dong_Ung_dung()
    {
        if (Ung_dung == null)
        {
            Ung_dung = new XL_UNG_DUNG();
            Ung_dung.Khoi_dong_Du_lieu_Ung_dung();
        }
        return Ung_dung;
    }
    void Khoi_dong_Du_lieu_Ung_dung()
    {
        Du_lieu_Ung_dung = XL_LUU_TRU.Doc_Du_lieu();
        Cua_hang = (XmlElement)Du_lieu_Ung_dung.GetElementsByTagName("Cong_ty")[0];
        var DS_Nhom_Lap_top = (XmlElement)Cua_hang.GetElementsByTagName("Danh_sach_Nhom_Lap_top")[0];
        Danh_sach_Nhom_Lap_top = XL_NGHIEP_VU.Tao_Danh_sach(DS_Nhom_Lap_top, "Nhom_Lap_top");
        var DS_Lap_top = (XmlElement)Du_lieu_Ung_dung.GetElementsByTagName("Danh_sach_Lap_top")[0];
        Danh_sach_Lap_top = XL_NGHIEP_VU.Tao_Danh_sach(DS_Lap_top, "Laptop");

        var DS_Nguoi_dung = (XmlElement)Cua_hang.GetElementsByTagName("Danh_sach_Nguoi_dung")[0];
        Danh_sach_Nguoi_dung = XL_NGHIEP_VU.Tao_Danh_sach(DS_Nguoi_dung, "Nguoi_dung");
    }
    //============= Xử lý Chức năng ========
    public string Dang_nhap(string Ten_Dang_nhap, string Mat_khau)
    {

        var Nguoi_dung = Danh_sach_Nguoi_dung.FirstOrDefault(
            x => x.GetAttribute("Ten_Dang_nhap") == Ten_Dang_nhap && x.GetAttribute("Mat_khau") == Mat_khau);
        var Chuoi_HTML = "";
        if (Nguoi_dung != null)
        {
            var Ma_so_Nhom_Nguoi_dung = Nguoi_dung.SelectSingleNode("Nhom_Nguoi_dung/@Ma_so").Value;
            if (Ma_so_Nhom_Nguoi_dung == "NHAP_HANG")
            {
                var Dia_chi_MH_Dang_nhap = "http://localhost:64817/1-Man_hinh_Giao_dien/MH_Dang_nhap.cshtml";
                var Tham_so = $"Th_Ma_so_Chuc_nang=DANG_NHAP&Th_Ten_Dang_nhap={Ten_Dang_nhap}&Th_Mat_khau={Mat_khau}";
                var Dia_chi_Xu_ly = $"{Dia_chi_MH_Dang_nhap}?{Tham_so}";
                HttpContext.Current.Response.Redirect(Dia_chi_Xu_ly);
            }
            else if(Ma_so_Nhom_Nguoi_dung == "BAN_HANG")
            {
                var Dia_chi_MH_Dang_nhap = "http://localhost:64820/1-Man_hinh_Giao_dien/MH_Dang_nhap.cshtml";
                var Tham_so = $"Th_Ma_so_Chuc_nang=DANG_NHAP&Th_Ten_Dang_nhap={Ten_Dang_nhap}&Th_Mat_khau={Mat_khau}";
                var Dia_chi_Xu_ly = $"{Dia_chi_MH_Dang_nhap}?{Tham_so}";
                HttpContext.Current.Response.Redirect(Dia_chi_Xu_ly);
            }
            else if (Ma_so_Nhom_Nguoi_dung == "QUAN_LY_BAN_HANG")
            {
                var Dia_chi_MH_Dang_nhap = "http://localhost:64819/1-Man_hinh_Giao_dien/MH_Dang_nhap.cshtml";
                var Tham_so = $"Th_Ma_so_Chuc_nang=DANG_NHAP&Th_Ten_Dang_nhap={Ten_Dang_nhap}&Th_Mat_khau={Mat_khau}";
                var Dia_chi_Xu_ly = $"{Dia_chi_MH_Dang_nhap}?{Tham_so}";
                HttpContext.Current.Response.Redirect(Dia_chi_Xu_ly);
            }
            else if (Ma_so_Nhom_Nguoi_dung == "QUAN_LY_NHAP_HANG")
            {
                var Dia_chi_MH_Dang_nhap = "http://localhost:64817/1-Man_hinh_Giao_dien/MH_Dang_nhap.cshtml";
                var Tham_so = $"Th_Ma_so_Chuc_nang=DANG_NHAP&Th_Ten_Dang_nhap={Ten_Dang_nhap}&Th_Mat_khau={Mat_khau}";
                var Dia_chi_Xu_ly = $"{Dia_chi_MH_Dang_nhap}?{Tham_so}";
                HttpContext.Current.Response.Redirect(Dia_chi_Xu_ly);
            }
        }

        else Chuoi_HTML = "Không hợp lệ";
        return Chuoi_HTML;
    }
    public string Khoi_dong_Nguoi_dung()
    {
        var Khach_Tham_quan = new XL_KHACH_THAM_QUAN();
        Khach_Tham_quan.Danh_sach_Lap_top = Danh_sach_Lap_top;
        Khach_Tham_quan.Danh_sach_Nhom_Lap_top = Danh_sach_Nhom_Lap_top;
        Khach_Tham_quan.Danh_sach_Lap_top_Xem = Danh_sach_Lap_top;
        Khach_Tham_quan.Danh_sach_Lap_top_Chon = new List<XmlElement>();
        HttpContext.Current.Session["Khach_Tham_quan"] = Khach_Tham_quan;

        var Chuoi_HTML = Tao_Chuoi_HTML_Ket_qua();
        return Chuoi_HTML;
    }

    public string Chon_Nhom_Lap_top(string Ma_so_Nhom_Lap_top)
    {

        var Khach_Tham_quan = (XL_KHACH_THAM_QUAN)HttpContext.Current.Session["Khach_Tham_quan"];
        Khach_Tham_quan.Danh_sach_Lap_top_Xem = XL_NGHIEP_VU.Tra_cuu_Lap_top(Ma_so_Nhom_Lap_top, Khach_Tham_quan.Danh_sach_Lap_top);
        var Chuoi_HTML = Tao_Chuoi_HTML_Ket_qua();
        return Chuoi_HTML;
    }
    public string Chon_Lap_top(string Ma_so_Lap_top)
    {
        var Khach_Tham_quan = (XL_KHACH_THAM_QUAN)HttpContext.Current.Session["Khach_Tham_quan"];
        // Xử lý 
        var Lap_top = Khach_Tham_quan.Danh_sach_Lap_top.FirstOrDefault(x => x.GetAttribute("Ma_so") == Ma_so_Lap_top);

        if (!Khach_Tham_quan.Danh_sach_Lap_top_Chon.Contains(Lap_top))
        {
            Lap_top.SetAttribute("So_luong", "1");
            Khach_Tham_quan.Danh_sach_Lap_top_Chon.Add(Lap_top);
        }
        else
        {
            var So_luong_Ton = int.Parse(Lap_top.GetAttribute("So_luong_Ton"));
            var So_luong = int.Parse(Lap_top.GetAttribute("So_luong"));
            if (So_luong < So_luong_Ton)
            {
                So_luong += 1;
                Lap_top.SetAttribute("So_luong", So_luong.ToString());
            }

        }
        // Tạo chuỗi HTML kết quả xem 
        var Chuoi_HTML = Tao_Chuoi_HTML_Ket_qua();
        return Chuoi_HTML;
    }
    public string Giam_So_luong_Lap_top(string Ma_so_Lap_top)
    {
        var Khach_Tham_quan = (XL_KHACH_THAM_QUAN)HttpContext.Current.Session["Khach_Tham_quan"];
        // Xử lý 
        var Lap_top = Khach_Tham_quan.Danh_sach_Lap_top.FirstOrDefault(x => x.GetAttribute("Ma_so") == Ma_so_Lap_top);
        var So_luong = int.Parse(Lap_top.GetAttribute("So_luong"));
        So_luong -= 1;
        Lap_top.SetAttribute("So_luong", So_luong.ToString());
        if (So_luong == 0)
            Khach_Tham_quan.Danh_sach_Lap_top_Chon.Remove(Lap_top);
        // Tạo chuỗi HTML kết quả xem 
        var Chuoi_HTML = Tao_Chuoi_HTML_Ket_qua();
        return Chuoi_HTML;
    }
    public string Dat_hang()
    {
        var Khach_Tham_quan = (XL_KHACH_THAM_QUAN)HttpContext.Current.Session["Khach_Tham_quan"];
        // Xử lý 

        // Tạo chuỗi HTML kết quả xem 
        var Chuoi_HTML = "<iframe src='MH_Dat_hang.cshtml' style='width:100%;height:1000vh;border:none'></iframe>";
        return Chuoi_HTML;
    }
    //============= Xử lý Chức năng ========
    public string Ghi_Phieu_Dat_hang_Moi(string Ho_ten, string Dia_chi, string Dien_thoai, XL_KHACH_THAM_QUAN Khach)
    {


        var Chuoi_XML = "<PHIEU_DAT />";
        var Tai_lieu = new XmlDocument();
        Tai_lieu.LoadXml(Chuoi_XML);
        var Phieu_dat = Tai_lieu.DocumentElement;
        Phieu_dat.SetAttribute("Ma_so", "");
        Phieu_dat.SetAttribute("Ngay", DateTime.Now.ToString());
        Phieu_dat.SetAttribute("Trang_thai", "CHUA_GIAO_HANG");
        var Khach_hang= (Tai_lieu.CreateElement("Khach_hang"));
        Khach_hang.SetAttribute("Ho_ten", Ho_ten);
        Khach_hang.SetAttribute("Dien_thoai", Dien_thoai);
        Khach_hang.SetAttribute("Dia_chi", Dia_chi);
        Phieu_dat.AppendChild(Khach_hang);
        var Ds = Tai_lieu.CreateElement("Danh_sach_Lap_top");
        Phieu_dat.AppendChild(Ds);
        Khach.Danh_sach_Lap_top_Chon.ForEach(Lap_top =>
        {

            var QA = Tai_lieu.CreateElement("Laptop");
            QA.SetAttribute("Ma_so", Lap_top.GetAttribute("Ma_so"));
            QA.SetAttribute("Ten", Lap_top.GetAttribute("Ten"));
            var Nhom_Lap_top=Tai_lieu.CreateElement("Nhom_Lap_top");
            Nhom_Lap_top.SetAttribute("Nhom_Lap_top", Lap_top.SelectSingleNode("Nhom_Lap_top/@Ma_so").Value);
            QA.AppendChild(Nhom_Lap_top);
            var Don_Gia = long.Parse(Lap_top.GetAttribute("Don_gia_Ban"));
            var So_luong = long.Parse(Lap_top.GetAttribute("So_luong"));
            var Tien = Don_Gia * So_luong;
            QA.SetAttribute("Don_gia", Lap_top.GetAttribute("Don_gia_Ban"));
            QA.SetAttribute("So_luong", Lap_top.GetAttribute("So_luong"));
            QA.SetAttribute("Tien", Tien.ToString());
            Ds.AppendChild(QA);
                /*var tailieu = new XmlDocument();
                tailieu.LoadXml(Lap_top.OuterXml);
                tailieu.remoa
                var Lap_top1 = tailieu.DocumentElement;
                var Lap_top2 = Tai_lieu.ImportNode(Lap_top1, true);
                Ds.AppendChild(Lap_top2);*/
        });
        Phieu_dat.AppendChild(Ds);
        var Kq_Ghi = XL_LUU_TRU.Ghi_Phieu_Dat_hang_moi(Phieu_dat);
        Khach.Thong_bao = "";
            if (Kq_Ghi == "OK")
            {
                Khach.Thong_bao = "Đặt phiếu Thành công";
                Khach.Danh_sach_Lap_top_Chon = new List<XmlElement>();
        }
            else
                Khach.Thong_bao = "Lỗi Hệ thống - Xin Thực hiện lại  ";


        var Chuoi_HTML = XL_THE_HIEN.Tao_chuoi_HTML_Thong_bao(Khach.Thong_bao);
        return Chuoi_HTML;

    }
    public string Tra_cuu(string Chuoi_Tra_cuu)
    {
        var Khach_Tham_quan = (XL_KHACH_THAM_QUAN)HttpContext.Current.Session["Khach_Tham_quan"];
        Khach_Tham_quan.Danh_sach_Lap_top_Xem = XL_NGHIEP_VU.Tra_cuu_Lap_top(Chuoi_Tra_cuu,
               Khach_Tham_quan.Danh_sach_Lap_top);
        var Danh_sach_Nhom_Lap_top_Xem = Danh_sach_Nhom_Lap_top;
        var Chuoi_HTML = Tao_Chuoi_HTML_Ket_qua();
        return Chuoi_HTML;
    }
    public string Tao_Chuoi_HTML_Ket_qua()
    {
        var Khach_Tham_quan = (XL_KHACH_THAM_QUAN)HttpContext.Current.Session["Khach_Tham_quan"];

        var Chuoi_HTML = $"<div>" +
                 $"{XL_THE_HIEN.Tao_chuoi_HTML_Thong_bao(Khach_Tham_quan.Thong_bao)}"+
                 $"{XL_THE_HIEN.Tao_Chuoi_HTML_Danh_sach_Lap_top_Chon(Khach_Tham_quan.Danh_sach_Lap_top_Chon)}" +
                 $"{XL_THE_HIEN.Tao_Chuoi_HTML_Danh_sach_Nhom_Lap_top_Xem(Khach_Tham_quan.Danh_sach_Nhom_Lap_top)}" +
                 $"{XL_THE_HIEN.Tao_Chuoi_HTML_Danh_sach_Lap_top_Xem(Khach_Tham_quan.Danh_sach_Lap_top_Xem)}" +
             $"</div>";
        return Chuoi_HTML;

    }
}
//************************* View/Presentation -Layers VL/PL **********************************
public partial class XL_THE_HIEN
{
    public static string Dia_chi_Media = $"{XL_LUU_TRU.Dia_chi_Dich_vu}/Media";
    public static CultureInfo Dinh_dang_VN = CultureInfo.GetCultureInfo("vi-VN");

    public static string Tao_chuoi_HTML_Thong_bao(string Thong_bao)
    {
        var Chuoi_HTML_Thong_bao = "<div class='btn btn-alert'>";
        Chuoi_HTML_Thong_bao += Thong_bao;
        Chuoi_HTML_Thong_bao += "</div>";
        return Chuoi_HTML_Thong_bao;
    }
    public static string Tao_Chuoi_HTML_Danh_sach_Lap_top_Xem(List<XmlElement> Danh_sach)
    {
        var Chuoi_HTML_Danh_sach = "<div class='row'>";
        Danh_sach.ForEach(Lap_top =>
        {
            var Ten = Lap_top.GetAttribute("Ten");
            var Ma_so = Lap_top.GetAttribute("Ma_so");
            var Don_gia_Ban = long.Parse(Lap_top.GetAttribute("Don_gia_Ban"));
            var So_luong_Ton = int.Parse(Lap_top.GetAttribute("So_luong_Ton"));

            var Dinh_dang_Trang_thai = ""; var Chuoi_Trang_thai = "";
            var Chuoi_Chuc_nang_Chon = $"<form method='post'>" +
                     "<input name='Th_Ma_so_Chuc_nang' type='hidden' value='CHON_LAP_TOP' />" +
                      $"<input name='Th_Ma_so_Lap_top' type='hidden' value='{Ma_so}' />" +
                      $"<button type='submit' class='btn btn-danger' >Chọn</button>" +
                 "</form>";
            if (So_luong_Ton == 0)
            {
                Dinh_dang_Trang_thai = ";opacity:0.7"; ;
                Chuoi_Trang_thai = "Tạm thời hết hàng";
                Chuoi_Chuc_nang_Chon = "";
            }

            var Chuoi_Hinh = $"<img src='{Dia_chi_Media}/{ Ma_so}.png' " +
                             "style='width:90px;height:90px;' />";

            var Chuoi_Thong_tin = $"<div class='btn' style='text-align:left'> " +
                          $"<div style='width:300px;white-space: nowrap;overflow: hidden;text-overflow: ellipsis;'>{ Ten}" +
                          $"</div>"+
                          $"Đơn giá Bán {  Don_gia_Ban.ToString("n0", Dinh_dang_VN) }" +
                          $"<br />{ Chuoi_Trang_thai }" +
                          $"</div>";
            var Chuoi_HTML = $"<div class='col-md-4' style='margin-bottom:10px;{Dinh_dang_Trang_thai}' >" +
                               $"{Chuoi_Hinh}" + $"{Chuoi_Thong_tin}" +
                                 $"{Chuoi_Chuc_nang_Chon}" +
                             "</div>";
            Chuoi_HTML_Danh_sach += Chuoi_HTML;
        });

        Chuoi_HTML_Danh_sach += "</div>";
        return Chuoi_HTML_Danh_sach;
    }

    public static string Tao_Chuoi_HTML_Danh_sach_Lap_top_Chon(List<XmlElement> Danh_sach)
    {
        var Chuoi_HTML_Danh_sach = "<div class='row'>";
        var Chuoi_Chuc_nang_Dat_hang = $"<div style='margin-left:10px' ><form method='post'>" +
                      "<input name='Th_Ma_so_Chuc_nang' type='hidden' value='DAT_HANG' />" +
                       $"<button type='submit' class='btn btn-danger' >Đặt hàng</button>" +
                  "</form></div>";
        if (Danh_sach.Count > 0)
            Chuoi_HTML_Danh_sach += Chuoi_Chuc_nang_Dat_hang;

        Danh_sach.ForEach(Lap_top =>
        {
            var Ten = Lap_top.GetAttribute("Ten");
            var Ma_so = Lap_top.GetAttribute("Ma_so");
            var Don_gia_Ban = long.Parse(Lap_top.GetAttribute("Don_gia_Ban"));
            var So_luong_Ton = int.Parse(Lap_top.GetAttribute("So_luong_Ton"));
            var So_luong = int.Parse(Lap_top.GetAttribute("So_luong"));
            var Dinh_dang_Trang_thai = ""; var Chuoi_Trang_thai = "";
            var Chuoi_Chuc_nang_Giam_So_luong = $"<form method='post'>" +
                        "<input name='Th_Ma_so_Chuc_nang' type='hidden' value='GIAM_SO_LUONG_LAP_TOP' />" +
                         $"<input name='Th_Ma_so_Lap_top' type='hidden' value='{Ma_so}' />" +
                         $"<button type='submit' class='btn btn-danger' >-</button>" +
                    "</form>";


            var Chuoi_Hinh = $"<img src='{Dia_chi_Media}/{ Ma_so}.png' " +
                             "style='width:90px;height:90px;' />";

            var Chuoi_Thong_tin = $"<div class='btn' style='text-align:left'> " +
                          $"{ Ten}" +
                          $"<br />Đơn giá Bán {  Don_gia_Ban.ToString("n0", Dinh_dang_VN) }" +
                          $"<br />Số lượng Đặt {  So_luong.ToString("n0", Dinh_dang_VN) }" +
                          $"<br />{ Chuoi_Trang_thai }" +
                          $"</div>";
            var Chuoi_HTML = $"<div class='col-md-4' style='margin-bottom:10px;{Dinh_dang_Trang_thai}' >" +
                               $"{Chuoi_Hinh}" + $"{Chuoi_Thong_tin}" +
                                 $"{Chuoi_Chuc_nang_Giam_So_luong}" +
                             "</div>";
            Chuoi_HTML_Danh_sach += Chuoi_HTML;
        });

        Chuoi_HTML_Danh_sach += "</div>";
        return Chuoi_HTML_Danh_sach;
    }
    public static string Tao_Chuoi_HTML_Danh_sach_Nhom_Lap_top_Xem(List<XmlElement> Danh_sach)
    {
        var Chuoi_HTML_Danh_sach = "<div class='' style='margin:10px'>";
        Danh_sach.ForEach(Nhom_Lap_top =>
        {
            var Ten = Nhom_Lap_top.GetAttribute("Ten");
            var Ma_so = Nhom_Lap_top.GetAttribute("Ma_so");
            var Chuoi_Hinh = $"";
            var Chuoi_Chuc_nang_Chon = "<form method='post'>" +
                                         "<input name='Th_Ma_so_Chuc_nang' type='hidden' value='CHON_NHOM_LAP_TOP' />" +
                                          $"<input name='Th_Ma_so_Nhom_Lap_top' type='hidden' value='{Ma_so}' />" +
                                          $"<button type='submit' class='btn btn-primary'>{Ten}</button>" +
                                        "</form>";
            var Chuoi_Thong_tin = $"<div class='' style=''> " +
                          $"{Chuoi_Chuc_nang_Chon} " +
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
    public static List<XmlElement> Tra_cuu_Lap_top(
          string Chuoi_Tra_cuu, List<XmlElement> Danh_sach)
    {
        Chuoi_Tra_cuu = Chuoi_Tra_cuu.ToUpper();
        var Danh_sach_Kq = new List<XmlElement>();
        Danh_sach_Kq = Danh_sach.FindAll(x => x.GetAttribute("Ten").ToUpper().Contains(Chuoi_Tra_cuu)
           || x.SelectSingleNode("Nhom_Lap_top/@Ma_so").Value == Chuoi_Tra_cuu);

        return Danh_sach_Kq;
    }
    public static XmlElement Tra_cuu_Nhom_Lap_top(string Ma_so, XmlElement Danh_sach_Nhom_Lap_top)
    {
        var Kq = (XmlElement)null;
        foreach (XmlElement Nhom_Lap_top in Danh_sach_Nhom_Lap_top.GetElementsByTagName("Laptop"))
        {
            if (Ma_so == Nhom_Lap_top.GetAttribute("Ma_so"))
                Kq = Nhom_Lap_top;

        }
        return Kq;
    }
}
//************************* Data-Layers DL **********************************
public partial class XL_LUU_TRU
{
    // Cục bộ
    public static string Dia_chi_Dich_vu = "http://localhost:61828";

    static string Dia_chi_Dich_vu_Du_lieu = $"{Dia_chi_Dich_vu}/1-Dich_vu_Giao_tiep/DV_Chinh.cshtml";

    public static XmlElement Doc_Du_lieu()
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
    public static string Ghi_Phieu_Dat_hang_moi(XmlElement Phieu_dat)
    {
        var Kq = "OK";

        try
        {
            var Xu_ly = new WebClient();
            Xu_ly.Encoding = System.Text.Encoding.UTF8;
            var Tham_so = $"Ma_so_Xu_ly=GHI_PHIEU_DAT_HANG_MOI";
            var Dia_chi_Xu_ly = $"{Dia_chi_Dich_vu_Du_lieu}?{Tham_so}";
            var Chuoi_XML_Phieu_dat = Phieu_dat.OuterXml;
            var Chuoi_XML_Kq = Xu_ly.UploadString(Dia_chi_Xu_ly, Chuoi_XML_Phieu_dat).Trim();
            var Tai_lieu = new XmlDocument();
            Tai_lieu.LoadXml(Chuoi_XML_Kq);
            Kq = Tai_lieu.DocumentElement.GetAttribute("Kq");
        }
        catch (Exception Loi)
        {
            Kq = Loi.Message;
        }
        /*if (Kq == "OK")
        {
            var So_luong_Ton = int.Parse(Lap_top.GetAttribute("So_luong_Ton"));
            var So_luong = int.Parse(Ban_hang.GetAttribute("So_luong"));
            So_luong_Ton -= So_luong;
            Lap_top.SetAttribute("So_luong_Ton", So_luong_Ton.ToString());
            var Doanh_thu = long.Parse(Lap_top.GetAttribute("Doanh_thu"));
            var Ban = long.Parse(Ban_hang.GetAttribute("Tien"));
            Doanh_thu += Ban;
            Lap_top.SetAttribute("Doanh_thu", Doanh_thu.ToString());
        }*/
        return Kq;

    }


}