using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Loachs.Entity;
using Loachs.Data;
using System.Data.SqlClient;

namespace Loachs.Data.MSSQL
{
    public class User : IUser
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="_userinfo"></param>
        /// <returns></returns>
        public int InsertUser(UserInfo _userinfo)
        {
            string cmdText = @" insert into [loachs_users](
                                [Type],[UserName],[Name],[Password],[Email],[SiteUrl],[AvatarUrl],[Description],[displayorder],[Status],[PostCount],[CommentCount],[CreateDate])
                                values (
                                @Type,@UserName,@Name,@Password,@Email,@SiteUrl,@AvatarUrl,@Description,@Displayorder,@Status, @PostCount,@CommentCount,@CreateDate )";
            SqlParameter[] prams = { 
                                        MSSQLHelper.MakeInParam("@Type", SqlDbType.Int,4, _userinfo.Type),
                                        MSSQLHelper.MakeInParam("@UserName", SqlDbType.VarChar,50, _userinfo.UserName),
                                        MSSQLHelper.MakeInParam("@Name", SqlDbType.VarChar,50, _userinfo.Name),
                                        MSSQLHelper.MakeInParam("@Password", SqlDbType.VarChar,50, _userinfo.Password),
                                        MSSQLHelper.MakeInParam("@Email", SqlDbType.VarChar,50, _userinfo.Email),
                                        MSSQLHelper.MakeInParam("@SiteUrl", SqlDbType.VarChar,255, _userinfo.SiteUrl),
                                        MSSQLHelper.MakeInParam("@AvatarUrl", SqlDbType.VarChar,255, _userinfo.AvatarUrl),
                                        MSSQLHelper.MakeInParam("@description", SqlDbType.VarChar,255, _userinfo.Description),
                                        MSSQLHelper.MakeInParam("@Displayorder", SqlDbType.Int,4, _userinfo.Displayorder),
                                        MSSQLHelper.MakeInParam("@Status", SqlDbType.Int,4, _userinfo.Status),                           
                                        MSSQLHelper.MakeInParam("@PostCount", SqlDbType.Int,4, _userinfo.PostCount),
                                        MSSQLHelper.MakeInParam("@CommentCount", SqlDbType.Int,4, _userinfo.CommentCount),
                                        MSSQLHelper.MakeInParam("@CreateDate", SqlDbType.Date,8, _userinfo.CreateDate),
                                        
                                    };
            int r = MSSQLHelper.ExecuteNonQuery(CommandType.Text, cmdText, prams);
            if (r > 0)
            {
                return Convert.ToInt32(MSSQLHelper.ExecuteScalar("select top 1 [UserId] from [loachs_users]  order by [UserId] desc"));
            }
            return 0;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="_userinfo"></param>
        /// <returns></returns>
        public int UpdateUser(UserInfo _userinfo)
        {
            string cmdText = @"update [loachs_users] set
                                [Type]=@Type,
                                [UserName]=@UserName,
                                [Name]=@Name,
                                [Password]=@Password,
                                [Email]=@Email,
                                [SiteUrl]=@SiteUrl,
                                [AvatarUrl]=@AvatarUrl,
                                [Description]=@Description,
                                [Displayorder]=@Displayorder,
                                [Status]=@Status,
                                [PostCount]=@PostCount,
                                [CommentCount]=@CommentCount,
                                [CreateDate]=@CreateDate
                                where UserId=@UserId";
            SqlParameter[] prams = { 
                                        MSSQLHelper.MakeInParam("@Type", SqlDbType.Int,4, _userinfo.Type),
                                        MSSQLHelper.MakeInParam("@UserName", SqlDbType.VarChar,50, _userinfo.UserName),
                                        MSSQLHelper.MakeInParam("@Name", SqlDbType.VarChar,50, _userinfo.Name),
                                        MSSQLHelper.MakeInParam("@Password", SqlDbType.VarChar,50, _userinfo.Password),
                                        MSSQLHelper.MakeInParam("@Email", SqlDbType.VarChar,50, _userinfo.Email),
                                        MSSQLHelper.MakeInParam("@SiteUrl", SqlDbType.VarChar,255, _userinfo.SiteUrl),
                                        MSSQLHelper.MakeInParam("@AvatarUrl", SqlDbType.VarChar,255, _userinfo.AvatarUrl),
                                        MSSQLHelper.MakeInParam("@Description", SqlDbType.VarChar,255, _userinfo.Description),
                                        MSSQLHelper.MakeInParam("@Displayorder", SqlDbType.Int,4, _userinfo.Displayorder),
                                        MSSQLHelper.MakeInParam("@Status", SqlDbType.Int,4, _userinfo.Status),                           
                                        MSSQLHelper.MakeInParam("@PostCount", SqlDbType.Int,4, _userinfo.PostCount),
                                        MSSQLHelper.MakeInParam("@CommentCount", SqlDbType.Int,4, _userinfo.CommentCount),
                                        MSSQLHelper.MakeInParam("@CreateDate", SqlDbType.Date,8, _userinfo.CreateDate),
                                        MSSQLHelper.MakeInParam("@UserId", SqlDbType.Int,4, _userinfo.UserId),
                                    };
            return MSSQLHelper.ExecuteNonQuery(CommandType.Text, cmdText, prams);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int DeleteUser(int userid)
        {
            string cmdText = "delete from [loachs_users] where [userid] = @userid";
            SqlParameter[] prams = { 
								        MSSQLHelper.MakeInParam("@userid",SqlDbType.Int,4,userid)
							        };
            return MSSQLHelper.ExecuteNonQuery(CommandType.Text, cmdText, prams);
        }

       
        ///// <summary>
        ///// 获取实体
        ///// </summary>
        ///// <param name="userName"></param>
        ///// <param name="password"></param>
        ///// <returns></returns>
        //public UserInfo GetUser(string userName, string password)
        //{
        //    string cmdText = "select * from [loachs_users] where [userName] = @userName and [Password]=@password";
        //    SqlParameter[] prams = { 
        //                        MSSQLHelper.MakeInParam("@userName",SqlDbType.VarChar,50,userName),
        //                        MSSQLHelper.MakeInParam("@password",SqlDbType.VarChar,50,password),
        //                    };
        //    List<UserInfo> list = DataReaderToUserList(MSSQLHelper.ExecuteReader(CommandType.Text, cmdText, prams));
        //    if (list.Count > 0)
        //    {
        //        return list[0];
        //    }
        //    return null;
         
        //}

      

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <returns></returns>
        public List<UserInfo> GetUserList()
        {
            string cmdText = "select * from [loachs_users]  order by [displayorder] asc,[userid] asc";
            return DataReaderToUserList(MSSQLHelper.ExecuteReader(cmdText));
 
        }

        /// <summary>
        /// 数据转换
        /// </summary>
        /// <param name="read"></param>
        /// <returns></returns>
        private List<UserInfo> DataReaderToUserList(SqlDataReader read)
        {
            List<UserInfo> list = new List<UserInfo>();
            while (read.Read())
            {
                UserInfo _userinfo = new UserInfo();
                _userinfo.UserId = Convert.ToInt32(read["UserId"]);
                _userinfo.Type = Convert.ToInt32(read["Type"]);
                _userinfo.UserName = Convert.ToString(read["UserName"]);
                _userinfo.Name = Convert.ToString(read["Name"]);
                _userinfo.Password = Convert.ToString(read["Password"]);
                _userinfo.Email = Convert.ToString(read["Email"]);
                _userinfo.SiteUrl = Convert.ToString(read["SiteUrl"]);
                _userinfo.AvatarUrl = Convert.ToString(read["AvatarUrl"]);
                _userinfo.Description = Convert.ToString(read["Description"]);
                _userinfo.Displayorder = Convert.ToInt32(read["Displayorder"]);
                _userinfo.Status = Convert.ToInt32(read["Status"]);
                _userinfo.PostCount = Convert.ToInt32(read["PostCount"]);
                _userinfo.CommentCount = Convert.ToInt32(read["CommentCount"]);
                _userinfo.CreateDate = Convert.ToDateTime(read["CreateDate"]);
             

                list.Add(_userinfo);
            }
            read.Close();
            return list;
        }

        
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool ExistsUserName(string userName)
        {
            string cmdText = "select count(1) from [loachs_users] where [userName] = @userName ";
            SqlParameter[] prams = { 
                                        MSSQLHelper.MakeInParam("@userName",SqlDbType.VarChar,50,userName),
							        };
            return Convert.ToInt32(MSSQLHelper.ExecuteScalar(CommandType.Text, cmdText, prams)) > 0;
        }
    }
}
