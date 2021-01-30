using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HR_SOP.Models.Manager
{
    public class UsersMN
    {
        ConnectionSQL aConnectionSQL = new ConnectionSQL();
        public DataTable ListUsers(string TenDangNhap, string HoTen,string SoDienThoai,string Email,
            string ChucVu, string PhongBan, string IsDeleted)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListUsers";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TenDangNhap", TenDangNhap);
                cmd.Parameters.AddWithValue("@HoTen", HoTen);
                cmd.Parameters.AddWithValue("@SoDienThoai", SoDienThoai);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@ChucVu", ChucVu);
                cmd.Parameters.AddWithValue("@PhongBan", PhongBan);
                cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                SqlDataAdapter ds_adapter = new SqlDataAdapter();
                ds_adapter.SelectCommand = cmd;
                ds_adapter.Fill(aData);
                aConnectionSQL.Close();
                return aData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetManagerCurrent()
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Users where IsDeleted = 0 and TenDangNhap =(select top 1 UserName from UserInPosition where Position='C-00008')";
                SqlDataAdapter ds_adapter = new SqlDataAdapter();
                ds_adapter.SelectCommand = cmd;
                ds_adapter.Fill(aData);
                aConnectionSQL.Close();
                return aData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertOrUpdateUser(string ID, string TenDangNhap, string MatKhau,
            string HoTen, string User, string IsDeleted,string Email,string SoDienThoai,string DCC)
        {
            try
            {
                
                SqlCommand cmd = new SqlCommand();
                DataTable aData = new DataTable();
                
                if (String.IsNullOrEmpty(ID))
                {
                    #region
                    DataTable aRow = CheckExistUser(TenDangNhap);
                    if (aRow.Rows.Count > 0)
                    {
                        return 3;
                    }
                    else
                    {
                        aConnectionSQL.Open();
                        cmd.Connection = aConnectionSQL.Connection();
                        cmd.CommandType = CommandType.Text;
                        MatKhau = Utils.MD5(MatKhau);
                        cmd.CommandText = "insert into Users(ID,TenDangNhap,MatKhau,HoTen,NgayTao,NguoiTao,IsDeleted,IsLocked,Email,SoDienThoai,DCC)"
                            + "values(NEWID(), @TenDangNhap, @MatKhau, @HoTen, GETDATE(), @NguoiTao, @IsDeleted, 0,@Email,@SoDienThoai,@DCC)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@TenDangNhap", TenDangNhap);
                        cmd.Parameters.AddWithValue("@MatKhau", MatKhau);
                        cmd.Parameters.AddWithValue("@HoTen", HoTen);
                        cmd.Parameters.AddWithValue("@NguoiTao", User);
                        cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.Parameters.AddWithValue("@SoDienThoai", SoDienThoai);
                        cmd.Parameters.AddWithValue("@DCC", DCC);
                        cmd.ExecuteNonQuery();
                        aConnectionSQL.Close();
                        return 1;
                    }
                    #endregion
                }
                else
                {
                    #region
                    aConnectionSQL.Open();
                    cmd.Connection = aConnectionSQL.Connection();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update Users set TenDangNhap = @TenDangNhap,HoTen = @HoTen,"
                        + "NgaySua = GETDATE(),NguoiSua = @NguoiSua,IsDeleted = @IsDeleted,Email=@Email,SoDienThoai=@SoDienThoai,DCC=@DCC where ID = @ID";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TenDangNhap", TenDangNhap);
                    cmd.Parameters.AddWithValue("@HoTen", HoTen);
                    cmd.Parameters.AddWithValue("@NguoiSua", User);
                    cmd.Parameters.AddWithValue("@IsDeleted", IsDeleted);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@SoDienThoai", SoDienThoai);
                    cmd.Parameters.AddWithValue("@DCC", DCC);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();
                    aConnectionSQL.Close();
                    return 2;
                    #endregion
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeletedUser(string ID, string User)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Users set IsDeleted = 1,NguoiXoa='" + User + "',NgayXoa=GETDATE() where ID = '" + ID + "'";
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ResetPassword(string ID, string MatKhau)
        {
            try
            {
                MatKhau = Utils.MD5(MatKhau);
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Users set MatKhau='" + MatKhau + "' where ID = '" + ID + "'";
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ChangePassword(string UserName, string MatKhau)
        {
            try
            {
                MatKhau = Utils.MD5(MatKhau);
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Users set MatKhau='" + MatKhau + "' where TenDangNhap = '" + UserName + "'";
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable CheckExistUser(string TenDangNhap)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckExistUser";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TenDangNhap", TenDangNhap);
                SqlDataAdapter ds_adapter = new SqlDataAdapter();
                ds_adapter.SelectCommand = cmd;
                ds_adapter.Fill(aData);
                aConnectionSQL.Close();
                return aData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable CheckLogin(string Username, string Password,string id_user)
        {
            DataTable aData = new DataTable();
            try
            {
                Password = Utils.MD5(Password);
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckLogin";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TenDangNhap", Username);
                cmd.Parameters.AddWithValue("@MatKhau", Password);
                cmd.Parameters.AddWithValue("@Id_User", id_user);
                SqlDataAdapter ds_adapter = new SqlDataAdapter();
                ds_adapter.SelectCommand = cmd;
                ds_adapter.Fill(aData);
                aConnectionSQL.Close();
                return aData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListPermitsionByUserName(string UserName)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListPermitsionByUserName";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UserName", UserName);
                SqlDataAdapter ds_adapter = new SqlDataAdapter();
                ds_adapter.SelectCommand = cmd;
                ds_adapter.Fill(aData);
                aConnectionSQL.Close();
                return aData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListDetailUserByDepartment(string Department,string Position)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListDetailUserByDepartment";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Department", Department);
                cmd.Parameters.AddWithValue("@Position", Position);
                SqlDataAdapter ds_adapter = new SqlDataAdapter();
                ds_adapter.SelectCommand = cmd;
                ds_adapter.Fill(aData);
                aConnectionSQL.Close();
                return aData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListUserManagerRoom(string Username)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListUserManagerRoom";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Username", Username);
                SqlDataAdapter ds_adapter = new SqlDataAdapter();
                ds_adapter.SelectCommand = cmd;
                ds_adapter.Fill(aData);
                aConnectionSQL.Close();
                return aData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable CheckLevel(string Username)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_CheckLevel";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Username", Username);
                SqlDataAdapter ds_adapter = new SqlDataAdapter();
                ds_adapter.SelectCommand = cmd;
                ds_adapter.Fill(aData);
                aConnectionSQL.Close();
                return aData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteManager_Room(string Username)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"delete Manager_Room where Username='" + Username + "'";
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertManager_Room(string Username, string Username_Manager)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"insert into Manager_Room(ID,Username,Username_Manager,CreatedBy,CreatedDate) values (NEWID(), '"
                +Username+"', '"+ Username_Manager + "','"+LoginSession.UserName()+"',GETDATE())";
                cmd.ExecuteNonQuery();
                aConnectionSQL.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}