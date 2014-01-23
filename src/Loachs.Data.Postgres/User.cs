using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Loachs.Entity;
using Loachs.Data;
using Npgsql;
using NpgsqlTypes;

namespace Loachs.Data.Postgres
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
            string cmdText = @" insert into Loachs_Users(
                                Type,UserName,Name,Password,Email,SiteUrl,AvatarUrl,Description,displayorder,Status,PostCount,CommentCount,CreateDate)
                                values (
                                @Type,@UserName,@Name,@Password,@Email,@SiteUrl,@AvatarUrl,@Description,@Displayorder,@Status, @PostCount,@CommentCount,@CreateDate )";
            NpgsqlParameter[] prams = { 
                                        PGSQLHelper.MakeInParam("@Type", NpgsqlDbType.Integer,4, _userinfo.Type),
                                        PGSQLHelper.MakeInParam("@UserName", NpgsqlDbType.Varchar,50, _userinfo.UserName),
                                        PGSQLHelper.MakeInParam("@Name", NpgsqlDbType.Varchar,50, _userinfo.Name),
                                        PGSQLHelper.MakeInParam("@Password", NpgsqlDbType.Varchar,50, _userinfo.Password),
                                        PGSQLHelper.MakeInParam("@Email", NpgsqlDbType.Varchar,50, _userinfo.Email),
                                        PGSQLHelper.MakeInParam("@SiteUrl", NpgsqlDbType.Varchar,255, _userinfo.SiteUrl),
                                        PGSQLHelper.MakeInParam("@AvatarUrl", NpgsqlDbType.Varchar,255, _userinfo.AvatarUrl),
                                        PGSQLHelper.MakeInParam("@description", NpgsqlDbType.Varchar,255, _userinfo.Description),
                                        PGSQLHelper.MakeInParam("@Displayorder", NpgsqlDbType.Integer,4, _userinfo.Displayorder),
                                        PGSQLHelper.MakeInParam("@Status", NpgsqlDbType.Integer,4, _userinfo.Status),                          
                                        PGSQLHelper.MakeInParam("@PostCount", NpgsqlDbType.Integer,4, _userinfo.PostCount),
                                        PGSQLHelper.MakeInParam("@CommentCount", NpgsqlDbType.Integer,4, _userinfo.CommentCount),
                                        PGSQLHelper.MakeInParam("@CreateDate", NpgsqlDbType.Date,8, _userinfo.CreateDate),
                                        
                                    };
            int r = PGSQLHelper.ExecuteNonQuery(CommandType.Text, cmdText, prams);
            if (r > 0)
            {
                //return Convert.ToInt32(PGSQLHelper.ExecuteScalar("select top 1 UserId from Loachs_Users  order by UserId desc"));
                return Convert.ToInt32(PGSQLHelper.ExecuteScalar("select UserId from Loachs_Users  order by UserId desc limit 1 offset 0"));

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
            string cmdText = @"update Loachs_Users set
                                Type=@Type,
                                UserName=@UserName,
                                Name=@Name,
                                Password=@Password,
                                Email=@Email,
                                SiteUrl=@SiteUrl,
                                AvatarUrl=@AvatarUrl,
                                Description=@Description,
                                Displayorder=@Displayorder,
                                Status=@Status,
                                PostCount=@PostCount,
                                CommentCount=@CommentCount,
                                CreateDate=@CreateDate
                                where UserId=@UserId";
            NpgsqlParameter[] prams = { 
                                        PGSQLHelper.MakeInParam("@Type", NpgsqlDbType.Integer,4, _userinfo.Type),
                                        PGSQLHelper.MakeInParam("@UserName", NpgsqlDbType.Varchar,50, _userinfo.UserName),
                                        PGSQLHelper.MakeInParam("@Name", NpgsqlDbType.Varchar,50, _userinfo.Name),
                                        PGSQLHelper.MakeInParam("@Password", NpgsqlDbType.Varchar,50, _userinfo.Password),
                                        PGSQLHelper.MakeInParam("@Email", NpgsqlDbType.Varchar,50, _userinfo.Email),
                                        PGSQLHelper.MakeInParam("@SiteUrl", NpgsqlDbType.Varchar,255, _userinfo.SiteUrl),
                                        PGSQLHelper.MakeInParam("@AvatarUrl", NpgsqlDbType.Varchar,255, _userinfo.AvatarUrl),
                                        PGSQLHelper.MakeInParam("@Description", NpgsqlDbType.Varchar,255, _userinfo.Description),
                                        PGSQLHelper.MakeInParam("@Displayorder", NpgsqlDbType.Integer,4, _userinfo.Displayorder),
                                        PGSQLHelper.MakeInParam("@Status", NpgsqlDbType.Integer,4, _userinfo.Status),                           
                                        PGSQLHelper.MakeInParam("@PostCount", NpgsqlDbType.Integer,4, _userinfo.PostCount),
                                        PGSQLHelper.MakeInParam("@CommentCount", NpgsqlDbType.Integer,4, _userinfo.CommentCount),
                                        PGSQLHelper.MakeInParam("@CreateDate", NpgsqlDbType.Date,8, _userinfo.CreateDate),
                                        PGSQLHelper.MakeInParam("@UserId", NpgsqlDbType.Integer,4, _userinfo.UserId),
                                    };
            return PGSQLHelper.ExecuteNonQuery(CommandType.Text, cmdText, prams);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int DeleteUser(int userid)
        {
            string cmdText = "delete from Loachs_Users where userid = @userid";
            NpgsqlParameter[] prams = { 
								        PGSQLHelper.MakeInParam("@userid",NpgsqlDbType.Integer,4,userid)
							        };
            return PGSQLHelper.ExecuteNonQuery(CommandType.Text, cmdText, prams);
        }

       
        ///// <summary>
        ///// 获取实体
        ///// </summary>
        ///// <param name="userName"></param>
        ///// <param name="password"></param>
        ///// <returns></returns>
        //public UserInfo GetUser(string userName, string password)
        //{
        //    string cmdText = "select * from [Loachs_Users] where [userName] = @userName and [Password]=@password";
        //    NpgsqlParameter[] prams = { 
        //                        PGSQLHelper.MakeInParam("@userName",NpgsqlDbType.Varchar,50,userName),
        //                        PGSQLHelper.MakeInParam("@password",NpgsqlDbType.Varchar,50,password),
        //                    };
        //    List<UserInfo> list = DataReaderToUserList(PGSQLHelper.ExecuteReader(CommandType.Text, cmdText, prams));
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
            string cmdText = "select * from Loachs_Users  order by displayorder asc,userid asc";
            return DataReaderToUserList(PGSQLHelper.ExecuteReader(cmdText));
 
        }

        /// <summary>
        /// 数据转换
        /// </summary>
        /// <param name="read"></param>
        /// <returns></returns>
        private List<UserInfo> DataReaderToUserList(NpgsqlDataReader read)
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
            string cmdText = "select count(1) from Loachs_Users where userName = @userName ";
            NpgsqlParameter[] prams = { 
                                        PGSQLHelper.MakeInParam("@userName",NpgsqlDbType.Varchar,50,userName),
							        };
            return Convert.ToInt32(PGSQLHelper.ExecuteScalar(CommandType.Text, cmdText, prams)) > 0;
        }
    }
}
