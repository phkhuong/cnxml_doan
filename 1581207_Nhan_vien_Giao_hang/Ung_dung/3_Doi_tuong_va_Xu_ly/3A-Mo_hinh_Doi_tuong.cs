using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
#region "********Nhân viên Nhập hàng ***********"
// Du_lieu:
//    Danh_sach_Dien_thoai: 
//      * Dien_thoai :Ma_so,Ten,Don_gia_Ban,Don_gia_Nhap,So_luong_Ton,Doanh_thu
//          Nhom_Dien_thoai:Ma_so,Ten
//   Cua_hang:Ma_so,Ten
//     Danh_sach_Nhom_Dien_thoai:
//       * Nhom_Dien_thoai:Ma_so,Ten,So_luong_Ton,Doanh_thu
//     Danh_sach_Nguoi_dung:(Chỉ có các Nhân viên Nhập hàng )
//       * Nguoi_dung :Ma_so,Ho_ten,Ten_Dang_nhap,Mat_khau
//          Nhom_Nguoi_dung:Ma_so,Ten
//          Danh_sach_Nhom_Dien_thoai:
//              * Nhom_Dien_thoai:Ma_so,Ten

#endregion

public class XL_NGUOI_DUNG_DANG_NHAP
{
    public string Ho_ten, Ma_so = "";
    public string Thong_bao = "";
    

    public List<XL_PHIEU_DAT> Danh_sach_Phieu_dat = new List<XL_PHIEU_DAT>();
    public List<XL_PHIEU_DAT> Danh_sach_Phieu_dat_Xem = new List<XL_PHIEU_DAT>();
    public List<XmlElement> Danh_sach_Tinh_trang = new List<XmlElement>();
}

//<Phieu_dat Ma_so = "PHIEU_DAT_1" Ngay_Dat="09/01/2018 12:00:00 AM" Ngay_Phan_cong="09/01/2018 12:00:00 AM" Ngay_Thanh_toan="09/01/2018 12:00:00 AM" Tien="15490000" Tinh_trang="DA_THANH_TOAN">
//	<Danh_sach_Laptop>
//		<Laptop
//            Ma_so = "LAPTOP_65"

//            Ten="Máy xách tay/ Laptop Acer A515-51G-51EM (NX.GTCSV.002) (Đen)" 
//			Don_gia_Ban="15490000"
//			So_luong="1"
//		/>
//	</Danh_sach_Laptop>
//	<Khach_hang Ho_ten = "Nguyễn Văn C" Dia_chi="123 Phạm Ngũ Lão, Quận 1" So_Dien_thoai="012385640" />
//	<Nhan_vien Ma_so = "NV_8" Ho_ten="Nguyễn Văn C" />
//</Phieu_dat>

public class XL_PHIEU_DAT
{
    public string Ma_so;
    public DateTime Ngay_dat;
    public DateTime Ngay_Phan_cong;
    public DateTime Ngay_Thanh_toan;
    public DateTime Ngay_huy;
    public long Tien;
    public string Tinh_trang;

    public List<XmlElement> Danh_sach_Laptop = new List<XmlElement>();
    public XmlElement Khach_hang;
    public XmlElement Nhan_vien;
}
