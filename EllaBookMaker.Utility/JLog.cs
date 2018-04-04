/*******************************************************************
 * * 版权所有： 郑州点读科技杭州研发中心
 * * 文件名称： JLog.cs
 * * 功   能：  日志输出
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2018-04-04 10:05:42
 * * 修改记录： 
 * * 日期时间： 2018-04-04 10:05:42  修改人：王建军  创建
 * *******************************************************************/
using System.Diagnostics.Eventing.Reader;
namespace EllaBookMaker.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using log4net;
    using log4net.Core;
    public class JLog : Singleton<JLog>
    {
        log4net.ILog MyLog = null;
        
        public JLog()
        {
            try
            {
                MyLog = log4net.LogManager.GetLogger("WLog");
            }
            catch (Exception)
            {
                throw new NotSupportedException();
            }
        }
        /// <summary>
        /// 总是打印的日志
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="ex"></param>
        /// <param name="MethodName"></param>
        /// <param name="ModuleName"></param>
        public void Info(object Message,Exception ex=null, string MethodName="",string ModuleName="")
        {
            if (MyLog == null)
                return;
            string MyModuleName = "Module：" + ModuleName + " ";
            string MyMethodName = "Method：" + MethodName + " ";
            StringBuilder sb = new StringBuilder(MyModuleName);
            sb.Append(MyMethodName);
            sb.Append(Message.ToString());
            Message = sb.ToString();
            if (ex == null)
            {
                MyLog.Info(Message);
            }
            else
            {
                MyLog.Info(Message, ex);
            }
             
        }

        /// <summary>
        /// 的配置允许的情况下，只有Debug才输出的日志,Release不输出
        /// 同时打印在VS输出窗口中
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="ex"></param>
        /// <param name="MethodName"></param>
        /// <param name="ModuleName"></param>
        public void Debug(object Message, Exception ex=null, string MethodName = "", string ModuleName = "")
        {
            
            if (MyLog == null|| !MyLog.IsDebugEnabled)
                return;

            string MyModuleName = "Module：" + ModuleName + " ";
            string MyMethodName = "Method：" + MethodName + " ";
            StringBuilder sb = new StringBuilder(MyModuleName);
            sb.Append(MyMethodName);
            sb.Append(Message.ToString());
            Message = sb.ToString();
            if (ex != null)
            {
                MyLog.Debug(Message, ex);
            }
            else
            {
                MyLog.Debug(Message);
            }
            System.Diagnostics.Debug.WriteLine(Message.ToString());
        }

        /// <summary>
        /// 在配置允许的情况下，输出错误信息，并打印
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="ex"></param>
        /// <param name="MethodName"></param>
        /// <param name="ModuleName"></param>
        public void Error(object Message, Exception ex=null, string MethodName = "", string ModuleName = "")
        {
            if (MyLog == null || !MyLog.IsErrorEnabled)
                return;
            string MyModuleName = "Module：" + ModuleName + " ";
            string MyMethodName = "Method：" + MethodName + " ";
            StringBuilder sb = new StringBuilder(MyModuleName);
            sb.Append(MyMethodName);
            sb.Append(Message.ToString());
            Message = sb.ToString();
            if (ex == null)
            {
                MyLog.Error(Message);
            }
            else
            {
                MyLog.Error(Message, ex);
            }
            System.Diagnostics.Debug.WriteLine(Message.ToString());
                
        }

        /// <summary>
        /// 在配置允许的情况下，输出告警信息
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="ex"></param>
        /// <param name="MethodName"></param>
        /// <param name="ModuleName"></param>
        public void Warn(object Message, Exception ex=null, string MethodName = "", string ModuleName = "")
        {
            if (MyLog == null||!MyLog.IsWarnEnabled)
                return;
            string MyModuleName = "Module：" + ModuleName + " ";
            string MyMethodName = "Method：" + MethodName + " ";
            StringBuilder sb = new StringBuilder(MyModuleName);
            sb.Append(MyMethodName);
            sb.Append(Message.ToString());
            Message = sb.ToString();
            if (ex != null)
            {
                MyLog.Warn(Message, ex);
            }
            else
            {
                MyLog.Warn(Message);
            }
            System.Diagnostics.Debug.WriteLine(Message.ToString());
        }

        /// <summary>
        /// 在配置允许的情况下，输出致命错误信息
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="ex"></param>
        /// <param name="MethodName"></param>
        /// <param name="ModuleName"></param>
        public void Fatal(object Message, Exception ex=null, string MethodName = "", string ModuleName = "")
        {
            if (MyLog == null||!MyLog.IsFatalEnabled)
                return;
            string MyModuleName = "Module：" + ModuleName + " ";
            string MyMethodName = "Method：" + MethodName + " ";
            StringBuilder sb = new StringBuilder(MyModuleName);
            sb.Append(MyMethodName);
            sb.Append(Message.ToString());
            Message = sb.ToString();
            if (ex != null)
            {
                MyLog.Fatal(Message, ex);
            }
            else
            {
                MyLog.Fatal(Message);
            }
            System.Diagnostics.Debug.WriteLine(Message.ToString());
        }
    }
}
