// See https://aka.ms/new-console-template for more information
using DongTaBhxh.Domain.Data;
using DongTaBhxh.Domain.Models.EmployeeDb;
using ErpBhxhGiaLai.DataAccess;
using ErpBhxhGiaLai.DataAccess.Data;
using ErpBhxhGiaLai.Domain.Models;
using ErpBhxhGiaLai.Domain.Models.ErpBhxhDB;
using Microsoft.EntityFrameworkCore;

Console.OutputEncoding = System.Text.Encoding.UTF8;

//ErpBhxhGiaLaiContext dbContext = new();
//var dongtaDbContext = new DongTaBhxhDbContext();

//var staff = await dbContext.NhanVien.Where(x => x.Id == 5).Include(x => x.Luong).FirstAsync();
//Console.WriteLine(staff?.Luong.MaHsl);

//foreach (var item in await dbContext.NhanVien.Where(x => x.Id > 0).Include(x => x.Luong).ToListAsync())
//{
//    dongtaDbContext.Employees.Add(GetEmployee(item));
//}
//await dongtaDbContext.SaveChangesAsync();

//static Employee GetEmployee(NhanVien item)
//{
//    int index = item.HoTen.LastIndexOf(" ");
//    string firstname = item.HoTen[..index];
//    string lastname = item.HoTen[(index + 1)..];
//    return new Employee
//    {
//        AccountBank = item.AccountNumber,
//        Address = item.DiaChi,
//        Birthdate = item.NgaySinh,
//        Email = item.Email,
//        FirstName = firstname,
//        LastName = lastname,
//        IdentityCard = item.Cmnd,
//        Phone = item.DienThoai,
//        PostId = item.MaChucVu,
//        //DepartmentId = 1,
//        SalaryCoefficientId = item.Luong?.MaHsl,
//        IsQuitJob = item.NghiViec,
//        SortOrder = item.SortOrder,
//        EmployeeId = item.Id
//    };
//}

//string s = "Dương trọng Chính";
//var name = s.Split(" ");
//Console.WriteLine(name[^1]);
//int index = s.LastIndexOf(" ");
//Console.WriteLine(index);
//Console.WriteLine("Họ:{0}", s.Substring(0, index));
//Console.WriteLine("tên:{0}", s.Substring(index + 1));