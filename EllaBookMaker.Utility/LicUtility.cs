using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EllaBookMaker.Utility
{
  public    class LicUtility
    {

        private static string DefaultKey = "2013sunflowers2014";
        #region 授权相关
        public static bool CheckLicence(string filePath)
        {
            try
            {

                if (!File.Exists(filePath))
                {
                    return false;
                }

                System.IO.StreamReader txtReader = new System.IO.StreamReader(filePath, new System.Text.UTF8Encoding(false));
                string licence = txtReader.ReadToEnd();
                txtReader.Dispose();
                string machineCode = Decrypt(licence, DefaultKey);

                if (machineCode == GetMachineNumber())
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                JLog.Instance.Error("licenceError", e);
                return false;
            }
        }

        public static bool GenerateLicence(string applyCode, string filePath)
        {
            try
            {
                System.IO.StreamWriter txtSW = new System.IO.StreamWriter(filePath);
                txtSW.Write(Encrypt(applyCode, DefaultKey));
                txtSW.Flush();
                txtSW.Close();
                txtSW.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 取得设备硬盘的卷标号
        /// </summary>
        /// <returns></returns>
        public static string GetDiskVolumeSerialNumber()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        }
        /// <summary>
        /// 获得CPU的序列号
        /// </summary>
        /// <returns></returns>
        public static string GetCpu()
        {
            string strCpu = null;
            ManagementClass myCpu = new ManagementClass("win32_Processor");
            ManagementObjectCollection myCpuConnection = myCpu.GetInstances();
            foreach (ManagementObject myObject in myCpuConnection)
            {
                strCpu = myObject.Properties["Processorid"].Value.ToString();
                break;
            }
            return strCpu;
        }
        /// <summary>
        /// 获取MAC地地址
        /// </summary>
        /// <returns></returns>
        public static string GetNetworkAdapter()
        {
            string adapteCode = string.Empty;
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc2 = mc.GetInstances();
            foreach (ManagementObject mo in moc2)
            {
                if ((bool)mo["IPEnabled"] == true)
                {
                    adapteCode = mo["MacAddress"].ToString();
                    mo.Dispose();
                    break;
                }
                mo.Dispose();
            }

            return adapteCode;
        }
        /// <summary>
        /// 生成机器码
        /// </summary>
        /// <returns></returns>
        public static string GetMachineNumber()
        {
            string strNum = GetCpu() + GetDiskVolumeSerialNumber();//获得24位Cpu和硬盘序列号
            string strMNum = strNum.Substring(0, 24);//从生成的字符串中取出前24个字符做为机器码
            return strMNum;
        }

        ///<summary>
        /// 使用给定密钥加密
        ///</summary>
        ///<param name="strText"></param>
        ///<param name="sKey">密钥</param>
        ///<returns></returns>
        public static string Encrypt(string strText, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(strText);

            des.Key = ASCIIEncoding.ASCII.GetBytes(MD5Encrypt(sKey).Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(MD5Encrypt(sKey).Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        ///<summary>
        /// 使用给定密钥解密
        ///</summary>
        ///<param name="strText"></param>
        ///<param name="sKey"></param>
        ///<returns></returns>
        public static string Decrypt(string strText, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len = strText.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(strText.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = Encoding.ASCII.GetBytes(MD5Encrypt(sKey).Substring(0, 8));
            des.IV = Encoding.ASCII.GetBytes(MD5Encrypt(sKey).Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }
        /// <summary>
        /// MD5编码
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string text)
        {
            MD5CryptoServiceProvider md5Encrypt = new MD5CryptoServiceProvider();
            byte[] palindata = Encoding.Default.GetBytes(text);//将要加密的字符串转换为字节数组
            byte[] encryptdata = md5Encrypt.ComputeHash(palindata);//将字符串加密后也转换为字符数组
            return Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为加密字符串
        }

        #endregion
    }
}
