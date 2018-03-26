using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#region "********Dữ liệu Ứng dụng ***********"
// Du_lieu:
//   Cong_ty:Ma_so,Ten
//     Danh_sach_Nhom_Lap_top:
//       * Nhom_Lap_top:Ma_so,Ten
//     Danh_sach_Mau_sac:
//       * Mau_sac:Ma_so,Ten
//     Danh_sach_Tinh_nang:
//       * Tinh_nang:Ma_so,Ten
//     Danh_sach_CPU_Series:
//       * CPU_Series:Ma_so,Ten
//     Danh_sach_Card_Man_hinh_Ngoai:
//       * Card_Man_hinh_Ngoai:Ma_so,Ten
//     Danh_sach_Chuan_Man_hinh:
//       * Chuan_Man_hinh:Ma_so,Ten
//     Danh_sach_Kich_thuoc_Man_hinh:
//       * Kich_thuoc_Man_hinh:Ma_so,Ten
//     Danh_sach_Dung_luong_Ram:
//       * Dung_luong_Ram:Ma_so,Ten
//     Danh_sach_HDD:
//       * HDD:Ma_so,Ten
//     Danh_sach_SDD:
//       * SSD:Ma_so,Ten
//     Danh_sach_Cua_hang:
//       * Cua_hang:Ma_so,Ten, Dia_chi, So_dien_thoai
//     Danh_sach_Nhan_vien:
//       * Nhan_vien :Ma_so,Ho_ten,Ten_Dang_nhap,Mat_khau
//          Nhom_Nhan_vien:Ma_so,Ten
//          Cua_hang:Ma_so,Ten, Dia_chi, So_dien_thoai
//          Danh_sach_Nhom_Lap_top:
//           * Nhom_Lap_top:Ma_so,Ten
//   Danh_sach_Laptop: 
//      * Laptop :Ma_so,Ten,Don_gia_Ban,CPU,Loai_Ram,Card_Man_hinh_Onboard,He_dieu_hanh,Trong_luong,Bao_hanh
//          Nhom_Lap_top:Ma_so,Ten
//          Tinh_nang:Ma_so,Ten
//          CPU_Series:Ma_so,Ten
//          Chuan_Man_hinh:Ma_so,Ten
//          Mau_sac:Ma_so,Ten
//          Kich_thuoc_Man_hinh:Ma_so,Ten
//          Dung_luong_Ram:Ma_so,Ten
//          Card_Man_hinh_Ngoai:Ma_so,Ten
//          HDD:Ma_so,Ten
//          SSD:Ma_so,Ten
//          Danh_sach_Dat_hang:
//            * Dat_hang : Ngay_Dat,Ngay_Phan_cong,Ngay_Thanh_toan,Ngay_Huy,So_luong,Don_gia,Tien,Cua_hang,Trang_thai
//                  Khach_hang: Ho_ten, Dia_chi, So_Dien_thoai
//          Danh_sach_Ban_hang:
//            * Ban_hang : Ngay,So_luong,Don_gia,Tien
#endregion

#region "********Dữ liệu của Phân hệ Khách Tham quan ***********"

//###############
// Du_lieu:
//   Cong_ty:Ma_so,Ten
//     Danh_sach_Nhom_Lap_top:
//       * Nhom_Lap_top:Ma_so,Ten
//     Danh_sach_Mau_sac:
//       * Mau_sac:Ma_so,Ten
//     Danh_sach_Tinh_nang:
//       * Tinh_nang:Ma_so,Ten
//     Danh_sach_CPU_Series:
//       * CPU_Series:Ma_so,Ten
//     Danh_sach_Card_Man_hinh_Ngoai:
//       * Card_Man_hinh_Ngoai:Ma_so,Ten
//     Danh_sach_Chuan_Man_hinh:
//       * Chuan_Man_hinh:Ma_so,Ten
//     Danh_sach_Kich_thuoc_Man_hinh:
//       * Kich_thuoc_Man_hinh:Ma_so,Ten
//     Danh_sach_Dung_luong_Ram:
//       * Dung_luong_Ram:Ma_so,Ten
//     Danh_sach_HDD:
//       * HDD:Ma_so,Ten
//     Danh_sach_SDD:
//       * SSD:Ma_so,Ten
//     Danh_sach_Cua_hang:
//       * Cua_hang:Ma_so,Ten, Dia_chi, So_dien_thoai
//   Danh_sach_Laptop: 
//      * Laptop :Ma_so,Ten,Don_gia_Ban,CPU,Loai_Ram,Card_Man_hinh_Onboard,He_dieu_hanh,Trong_luong,Bao_hanh
//          Nhom_Lap_top:Ma_so,Ten
//          Tinh_nang:Ma_so,Ten
//          CPU_Series:Ma_so,Ten
//          Chuan_Man_hinh:Ma_so,Ten
//          Mau_sac:Ma_so,Ten
//          Kich_thuoc_Man_hinh:Ma_so,Ten
//          Dung_luong_Ram:Ma_so,Ten
//          Card_Man_hinh_Ngoai:Ma_so,Ten
//          HDD:Ma_so,Ten
//          SSD:Ma_so,Ten
#endregion

#region "********Dữ liệu của Phân hệ Nhân viên Giao hàng ***********"
// Du_lieu:
//   Cong_ty:Ma_so,Ten
//     Danh_sach_Nhom_Lap_top:
//       * Nhom_Lap_top:Ma_so,Ten
//     Danh_sach_Mau_sac:
//       * Mau_sac:Ma_so,Ten
//     Danh_sach_Tinh_nang:
//       * Tinh_nang:Ma_so,Ten
//     Danh_sach_CPU_Series:
//       * CPU_Series:Ma_so,Ten
//     Danh_sach_Card_Man_hinh_Ngoai:
//       * Card_Man_hinh_Ngoai:Ma_so,Ten
//     Danh_sach_Chuan_Man_hinh:
//       * Chuan_Man_hinh:Ma_so,Ten
//     Danh_sach_Kich_thuoc_Man_hinh:
//       * Kich_thuoc_Man_hinh:Ma_so,Ten
//     Danh_sach_Dung_luong_Ram:
//       * Dung_luong_Ram:Ma_so,Ten
//     Danh_sach_HDD:
//       * HDD:Ma_so,Ten
//     Danh_sach_SDD:
//       * SSD:Ma_so,Ten
//     Danh_sach_Cua_hang:
//       * Cua_hang:Ma_so,Ten, Dia_chi, So_dien_thoai
//     Danh_sach_Nhan_vien:
//       * Nhan_vien :Ma_so,Ho_ten,Ten_Dang_nhap,Mat_khau
//          Nhom_Nhan_vien:Ma_so,Ten
//          Cua_hang:Ma_so,Ten, Dia_chi, So_dien_thoai
//          Danh_sach_Nhom_Lap_top:
//           * Nhom_Lap_top:Ma_so,Ten
//   Danh_sach_Laptop: 
//      * Laptop :Ma_so,Ten,Don_gia_Ban,CPU,Loai_Ram,Card_Man_hinh_Onboard,He_dieu_hanh,Trong_luong,Bao_hanh
//          Nhom_Lap_top:Ma_so,Ten
//          Tinh_nang:Ma_so,Ten
//          CPU_Series:Ma_so,Ten
//          Chuan_Man_hinh:Ma_so,Ten
//          Mau_sac:Ma_so,Ten
//          Kich_thuoc_Man_hinh:Ma_so,Ten
//          Dung_luong_Ram:Ma_so,Ten
//          Card_Man_hinh_Ngoai:Ma_so,Ten
//          HDD:Ma_so,Ten
//          SSD:Ma_so,Ten
//          Danh_sach_Dat_hang:
//            * Dat_hang : Ngay_Dat,Ngay_Phan_cong,Ngay_Thanh_toan,Ngay_Huy,So_luong,Don_gia,Tien,Cua_hang,Trang_thai
//                  Khach_hang: Ho_ten, Dia_chi, So_Dien_thoai
#endregion

#region "********Dữ liệu của Phân hệ Nhân viên Bán hàng ***********"
// Du_lieu:
//   Cong_ty:Ma_so,Ten
//     Danh_sach_Nhom_Lap_top:
//       * Nhom_Lap_top:Ma_so,Ten
//     Danh_sach_Mau_sac:
//       * Mau_sac:Ma_so,Ten
//     Danh_sach_Tinh_nang:
//       * Tinh_nang:Ma_so,Ten
//     Danh_sach_CPU_Series:
//       * CPU_Series:Ma_so,Ten
//     Danh_sach_Card_Man_hinh_Ngoai:
//       * Card_Man_hinh_Ngoai:Ma_so,Ten
//     Danh_sach_Chuan_Man_hinh:
//       * Chuan_Man_hinh:Ma_so,Ten
//     Danh_sach_Kich_thuoc_Man_hinh:
//       * Kich_thuoc_Man_hinh:Ma_so,Ten
//     Danh_sach_Dung_luong_Ram:
//       * Dung_luong_Ram:Ma_so,Ten
//     Danh_sach_HDD:
//       * HDD:Ma_so,Ten
//     Danh_sach_SDD:
//       * SSD:Ma_so,Ten
//     Danh_sach_Cua_hang:
//       * Cua_hang:Ma_so,Ten, Dia_chi, So_dien_thoai
//     Danh_sach_Nhan_vien:
//       * Nhan_vien :Ma_so,Ho_ten,Ten_Dang_nhap,Mat_khau
//          Nhom_Nhan_vien:Ma_so,Ten
//          Cua_hang:Ma_so,Ten, Dia_chi, So_dien_thoai
//          Danh_sach_Nhom_Lap_top:
//           * Nhom_Lap_top:Ma_so,Ten
//   Danh_sach_Laptop: 
//      * Laptop :Ma_so,Ten,Don_gia_Ban,CPU,Loai_Ram,Card_Man_hinh_Onboard,He_dieu_hanh,Trong_luong,Bao_hanh
//          Nhom_Lap_top:Ma_so,Ten
//          Tinh_nang:Ma_so,Ten
//          CPU_Series:Ma_so,Ten
//          Chuan_Man_hinh:Ma_so,Ten
//          Mau_sac:Ma_so,Ten
//          Kich_thuoc_Man_hinh:Ma_so,Ten
//          Dung_luong_Ram:Ma_so,Ten
//          Card_Man_hinh_Ngoai:Ma_so,Ten
//          HDD:Ma_so,Ten
//          SSD:Ma_so,Ten
//          Danh_sach_Ban_hang:
//            * Ban_hang : Ngay,So_luong,Don_gia,Tien
#endregion

#region "********Dữ liệu của Phân hệ Quản lý Giao hàng ***********"
// Du_lieu:
//   Cong_ty:Ma_so,Ten
//     Danh_sach_Nhom_Lap_top:
//       * Nhom_Lap_top:Ma_so,Ten
//     Danh_sach_Mau_sac:
//       * Mau_sac:Ma_so,Ten
//     Danh_sach_Tinh_nang:
//       * Tinh_nang:Ma_so,Ten
//     Danh_sach_CPU_Series:
//       * CPU_Series:Ma_so,Ten
//     Danh_sach_Card_Man_hinh_Ngoai:
//       * Card_Man_hinh_Ngoai:Ma_so,Ten
//     Danh_sach_Chuan_Man_hinh:
//       * Chuan_Man_hinh:Ma_so,Ten
//     Danh_sach_Kich_thuoc_Man_hinh:
//       * Kich_thuoc_Man_hinh:Ma_so,Ten
//     Danh_sach_Dung_luong_Ram:
//       * Dung_luong_Ram:Ma_so,Ten
//     Danh_sach_HDD:
//       * HDD:Ma_so,Ten
//     Danh_sach_SDD:
//       * SSD:Ma_so,Ten
//     Danh_sach_Cua_hang:
//       * Cua_hang:Ma_so,Ten, Dia_chi, So_dien_thoai
//     Danh_sach_Nhan_vien:
//       * Nhan_vien :Ma_so,Ho_ten,Ten_Dang_nhap,Mat_khau
//          Nhom_Nhan_vien:Ma_so,Ten
//          Cua_hang:Ma_so,Ten, Dia_chi, So_dien_thoai
//          Danh_sach_Nhom_Lap_top:
//           * Nhom_Lap_top:Ma_so,Ten
//   Danh_sach_Laptop: 
//      * Laptop :Ma_so,Ten,Don_gia_Ban,CPU,Loai_Ram,Card_Man_hinh_Onboard,He_dieu_hanh,Trong_luong,Bao_hanh
//          Nhom_Lap_top:Ma_so,Ten
//          Tinh_nang:Ma_so,Ten
//          CPU_Series:Ma_so,Ten
//          Chuan_Man_hinh:Ma_so,Ten
//          Mau_sac:Ma_so,Ten
//          Kich_thuoc_Man_hinh:Ma_so,Ten
//          Dung_luong_Ram:Ma_so,Ten
//          Card_Man_hinh_Ngoai:Ma_so,Ten
//          HDD:Ma_so,Ten
//          SSD:Ma_so,Ten
//          Danh_sach_Dat_hang:
//            * Dat_hang : Ngay_Dat,Ngay_Phan_cong,Ngay_Thanh_toan,Ngay_Huy,So_luong,Don_gia,Tien,Cua_hang,Trang_thai
//                  Khach_hang: Ho_ten, Dia_chi, So_Dien_thoai
#endregion


//*************************** Đối tượng Dữ liệu   *********
public partial class XL_DU_LIEU
{
	public XL_CONG_TY Cong_ty = new XL_CONG_TY();
	public List<XL_NHOM_LAP_TOP> Danh_sach_Nhom_Lap_top = new List<XL_NHOM_LAP_TOP>();
}
//*************************** Đối tượng Tổ chức  *********
public class XL_CONG_TY
{
	public string Ten, Ma_so = "", Dien_thoai, Dia_chi;
	public List<XL_MAU_SAC> Danh_sach_Mau_sac = new List<XL_MAU_SAC>();
	public List<XL_NHAN_VIEN_BAN_HANG> Danh_sach_Nhan_vien = new List<XL_NHAN_VIEN_BAN_HANG>();
	public List<XL_LAP_TOP> Danh_sach_Lap_top = new List<XL_LAP_TOP>();
}
public class XL_MAU_SAC
{
	public string Ten = "";
	public int Ma_so = 0;
	public List<XL_TINH_NANG> Danh_sach_Tinh_nang = new List<XL_TINH_NANG>();
}

public class XL_TINH_NANG
{
	public string Ten = "";
	public int Ma_so = 0;
	public List<XL_HDD> Danh_sach_HDD = new List<XL_HDD>();

}

public class XL_NHAN_VIEN_BAN_HANG
{
	public string Ho_ten, Ma_so = "", Ten_Dang_nhap, Mat_khau;
}
public class XL_LAP_TOP
{
	public string Ho_ten, Ma_so = "";
	public int Don_gia_ban;
	public string Loai_ram, Card_man_hinh, He_dieu_hanh;
	public XL_HDD Danh_sach_HDD = new XL_HDD();
}
public class XL_HDD
{ 
	public string Ten, Ma_so = "";

}
public class XL_SDD
{
	public string Ten, Ma_so = "";

}
//*************************** Đối tượng Xử lý Chính *********
public class XL_NHOM_LAP_TOP
{
	public List<XL_CPU_Series> Danh_sach_CPU = new List<XL_CPU_Series>();
	public List<XL_HDD> Danh_sach_HDD = new List<XL_HDD>();
	public List<XL_RAM> Danh_sach_RAM = new List<XL_RAM>();

	public long Doanh_thu;
}

public class XL_CPU_Series
{
	public int Ma_so = 0;
	public string Ten;
}

public class XL_CARD_MAN_HINH_NGOAI
{
	public XL_HDD Danh_sach_HDD = new XL_HDD();
	public string Ten;
	public int Ma_so;
}
public class XL_KICH_THUOC_MAN_HINH
{
	public string Ten;
	public int Ma_so;
}
public class XL_RAM
{
	public string Ma_so = "";
	public string Ten;
}
public class XL_CUA_HANG
{
	public string Ten, Ma_so, Dia_chi, So_dien_thoai;
}
public class XL_BAN_HANG
{
	public List<XL_LAP_TOP> Danh_sach_Lap_top= new List<XL_LAP_TOP>();
}