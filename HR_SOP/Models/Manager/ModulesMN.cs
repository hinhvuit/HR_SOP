using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HR_SOP.Models.Manager
{
    public class ModulesMN
    {

        ConnectionSQL aConnectionSQL = new ConnectionSQL();
        public DataTable ListSetPermitsion(string CodeRole)
        {
            DataTable aData = new DataTable();
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListSetPermitsion";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CodeRole", CodeRole);
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

        public int InsertOrUpdateModule(string ID, string CodeRole, string Code, string Xem, string ThemMoi,
            string Sua, string Xoa, string BaoCao, string TimKiem, string ResetPass,string User)
        {
            try
            {
                aConnectionSQL.Open();
                SqlCommand cmd = new SqlCommand();
                DataTable aData = new DataTable();
                cmd.Connection = aConnectionSQL.Connection();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select ID from Modules where ID = @ID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID", ID);
                SqlDataAdapter ds_adapter = new SqlDataAdapter();
                ds_adapter.SelectCommand = cmd;
                ds_adapter.Fill(aData);


                if (aData.Rows.Count < 1)
                {
                    #region
                    cmd.CommandText = "insert into Modules(ID,Code,Xem,ThemMoi,Sua,Xoa,BaoCao,TimKiem,NgayTao,NguoiTao,ResetPass,CodeRole)"
                                    + "values(@ID, @Code, @Xem, @ThemMoi, @Sua, @Xoa, @BaoCao, @TimKiem, GETDATE(), @NguoiTao, @ResetPass, @CodeRole)";
                    cmd.Parameters.Clear();

                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@Code", Code);
                    cmd.Parameters.AddWithValue("@Xem", Xem);
                    cmd.Parameters.AddWithValue("@ThemMoi", ThemMoi);
                    cmd.Parameters.AddWithValue("@Sua", Sua);

                    cmd.Parameters.AddWithValue("@Xoa", Xoa);
                    cmd.Parameters.AddWithValue("@BaoCao", BaoCao);
                    cmd.Parameters.AddWithValue("@TimKiem", TimKiem);
                    cmd.Parameters.AddWithValue("@NguoiTao", User);
                    cmd.Parameters.AddWithValue("@ResetPass", ResetPass);

                    cmd.Parameters.AddWithValue("@CodeRole", CodeRole);
                    #endregion
                }
                else
                {
                    #region
                    cmd.CommandText = "update Modules set Xem=@Xem,ThemMoi=@ThemMoi,Sua=@Sua,Xoa=@Xoa,BaoCao=@BaoCao,TimKiem=@TimKiem,"
                                    + "NgaySua = GETDATE(),NguoiSua = @NguoiSua,ResetPass = @ResetPass,CodeRole = @CodeRole where ID = @ID";
                    cmd.Parameters.Clear();

                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@Xem", Xem);
                    cmd.Parameters.AddWithValue("@ThemMoi", ThemMoi);
                    cmd.Parameters.AddWithValue("@Sua", Sua);
                    cmd.Parameters.AddWithValue("@Xoa", Xoa);

                    cmd.Parameters.AddWithValue("@BaoCao", BaoCao);
                    cmd.Parameters.AddWithValue("@TimKiem", TimKiem);
                    cmd.Parameters.AddWithValue("@NguoiSua", User);
                    cmd.Parameters.AddWithValue("@ResetPass", ResetPass);
                    cmd.Parameters.AddWithValue("@CodeRole", CodeRole);
                    #endregion
                }
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