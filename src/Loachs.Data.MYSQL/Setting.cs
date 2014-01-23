using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

using Loachs.Entity;
using Loachs.Data;

namespace Loachs.Data.MYSQL
{
    public class Setting : ISetting
    {
        public bool UpdateSetting(SettingInfo setting)
        {
            string cmdText = @"update Loachs_Sites set setting=@setting";
            MySqlParameter[] prams = {
                                        MYSQLHelper.MakeInParam("@setting", MySqlDbType.VarChar,0,Serialize(setting)),
                                     };

            return MYSQLHelper.ExecuteNonQuery(CommandType.Text, cmdText, prams) == 1;
        }

        public SettingInfo GetSetting()
        {
            //string cmdText = "select top 1 [setting] from [Loachs_Sites]";
            string cmdText = "select setting from Loachs_Sites limit 0,1";


            string str = Convert.ToString(MYSQLHelper.ExecuteScalar(cmdText));

            object obj = DeSerialize(typeof(SettingInfo), str);
            if (obj == null)
            {
                return new SettingInfo();
            }

            return (SettingInfo)obj;
        }



        /// <summary>
        /// xml序列化成字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>xml字符串</returns>
        public static string Serialize(object obj)
        {
            string returnStr = "";

            XmlSerializer serializer = new XmlSerializer(obj.GetType());


            MemoryStream ms = new MemoryStream();
            XmlTextWriter xtw = null;
            StreamReader sr = null;
            try
            {
                xtw = new System.Xml.XmlTextWriter(ms, Encoding.UTF8);
                xtw.Formatting = System.Xml.Formatting.Indented;
                serializer.Serialize(xtw, obj);
                ms.Seek(0, SeekOrigin.Begin);
                sr = new StreamReader(ms);
                returnStr = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (xtw != null)
                    xtw.Close();
                if (sr != null)
                    sr.Close();
                ms.Close();
            }
            return returnStr;

        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static object DeSerialize(Type type, string s)
        {
            byte[] b = System.Text.Encoding.UTF8.GetBytes(s);
            try
            {
                XmlSerializer serializer = new XmlSerializer(type);

                return serializer.Deserialize(new MemoryStream(b));
            }
            catch
            {
                //  throw ex;
                return null;
            }
        }


    }
}
