/*******************************************************************
 * * 版权所有： 郑州点读科技杭州研发中心
 * * 文件名称： PublishHelper.cs
 * * 功   能：  发布参数配置信息统一存储
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2018-04-08 11:07:18
 * * 修改记录： 
 * * 日期时间： 2018-04-08 11:07:18  修改人：王建军  迁移
 * *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EllaBookMaker.Model
{
    /// <summary>
    /// 发布参数配置
    /// </summary>
    public static class PublishHelper
    {
        public static int CanvasWidth = 2730;
        public static int CanvasHeight = 1536;
        /// <summary>
        /// 获取素材变宽后需要减去一条边的长度
        /// </summary>
        /// <param name="clinetSize"></param>
        /// <returns></returns>
        public static double GetBlankWidth(ClientSize clinetSize)
        {
            //设备比例在高度为1536下的宽度
            double tempWidth = CanvasHeight * clinetSize.Width / clinetSize.Height;
            //   if (tempWidth > 2730) tempWidth = 2730;
            //需要减去的一条边的长度
            double oneCutSize = (CanvasWidth - tempWidth) * 0.5;
            if (oneCutSize < 0) oneCutSize = 0;
            //if (clinetSize.Width == 1136)
            //    oneCutSize = 0;
            return oneCutSize;
        }
        /// <summary>
        /// 获取比例
        /// </summary>
        /// <returns></returns>
        public static double GetRate(ClientSize clinetSize)
        {
            return clinetSize.Height/ CanvasHeight;
        }
        /// <summary>
        /// 获取终端类型列表
        /// </summary>
        /// <returns></returns>
        public static List<ClientSize> GetClients()
        {
            List<ClientSize> clients = new List<ClientSize>()
            {
                new ClientSize(2208, 1242,"Iphone2208"),
                new ClientSize(1334, 750, "Iphone1334"),
                new ClientSize(1136, 640, "Iphone1136"),
                new ClientSize(2048, 1536, "Ipad2048"),
                new ClientSize(1024, 768, "Ipad1024"),
                new ClientSize(2730, 1536, "Android")
                //new ClientSize(1920, 1080, "PC1920")
            };
            return clients;

        }
        /// <summary>
        /// 根据终端类别获取终端大小
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ClientSize GetClientByName(string name)
        {
            switch (name)
            {
                case "Iphone2208":
                    {
                        return new ClientSize(2208, 1242, "Iphone2208");
                    }
                case "Iphone1334":
                    {

                        return new ClientSize(1334, 750, "Iphone1334");

                    }
                case "Iphone1136":
                    {

                        return new ClientSize(1136, 640, "Iphone1136");

                    }
                case "Ipad2048":
                    {
                        return new ClientSize(2048, 1536, "Ipad2048");
                    }
                case "Ipad1024":
                    {
                        return new ClientSize(1024, 768, "Ipad1024");
                    }
                default:
                    return null;
            }
        }
        /// <summary>
        /// 是否为IPhone大小
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static bool IsIphoneSize(ClientSize size)
        {
            if (size.ClinetName.Contains("Iphone")) return true;
            return false;
        }
        /// <summary>
        /// 获取发布的
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static ClientSize GetPublishClinet(ClientSize size)
        {
            if (PublishHelper.IsIphoneSize(size)|| size.ClinetName == "PC1920")
                return new ClientSize(1136, 640, "Iphone1136");
            else if (size.ClinetName == "Ipad2048")
                return new ClientSize(1024, 768, "Ipad1024");
            else
                return size;
        }
        /// <summary>
        /// 获取剪切后的背景宽度
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static double GetNewWidth(ClientSize size) {
            return CanvasWidth - GetBlankWidth(size) * 2;
        }
    }
    /// <summary>
    /// 终端大小
    /// </summary>
    public class ClientSize
    {
        private double width;

        public double Width
        {
            get { return width; }
            set { width = value; }
        }
        private double height;

        public double Height
        {
            get { return height; }
            set { height = value; }
        }
        private string clinetName;

        private bool isChecked = true;

        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; }
        }
        public string ClinetName
        {
            get { return clinetName; }
            set { clinetName = value; }
        }
        public ClientSize(double width, double height, string clinetName)
        {
            Width = width;
            Height = height;
            ClinetName = clinetName;
        }
    }


}
