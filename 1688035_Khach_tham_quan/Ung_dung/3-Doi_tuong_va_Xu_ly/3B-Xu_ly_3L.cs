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
    //==================== Khở động ==============
    static XL_UNG_DUNG Ung_dung = null;
    XmlElement Du_lieu_Ung_dung;
    XmlElement Cong_ty;
    List<XmlElement> Danh_sach_Nhom_Laptop = new List<XmlElement>();
    List<XmlElement> Danh_sach_Laptop = new List<XmlElement>();

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
        Cong_ty = (XmlElement)Du_lieu_Ung_dung.GetElementsByTagName("Cong_ty")[0];
        var DS_Nhom_Laptop = (XmlElement)Cong_ty.GetElementsByTagName("Danh_sach_Nhom_Lap_top")[0];
        Danh_sach_Nhom_Laptop = XL_NGHIEP_VU.Tao_Danh_sach(DS_Nhom_Laptop, "Nhom_Laptop");
        var DS_Laptop = (XmlElement)Du_lieu_Ung_dung.GetElementsByTagName("Danh_sach_Laptop")[0];
        Danh_sach_Laptop = XL_NGHIEP_VU.Tao_Danh_sach(DS_Laptop, "Laptop");
    }
    //============= Xử lý Chức năng ========
    public string Khoi_dong_Nguoi_dung()
    {
        var Khach_Tham_quan = new XL_KHACH_THAM_QUAN();
        Khach_Tham_quan.Danh_sach_Laptop = Danh_sach_Laptop;
        Khach_Tham_quan.Danh_sach_Nhom_Laptop = Danh_sach_Nhom_Laptop;
        Khach_Tham_quan.Danh_sach_Laptop_Xem = Danh_sach_Laptop;
        Khach_Tham_quan.Danh_sach_Laptop_Chon = new List<XmlElement>();
        HttpContext.Current.Session["Khach_Tham_quan"] = Khach_Tham_quan;

        var Chuoi_HTML = Tao_Chuoi_HTML_Ket_qua();
        return Chuoi_HTML;
    }
    public string Xem_Danh_sach_Laptop()
    {
        var Nguoi_dung_Dang_nhap = (XL_KHACH_THAM_QUAN)HttpContext.Current.Session["Khach_Tham_quan"];
        Nguoi_dung_Dang_nhap.Danh_sach_Laptop_Xem = Danh_sach_Laptop;
        var Chuoi_HTML = Tao_Chuoi_HTML_Ket_qua();
        return Chuoi_HTML;
    }
    public string Chon_Nhom_Laptop(string Ma_so_Nhom_Laptop)
    {
        var Khach_Tham_quan = (XL_KHACH_THAM_QUAN)HttpContext.Current.Session["Khach_Tham_quan"];
        Khach_Tham_quan.Danh_sach_Laptop_Xem = XL_NGHIEP_VU.Tra_cuu_Laptop(Ma_so_Nhom_Laptop, Khach_Tham_quan.Danh_sach_Laptop);
        var Chuoi_HTML = Tao_Chuoi_HTML_Ket_qua();
        return Chuoi_HTML;
    }
    public string Them_Laptop(string Ma_so_Laptop)
    {
        var Khach_Tham_quan = (XL_KHACH_THAM_QUAN)HttpContext.Current.Session["Khach_Tham_quan"];
        // Xử lý 
        var Laptop = Khach_Tham_quan.Danh_sach_Laptop.FirstOrDefault(x => x.GetAttribute("Ma_so") == Ma_so_Laptop);

        if (!Khach_Tham_quan.Danh_sach_Laptop_Chon.Contains(Laptop))
        {
            Laptop.SetAttribute("So_luong", "1");
            Khach_Tham_quan.Danh_sach_Laptop_Chon.Add(Laptop);
        }
        else
        {
            var So_luong_Ton = int.Parse(Laptop.GetAttribute("So_luong_Ton"));
            var So_luong = int.Parse(Laptop.GetAttribute("So_luong"));
            if (So_luong < So_luong_Ton)
            {
                So_luong += 1;
                Laptop.SetAttribute("So_luong", So_luong.ToString());
            }

        }
        // Tạo chuỗi HTML kết quả xem 
        var Chuoi_HTML = Tao_Chuoi_HTML_Ket_qua();
        return Chuoi_HTML;
    }
    public string Giam_So_luong_Laptop(string Ma_so_Laptop)
    {
        var Khach_Tham_quan = (XL_KHACH_THAM_QUAN)HttpContext.Current.Session["Khach_Tham_quan"];
        // Xử lý 
        var Laptop = Khach_Tham_quan.Danh_sach_Laptop.FirstOrDefault(x => x.GetAttribute("Ma_so") == Ma_so_Laptop);
        var So_luong = int.Parse(Laptop.GetAttribute("So_luong"));
        So_luong -= 1;
        Laptop.SetAttribute("So_luong", So_luong.ToString());
        if (So_luong == 0)
            Khach_Tham_quan.Danh_sach_Laptop_Chon.Remove(Laptop);
        // Tạo chuỗi HTML kết quả xem 
        var Chuoi_HTML = Tao_Chuoi_HTML_Ket_qua();
        return Chuoi_HTML;
    }
    public string Dat_hang()
    {
        var Khach_Tham_quan = (XL_KHACH_THAM_QUAN)HttpContext.Current.Session["Khach_Tham_quan"];
        // Xử lý 

        // Tạo chuỗi HTML kết quả xem 
        var Chuoi_HTML = "<iframe class='KHUNG_CHUC_NANG' src='MH_Dat_hang.cshtml'  ></iframe>";
        return Chuoi_HTML;
    }
    public string Chon_Laptop(string Ma_so_Laptop)
    {
        var Khach_Tham_quan = (XL_KHACH_THAM_QUAN)HttpContext.Current.Session["Khach_Tham_quan"];
        // Xử lý 
        Khach_Tham_quan.Ma_so_Laptop_chon = Ma_so_Laptop;
        // Tạo chuỗi HTML kết quả xem 
        var Chuoi_HTML = "<iframe class='KHUNG_CHUC_NANG' src='MH_Chi_tiet_San_pham.cshtml'  ></iframe>";
        return Chuoi_HTML;
    }
    public string Tra_cuu(string Chuoi_Tra_cuu)
    {
        var Khach_Tham_quan = (XL_KHACH_THAM_QUAN)HttpContext.Current.Session["Khach_Tham_quan"];
        Khach_Tham_quan.Danh_sach_Laptop_Xem = XL_NGHIEP_VU.Tra_cuu_Laptop(Chuoi_Tra_cuu,
               Khach_Tham_quan.Danh_sach_Laptop);
        var Danh_sach_Nhom_Laptop_Xem = Danh_sach_Nhom_Laptop;
        var Chuoi_HTML = Tao_Chuoi_HTML_Ket_qua();
        return Chuoi_HTML;
    }

    public string Tao_Chuoi_HTML_Ket_qua()
    {
        var Khach_Tham_quan = (XL_KHACH_THAM_QUAN)HttpContext.Current.Session["Khach_Tham_quan"];

        var Chuoi_HTML = $"<div style='margin-top: 10px;'>" +
                 $"{XL_THE_HIEN.Tao_Chuoi_HTML_Danh_sach_Laptop_Chon(Khach_Tham_quan.Danh_sach_Laptop_Chon)}" +
                 $"{XL_THE_HIEN.Tao_Chuoi_HTML_Danh_sach_Laptop_Xem(Khach_Tham_quan.Danh_sach_Laptop_Xem)}" +
             $"</div>";
        return Chuoi_HTML;

    }
    public string Khoi_dong_Man_hinh_Dat_hang()
    {
        var Khach_Tham_quan = (XL_KHACH_THAM_QUAN)HttpContext.Current.Session["Khach_Tham_quan"];
        var Chuoi_HTML = XL_THE_HIEN.Tao_chuoi_Nhap_Thong_tin_Ca_nhan();
        return Chuoi_HTML;
    }
    public string Ghi_Dat_hang_Moi()
    {
        var Khach_Tham_quan = (XL_KHACH_THAM_QUAN)HttpContext.Current.Session["Khach_Tham_quan"];
        var Chuoi_XML = "<Phieu_dat />";
        var Tai_lieu_Phieu_dat = new XmlDocument();
        Tai_lieu_Phieu_dat.LoadXml(Chuoi_XML);
        var Phieu_dat = Tai_lieu_Phieu_dat.DocumentElement;

        var Chuoi_XML_Danh_sach = "<Danh_sach_Laptop />";
        var Tai_lieu_Danh_sach = new XmlDocument();
        Tai_lieu_Danh_sach.LoadXml(Chuoi_XML_Danh_sach);
        var Danh_sach = Tai_lieu_Danh_sach.DocumentElement;

        var Chuoi_XML_Khach_hang = "<Khach_hang />";
        var Tai_lieu_Khach_hang = new XmlDocument();
        Tai_lieu_Khach_hang.LoadXml(Chuoi_XML_Khach_hang);
        var Khach_hang = Tai_lieu_Khach_hang.DocumentElement;

        Phieu_dat.SetAttribute("Ngay_dat", DateTime.Now.ToString());
        Phieu_dat.SetAttribute("Tien", Khach_Tham_quan.Tinh_tong_tien().ToString());
        Phieu_dat.SetAttribute("Tinh_trang", "CHO_PHAN_CONG");
        
        foreach (XmlElement Laptop in Khach_Tham_quan.Danh_sach_Laptop_Chon)
        {
            Danh_sach.AppendChild(Tai_lieu_Danh_sach.ImportNode(Laptop, true));
        }
        Phieu_dat.AppendChild(Tai_lieu_Phieu_dat.ImportNode(Danh_sach, true));

        Khach_hang.SetAttribute("Ho_ten", Khach_Tham_quan.Ho_ten);
        Khach_hang.SetAttribute("Dia_chi", Khach_Tham_quan.Dia_chi);
        Khach_hang.SetAttribute("So_Dien_thoai", Khach_Tham_quan.So_Dien_thoai);
        Phieu_dat.AppendChild(Tai_lieu_Phieu_dat.ImportNode(Khach_hang, true));

        var Kq_Ghi = XL_LUU_TRU.Ghi_Phieu_dat_Moi(Khach_Tham_quan,Phieu_dat);

        if (Kq_Ghi == "OK")
            Khach_Tham_quan.Thong_bao = $"<div class='alert alert-success'>Bạn đã đặt hàng thành công</div>";
        else
            Khach_Tham_quan.Thong_bao = $"<div class='alert alert-warning'>Đã có lỗi xảy ra, vui lòng thực hiện lại</div>";

        var Chuoi_HTML = Khach_Tham_quan.Thong_bao;
        return Chuoi_HTML;

    }

}
    //************************* View/Presentation -Layers VL/PL **********************************
    public partial class XL_THE_HIEN
{
    public static string Dia_chi_Media = $"{XL_LUU_TRU.Dia_chi_Dich_vu}/Media";
    public static CultureInfo Dinh_dang_VN = CultureInfo.GetCultureInfo("vi-VN");


    public static string Tao_Chuoi_HTML_Danh_sach_Laptop_Xem(List<XmlElement> Danh_sach_Laptop)
    {
        var Chuoi_HTML_Danh_sach = "<div class='row'>";
        var Chuoi_Chuc_nang_Chon_Laptop = "<form id='CHON_LAPTOP' name='CHON_LAPTOP' method='post'>" +
                                        "<input name='Th_Ma_so_Chuc_nang' type='hidden' value='CHON_LAPTOP' />" +
                                         $"<input id='Th_Ma_so_Laptop' name='Th_Ma_so_Laptop' type='hidden' />" +
                                       "</form>";
        foreach (XmlElement Laptop in Danh_sach_Laptop)
        {
            var Ten = Laptop.GetAttribute("Ten");
            var Ma_so = Laptop.GetAttribute("Ma_so");
            var Don_gia_Ban = long.Parse(Laptop.GetAttribute("Don_gia_Ban"));
            var So_luong_Ton = int.Parse(Laptop.GetAttribute("So_luong_Ton"));
            var Dinh_dang_Trang_thai = ""; var Chuoi_Trang_thai = "";
            var Chuoi_Chuc_nang_Them_Laptop = "<form method='post'>" +
                                        "<input name='Th_Ma_so_Chuc_nang' type='hidden' value='THEM_LAPTOP' />" +
                                         $"<input name='Th_Ma_so_Laptop' type='hidden' value='{Ma_so}' />" +
                                         $"<button type='submit' class='btn btn-primary hidden-sm-down btn-block ADD_BUTTON'>THÊM VÀO GIỎ HÀNG <img class='MINI_CART_ICON' src='{Dia_chi_Media}/CART_ICON.svg' /></button>" +
                                       "</form>";
            var Chuoi_Xu_ly_Click = $"Th_Ma_so_Laptop.value='{Ma_so}';CHON_LAPTOP.submit() ";
            if (So_luong_Ton == 0)
            {
                Dinh_dang_Trang_thai = ";opacity:0.7"; ;
                Chuoi_Trang_thai = "Tạm thời hết hàng";
                Chuoi_Chuc_nang_Them_Laptop = "";
            }
            var Chuoi_Hinh = $"<div class='KHUNG_HINH mx-auto' onclick=\"" + $"{Chuoi_Xu_ly_Click}" + "\">" +
                                $"<img src='{Dia_chi_Media}/{Ma_so}.png' class='img-thumbnail HINH'/>" +
                             "</div>";
            var Chuoi_Thong_tin = $"<div onclick=\"" + $"{Chuoi_Xu_ly_Click}" + "\">" +
                                      $"<strong>{Ten}</strong>" +
                                      $"<br />Đơn giá: { Don_gia_Ban.ToString("c0", Dinh_dang_VN) }" +
                                      $"<br />{ Chuoi_Trang_thai }" +
                                  $"</div>";
            var Chuoi_HTML = $"<div class='KHUNG col-6 col-sm-4 col-md-3 col-lg-2' style='{Dinh_dang_Trang_thai}'>" +
                                 $"<div class='THONG_TIN'>" +
                                     $"{Chuoi_Hinh}" +
                                     $"{Chuoi_Thong_tin}" +
                                     $"{Chuoi_Chuc_nang_Them_Laptop}"+
                                 $"</div>" +
                             "</div>";
            Chuoi_HTML_Danh_sach += Chuoi_HTML;
        }
        
        Chuoi_HTML_Danh_sach += Chuoi_Chuc_nang_Chon_Laptop+"</div>";
        return Chuoi_HTML_Danh_sach;
    }

    public static string Tao_Chuoi_HTML_Danh_sach_Laptop_Chon(List<XmlElement> Danh_sach_Laptop)
    {

        var Chuoi_HTML_Danh_sach = "<div class='row'  >";
        if (Danh_sach_Laptop.Count > 0)
        {
            Chuoi_HTML_Danh_sach = "<div class='row' style='border:1px solid blue;margin:5px;padding:5px'>";
            var Chuoi_Chuc_nang_Dat_hang = "<div class='col-md-12'><form method='post'>" +
                                       "<input name='Th_Ma_so_Chuc_nang' type='hidden' value='DAT_HANG' />" +
                                        "<button type='submit' class='btn btn-danger'>Đặt hàng</button>" +
                                      "</form></div>";
            Chuoi_HTML_Danh_sach += Chuoi_Chuc_nang_Dat_hang;
        }



        foreach (XmlElement Laptop in Danh_sach_Laptop)
        {
            var Ten = Laptop.GetAttribute("Ten");
            var Ma_so = Laptop.GetAttribute("Ma_so");
            var Don_gia_Ban = long.Parse(Laptop.GetAttribute("Don_gia_Ban"));
            var Ban_hang =  (XmlElement)Laptop.GetElementsByTagName("Ban_hang")[0];
            var So_luong = Laptop.GetAttribute("So_luong");
            var Chuoi_Hinh = $"<img src='{Dia_chi_Media}/{ Ma_so}.png' " +
                             "style='width:90px;height:90px;' />";
            var Chuoi_Chuc_nang_Giam_So_luong = "<form method='post'>" +
                                       "<input name='Th_Ma_so_Chuc_nang' type='hidden' value='GIAM_SO_LUONG_LAPTOP' />" +
                                        $"<input name='Th_Ma_so_Laptop' type='hidden' value='{Ma_so}' />" +
                                        "<button type='submit' class='btn btn-danger'>-</button>" +
                                      "</form>";
            var Chuoi_Thong_tin = $"<div> " +
                          $"<strong>{Ten}</strong>" +
                          $"<br />{ Don_gia_Ban.ToString("c0", Dinh_dang_VN) }" +
                          $"<br />Số lượng: { So_luong}"+
                           $"{Chuoi_Chuc_nang_Giam_So_luong}" +
                          $"</div>";
            var Chuoi_HTML = $"<div class='col-md-3' style='margin-bottom:10px' >" +
                               $"{Chuoi_Hinh}" + $"{Chuoi_Thong_tin}" +
                             "</div>";
            Chuoi_HTML_Danh_sach += Chuoi_HTML;
        }
        Chuoi_HTML_Danh_sach += "</div>";

        return Chuoi_HTML_Danh_sach;
    }

    public static string Tao_chuoi_Nhap_Thong_tin_Ca_nhan()
    {
        var Chuoi_HTML = $"<form id='DAT_VE' method='post'>" +
            $"<input name='Th_Ma_so_Chuc_nang' type='hidden' value='DAT_HANG'/>" +
            $"<div class='form-group row'>" +
            $"<label for='Th_Ho_ten' class='col-2 col-form-label'>Họ Tên</label>" +
            $"<div class='col-10'>" +
            $"<input class='form-control' type='text' id='Th_Ho_ten' name='Th_Ho_ten'>" +
            $"</div></div>" +
            $"<div class='form-group row'>" +
            $"<label for='Th_Dia_chi' class='col-2 col-form-label'>Địa chỉ</label>" +
            $"<div class='col-10'>" +
            $"<input class='form-control' type='text' id='Th_Dia_chi' name='Th_Dia_chi'>" +
            $"</div></div>" +
            $"<div class='form-group row'>" +
            $"<label for='Th_So_Dien_thoai' class='col-2 col-form-label'>Số điện thoại</label>" +
            $"<div class='col-10'>" +
            $"<input class='form-control' type='text' id='Th_So_Dien_thoai' name='Th_So_Dien_thoai'>" +
            $"</div></div>" +
            $"<button type='submit' class='btn btn-primary'>Đặt hàng</button>" +
            $"</form>";
        return Chuoi_HTML;
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
    public static XmlElement Tim_Laptop(string Ma_so, List<XmlElement> Danh_sach)
    {
        var Kq = Danh_sach.FirstOrDefault(Laptop => Laptop.GetAttribute("Ma_so") == Ma_so);
        return Kq;
    }

    public static List<XmlElement> Tra_cuu_Laptop( string Chuoi_Tra_cuu, List<XmlElement> Danh_sach)
    {
        Chuoi_Tra_cuu = Chuoi_Tra_cuu.ToUpper();
        var Danh_sach_Kq = new List<XmlElement>();
        Danh_sach_Kq = Danh_sach.FindAll(x => x.GetAttribute("Ten").ToUpper().Contains(Chuoi_Tra_cuu)
           || x.SelectSingleNode("Nhom_Lap_top/@Ma_so").Value == Chuoi_Tra_cuu);
        return Danh_sach_Kq;
    }

    public static  XmlElement  Tra_cuu_Laptop(string Chuoi_Tra_cuu, XmlElement   Danh_sach_Laptop)
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

    // Ghi 
    public static string Ghi_Phieu_dat_Moi(XL_KHACH_THAM_QUAN Nguoi_dung,XmlElement Phieu_dat)
    {
        var Kq = "OK";

        try
        {
            var Xu_ly = new WebClient();
            Xu_ly.Encoding = System.Text.Encoding.UTF8;
            var Tham_so = $"Ma_so_Xu_ly=GHI_PHIEU_DAT_MOI";
            var Dia_chi_Xu_ly = $"{Dia_chi_Dich_vu_Du_lieu}?{Tham_so}";
            var Chuoi_XML_Dat_hang = Phieu_dat.OuterXml;
            var Chuoi_XML_Kq = Xu_ly.UploadString(Dia_chi_Xu_ly, Chuoi_XML_Dat_hang).Trim();
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

        }
        return Kq;
    }
}