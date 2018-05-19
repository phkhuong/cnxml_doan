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
    public bool Khoi_dong_Co_loi = false;
    XmlElement Du_lieu_Ung_dung;
    XmlElement Cong_ty;
    List<XmlElement> Danh_sach_Tinh_trang_Phieu_dat = new List<XmlElement>();
    List<XL_PHIEU_DAT> Danh_sach_Phieu_dat = new List<XL_PHIEU_DAT>();
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

        var DS_Nguoi_dung = (XmlElement)Cong_ty.GetElementsByTagName("Danh_sach_Nhan_vien")[0];
        Danh_sach_Nguoi_dung = XL_NGHIEP_VU.Tao_Danh_sach(DS_Nguoi_dung, "Quan_ly");
        var DS_Phieu_dat = (XmlElement)Du_lieu_Ung_dung.GetElementsByTagName("Danh_sach_Phieu_dat")[0];
        Danh_sach_Phieu_dat = XL_NGHIEP_VU.Tao_Danh_sach_Phieu_dat(DS_Phieu_dat);
        var DS_Tinh_trang = (XmlElement)Du_lieu_Ung_dung.GetElementsByTagName("Danh_sach_Tinh_trang_Phieu_dat")[0];
        Danh_sach_Tinh_trang_Phieu_dat = XL_NGHIEP_VU.Tao_Danh_sach(DS_Tinh_trang, "Tinh_trang");

    }
    //============= Xử lý Chức năng ========
    public XL_NGUOI_DUNG_DANG_NHAP Dang_nhap(string Ten_Dang_nhap, string Mat_khau)
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_DANG_NHAP)null;

        var Nguoi_dung = Danh_sach_Nguoi_dung.FirstOrDefault(
            x => x.GetAttribute("Ten_Dang_nhap") == Ten_Dang_nhap && x.GetAttribute("Mat_khau") == Mat_khau);
        if (Nguoi_dung != null)
        {
            var Danh_sach_Phieu_dat_cua_Nguoi_dung = Danh_sach_Phieu_dat;

            // Thống tin Online 
            Nguoi_dung_Dang_nhap = new XL_NGUOI_DUNG_DANG_NHAP();

            Nguoi_dung_Dang_nhap.Ho_ten = Nguoi_dung.GetAttribute("Ho_ten");
            Nguoi_dung_Dang_nhap.Ma_so = Nguoi_dung.GetAttribute("Ma_so");

            Nguoi_dung_Dang_nhap.Danh_sach_Phieu_dat_Xem = Danh_sach_Phieu_dat_cua_Nguoi_dung;
            Nguoi_dung_Dang_nhap.Danh_sach_Phieu_dat = Danh_sach_Phieu_dat_cua_Nguoi_dung;
            Nguoi_dung_Dang_nhap.Danh_sach_Tinh_trang = Danh_sach_Tinh_trang_Phieu_dat;

            // danh sach nhan vien truc thuoc
            var Danh_sach_Nhan_vien_Truc_thuoc = Nguoi_dung.GetElementsByTagName("Danh_sach_Nhan_vien_Truc_thuoc")[0];
            foreach(XmlElement nv in Danh_sach_Nhan_vien_Truc_thuoc)
            {
                Nguoi_dung_Dang_nhap.Danh_sach_Nhan_vien.Add(nv);
            }

            HttpContext.Current.Session["Nguoi_dung_Dang_nhap"] = Nguoi_dung_Dang_nhap;


        }

        return Nguoi_dung_Dang_nhap;
    }
    //11111111 Chức năng Xem111111111111111111111
    public string Khoi_dong()
    {
        var Chuoi_HTML = Tao_Chuoi_HTML_Ket_qua();
        return Chuoi_HTML;
    }
    public string Tra_cuu(string Chuoi_Tra_cuu)
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_DANG_NHAP)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];

        Nguoi_dung_Dang_nhap.Danh_sach_Phieu_dat_Xem = XL_NGHIEP_VU.Tra_cuu_Phieu_dat(
            Chuoi_Tra_cuu, Nguoi_dung_Dang_nhap.Danh_sach_Phieu_dat);

        var Chuoi_HTML = Tao_Chuoi_HTML_Ket_qua();
        return Chuoi_HTML;
    }
    public string Chon_Tinh_trang_Phieu_dat(string Ma_so_Tinh_trang)
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_DANG_NHAP)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];

        Ma_so_Tinh_trang = Ma_so_Tinh_trang.ToUpper();
        string chuoi_tra_cuu;
        switch (Ma_so_Tinh_trang)
        {
            case "CHỜ GIAO HÀNG":
                chuoi_tra_cuu = "CHO_GIAO_HANG";
                break;
            case "ĐÃ THANH TOÁN":
                chuoi_tra_cuu = "DA_THANH_TOAN";
                break;
            case "HỦY":
                chuoi_tra_cuu = "HUY";
                break;
            default:
                chuoi_tra_cuu = Ma_so_Tinh_trang;
                break;
        }

        Nguoi_dung_Dang_nhap.Danh_sach_Phieu_dat_Xem = XL_NGHIEP_VU.Tra_cuu_Phieu_dat(
        chuoi_tra_cuu, Nguoi_dung_Dang_nhap.Danh_sach_Phieu_dat);
        var Chuoi_HTML = Tao_Chuoi_HTML_Ket_qua();
        return Chuoi_HTML;
    }
    public string Chon_Nhan_vien_Phieu_dat(string Ma_so_Nhan_vien)
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_DANG_NHAP)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];

        
        string chuoi_tra_cuu = Ma_so_Nhan_vien;
        

        Nguoi_dung_Dang_nhap.Danh_sach_Phieu_dat_Xem = XL_NGHIEP_VU.Tra_cuu_Phieu_dat(
        chuoi_tra_cuu, Nguoi_dung_Dang_nhap.Danh_sach_Phieu_dat, true);
        var Chuoi_HTML = Tao_Chuoi_HTML_Ket_qua();
        return Chuoi_HTML;
    }
    //2222222Chức năng Ghi222222222222222

    public string Ghi_Phan_cong_Moi(string Ma_so_Phieu_dat, string Ma_so_Nhan_vien)
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_DANG_NHAP)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];

        var Hop_le = Ma_so_Phieu_dat != null && 
            Nguoi_dung_Dang_nhap.Danh_sach_Phieu_dat.Any(p => p.Ma_so == Ma_so_Phieu_dat) && 
            Nguoi_dung_Dang_nhap.Danh_sach_Nhan_vien.Any(nv => nv.GetAttribute("Ma_so") == Ma_so_Nhan_vien);
        if (Hop_le)
        {
            var Kq_Ghi = XL_LUU_TRU.Ghi_Phan_cong_moi(Ma_so_Phieu_dat, Ma_so_Nhan_vien);
            if (Kq_Ghi == "OK")
            {
                Kq_Ghi = XL_LUU_TRU.Ghi_Phan_cong_moi_Phan_he_Nhan_vien_Giao_hang(Ma_so_Phieu_dat, Ma_so_Nhan_vien);
                if(Kq_Ghi == "OK")
                {
                    Nguoi_dung_Dang_nhap.Thong_bao = $"Đã phân công {Ma_so_Phieu_dat} cho Nhân viên: {Ma_so_Nhan_vien}";
                    var Phieu_dat = Nguoi_dung_Dang_nhap.Danh_sach_Phieu_dat.First(p => p.Ma_so == Ma_so_Phieu_dat);
                    Phieu_dat.Tinh_trang = "CHO_GIAO_HANG";
                    Phieu_dat.Ngay_Phan_cong = DateTime.Now;
                    var doc = new XmlDocument();
                    var nv = doc.CreateElement("Nhan_vien");
                    nv.SetAttribute("Ma_so", Ma_so_Nhan_vien);
                    Phieu_dat.Nhan_vien = nv;
                }
                else
                    Nguoi_dung_Dang_nhap.Thong_bao = "Lỗi Hệ thống - Xin Thực hiện lại  ";
            }
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
                 $"{XL_THE_HIEN.Tao_Chuoi_HTML_Danh_sach_Tra_cuu(Nguoi_dung_Dang_nhap.Danh_sach_Tinh_trang, Nguoi_dung_Dang_nhap.Danh_sach_Nhan_vien)}" +
                 $"{XL_THE_HIEN.Tao_Chuoi_HTML_Danh_sach_Phieu_dat(Nguoi_dung_Dang_nhap.Danh_sach_Phieu_dat_Xem, Nguoi_dung_Dang_nhap.Danh_sach_Nhan_vien)}" +
             $"</div>";
        return Chuoi_HTML;

    }

    public string Ghi_Phieu_dat_Moi(string Chuoi_Xml_Dat_hang)
    {
        var Tai_lieu = new XmlDocument();
        Tai_lieu.LoadXml(Chuoi_Xml_Dat_hang);
        var Phieu_dat_xml = Tai_lieu.DocumentElement;
        var Tong_so_Phieu_dat = Danh_sach_Phieu_dat.Count;
        var So_Ma_so = Tong_so_Phieu_dat + 1;
        Phieu_dat_xml.SetAttribute("Ma_so", "PHIEU_DAT_" + So_Ma_so.ToString());
        var Phieu_dat = XL_NGHIEP_VU.Tao_Phieu_dat(Phieu_dat_xml);
        Danh_sach_Phieu_dat.Add(Phieu_dat);
        var Chuoi_Xml_Kq = $"<DU_LIEU Kq='OK' />";
        return Chuoi_Xml_Kq;
    }

    public string Ghi_Tinh_trang_Moi(string Ma_so_Phieu_dat, string Tinh_trang_moi)
    {

        var Phieu_dat = Danh_sach_Phieu_dat.FirstOrDefault(x => x.Ma_so == Ma_so_Phieu_dat);

        var Hop_le = Phieu_dat != null;
        var Chuoi_Kq_Ghi = "OK";
        if (Hop_le)
        {
            switch (Tinh_trang_moi)
            {
                case "1":
                    // thanh cong
                    Phieu_dat.Ngay_Thanh_toan = DateTime.Now;
                    Phieu_dat.Tinh_trang = "DA_THANH_TOAN";
                    break;
                case "2":
                    // huy
                    Phieu_dat.Ngay_huy = DateTime.Now;
                    Phieu_dat.Tinh_trang = "HUY";
                    break;
                case "3":
                    // cho giao hang
                    Phieu_dat.Ngay_Phan_cong = DateTime.Now;
                    Phieu_dat.Tinh_trang = "CHO_GIAO_HANG";
                    break;
            }
        }
        else
            Chuoi_Kq_Ghi = "Lỗi Hệ thống - Xin Thực hiện lại ";

        var Chuoi_Xml_Kq = $"<DU_LIEU Kq='{Chuoi_Kq_Ghi}' />";
        return Chuoi_Xml_Kq;

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

    public static string Tao_Chuoi_HTML_Danh_sach_Tra_cuu(List<XmlElement> Danh_sach_Tinh_trang, 
        List<XmlElement> Danh_sach_Nhan_vien)
    {
        var Chuoi_HTML_Danh_sach = "<div class='btn btn-primary' style='margin:10px 0'>";
        Danh_sach_Tinh_trang.ForEach(tinh_trang =>
        {
            var Ten = tinh_trang.GetAttribute("Ten");
            var Ma_so = tinh_trang.GetAttribute("Ma_so");


            var Chuoi_Chuc_nang_Chon = $"<form method='post'>" +
                                        "<input name='Th_Ma_so_Chuc_nang' type='hidden' value='CHON_TINH_TRANG_PHIEU_DAT' />" +
                                         $"<input name='Th_Ma_so_Tinh_trang_Phieu_dat' type='hidden' value='{Ma_so}' />" +
                                         $"<button type='submit' class ='btn btn-primary'>{Ten}</button>" +
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

        Danh_sach_Nhan_vien.ForEach(nv =>
        {
            var Ten = nv.GetAttribute("Ho_ten");
            var Ma_so = nv.GetAttribute("Ma_so");


            var Chuoi_Chuc_nang_Chon = $"<form method='post'>" +
                                        "<input name='Th_Ma_so_Chuc_nang' type='hidden' value='CHON_NHAN_VIEN_PHIEU_DAT' />" +
                                         $"<input name='Th_Ma_so_Nhan_vien_Phieu_dat' type='hidden' value='{Ma_so}' />" +
                                         $"<button type='submit' class ='btn btn-primary'>{Ten}</button>" +
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

    public static string Tao_Chuoi_HTML_Danh_sach_Phieu_dat(List<XL_PHIEU_DAT> Danh_sach_Phieu_dat, 
        List<XmlElement> Danh_sach_Nhan_vien)
    {
        var Chuoi_HTML_Danh_sach = "<div class='row'>";
        foreach (XL_PHIEU_DAT Phieu_dat in Danh_sach_Phieu_dat)
        {
            string tinh_trang_class;
            string tinh_trang;
            string Chuoi_Ngay = "";

            // ghi phan cong moi
            string Chuoi_Html_Lua_chon_Nhan_vien = "";
            foreach(XmlElement nv in Danh_sach_Nhan_vien)
            {
                var Ma_so = nv.GetAttribute("Ma_so");
                var Ho_ten = nv.GetAttribute("Ho_ten");
                Chuoi_Html_Lua_chon_Nhan_vien += $"<option value={Ma_so}>{Ho_ten}</option>";
            }
            string chuoi_HTML_form =
                chuoi_HTML_form = $@"
                    <form method='POST' class='form-inline'>
                        <input name='Th_Ma_so_Chuc_nang' type='hidden' value='GHI_PHAN_CONG_MOI' />    
                        <input name='Th_Ma_so_Phieu_dat' type='hidden' value='{Phieu_dat.Ma_so}' />
						<select name = 'Th_Ma_so_Nhan_vien' class='custom-select' required>
						  <option value = '' selected>Phân công Nhân viên Giao hàng</option>
						  {Chuoi_Html_Lua_chon_Nhan_vien}
                        </select >
                        <button type= 'submit' class='btn btn-primary mb-2'>Phân công</button>
					</form>
                    ";

            switch (Phieu_dat.Tinh_trang)
            {
                case "CHO_PHAN_CONG":
                    tinh_trang_class = "bg-warning";
                    tinh_trang = "CHỜ PHÂN CÔNG";
                    Chuoi_Ngay = $"Ngày Đặt: {Phieu_dat.Ngay_dat.ToString(Dinh_dang_VN)}";
                    break;
                case "CHO_GIAO_HANG":
                    tinh_trang_class = "bg-info";
                    tinh_trang = "CHỜ GIAO HÀNG";
                    Chuoi_Ngay = $"Ngày Phân công: {Phieu_dat.Ngay_Phan_cong.ToString(Dinh_dang_VN)}";
                    break;
                case "DA_THANH_TOAN":
                    tinh_trang_class = "bg-success";
                    tinh_trang = "ĐÃ THANH TOÁN";
                    Chuoi_Ngay = $"Ngày Thanh toán: {Phieu_dat.Ngay_Thanh_toan.ToString(Dinh_dang_VN)}";
                    break;
                case "HUY":
                    tinh_trang_class = "bg-danger";
                    tinh_trang = "HỦY";
                    Chuoi_Ngay = $"Ngày Hủy: {Phieu_dat.Ngay_huy.ToString(Dinh_dang_VN)}";
                    break;
                default:
                    tinh_trang_class = "";
                    tinh_trang = "";
                    break;
            }

            var Chuoi_Danh_sach_Laptop = "";
            foreach (XmlElement laptop in Phieu_dat.Danh_sach_Laptop)
            {
                var ten = laptop.GetAttribute("Ten");
                var gia = long.Parse(laptop.GetAttribute("Don_gia_Ban"));
                var soluong = laptop.GetAttribute("So_luong");

                Chuoi_Danh_sach_Laptop += $@"
                    <li class='list-group-item list-group-item-light d-flex justify-content-between align-items-center'>
					    {ten} - {gia.ToString("c0", Dinh_dang_VN)}
                        <span class='badge badge-primary badge-pill'>{soluong}</span>
					</li>
                ";
            }

            var Chuoi_Thong_tin =
            $@"<div class='col-sm-6 col-md-4'>
                <div class= 'card text-white {tinh_trang_class} mb-3'>
				  <div class='card-header'>{Phieu_dat.Ma_so} - {Phieu_dat.Tien.ToString("c0", Dinh_dang_VN)}
				  	<p>{Chuoi_Ngay}</p>
                    <p>Tình trạng: {tinh_trang}</p>
				  </div>
				  <div class='card-body'>
				    <h5 class='ard-title'>{Phieu_dat.Khach_hang.GetAttribute("Ho_ten")}</h5>
				    <p class='card-text'>Địa chỉ: {Phieu_dat.Khach_hang.GetAttribute("Dia_chi")} <br/>Số điện thoại: {Phieu_dat.Khach_hang.GetAttribute("So_Dien_thoai")} <br/></p>
				    <button class='btn btn-outline-dark' type='button' data-toggle='collapse' data-target='#{Phieu_dat.Ma_so}' aria-expanded='false' aria-controls='collapseExample'>
                        Danh sách Laptop đặt hàng
                    </button>
                    <div class='collapse' id='{Phieu_dat.Ma_so}'>
                        <ul class='list-group'>
					        {Chuoi_Danh_sach_Laptop}
					    </ul>
                    </div>
					<br/>
					{chuoi_HTML_form}
				  </div>
				</div>
			</div>";




            Chuoi_HTML_Danh_sach += Chuoi_Thong_tin;
        }

        Chuoi_HTML_Danh_sach += "</div>";
        return Chuoi_HTML_Danh_sach;
    }

    public static string Tao_Chuoi_HTML_Danh_sach_Nhan_vien(List<XmlElement> Danh_sach_Nhan_vien)
    {
        var Chuoi_HTML_Danh_sach = "<div class='row' style='margin-bottom: 1em;'>";

        foreach (XmlElement Nhan_vien in Danh_sach_Nhan_vien)
        {
            
            var Ma_so = Nhan_vien.GetAttribute("Ma_so");
            var Ho_ten = Nhan_vien.GetAttribute("Ho_ten");

            
            var Chuoi_Thong_tin = $"<div>" +
                                      $"<strong>{Ma_so}</strong>" +
                                      $"<br />Họ tên: { Ho_ten }" +
                                  $"</div>";
            var Chuoi_HTML = $"<div class='KHUNG col-6 col-sm-6 col-md-4 col-lg-3'>" +
                                 $"<div class='THONG_TIN'>" +
                                     $"{Chuoi_Thong_tin}" +
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
    public static List<XL_PHIEU_DAT> Tra_cuu_Phieu_dat(
          string Chuoi_Tra_cuu, List<XL_PHIEU_DAT> Danh_sach, bool Tra_cuu_nhan_vien = false)
    {
        if(Tra_cuu_nhan_vien == true)
        {
            return Danh_sach.FindAll(p => p.Nhan_vien != null && p.Nhan_vien.GetAttribute("Ma_so") == Chuoi_Tra_cuu);
        } 
        if (DateTime.TryParse(Chuoi_Tra_cuu, XL_THE_HIEN.Dinh_dang_VN, DateTimeStyles.None, out DateTime Ngay_Tra_cuu))
        {
            return Danh_sach.FindAll(x => x.Ngay_Phan_cong.Date == Ngay_Tra_cuu.Date);
        }

        Chuoi_Tra_cuu = Chuoi_Tra_cuu.ToUpper();
        // tra cứu tình trạng có dấu
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_DANG_NHAP)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        var node = Nguoi_dung_Dang_nhap.Danh_sach_Tinh_trang.FirstOrDefault(t => t.GetAttribute("Ten") == Chuoi_Tra_cuu);
        if (node != null)
            Chuoi_Tra_cuu = node.GetAttribute("Ma_so");

        var kq = Danh_sach.FindAll(x => x.Ma_so.Contains(Chuoi_Tra_cuu) || x.Tinh_trang == Chuoi_Tra_cuu
           || x.Khach_hang.GetAttribute("Ho_ten").ToUpper().Contains(Chuoi_Tra_cuu));
        return kq;
    }
    public static List<XmlElement> Tao_Danh_sach(XmlElement Danh_sach_Nguon, string Loai_Doi_tuong)
    {
        var Danh_sach = new List<XmlElement>();
        foreach (XmlElement Doi_tuong in Danh_sach_Nguon.GetElementsByTagName(Loai_Doi_tuong))
        {
            Danh_sach.Add(Doi_tuong);
        }
        return Danh_sach;
    }

    public static List<XL_PHIEU_DAT> Tao_Danh_sach_Phieu_dat(XmlElement Danh_sach_Xml_nguon)
    {
        List<XL_PHIEU_DAT> Danh_sach_Phieu_dat = new List<XL_PHIEU_DAT>();
        foreach (XmlElement Phieu_dat_xml in Danh_sach_Xml_nguon.GetElementsByTagName("Phieu_dat"))
        {
            var Phieu_dat =Tao_Phieu_dat(Phieu_dat_xml);

            Danh_sach_Phieu_dat.Add(Phieu_dat);
        }

        return Danh_sach_Phieu_dat;
    }
    public static XL_PHIEU_DAT Tao_Phieu_dat(XmlElement Phieu_dat_xml)
    {
        XL_PHIEU_DAT Phieu_dat = new XL_PHIEU_DAT();
        Phieu_dat.Ma_so = Phieu_dat_xml.GetAttribute("Ma_so");
        string ngay = Phieu_dat_xml.GetAttribute("Ngay_dat");
        if (ngay != "")
            Phieu_dat.Ngay_dat = DateTime.Parse(ngay, XL_THE_HIEN.Dinh_dang_VN);

        ngay = Phieu_dat_xml.GetAttribute("Ngay_Phan_cong");
        if (ngay != "")
            Phieu_dat.Ngay_Phan_cong = DateTime.Parse(ngay, XL_THE_HIEN.Dinh_dang_VN);

        ngay = Phieu_dat_xml.GetAttribute("Ngay_Thanh_toan");
        if (ngay != "")
            Phieu_dat.Ngay_Thanh_toan = DateTime.Parse(ngay, XL_THE_HIEN.Dinh_dang_VN);
        ngay = Phieu_dat_xml.GetAttribute("Ngay_Huy");
        if (ngay != "")
            Phieu_dat.Ngay_huy = DateTime.Parse(ngay, XL_THE_HIEN.Dinh_dang_VN);

        Phieu_dat.Tien = long.Parse(Phieu_dat_xml.GetAttribute("Tien"));
        Phieu_dat.Tinh_trang = Phieu_dat_xml.GetAttribute("Tinh_trang");

        Phieu_dat.Khach_hang = (XmlElement)Phieu_dat_xml.GetElementsByTagName("Khach_hang")[0];
        Phieu_dat.Nhan_vien = (XmlElement)Phieu_dat_xml.GetElementsByTagName("Nhan_vien")[0];

        Phieu_dat.Danh_sach_Laptop = Tao_Danh_sach((XmlElement)Phieu_dat_xml.GetElementsByTagName("Danh_sach_Laptop")[0], "Laptop");
        return Phieu_dat;
    }
}

//************************* Data-Layers DL **********************************
public partial class XL_LUU_TRU
{
    
    public static string Dia_chi_Dich_vu = "http://localhost:61828";
    static string Dia_chi_Dich_vu_Du_lieu = $"{Dia_chi_Dich_vu}/1-Dich_vu_Giao_tiep/DV_Quan_ly_Giao_hang.cshtml";
    static string Dia_chi_Phan_he_Nhan_vien_Giao_hang = $"http://localhost:64817/1-Dich_vu_Giao_tiep/DV_Chinh.cshtml";

    // Đọc
    public static XmlElement Doc_Du_lieu()
    {
        var Chuoi_XML = "<Du_lieu  />";
        var Xu_ly = new WebClient();
        Xu_ly.Encoding = System.Text.Encoding.UTF8;
        var Tham_so = "Ma_so_Xu_ly=KHOI_DONG_DU_LIEU";
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

    public static string Ghi_Tinh_trang_moi(string Ma_so_Phieu_dat, string Tinh_trang_moi)
    {
        var Kq = "OK";

        try
        {
            var Xu_ly = new WebClient();
            Xu_ly.Encoding = System.Text.Encoding.UTF8;
            var Tham_so = $"Ma_so_Xu_ly=GHI_TINH_TRANG_MOI&Ma_so_Phieu_dat={Ma_so_Phieu_dat}&Tinh_trang_moi={Tinh_trang_moi}";
            var Dia_chi_Xu_ly = $"{Dia_chi_Dich_vu_Du_lieu}?{Tham_so}";

            var Chuoi_XML_Kq = Xu_ly.DownloadString(Dia_chi_Xu_ly).Trim();
            var Tai_lieu = new XmlDocument();
            Tai_lieu.LoadXml(Chuoi_XML_Kq);
            Kq = Tai_lieu.DocumentElement.GetAttribute("Kq");
        }
        catch (Exception Loi)
        {
            Kq = Loi.Message;
        }

        return Kq;
    }
    public static string Ghi_Phan_cong_moi(string Ma_so_Phieu_dat, string Ma_so_Nhan_vien)
    {
        var Kq = "OK";

        try
        {
            var Xu_ly = new WebClient();
            Xu_ly.Encoding = System.Text.Encoding.UTF8;
            var Tham_so = $"Ma_so_Xu_ly=GHI_PHAN_CONG_MOI&Ma_so_Phieu_dat={Ma_so_Phieu_dat}&Ma_so_Nhan_vien={Ma_so_Nhan_vien}";
            var Dia_chi_Xu_ly = $"{Dia_chi_Dich_vu_Du_lieu}?{Tham_so}";

            var Chuoi_XML_Kq = Xu_ly.DownloadString(Dia_chi_Xu_ly).Trim();
            var Tai_lieu = new XmlDocument();
            Tai_lieu.LoadXml(Chuoi_XML_Kq);
            Kq = Tai_lieu.DocumentElement.GetAttribute("Kq");
        }
        catch (Exception Loi)
        {
            Kq = Loi.Message;
        }

        return Kq;
    }

    public static string Ghi_Phan_cong_moi_Phan_he_Nhan_vien_Giao_hang(string Ma_so_Phieu_dat, string Ma_so_Nhan_vien)
    {
        var Kq = "OK";

        try
        {
            var Xu_ly = new WebClient();
            Xu_ly.Encoding = System.Text.Encoding.UTF8;
            var Tham_so = $"Ma_so_Xu_ly=GHI_PHAN_CONG_MOI&Ma_so_Phieu_dat={Ma_so_Phieu_dat}&Ma_so_Nhan_vien={Ma_so_Nhan_vien}";
            var Dia_chi_Xu_ly = $"{Dia_chi_Phan_he_Nhan_vien_Giao_hang}?{Tham_so}";

            var Chuoi_XML_Kq = Xu_ly.DownloadString(Dia_chi_Xu_ly).Trim();
            var Tai_lieu = new XmlDocument();
            Tai_lieu.LoadXml(Chuoi_XML_Kq);
            Kq = Tai_lieu.DocumentElement.GetAttribute("Kq");
        }
        catch (Exception Loi)
        {
            Kq = Loi.Message;
        }

        return Kq;
    }
}