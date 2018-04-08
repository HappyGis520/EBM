/*******************************************************************
 * * 版权所有： 郑州点读科技杭州研发中心
 * * 文件名称： NewtonsoftHelper.cs
 * * 功   能：  JSON序列化和反序列化辅助类
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2018-04-08 14:07:19
 * * 修改记录： 
 * * 日期时间： 2018-04-08 14:07:19  修改人：王建军  迁移
 * *******************************************************************/
using Newtonsoft.Json;
using System;
using System.IO;
using EllaBookMaker.Utility;

namespace EllaBookMaker.Utility
{
    /// <summary>
    /// JSON序列化和反序列化辅助类
    /// </summary>
    public class NewtonsoftHelper
    {
        /// <summary>
        /// JSON序列化
        /// </summary>
        public static string JsonSerializer<T>(T t)
        {
            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            setting.NullValueHandling = NullValueHandling.Ignore;
            setting.Converters.Add(new MinifiedNumArrayConverter());
            return JsonConvert.SerializeObject(t, setting);
        }

        /// <summary>
        /// JSON反序列化
        /// </summary>
        public static T DeserializeObject<T>(string str)
        {
            //替换中文符号
            str = str.Replace("“", "\"").Replace("”", "\"").Replace("：", ":").Replace("，", ",");
            try
            {
                return JsonConvert.DeserializeObject<T>(str);
            }
            catch(Exception e)  
            {
                JLog.Instance.Debug("Json序列化报错", e);
                return default(T); 
            }
        }
        public static string ConvertJsonString(string str)
        {
            //格式化json字符串
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
        }
    }

    public class MinifiedNumArrayConverter : JsonConverter
    {
        private void dumpNumArray<T>(JsonWriter writer, T[] array)
        {

            foreach (T n in array)
            {

                var s = n.ToString();
                if (s.EndsWith(".0"))

                    writer.WriteRawValue(s.Substring(0, s.Length - 2));

                else if (s.Contains("."))


                    writer.WriteRawValue(s.TrimEnd('0'));

                else

                    writer.WriteRawValue(s);

            }

        }



        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)

        {

            writer.WriteStartArray();

            Type t = value.GetType();

            if (t == dblArrayType)

                dumpNumArray<double>(writer, (double[])value);

            else if (t == decArrayType)


                dumpNumArray<decimal>(writer, (decimal[])value);

            else

                throw new NotImplementedException();

            writer.WriteEndArray();

        }



        private Type dblArrayType = typeof(double[]);

        private Type decArrayType = typeof(decimal[]);



        public override bool CanConvert(Type objectType)

        {

            if (objectType == dblArrayType || objectType == decArrayType)

                return true;

            return false;

        }



        public override bool CanRead
        {

            get { return false; }

        }



        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }





    }


}
