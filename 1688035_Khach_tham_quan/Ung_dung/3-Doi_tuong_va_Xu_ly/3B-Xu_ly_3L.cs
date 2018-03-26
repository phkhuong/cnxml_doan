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
    List<XmlElement> Danh_sach_Mau_sac = new List<XmlElement>();
    List<XmlElement> Danh_sach_Laptop = new List<XmlElement>();
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
        Cong_ty = (XmlElement)Du_lieu_Ung_dung.GetElementsByTagName("Cong_ty")[0];
        var DS_Nhom_Laptop = (XmlElement)Cong_ty.GetElementsByTagName("Danh_sach_Nhom_Lap_top")[0];
        Danh_sach_Nhom_Laptop = XL_NGHIEP_VU.Tao_Danh_sach(DS_Nhom_Laptop, "Nhom_Lap_top");
        var DS_Mau_sac = (XmlElement)Cong_ty.GetElementsByTagName("Danh_sach_Mau_sac")[0];
        Danh_sach_Mau_sac = XL_NGHIEP_VU.Tao_Danh_sach(DS_Mau_sac, "Mau_sac");
        var DS_Laptop = (XmlElement)Du_lieu_Ung_dung.GetElementsByTagName("Danh_sach_Laptop")[0];
        Danh_sach_Laptop = XL_NGHIEP_VU.Tao_Danh_sach(DS_Laptop, "Laptop");

        var DS_Nguoi_dung = (XmlElement)Cong_ty.GetElementsByTagName("Danh_sach_Nhan_vien")[0];
        Danh_sach_Nguoi_dung = XL_NGHIEP_VU.Tao_Danh_sach(DS_Nguoi_dung, "Nhan_vien", "Quan_ly");
    }
    //============= Xử lý Chức năng ========
    public string Khoi_dong_Nguoi_dung()
    {
        var Khach_Tham_quan = new XL_KHACH_THAM_QUAN();
        Khach_Tham_quan.Danh_sach_Laptop = Danh_sach_Laptop;
        Khach_Tham_quan.Danh_sach_Nhom_Laptop = Danh_sach_Nhom_Laptop;
        Khach_Tham_quan.Danh_sach_Mau_sac = Danh_sach_Mau_sac;
        Khach_Tham_quan.Danh_sach_Laptop_Xem = Danh_sach_Laptop;
        Khach_Tham_quan.Danh_sach_Laptop_Chon = new List<XmlElement>();
        HttpContext.Current.Session["Khach_Tham_quan"] = Khach_Tham_quan;

        var Chuoi_HTML = Tao_Chuoi_HTML_Ket_qua();
        return Chuoi_HTML;
    }
    public string Khoi_dong_MH_Chi_tiet_San_Pham()
    {
        var Khach_Tham_quan = (XL_KHACH_THAM_QUAN)HttpContext.Current.Session["Khach_Tham_quan"];
        var Chuoi_HTML = Tao_Chuoi_HTML_MH_Chi_tiep_Laptop();
        return Chuoi_HTML;
    }
    public string Dang_nhap(string Ten_Dang_nhap, string Mat_khau)
    {

        var Nguoi_dung = Danh_sach_Nguoi_dung.FirstOrDefault(
            x => x.GetAttribute("Ten_Dang_nhap") == Ten_Dang_nhap && x.GetAttribute("Mat_khau") == Mat_khau);
        var Chuoi_HTML = "";
        if (Nguoi_dung != null)
        {
            var Ma_so_Nhom_Nguoi_dung_Nhan_vien = "";
            var Ma_so_Nhom_Nguoi_dung_Quan_ly = "";
            try
            {
                Ma_so_Nhom_Nguoi_dung_Nhan_vien = Nguoi_dung.SelectSingleNode("Nhom_Nhan_vien/@Ma_so").Value;
            }
            catch(NullReferenceException e)
            {
                Ma_so_Nhom_Nguoi_dung_Quan_ly = Nguoi_dung.SelectSingleNode("Nhom_Quan_ly/@Ma_so").Value;
            }
            
            if (Ma_so_Nhom_Nguoi_dung_Nhan_vien == "NHAP_HANG")
            {
                var Dia_chi_MH_Dang_nhap = "http://localhost:65475/1-Man_hinh_Giao_dien/MH_Dang_nhap.cshtml";
                var Tham_so = $"Th_Ma_so_Chuc_nang=DANG_NHAP&Th_Ten_Dang_nhap={Ten_Dang_nhap}&Th_Mat_khau={Mat_khau}";
                var Dia_chi_Xu_ly = $"{Dia_chi_MH_Dang_nhap}?{Tham_so}";
                HttpContext.Current.Response.Redirect(Dia_chi_Xu_ly);
            }
            else if (Ma_so_Nhom_Nguoi_dung_Nhan_vien == "BAN_HANG")
            {
                var Dia_chi_MH_Dang_nhap = "http://localhost:64820/1-Man_hinh_Giao_dien/MH_Dang_nhap.cshtml";
                var Tham_so = $"Th_Ma_so_Chuc_nang=DANG_NHAP&Th_Ten_Dang_nhap={Ten_Dang_nhap}&Th_Mat_khau={Mat_khau}";
                var Dia_chi_Xu_ly = $"{Dia_chi_MH_Dang_nhap}?{Tham_so}";
                HttpContext.Current.Response.Redirect(Dia_chi_Xu_ly);
            }
            else if (Ma_so_Nhom_Nguoi_dung_Nhan_vien == "GIAO_HANG")
            {
                var Dia_chi_MH_Dang_nhap = "http://localhost:64817/1-Man_hinh_Giao_dien/MH_Dang_nhap.cshtml";
                var Tham_so = $"Th_Ma_so_Chuc_nang=DANG_NHAP&Th_Ten_Dang_nhap={Ten_Dang_nhap}&Th_Mat_khau={Mat_khau}";
                var Dia_chi_Xu_ly = $"{Dia_chi_MH_Dang_nhap}?{Tham_so}";
                HttpContext.Current.Response.Redirect(Dia_chi_Xu_ly);
            }
            else if (Ma_so_Nhom_Nguoi_dung_Quan_ly == "QUAN_LY_GIAO_HANG")
            {
                var Dia_chi_MH_Dang_nhap = "http://localhost:64784/1-Giao_dien_The_hien/MH_Dang_nhap.cshtml";
                var Tham_so = $"Th_Ma_so_Chuc_nang=DANG_NHAP&Th_Ten_Dang_nhap={Ten_Dang_nhap}&Th_Mat_khau={Mat_khau}";
                var Dia_chi_Xu_ly = $"{Dia_chi_MH_Dang_nhap}?{Tham_so}";
                HttpContext.Current.Response.Redirect(Dia_chi_Xu_ly);
            }
            else if (Ma_so_Nhom_Nguoi_dung_Quan_ly == "QUAN_LY_BAN_HANG")
            {
                var Dia_chi_MH_Dang_nhap = "http://localhost:64819/1-Man_hinh_Giao_dien/MH_Dang_nhap.cshtml";
                var Tham_so = $"Th_Ma_so_Chuc_nang=DANG_NHAP&Th_Ten_Dang_nhap={Ten_Dang_nhap}&Th_Mat_khau={Mat_khau}";
                var Dia_chi_Xu_ly = $"{Dia_chi_MH_Dang_nhap}?{Tham_so}";
                HttpContext.Current.Response.Redirect(Dia_chi_Xu_ly);
            }
        }

        else Chuoi_HTML = $"<div class='alert alert-warning'>Đăng nhập không hợp lệ</div>";
        Chuoi_HTML += Tao_Chuoi_HTML_Ket_qua();
        return Chuoi_HTML;
    }
    
    public string Xem_Danh_sach_Laptop()
    {
        var Khach_Tham_quan = (XL_KHACH_THAM_QUAN)HttpContext.Current.Session["Khach_Tham_quan"];
        Khach_Tham_quan.Danh_sach_Laptop_Xem = Danh_sach_Laptop;
        var Chuoi_HTML = Tao_Chuoi_HTML_Ket_qua();
        return Chuoi_HTML;
    }
    public string Loc(Dictionary<string,string[]> Dieu_kien_Loc)
    {
        var Khach_Tham_quan = (XL_KHACH_THAM_QUAN)HttpContext.Current.Session["Khach_Tham_quan"];
        
        var Chuoi_XPath = $"//Laptop";
        foreach (KeyValuePair<string, string[]> dk in Dieu_kien_Loc)
        {
            for (int i = 0; i < dk.Value.Length; i++)
            {
                if (i == dk.Value.Length - 1)
                    Chuoi_XPath += $"{dk.Key}/@Ma_so = '{dk.Value[i]}']";
                else if (i == 0)
                    Chuoi_XPath += $"[{dk.Key}/@Ma_so = '{dk.Value[i]}' or ";
                else
                    Chuoi_XPath += $"{dk.Key}/@Ma_so = '{dk.Value[i]}' or ";
            }
        }
        //for(int i = 0; i<Hangs.Length;i++)
        //{
        //    if(i != Hangs.Length-1)
        //        Chuoi_XPath += $"Nhom_Lap_top/@Ma_so = '{Hangs[i]}' or ";
        //    else
        //        Chuoi_XPath += $"Nhom_Lap_top/@Ma_so = '{Hangs[i]}']";
        //}
        var Danh_sach_Laptop = Du_lieu_Ung_dung.SelectSingleNode("//Danh_sach_Laptop");
        var Danh_sach_Laptop_xem = Danh_sach_Laptop.SelectNodes(Chuoi_XPath);
        Khach_Tham_quan.Danh_sach_Laptop_Xem = XL_NGHIEP_VU.Tao_Danh_sach(Danh_sach_Laptop_xem);
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
    public string Them_Laptop(string Ma_so_Laptop, int So_luong_them)
    {
        var Khach_Tham_quan = (XL_KHACH_THAM_QUAN)HttpContext.Current.Session["Khach_Tham_quan"];
        // Xử lý 
        var Laptop = Khach_Tham_quan.Danh_sach_Laptop.FirstOrDefault(x => x.GetAttribute("Ma_so") == Ma_so_Laptop);

        if (!Khach_Tham_quan.Danh_sach_Laptop_Chon.Contains(Laptop))
        {
            Laptop.SetAttribute("So_luong", So_luong_them.ToString());
            Khach_Tham_quan.Danh_sach_Laptop_Chon.Add(Laptop);
        }
        else
        {
            var So_luong_Ton = int.Parse(Laptop.GetAttribute("So_luong_Ton"));
            var So_luong = int.Parse(Laptop.GetAttribute("So_luong"));
            if (So_luong+ So_luong_them <= So_luong_Ton)
            {
                So_luong += So_luong_them;
                Laptop.SetAttribute("So_luong", So_luong.ToString());
            }

        }
        // Tạo chuỗi HTML kết quả xem 
        var Chuoi_HTML = Khoi_dong_MH_Chi_tiet_San_Pham();
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

        var Chuoi_HTML = $"<div class='row' style='margin-top: 20px;'>" +
            $"<div class='col-md-3'>" +
                $"{XL_THE_HIEN.Tao_chuoi_Loc_Laptop(Khach_Tham_quan)}"+
            $"</div>"+
            $"<div class='col-md-9'>" +
                 $"{XL_THE_HIEN.Tao_Chuoi_HTML_Danh_sach_Laptop_Chon(Khach_Tham_quan.Danh_sach_Laptop_Chon)}" +
                 $"{XL_THE_HIEN.Tao_Chuoi_HTML_Danh_sach_Laptop_Xem(Khach_Tham_quan.Danh_sach_Laptop_Xem)}" +
             $"</div></div>";
        return Chuoi_HTML;

    }
    public string Tao_Chuoi_HTML_MH_Chi_tiep_Laptop()
    {
        var Khach_Tham_quan = (XL_KHACH_THAM_QUAN)HttpContext.Current.Session["Khach_Tham_quan"];
        var Chuoi_HTML =
                 $"{XL_THE_HIEN.Tao_Chuoi_HTML_Danh_sach_Laptop_Chon(Khach_Tham_quan.Danh_sach_Laptop_Chon)}" +
                 $"{XL_THE_HIEN.Tao_chuoi_Chi_tiet_Laptop(Khach_Tham_quan)}";
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

        var Kq_Ghi = XL_LUU_TRU.Ghi_Phieu_dat_Moi(Khach_Tham_quan, Phieu_dat);

        if (Kq_Ghi == "OK")
        {
            Khach_Tham_quan.Thong_bao = $"<div class='alert alert-success'>Bạn đã đặt hàng thành công</div>";
            Khach_Tham_quan.Danh_sach_Laptop_Chon = new List<XmlElement>();
        }
            
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
            var Chuoi_HTML = $"<div class='KHUNG col-6 col-sm-4 col-md-3 col-lg-3' style='{Dinh_dang_Trang_thai}'>" +
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

    public static string Tao_chuoi_Loc_Laptop(XL_KHACH_THAM_QUAN Khach_Tham_quan)
    {
        var Chuoi_HTML = "<form method='post'>";
        Chuoi_HTML += $"<button type='submit' class='btn btn-primary'>Lọc</button>" +
            $"<input name='Th_Ma_so_Chuc_nang' type='hidden' value='LOC' />";
        Chuoi_HTML += $"<div class='form-group row'><label class='col-4 col-form-label' for='Th_Nhom_Laptop'><strong>Hãng</strong></label>" +
   $"<div class='col-8'><div class='row'>";
        for(int i =0; i< Khach_Tham_quan.Danh_sach_Nhom_Laptop.Count; i++)
        {
            var Ma_so_Nhom_Laptop = Khach_Tham_quan.Danh_sach_Nhom_Laptop[i].GetAttribute("Ma_so");
            if (i == 0 || i == 3)
            {
                Chuoi_HTML += $"<div class='col-6'>" +
                    $"<div class='Th_Nhom_Laptop'><label for='{Ma_so_Nhom_Laptop}'>" +
                    $"<input type='checkbox' name='Th_Nhom_Laptop' id='{Ma_so_Nhom_Laptop}' value='{Ma_so_Nhom_Laptop}'>{Ma_so_Nhom_Laptop }</label></div>";
            }
            else if (i==2 || i==5)
            {
                Chuoi_HTML += $"<div class='Th_Nhom_Laptop'><label for='{Ma_so_Nhom_Laptop}'>" +
                   $"<input type='checkbox' name='Th_Nhom_Laptop' id='{Ma_so_Nhom_Laptop}' value='{Ma_so_Nhom_Laptop}'>{Ma_so_Nhom_Laptop }</label></div></div>";
            }
            else
            {
                Chuoi_HTML += $"<div class='Th_Nhom_Laptop'><label for='{Ma_so_Nhom_Laptop}'>" +
                   $"<input type='checkbox' name='Th_Nhom_Laptop' id='{Ma_so_Nhom_Laptop}' value='{Ma_so_Nhom_Laptop}'>{Ma_so_Nhom_Laptop }</label></div>";
            }
        }
        Chuoi_HTML += $"</div></div></div>";

        Chuoi_HTML += $"<div class='form-group row'><label class='col-4 col-form-label' for='Th_Mau_sac'><strong>Màu sắc</strong></label>" +
   $"<div class='col-8'><div class='row'>";
        for (int i = 0; i < Khach_Tham_quan.Danh_sach_Mau_sac.Count; i++)
        {
            var Ma_so_Mau_sac = Khach_Tham_quan.Danh_sach_Mau_sac[i].GetAttribute("Ma_so");
            var Ten_Mau_sac = Khach_Tham_quan.Danh_sach_Mau_sac[i].GetAttribute("Ten");
            if (i == 0 || i == 4)
            {
                Chuoi_HTML += $"<div class='col-6'>" +
                    $"<div class='Th_Mau_sac'><label for='{Ma_so_Mau_sac}'>" +
                    $"<input type='checkbox' name='Th_Mau_sac' id='{Ma_so_Mau_sac}' value='{Ma_so_Mau_sac}'>{Ten_Mau_sac}</label></div>";
            }
            else if (i == 3 || i == 7)
            {
                Chuoi_HTML += $"<div class='Th_Mau_sac'><label for='{Ma_so_Mau_sac}'>" +
                   $"<input type='checkbox' name='Th_Mau_sac' id='{Ma_so_Mau_sac}' value='{Ma_so_Mau_sac}'>{Ten_Mau_sac}</label></div></div>";
            }
            else
            {
                Chuoi_HTML += $"<div class='Th_Mau_sac'><label for='{Ma_so_Mau_sac}'>" +
                   $"<input type='checkbox' name='Th_Mau_sac' id='{Ma_so_Mau_sac}' value='{Ma_so_Mau_sac}'>{Ten_Mau_sac}</label></div>";
            }
        }
        Chuoi_HTML += $"</div></div></div>";
        Chuoi_HTML += "</form>";
        return Chuoi_HTML;
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

    public static string Tao_chuoi_Chi_tiet_Laptop(XL_KHACH_THAM_QUAN Nguoi_dung)
    {
        var Ma_so = Nguoi_dung.Ma_so_Laptop_chon;
        var Laptop = XL_NGHIEP_VU.Tim_Laptop(Ma_so, Nguoi_dung.Danh_sach_Laptop);
        var Dia_chi_Media = XL_LUU_TRU.Dia_chi_Dich_vu + "/Media";
        var Dinh_dang_VN = CultureInfo.GetCultureInfo("vi-VN");
        var Ten = Laptop.GetAttribute("Ten");
        var Don_gia_Ban = long.Parse(Laptop.GetAttribute("Don_gia_Ban"));
        var Trong_luong = Laptop.GetAttribute("Trong_luong");
        var He_dieu_hanh = Laptop.GetAttribute("He_dieu_hanh");
        var CPU = Laptop.GetAttribute("CPU");
        var Card_Onboard = Laptop.GetAttribute("Card_Man_hinh_Onboard");
        var Bao_hanh = Laptop.GetAttribute("Bao_hanh");

        //var Nhom_Laptop_Element = (XmlElement)Laptop.GetElementsByTagName("Nhom_Laptop")[0];
        var Nhom_Laptop = XL_NGHIEP_VU.Get_Ten_Chi_tiet_Laptop(Laptop, "Nhom_Lap_top");
        var Ten_CPU = XL_NGHIEP_VU.Get_Ten_Chi_tiet_Laptop(Laptop, "CPU_Series");
        var Dung_luong_Ram = XL_NGHIEP_VU.Get_Ten_Chi_tiet_Laptop(Laptop, "Dung_luong_Ram");
        var Kich_thuoc_Man_hinh = XL_NGHIEP_VU.Get_Ten_Chi_tiet_Laptop(Laptop, "Kich_thuoc_Man_hinh");
        var Chuan_Man_hinh = XL_NGHIEP_VU.Get_Ten_Chi_tiet_Laptop(Laptop, "Chuan_Man_hinh");
        var Tinh_nang = XL_NGHIEP_VU.Get_Ten_Chi_tiet_Laptop(Laptop, "Tinh_nang");
        var Mau_sac = XL_NGHIEP_VU.Get_Ten_Chi_tiet_Laptop(Laptop, "Mau_sac");
        var Card_Man_hinh_ngoai = XL_NGHIEP_VU.Get_Ten_Chi_tiet_Laptop(Laptop, "Card_Man_hinh_Ngoai");
        var HDD = XL_NGHIEP_VU.Get_Ten_Chi_tiet_Laptop(Laptop, "HDD");
        var SSD = XL_NGHIEP_VU.Get_Ten_Chi_tiet_Laptop(Laptop, "SSD");

        var Chuoi_HTML = $"<div class='row'>";
        var Chuoi_Hinh = $"<img src='{Dia_chi_Media}/{ Ma_so}.png'>";
        var Chuoi_Thong_tin_Chinh = $"<h4 style='border-bottom: 1px solid #949798;'>{Ten}</h4>"+
                $"<h3 style='margin-top:0px; padding-bottom:10px;'><strong>{Don_gia_Ban}</strong></h3>"+
                $"<p style='border-bottom: 1px solid #949798;padding-bottom:10px;'>CPU: {Ten_CPU}"+
                    $"<br />Ram: { Dung_luong_Ram}"+
                    $"<br />Màn hình: { Kich_thuoc_Man_hinh} { Chuan_Man_hinh}"+
                    $"<br />Trọng lượng: { Trong_luong}"+
                    $"<br />Hệ điều hành: { He_dieu_hanh}"+
                $"</p>";
        var Chuoi_Thong_tin_Phu = $"<h5>Thông số kỹ thuật</h5><table class='table'>"+
                    $"<tbody>"+
                        $"<tr><th scope='row'>Bảo hành</th><td>{Bao_hanh}</td></tr>"+
                        $"<tr><th scope='row'>Thương hiệu</th><td>{Nhom_Laptop}</td></tr>"+
                        $"<tr><th scope='row'>Tính năng</th><td>{Tinh_nang}</td></tr>"+
                        $"<tr><th scope='row'>Màu sắc</th><td>{Mau_sac}</td></tr>"+
                        $"<tr><th scope='row'>CPU series</th><td>{Ten_CPU}</td></tr>"+
                        $"<tr><th scope='row'>CPU</th><td>{CPU}</td></tr>"+
                        $"<tr><th scope='row'>Hệ điều hành</th><td>{He_dieu_hanh}</td</tr>"+
                        $"<tr><th scope='row'>Ram</th><td>{Dung_luong_Ram}</td></tr>"+
                        $"<tr><th scope='row'>Kích thước màn hình</th><td>{Kich_thuoc_Man_hinh}</td</tr>"+
                        $"<tr><th scope='row'>Chuẩn màn hình</th><td>{Chuan_Man_hinh}</td></tr>" +
                        $"<tr><th scope='row'>Card màn hình Onboard</th><td>{Card_Onboard}</td></tr>" +
                        $"<tr><th scope='row'>Card màn hình gắn ngoài</th><td>{Card_Man_hinh_ngoai}</td></tr>" +
                        $"<tr><th scope='row'>HDD</th><td>{HDD}</td></tr>" +
                        $"<tr><th scope='row'>SSD</th><td>{SSD}</td></tr>" +
                        $"<tr><th scope='row'>Trọng lượng</th><td>{Trong_luong}</td></tr>" +
                    $"</tbody>"+
                $"</table>";
        var Chuoi_Dat_hang = $"<form method='post'><div class='row'>"+
            $"<input name='Th_Ma_so_Chuc_nang' type='hidden' value='THEM_LAPTOP'/>" +
                        $"<div class='col-md-5' >"+
                        $"<div class='form-group row'><label for='example-text-input' class='col-5 col-form-label' style='margin-top: 10px'>Số lượng</label>" +
                            $"<div class='col-7' style='margin-top: 10px'>" +
                                $"<input class='form-control' type='number' name='Th_So_luong' min='1' max='5' value='1'>" +
                            $"</div></div>"+
                            
                        $"</div>"+
                        $"<div class='col-md-5 offset-md-2'>"+
                            $"<button type='submit' class='btn btn-primary btn-block ADD_BUTTON'>THÊM VÀO GIỎ HÀNG <img class='MINI_CART_ICON' src='{Dia_chi_Media}/CART_ICON.svg' /></button>"+
                        $"</div></div></form>";
        Chuoi_HTML += $"<div class='col-md-5'>" + Chuoi_Hinh + $"</div>" +
            $"<div class='col-md-7'>" + Chuoi_Thong_tin_Chinh + Chuoi_Dat_hang + $"</div>" +
            $"<div>" + Chuoi_Thong_tin_Phu + $"</div></div>";
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
    public static List<XmlElement> Tao_Danh_sach(XmlNodeList Danh_sach_Nguon)
    {
        var Danh_sach = new List<XmlElement>();
        foreach (XmlNode Doi_tuong in Danh_sach_Nguon)
        {
            var Doi_tuong_Xml = (XmlElement)Doi_tuong;
            Danh_sach.Add(Doi_tuong_Xml);
        }
        return Danh_sach;
    }
    public static List<XmlElement> Tao_Danh_sach(XmlElement Danh_sach_Nguon, string Loai_Doi_tuong_1, string Loai_Doi_tuong_2)
    {
        var Danh_sach = new List<XmlElement>();
        foreach (XmlElement Doi_tuong in Danh_sach_Nguon.GetElementsByTagName(Loai_Doi_tuong_1))
        {
            Danh_sach.Add(Doi_tuong);
        }
        foreach (XmlElement Doi_tuong in Danh_sach_Nguon.GetElementsByTagName(Loai_Doi_tuong_2))
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