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
                MyLog = log4net.LogManager.GetLogger("NTCLog");
            }
            catch (Exception)
            {
                //MyLog = log4net.LogManager.GetLogger("NTCLog");
                throw;
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
            
            if (MyLog == null)
                return;

            string MyModuleName = "Module：" + ModuleName + " ";
            string MyMethodName = "Method：" + MethodName + " ";
            StringBuilder sb = new StringBuilder(MyModuleName);
            sb.Append(MyMethodName);
            sb.Append(Message.ToString());
            Message = sb.ToString();
            if (MyLog.IsDebugEnabled)
            {
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
        }

        private void Error(object Message, Exception ex, string MethodName = "", string ModuleName = "")
        {
            if (MyLog == null)
                return;
            if (!string.IsNullOrEmpty(ModuleName))
            {
                _ModuleName = string.IsNullOrEmpty(ModuleName) ? string.Empty : "Module：" + ModuleName + " ";
                Message = _ModuleName + _MethodName + Message.ToString();
            }
            Message = _ModuleName + _MethodName + Message.ToString();
            if (MyLog.IsErrorEnabled)
                MyLog.Error(Message, ex);
        }
        public void Error(object Message, string MethodName = "", string ModuleName = "")
        {
            if (MyLog == null)
                return;
            ModuleName = string.IsNullOrEmpty(ModuleName) ? _ModuleName : ModuleName;
            MethodName = string.IsNullOrEmpty(MethodName) ? _MethodName : MethodName;
            Message = string.Format("{0}:{1}:{2}",ModuleName,MethodName,Message);
            if (MyLog.IsErrorEnabled)
                MyLog.Error(Message);
            System.Diagnostics.Debug.WriteLine(Message.ToString());
        }

        private void Warn(object Message, Exception ex, string MethodName = "", string ModuleName = "")
        {
            if (MyLog == null)
                return;
            if (!string.IsNullOrEmpty(ModuleName))
            {
                _ModuleName = string.IsNullOrEmpty(ModuleName) ? string.Empty : "Module：" + ModuleName + " ";
                Message = _ModuleName + _MethodName + Message.ToString();
            }
            if (MyLog.IsWarnEnabled)
                MyLog.Warn(Message, ex);
        }
        private void Warn(object Message, string MethodName = "", string ModuleName = "")
        {
            if (MyLog == null)
                return;
            if (!string.IsNullOrEmpty(ModuleName))
            {
                _ModuleName = string.IsNullOrEmpty(ModuleName) ? string.Empty : "Module：" + ModuleName + " ";
                Message = _ModuleName + _MethodName + Message.ToString();
            }
            if (MyLog.IsWarnEnabled)
                MyLog.Warn(Message);
        }

        private void Fatal(object Message, Exception ex, string MethodName = "", string ModuleName = "")
        {
            if (MyLog == null)
                return;
            if (!string.IsNullOrEmpty(ModuleName))
            {
                _ModuleName = string.IsNullOrEmpty(ModuleName) ? string.Empty : "Module：" + ModuleName + " ";
                Message = _ModuleName + _MethodName + Message.ToString();
            }
            if (MyLog.IsFatalEnabled)
                MyLog.Fatal(Message, ex);
        }
        public void Fatal(object Message, string MethodName = "", string ModuleName = "")
        {
            if (MyLog == null)
                return;
            if (!string.IsNullOrEmpty(ModuleName))
            {
                _ModuleName = string.IsNullOrEmpty(ModuleName) ? string.Empty : "所属模块：" + ModuleName + " ";
                Message = _ModuleName + _MethodName + Message.ToString();
            }
            if (MyLog.IsFatalEnabled)
                MyLog.Fatal(Message);
        }

    }
}
