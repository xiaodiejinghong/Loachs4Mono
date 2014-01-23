using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Npgsql;
using Loachs.Entity;
using Loachs.Data;
using NpgsqlTypes;

namespace Loachs.Data.Postgres
{
    public class Category : ICategory
    {
        /// <summary>
        /// 检查别名是否重复
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        private bool CheckSlug(CategoryInfo term)
        {
            while (true)
            {
                string cmdText = string.Empty;
                if (term.CategoryId == 0)
                {
                    cmdText = string.Format("select count(1) from Loachs_Terms where Slug='{0}' and type={1}", term.Slug, (int)TermType.Category);
                }
                else
                {
                    cmdText = string.Format("select count(1) from Loachs_Terms where Slug='{0}'  and type={1} and termid<>{2}", term.Slug, (int)TermType.Category, term.CategoryId);
                }
                int r = Convert.ToInt32(PGSQLHelper.ExecuteScalar(cmdText));
                if (r == 0)
                {
                    return true;
                }
                term.Slug += "-2";
            }
        }

        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public int InsertCategory(CategoryInfo category)
        {
            CheckSlug(category);

            string cmdText = @"insert into Loachs_Terms
                            (
                            Type,Name,Slug,Description,Displayorder,Count,CreateDate
                            )
                            values
                            (
                            @Type,@Name,@Slug,@Description,@Displayorder,@Count,@CreateDate
                            )";
            NpgsqlParameter[] prams = { 
                                PGSQLHelper.MakeInParam("@Type",NpgsqlDbType.Integer,1,(int)TermType.Category),
								PGSQLHelper.MakeInParam("@Name",NpgsqlDbType.Varchar,255,category.Name),
                                PGSQLHelper.MakeInParam("@Slug",NpgsqlDbType.Varchar,255,category.Slug),
								PGSQLHelper.MakeInParam("@Description",NpgsqlDbType.Varchar,255,category.Description),
                                PGSQLHelper.MakeInParam("@Displayorder",NpgsqlDbType.Integer,4,category.Displayorder),
								PGSQLHelper.MakeInParam("@Count",NpgsqlDbType.Integer,4,category.Count),
								PGSQLHelper.MakeInParam("@CreateDate",NpgsqlDbType.Date,8,category.CreateDate)
							};
            PGSQLHelper.ExecuteScalar(CommandType.Text, cmdText, prams);

            //int newId = Convert.ToInt32(PGSQLHelper.ExecuteScalar("select top 1 [termid] from [Loachs_Terms] order by [termid] desc"));
            int newId = Convert.ToInt32(PGSQLHelper.ExecuteScalar("select termid from Loachs_Terms order by termid desc limit 1 offset 0"));


            return newId;
        }

        public int UpdateCategory(CategoryInfo category)
        {
            CheckSlug(category);

            string cmdText = @"update Loachs_Terms set
                                Type=@Type,
                                Name=@Name,
                                Slug=@Slug,
                                Description=@Description,
                                Displayorder=@Displayorder,
                                Count=@Count,
                                CreateDate=@CreateDate
                                where termid=@termid";
            NpgsqlParameter[] prams = { 
                                PGSQLHelper.MakeInParam("@Type",NpgsqlDbType.Integer,1,(int)TermType.Category),
								PGSQLHelper.MakeInParam("@Name",NpgsqlDbType.Varchar,255,category.Name),
                                PGSQLHelper.MakeInParam("@Slug",NpgsqlDbType.Varchar,255,category.Slug),
								PGSQLHelper.MakeInParam("@Description",NpgsqlDbType.Varchar,255,category.Description),
                                PGSQLHelper.MakeInParam("@Displayorder",NpgsqlDbType.Integer,4,category.Displayorder),
								PGSQLHelper.MakeInParam("@Count",NpgsqlDbType.Integer,4,category.Count),
								PGSQLHelper.MakeInParam("@CreateDate",NpgsqlDbType.Date,8,category.CreateDate),
                                PGSQLHelper.MakeInParam("@termid",NpgsqlDbType.Integer,1,category.CategoryId),
							};
            return Convert.ToInt32(PGSQLHelper.ExecuteScalar(CommandType.Text, cmdText, prams));
        }

        public int DeleteCategory(int categoryId)
        {
            string cmdText = "delete from Loachs_Terms where termid = @termid";
            NpgsqlParameter[] prams = { 
								PGSQLHelper.MakeInParam("@termid",NpgsqlDbType.Integer,4,categoryId)
							};
            return PGSQLHelper.ExecuteNonQuery(CommandType.Text, cmdText, prams);


        }

        public CategoryInfo GetCategory(int categoryId)
        {
            string cmdText = "select * from Loachs_Terms where termid = @termid";
            NpgsqlParameter[] prams = { 
								PGSQLHelper.MakeInParam("@termid",NpgsqlDbType.Integer,4,categoryId)
							};

            List<CategoryInfo> list = DataReaderToList(PGSQLHelper.ExecuteReader(CommandType.Text, cmdText, prams));
            return list.Count > 0 ? list[0] : null;
        }

        /// <summary>
        /// 获取全部分类
        /// </summary>
        /// <returns></returns>
        public List<CategoryInfo> GetCategoryList()
        {
            string condition = " type=" + (int)TermType.Category;

            string cmdText = "select * from Loachs_Terms where " + condition + "  order by displayorder asc,termid asc";

            return DataReaderToList(PGSQLHelper.ExecuteReader(cmdText));

        }
 
    

        /// <summary>
        /// 转换实体
        /// </summary>
        /// <param name="read">NpgsqlDataReader</param>
        /// <returns>CategoryInfo</returns>
        private static List<CategoryInfo> DataReaderToList(NpgsqlDataReader read)
        {
            List<CategoryInfo> list = new List<CategoryInfo>();
            while (read.Read())
            {
                CategoryInfo category = new CategoryInfo();
                category.CategoryId = Convert.ToInt32(read["termid"]);
              //  category.Type = Convert.ToInt32(read["Type"]);
                category.Name = Convert.ToString(read["Name"]);
                category.Slug = Convert.ToString(read["Slug"]);
                category.Description = Convert.ToString(read["Description"]);
                category.Displayorder = Convert.ToInt32(read["Displayorder"]);
                category.Count = Convert.ToInt32(read["Count"]);
                category.CreateDate = Convert.ToDateTime(read["CreateDate"]);

                list.Add(category);
            }
            read.Close();
            return list;
        }

        
    }
}
