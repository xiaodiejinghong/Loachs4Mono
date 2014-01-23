using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Loachs.Entity;
using Loachs.Data;
using MySql.Data.MySqlClient;

namespace Loachs.Data.MYSQL
{
    public class Comment : IComment
    {

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public int InsertComment(CommentInfo comment)
        {
            string cmdText = @"insert into Loachs_Comments(
                            PostId, ParentId,UserId,Name,Email,SiteUrl,Content,EmailNotify,IpAddress,CreateDate,Approved)
                             values (
                            @PostId, @ParentId,@UserId,@Name,@Email,@SiteUrl,@Content,@EmailNotify,@IpAddress,@CreateDate,@Approved)";

            MySqlParameter[] prams = { 
                                        MYSQLHelper.MakeInParam("@PostId", MySqlDbType.Int32,4, comment.PostId),
                                        MYSQLHelper.MakeInParam("@ParentId", MySqlDbType.Int32,4, comment.ParentId),
                                        MYSQLHelper.MakeInParam("@UserId", MySqlDbType.Int32,4, comment.UserId),
                                        MYSQLHelper.MakeInParam("@Name", MySqlDbType.VarChar,255, comment.Name),
                                        MYSQLHelper.MakeInParam("@Email", MySqlDbType.VarChar,255, comment.Email),
                                        MYSQLHelper.MakeInParam("@SiteUrl", MySqlDbType.VarChar,255, comment.SiteUrl),
                                        MYSQLHelper.MakeInParam("@Content", MySqlDbType.VarChar,255, comment.Content),
                                        MYSQLHelper.MakeInParam("@EmailNotify", MySqlDbType.Int32,4 ,    comment.EmailNotify),
                                        MYSQLHelper.MakeInParam("@IpAddress", MySqlDbType.VarChar,255, comment.IpAddress),
                                        MYSQLHelper.MakeInParam("@CreateDate", MySqlDbType.Date,8, comment.CreateDate),
                                        MYSQLHelper.MakeInParam("@Approved", MySqlDbType.Int32,4 ,   comment.Approved),
            };
            MYSQLHelper.ExecuteNonQuery(CommandType.Text, cmdText, prams);

            int newId = Convert.ToInt32(MYSQLHelper.ExecuteScalar("select  CommentId from Loachs_Comments  order by CommentId desc limit 0,1"));
            return newId;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public int UpdateComment(CommentInfo comment)
        {
            string cmdText = @"update Loachs_Comments set 
                            PostId=@PostId,                            
                            ParentId=@ParentId,
                            UserId=@UserId,
                            Name=@Name,
                            Email=@Email,
                            SiteUrl=@SiteUrl,
                            Content=@Content,
                            EmailNotify=@EmailNotify,
                            IpAddress=@IpAddress,
                            CreateDate=@CreateDate,
                            Approved=@Approved
                            where CommentId=@CommentId ";

            MySqlParameter[] prams = { 
                                        MYSQLHelper.MakeInParam("@PostId", MySqlDbType.Int32,4, comment.PostId),
                                        MYSQLHelper.MakeInParam("@ParentId", MySqlDbType.Int32,4, comment.ParentId),
                                        MYSQLHelper.MakeInParam("@UserId", MySqlDbType.Int32,4, comment.UserId),
                                        MYSQLHelper.MakeInParam("@Name", MySqlDbType.VarChar,255, comment.Name),
                                        MYSQLHelper.MakeInParam("@Email", MySqlDbType.VarChar,255, comment.Email),
                                        MYSQLHelper.MakeInParam("@SiteUrl", MySqlDbType.VarChar,255, comment.SiteUrl),
                                        MYSQLHelper.MakeInParam("@Content", MySqlDbType.VarChar,255, comment.Content),
                                        MYSQLHelper.MakeInParam("@EmailNotify", MySqlDbType.Int32,4 ,    comment.EmailNotify),
                                        MYSQLHelper.MakeInParam("@IpAddress", MySqlDbType.VarChar,255, comment.IpAddress),
                                        MYSQLHelper.MakeInParam("@CreateDate", MySqlDbType.Date,8, comment.CreateDate),
                                        MYSQLHelper.MakeInParam("@Approved", MySqlDbType.Int32,4 ,   comment.Approved),
                                        MYSQLHelper.MakeInParam("@CommentId", MySqlDbType.Int32,4, comment.CommentId),

                                    };
            return MYSQLHelper.ExecuteNonQuery(CommandType.Text, cmdText, prams);

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public int DeleteComment(int commentId)
        {
            CommentInfo comment = GetComment(commentId);        //删除前

            string cmdText = "delete from Loachs_Comments where commentId = @commentId";
            MySqlParameter[] prams = { 
								MYSQLHelper.MakeInParam("@commentId",MySqlDbType.Int32,4,commentId)
							};

            int result = MYSQLHelper.ExecuteNonQuery(CommandType.Text, cmdText, prams);
            return result;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public CommentInfo GetComment(int commentId)
        {
            string cmdText = "select * from Loachs_Comments where commentId = @commentId";
            MySqlParameter[] prams = { 
								        MYSQLHelper.MakeInParam("@commentId",MySqlDbType.Int32,4,commentId)
							          };
            List<CommentInfo> list = DataReaderToCommentList(MYSQLHelper.ExecuteReader(cmdText, prams));

            return list.Count > 0 ? list[0] : null;
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public List<CommentInfo> GetCommentList(int pageSize, int pageIndex, out int totalRecord, int order, int userId, int postId, int parentId, int approved, int emailNotify, string keyword)
        {
            string condition = " 1=1 ";// "[ParentId]=0 and [PostId]=" + postId;

            if (userId != -1)
            {
                condition += " and userid=" + userId;
            }
            if (postId != -1)
            {
                condition += " and postId=" + postId;
            }
            if (parentId != -1)
            {
                condition += " and parentId=" + parentId;
            }

            if (approved != -1)
            {
                condition += " and approved=" + approved;
            }

            if (emailNotify != -1)
            {
                condition += " and emailNotify=" + emailNotify;
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                condition += string.Format(" and (content like '%{0}%' or author like '%{0}%' or ipaddress like '%{0}%' or email like '%{0}%'  or siteurl like '%{0}%' )", keyword);
            }

            string cmdTotalRecord = "select count(1) from Loachs_Comments where " + condition;
            totalRecord = Convert.ToInt32(MYSQLHelper.ExecuteScalar(CommandType.Text, cmdTotalRecord));

            //   throw new Exception(cmdTotalRecord);

            string cmdText = MYSQLHelper.GetPageSql("Loachs_Comments", "CommentId", "*", pageSize, pageIndex, order, condition);
            return DataReaderToCommentList(MYSQLHelper.ExecuteReader(cmdText));
        }

        /// <summary>
        /// 根据日志ID删除评论
        /// </summary>
        /// <param name="postId">日志ID</param>
        /// <returns></returns>
        public int DeleteCommentByPost(int postId)
        {
            string cmdText = "delete from Loachs_Comments where postId = @postId";
            MySqlParameter[] prams = { 
								MYSQLHelper.MakeInParam("@postId",MySqlDbType.Int32,4,postId)
							};
            int result = MYSQLHelper.ExecuteNonQuery(CommandType.Text, cmdText, prams);

            return result;
        }

        /// <summary>
        /// 数据转换
        /// </summary>
        /// <param name="read"></param>
        /// <returns></returns>
        private List<CommentInfo> DataReaderToCommentList(MySqlDataReader read)
        {
            List<CommentInfo> list = new List<CommentInfo>();
            while (read.Read())
            {
                CommentInfo comment = new CommentInfo();
                comment.CommentId = Convert.ToInt32(read["CommentId"]);
                comment.ParentId = Convert.ToInt32(read["ParentId"]);
                comment.PostId = Convert.ToInt32(read["PostId"]);
                comment.UserId = Convert.ToInt32(read["UserId"]);
                comment.Name = Convert.ToString(read["Name"]);
                comment.Email = Convert.ToString(read["Email"]);
                comment.SiteUrl = Convert.ToString(read["SiteUrl"]);
                comment.Content = Convert.ToString(read["Content"]);
                comment.EmailNotify = Convert.ToInt32(read["EmailNotify"]);
                comment.IpAddress = Convert.ToString(read["IpAddress"]);
                comment.CreateDate = Convert.ToDateTime(read["CreateDate"]);
                comment.Approved = Convert.ToInt32(read["Approved"]);
                list.Add(comment);
            }
            read.Close();
            return list;
        }


        /// <summary>
        /// 统计评论
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="postId"></param>
        /// <param name="incChild"></param>
        /// <returns></returns>
        public int GetCommentCount(int userId, int postId, bool incChild)
        {
            string condition = " 1=1 ";
            if (userId != -1)
            {
                condition += " and userId = " + userId;
            }
            if (postId != -1)
            {
                condition += " and postId = " + postId;
            }
            if (incChild == false)
            {
                condition += " and parentid=0";
            }
            string cmdText = "select count(1) from Loachs_Comments where " + condition;
            return Convert.ToInt32(MYSQLHelper.ExecuteScalar(CommandType.Text, cmdText));
        }
    }
}
