/*******************************************************************
 * * 版权所有： 郑州点读科技杭州研发中心
 * * 文件名称： ExportHelper.cs
 * * 功   能：   用于书籍发布或导出的类
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2018-04-08 14:03:44
 * * 修改记录： 
 * * 日期时间： 2018-04-08 14:03:44  修改人：王建军  迁移/整理
 * *******************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using EllaBookMaker.Model;
using EllaBookMaker.Utility;


namespace Sunflowers
{
    /********************************************************************************
     ** 作者： 张天奇
     ** 创始时间：2016-6-24
     ** 描述：
     ** 用于书籍发布或导出的类
     *********************************************************************************/
    public class ExportHelper
    {

        /// <summary>
        /// 默认DPI
        /// </summary>
        public const int DPI = 72;
        /// <summary>
        /// 去重复切片容器
        /// </summary>
        public static Dictionary<string, string> pathAndImageName = new Dictionary<string, string>();
        /// <summary>
        /// 导出过的音频
        /// </summary>
        public static Dictionary<string, string> audioPathVisited = new Dictionary<string, string>();
        ///// <summary>
        ///// 进度条
        ///// </summary>
        //public static ProgressControl progress;
        /// <summary>
        /// 导出工程文件
        /// </summary>
        /// <param name="curBook"></param>
        /// <param name="SavePath"></param>
        private static Process ImageCompressPro = null;
        private static Process ExportAudioProc = null;
        private static readonly string lameEXE = AppDomain.CurrentDomain.BaseDirectory + "lame.exe";
        private static readonly string pngquantEXE = AppDomain.CurrentDomain.BaseDirectory + "pngquant.exe";

        public static void ExportImage(string srcImgPath, string destImgPath, double rate, bool isCompress = true)
        {
            if (File.Exists(srcImgPath))
            {
                Util.ScaleRateImage(srcImgPath, destImgPath, rate);
                if (isCompress) CompressPng(destImgPath);
            }
        }



        //public static void CopyBookToFile(Main win, Book orgBook, string SavePath, Grid gridWindownContain = null, bool doComp = false)
        //{


        //    if (IsExporting == true)
        //    {
        //        return;
        //    }
        //    ThreadPool.QueueUserWorkItem(delegate
        //   {

        //       //---------------添加进度条-------------------------------------------------------
        //       if (gridWindownContain != null)
        //       {
        //           Application.Current.Dispatcher.BeginInvoke(new Action(() =>
        //           {
        //               progress = new ProgressControl();
        //               if (progress != null)
        //                   gridWindownContain.Children.Add(progress);
        //               progress.ShowWord(string.Empty);
        //           }));
        //       }
        //       Thread.Sleep(30);
        //       IsExporting = true;
        //       #region 变量声明
        //       string rootFolder = SavePath + "\\导出的工程文件夹\\";
        //       string imageFolder = rootFolder + "{0}\\图片\\切片";
        //       string bgFolder = rootFolder + "{0}\\图片\\背景";
        //       string soundFolder = rootFolder + "{0}\\音频\\切片音";
        //       string bgSoundFolder = rootFolder + "{0}\\音频\\背景音";
        //       string movieFolder = rootFolder + "{0}\\视频";
        //       string particleFolder = rootFolder + "{0}\\粒子效果";
        //       string destBgPicPath;
        //       string destBgSoundPath;
        //       string pageDir;
        //       List<Sprite> normalSprites;
        //       List<Particle> particles;
        //       List<Sprite> movies;
        //       Sprite onePiece;
        //       string destFilePath = string.Empty;
        //       string destSoundPath = string.Empty;
        //       #endregion
        //       Util.CreateDir(rootFolder);


        //       orgBook.SaveTo(rootFolder + "Project.sfb");
        //       Book curBook = Book.Open(rootFolder + "Project.sfb");
        //       if (curBook == null)
        //       {
        //           MessageBox.Show("无效电子书工程文件");
        //           return;
        //       }
        //       if (!string.IsNullOrEmpty(orgBook.SavePath))
        //       {
        //           Book.CheckBookFile(orgBook.SavePath, curBook);
        //       }

        //       string prefixName = string.Empty;
        //       if (string.IsNullOrEmpty(curBook.PrefixName))
        //           prefixName = "b1_";

        //       //封面
        //       if (File.Exists(curBook.CoverPath))
        //       {
        //           CopyImageComp(curBook.CoverPath, rootFolder + "Conver.png", doComp);
        //           curBook.CoverPath = rootFolder + "Conver.png";
        //       }
        //       for (int pageIndex = 0; pageIndex < curBook.Pages.Count; pageIndex++)
        //       {

        //           Application.Current.Dispatcher.BeginInvoke(new Action(() =>
        //           {
        //               progress.progressBar.Maximum = curBook.Pages.Count;
        //               progress.progressBar.Value = pageIndex + 1;
        //           }));
        //           #region 页面
        //           Page page = curBook.Pages[pageIndex];
        //           pageDir = "p" + (pageIndex + 1);
        //           Util.CreateDir(string.Format(bgFolder, pageDir));

        //           //背景图
        //           destBgPicPath = string.Format(bgFolder, pageDir) + "\\" + prefixName + page.BgImageName + ".jpg";
        //           progress.ShowFileName(destBgPicPath);
        //           if (!string.IsNullOrEmpty(page.BgImagePath) && File.Exists(page.BgImagePath))
        //           {
        //               CopyImageComp(page.BgImagePath, destBgPicPath, doComp);
        //               page.BgImagePath = destBgPicPath;
        //           }
        //           else
        //           {
        //               if (string.IsNullOrEmpty(page.BgImagePath))
        //               {
        //                   Util.CreateEmptyPngFile(destBgPicPath, 2730, 1536, DPI);
        //               }
        //           }
        //           Util.CreateDir(string.Format(bgSoundFolder, pageDir));
        //           //背景音乐
        //           if (!string.IsNullOrEmpty(page.BgSoundPath))
        //           {
        //               destBgSoundPath = string.Format(bgSoundFolder, pageDir) + "\\" + prefixName + page.BgImageName + ".mp3";
        //               progress.ShowFileName(destBgSoundPath);
        //               if (File.Exists(page.BgSoundPath))
        //               {
        //                   if (doComp) ExportAudio(AudioSize.Bit32, page.BgSoundPath, destBgSoundPath);
        //                   else CopyFile(page.BgSoundPath, destBgSoundPath, true);
        //                   page.BgSoundPath = destBgSoundPath;
        //               }
        //           }
        //           normalSprites = new List<Sprite>();
        //           particles = new List<Particle>();
        //           movies = new List<Sprite>();
        //           for (int i = 0; i < page.Sprites.Count; i++)
        //           {
        //               onePiece = page.Sprites[i];
        //               if (onePiece.GetType() == typeof(Particle))
        //                   particles.Add(onePiece as Particle);
        //               else
        //                   if (onePiece.buddyMovie != null)
        //               {
        //                   movies.Add(onePiece);
        //               }
        //               else if (!string.IsNullOrEmpty(onePiece._sourceImagePath) && onePiece._sourceImagePath.Contains("Stop_Red.png"))
        //               { }
        //               else
        //                   normalSprites.Add(onePiece);
        //           }
        //           #endregion

        //           #region 切片
        //           Util.CreateDir(string.Format(imageFolder, pageDir));

        //           for (int i = 0; i < normalSprites.Count; i++)
        //           {
        //               onePiece = normalSprites[i];
        //               if (onePiece.ImgName == string.Empty) continue;
        //               //----------------------------------------------------------------------------------------

        //               //如果是帧动画的素材文件，导出总切片和单一切片素材和plist文件
        //               if (onePiece.haveAnimation(AnimationStyle.帧动画))
        //               {
        //                   destFilePath = string.Format(imageFolder, pageDir) + "\\" + prefixName + onePiece.ImgName + ".plist";
        //                   CopyFile(onePiece.FrameAnimPlistPath, destFilePath, true);
        //                   onePiece.FrameAnimPlistPath = destFilePath;
        //                   destFilePath = string.Format(imageFolder, pageDir) + "\\" + prefixName + onePiece.ImgName + ".png";
        //                   progress.ShowFileName(destFilePath);
        //                   CopyImageComp(onePiece.SourceImagePath, destFilePath, doComp);
        //                   onePiece.SourceImagePath = destFilePath;
        //               }
        //               else if (onePiece.haveAnimation(AnimationStyle.骨骼动画))
        //               {
        //                   destFilePath = string.Format(imageFolder, pageDir) + "\\" + prefixName + onePiece.ImgName + ".png";

        //                   if (Util.IsBone(destFilePath)) continue;

        //                   progress.ShowFileName(destFilePath);

        //                   CopyFile(onePiece.SourceImagePath, destFilePath, true);
        //                   CopyImageComp(onePiece.SourceImagePath.ToTexPngPath(), destFilePath.ToTexPngPath(), doComp);
        //                   CopyFile(onePiece.SourceImagePath.ToTexJsonPath(), destFilePath.ToTexJsonPath(), true);
        //                   CopyFile(onePiece.SourceImagePath.ToSkeJsonPath(), destFilePath.ToSkeJsonPath(), true);

        //                   onePiece.SourceImagePath = destFilePath;
        //                   onePiece.TexPngPath = destFilePath.ToTexPngPath();
        //                   onePiece.TexJsonPath = destFilePath.ToTexJsonPath();
        //                   onePiece.SkeJsonPath = destFilePath.ToSkeJsonPath();

        //               }
        //               else
        //               {
        //                   if (!string.IsNullOrEmpty(onePiece.SourceImagePath))
        //                   {
        //                       destFilePath = string.Format(imageFolder, pageDir) + "\\" + prefixName + onePiece.ImgName + ".png";
        //                       progress.ShowFileName(destFilePath);
        //                       CopyImageComp(onePiece.SourceImagePath, destFilePath, doComp);
        //                       onePiece.SourceImagePath = destFilePath;
        //                       #region 如果是游戏连线游戏 要导出两张切片
        //                       if (onePiece.GetType() == typeof(OrderLineSprite))
        //                       {
        //                           OrderLineSprite ols = onePiece as OrderLineSprite;
        //                           if (ols != null && ols.IsGameSprite)
        //                           {
        //                               destFilePath = string.Format(imageFolder, pageDir) + "\\" + prefixName + onePiece.ImgName + "_t.png";
        //                               if (File.Exists(ols.TouchedImagePath))
        //                               {
        //                                   CopyImageComp(ols.TouchedImagePath, destFilePath, doComp);
        //                                   ols.TouchedImagePath = destFilePath;
        //                               }
        //                               else
        //                               {
        //                                   // Util.CreateEmptyPngFile(destFilePath, (int)(onePiece.Width), (int)(onePiece.Height), DPI);
        //                               }
        //                           }
        //                       }
        //                       #endregion
        //                   }

        //               }

        //               //----------------------------------------------------------------------------------------
        //               //声音
        //               Util.CreateDir(string.Format(soundFolder, pageDir));
        //               if (!string.IsNullOrEmpty(onePiece.SoundSourcePath))
        //               {
        //                   destSoundPath = string.Format(soundFolder, pageDir) + "\\" + prefixName + onePiece.ImgName + ".mp3";
        //                   progress.ShowFileName(destSoundPath);
        //                   if (File.Exists(onePiece.SoundSourcePath))
        //                   {
        //                       if (doComp)
        //                           ExportAudio(AudioSize.Bit32, onePiece.SoundSourcePath, destSoundPath);
        //                       else
        //                           CopyFile(onePiece.SoundSourcePath, destSoundPath, true);
        //                       onePiece.SoundSourcePath = destSoundPath;
        //                   }
        //               }
        //               #region 动画
        //               foreach (AnimationGroup ag in onePiece.AnimationGroupSet)
        //               {
        //                   foreach (Animation anim in ag.AnimationSet)
        //                   {
        //                       if (anim.GetType() == typeof(ReplaceAnimation))
        //                       {
        //                           List<ImageAudioObj> list = ((ReplaceAnimation)anim).ImgAudioList;

        //                           destFilePath = string.Format(imageFolder, pageDir) + "\\" + prefixName + onePiece.ImgName;
        //                           destSoundPath = string.Format(soundFolder, pageDir) + "\\" + prefixName + onePiece.ImgName;

        //                           for (int j = 0; j < list.Count; j++)
        //                           {
        //                               ImageAudioObj obj = list[j];
        //                               if (!string.IsNullOrEmpty(obj.ImagePath) && File.Exists(obj.ImagePath))
        //                               {
        //                                   CopyImageComp(obj.ImagePath, destFilePath + "_" + (j + 2).ToString() + ".png", doComp);
        //                                   obj.ImagePath = destFilePath + "_" + (j + 2).ToString() + ".png";
        //                               }
        //                               if (!string.IsNullOrEmpty(obj.AudioPath) && File.Exists(obj.AudioPath))
        //                               {
        //                                   if (doComp)
        //                                       ExportAudio(AudioSize.Bit32, obj.AudioPath, destSoundPath + "_" + (j + 2).ToString() + ".mp3");
        //                                   else
        //                                       CopyFile(obj.AudioPath, destSoundPath + "_" + (j + 2).ToString() + ".mp3", true);
        //                                   obj.AudioPath = destSoundPath + "_" + (j + 2).ToString() + ".mp3";
        //                               }
        //                           }
        //                       }
        //                   }
        //               }
        //               #endregion
        //           }
        //           #endregion

        //           #region  粒子效果
        //           Particle particle;
        //           string particleName;
        //           Util.CreateDir(string.Format(particleFolder, pageDir));
        //           for (int i = 0; i < particles.Count; i++)
        //           {
        //               particle = particles[i];
        //               particleName = "p" + (i + 1);
        //               destFilePath = string.Format(particleFolder, pageDir) + "\\" + particleName + ".png";
        //               if (File.Exists(particle._sourceImagePath))
        //               {
        //                   CopyImageComp(particle._sourceImagePath, destFilePath, doComp);
        //                   particle._sourceImagePath = destFilePath;
        //               }
        //               else
        //               {
        //                   MessageBox.Show("找不到粒子效果图片" + particle._sourceImagePath);
        //                   IsExporting = false;
        //                   Application.Current.Dispatcher.BeginInvoke(new Action(() =>
        //                   {
        //                       if (progress != null)
        //                       {
        //                           gridWindownContain.Children.Remove(progress);
        //                           progress = null;
        //                       }
        //                   }));
        //                   return;
        //               }

        //           }
        //           #endregion

        //           #region  视频
        //           Sprite movieSprite;
        //           string movieName;
        //           Util.CreateDir(string.Format(movieFolder, pageDir));
        //           for (int i = 0; i < movies.Count; i++)
        //           {
        //               movieSprite = movies[i];
        //               movieName = "p" + (i + 1);
        //               Movie movie = movieSprite.buddyMovie;
        //               if (movie == null) continue;
        //               destFilePath = string.Format(movieFolder, pageDir) + "\\" + movieName + movie.VideoName.Substring(movie.VideoName.LastIndexOf("."));
        //               CopyFile(movie.VideoSourcePath, destFilePath, true);
        //               movie.VideoSourcePath = destFilePath;

        //               if (!string.IsNullOrEmpty(movieSprite.SourceImagePath))
        //               {
        //                   destFilePath = string.Format(imageFolder, pageDir) + "\\" + prefixName + movieSprite.ImgName + ".png";
        //                   progress.ShowFileName(destFilePath);
        //                   CopyImageComp(movieSprite.SourceImagePath, destFilePath, doComp);
        //                   movieSprite.SourceImagePath = destFilePath;
        //               }
        //           }
        //           #endregion

        //           #region 字幕
        //           if (page.ziMuSouce != null)
        //           {
        //               //导出字幕文字音
        //               if (File.Exists(page.BgWordSoundPath))
        //               {

        //                   destFilePath = System.IO.Path.Combine(string.Format(bgSoundFolder, pageDir), prefixName + "wordAudio" + (page.PageIndex) + ".mp3");
        //                   CopyFile(page.BgWordSoundPath, destFilePath, true);
        //                   page.BgWordSoundPath = destFilePath;
        //               }
        //               if (page.ziMuSouce != null)
        //               {
        //                   var items = page.ziMuSouce.ZimuItems.Union(page.ziMuSouce.ZiMuFSItems).ToList();
        //                   DiWenModel dw = null;
        //                   for (int i = 0; i < items.Count; i++)
        //                   {
        //                       dw = items[i].DiWenSouce;
        //                       if (dw != null && !dw.IsDefault && File.Exists(dw.FilePath))
        //                       {
        //                           destFilePath = Path.Combine(string.Format(bgSoundFolder, pageDir), prefixName + (page.PageIndex) + "_dw" + (i + 1) + ".png");
        //                           CopyImageComp(dw.FilePath, destFilePath, doComp);
        //                           dw.FilePath = destFilePath;
        //                       }
        //                   }
        //               }

        //           }
        //           #endregion
        //       }
        //       Application.Current.Dispatcher.BeginInvoke(new Action(() =>
        //       {
        //           progress.ShowFileName(rootFolder + "Project.sfb");
        //           curBook.Save(rootFolder + "Project.sfb");

        //       }));

        //       IsExporting = false;
        //       if (gridWindownContain != null)
        //       {
        //           Application.Current.Dispatcher.BeginInvoke(new Action(() =>
        //           {
        //               if (progress != null)
        //               {
        //                   gridWindownContain.Children.Remove(progress);
        //                   progress = null;
        //               }
        //               MessageBoxResult result = MessageBox.Show(MainWin, "是否打开导出文件夹", "信息提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
        //               if (result == MessageBoxResult.Yes)
        //               {
        //                   Process.Start(new ProcessStartInfo() { FileName = "Explorer.exe ", Arguments = rootFolder, UseShellExecute = true });
        //               }
        //           }));

        //       }
        //       Dispose();
        //   });
        //}




        #region 发布书籍优化（第二版）


        public class ExportObject
        {
            public string orgPath;
            public string destPath;
            public double scale;
            public ExportImageType type;
            public double width;
            public double height;
            public double cutWidth;
            public const int DPI = 72;
            public AudioSize audioSize;
            public double cornerRadius;
            public Color color;
            public bool IsVerticalFlip = false;
            public bool IsHorizontalFlip = false;
            public bool IsFeather = false;
        }
        public static double appendResourceScale = 1;
        /// <summary>
        /// 发布书籍
        /// </summary>
        /// <param name="curBook">书籍对象</param>
        /// <param name="SavePath">发布文件夹路径</param>
        /// <param name="clinetSize">发布版本</param>
        /// <param name="typePix">预览后缀</param>
        /// <param name="startPageIndex">从第几页发布（从0开始）</param>
        /// <param name="endIndex">发布结束页码</param>
        public static void DoExportAllFiles(Book curBook, string SavePath, ClientSize clinetSize, string typePix, int startIndex, int endIndex, bool compress = true)
        {
            Stopwatch sw = new Stopwatch();
            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
            sw.Start();
            //----------------------------------处理导出参数--------------------------------------------------------------------
            #region 处理导出参数
            string clientPath;  //版本文件夹（Root）
            double rate;        //缩放系数
            double cutWidth;    //背景剪切宽度
            string imageFolder; //图片文件夹
            string soundFolder; //音频文件夹
            string movieFolder; //视频文件夹
            string ziMuFolder;  //字幕文件夹
            string ziMuFontsFolder;
            clientPath = SavePath + "\\" + clinetSize.ClinetName + typePix;
            rate = PublishHelper.GetRate(clinetSize) * appendResourceScale;
            cutWidth = PublishHelper.GetBlankWidth(clinetSize);

            //将参数保存在全局 用于json生成
            TotalStats.GetCurrentStats().rate = rate;
            TotalStats.GetCurrentStats().cutWidth = cutWidth;

            imageFolder = clientPath + "\\image";
            soundFolder = clientPath + "\\sound";
            movieFolder = clientPath + "\\movie";
            ziMuFolder = clientPath + "\\component\\subtitle\\resource";
            ziMuFontsFolder = clientPath + "\\component\\subtitle\\resource\\fonts";
            #endregion

            //所以导出素材集合
            List<ExportObject> exportObjs = new List<ExportObject>();
            //找出要编译的页
            var exportPages = curBook.Pages.Skip(startIndex).Take(endIndex - startIndex).ToList();
            //----------------------------------预处理书籍--------------------------------------------------------------------

            //创建文件夹
            Util.CreateDir(clientPath);
            Util.CreateDir(imageFolder);
            Util.CreateDir(soundFolder);
            Util.CreateDir(movieFolder);
            Util.CreateDir(ziMuFolder);
            Util.CreateDir(ziMuFontsFolder);

            //处理书籍前缀
            if (string.IsNullOrEmpty(curBook.PrefixName))
            {
                curBook.PrefixName = "b1";
                curBook.RenameAllPages();
            }

            //显示发布文件
            bool showProgress = curBook.progress != null;
            //素材大小后缀（用于区分同一文件 不同大小）
            string sizePix = string.Empty;
            //-----------------------------------导出-------------------------------------------------------------------

            //Json 
            if (showProgress) curBook.progress.ShowFileName("正在编译Json.....");
            DoExportJson(curBook, clientPath, clinetSize, exportPages);
            //封面
            exportObjs.Add(new ExportObject()
            {
                orgPath = curBook.CoverPath,
                destPath = Path.Combine(imageFolder, "Conver.png"),
                scale = rate,
                type = ExportImageType.普通图片
            });
            if (showProgress) curBook.progress.ShowFileName("正在整理导出书籍.....");
            //导出字幕字体
            //exportObjs.Add(new ExportObject()
            //{
            //    orgPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fonts\\msyh.ttf"),
            //    destPath = Path.Combine(ziMuFolder, "fonts\\msyh.ttf"),
            //    type = ExportImageType.直接拷贝
            //});
            //导出所有页码
            exportPages.ForEach(pg =>
            {
                DoExportOnePage(
                                 exportObjs: exportObjs,
                                 curBook: curBook,              //当前书籍
                                 page: pg,                      //当前书页
                                 Index: exportPages.IndexOf(pg),//页索引
                                 clinetSize: clinetSize,        //导出版本
                                 cutWidth: cutWidth,            //背景剪切宽度
                                 rate: rate,                    //缩放系数
                                 imageFolder: imageFolder,      //图片文件夹
                                 soundFolder: soundFolder,      //音频文件夹
                                 movieFolder: movieFolder,      //视频文件夹
                                 ziMuFolder: ziMuFolder         //字幕文件夹
                             );
            });

            Console.WriteLine("创建书籍对象用时:" + sw.ElapsedMilliseconds * 0.001 + "秒");

            sw.Restart();
            ExportSource(exportObjs, showProgress, curBook.progress, false);
            Console.WriteLine("导出对象用时:" + sw.ElapsedMilliseconds * 0.001 + "秒");
            sw.Restart();

            if (showProgress) curBook.progress.ShowFileName("正在压缩切片.....");
            if (compress)
            {
                //统一压缩图片
                CompressPng(Path.Combine(imageFolder, "*.png"));
                Console.WriteLine("压缩图像用时:" + sw.ElapsedMilliseconds * 0.001 + "秒");
            }

            if (showProgress) curBook.progress.ShowFileName("生成压缩文件中.....");
            sw.Restart();

            //文件压缩
            ZipFloClass Zc = new ZipFloClass();
            Zc.ZipFile(clientPath, clientPath + ".zip");
            Console.WriteLine("压缩包生成用时:" + sw.ElapsedMilliseconds * 0.001 + "秒");
            sw.Stop();

            Console.WriteLine("总共用时:" + sw1.ElapsedMilliseconds * 0.001 + "秒");
            sw1.Stop();
            exportObjs.Clear();
            Dispose();

            if (!curBook.Settings.SaveExportDir) Directory.Delete(clientPath, true);

        }
        public static void DoExportJson(Book book, string clientpath, ClientSize size, List<Page> pages)
        {

            JsonExportHelper.ExportJson(book, pages, Path.Combine(clientpath, "json.txt"), size);
            JsonExportHelper.ExportBookInfo(book, Path.Combine(clientpath, "bookjson.txt"));
            JsonExportHelper.ExportZiMuJson(book, pages, Path.Combine(clientpath, "component/subtitle/subtitle.txt"), size);

        }
        /// <summary>
        /// 导出一页素材
        /// </summary>
        /// <param name="curBook">当前书籍</param>
        /// <param name="page">当前书页</param>
        /// <param name="clinetSize">导出版本</param>
        /// <param name="cutWidth">背景剪切宽度</param>
        /// <param name="rate">缩放系数</param>
        /// <param name="imageFolder">图片文件夹</param>
        /// <param name="soundFolder">音频文件夹</param>
        /// <param name="movieFolder">视频文件夹</param>
        /// <param name="showProgress">是否显示解压文字</param>
        /// <param name="progress">进度条控件</param>
        private static void DoExportOnePage(List<ExportObject> exportObjs, Book curBook, Page page, int Index, ClientSize clinetSize, double cutWidth, double rate, string imageFolder, string soundFolder, string movieFolder, string ziMuFolder)
        {
            string sizePix = string.Empty;
            #region 背景图
            //背景图

            if (File.Exists(page.BgImagePath))
            {
                exportObjs.Add(new ExportObject()
                {
                    orgPath = page.BgImagePath,
                    destPath = Path.Combine(imageFolder, page.BgImageName + ".png"),
                    type = ExportImageType.页面背景,
                    cutWidth = cutWidth,
                    scale = rate,
                    width = PublishHelper.GetNewWidth(clinetSize),
                    height = PublishHelper.CanvasHeight,
                });
            }
            else if (string.IsNullOrEmpty(page.BgImagePath))
            {
                exportObjs.Add(new ExportObject()
                {
                    orgPath = string.Empty,
                    destPath = Path.Combine(imageFolder, page.BgImageName + ".png"),
                    type = ExportImageType.空图片,
                    width = clinetSize.Width * rate,
                    height = clinetSize.Height * rate,
                });
            }
            #endregion

            #region 背景音乐
            //背景音乐
            if (File.Exists(page.BgSoundPath))
            {
                //输出路径
                string destBgSoundPath = Path.Combine(soundFolder, page.BgSoundName + ".mp3");

                //导出音频
                if (pathAndImageName.ContainsKey(page.BgSoundPath))
                    exportObjs.Add(new ExportObject()
                    {
                        orgPath = page.BgSoundPath,
                        destPath = Path.Combine(soundFolder, page.BgSoundName + ".mp3"),
                        audioSize = curBook.Settings.AudioSize,
                        type = ExportImageType.音频
                    });
            }
            #endregion

            #region 切片集合过滤
            //过滤找不到图片文件的切片
            List<Sprite> allsprites = page.Sprites.Where(sp =>
            {
                return !(!sp.IsEnabled||
                sp._sourceImagePath.Contains("Stop_Red.png") ||
                (sp.GetType() == typeof(DrawingBoardSprite) && (sp as DrawingBoardSprite).IsDrawingBoard));
            }).ToList();
            //普通切片
            List<Sprite> sprites = allsprites.FindAll(sp => sp.buddyMovie == null && sp.GetType() != typeof(Particle));
            //粒子切片
            List<Sprite> particles = allsprites.FindAll(sp => sp.GetType() == typeof(Particle));
            //视频切片
            List<Sprite> movies = allsprites.FindAll(sp => sp.buddyMovie != null);

            #endregion

            #region 切片

            for (int i = 0; i < sprites.Count; i++)
            {
                Sprite onePiece = sprites[i];
                DoExportOneSprite(
                                     exportObjs: exportObjs,
                                     curBook: curBook,          //当前书籍
                                     onePiece: onePiece,        //当前切片
                                     clinetSize: clinetSize,    //导出版本
                                     cutWidth: cutWidth,        //背景剪切宽度
                                     rate: rate,                //缩放系数
                                     imageFolder: imageFolder,  //图片文件夹
                                     soundFolder: soundFolder,  //音频文件夹
                                     movieFolder: movieFolder  //视频文件夹
                                  );
            }
            #endregion

            #region  粒子效果
            if (particles != null)
            {
                particles.ForEach(sp =>
                {
                    exportObjs.Add(new ExportObject()
                    {
                        orgPath = sp._sourceImagePath,
                        destPath = Path.Combine(imageFolder, sp.ImgName + ".png"),
                        scale = rate,
                        type = ExportImageType.普通图片
                    });
                });
            }

            #endregion

            #region  视频
            if (movies != null)
            {
                movies.ForEach(sp =>
                {
                    exportObjs.Add(new ExportObject()
                    {
                        orgPath = sp.buddyMovie.VideoSourcePath,
                        destPath = Path.Combine(movieFolder, sp.buddyMovie.VideoName),
                        type = ExportImageType.直接拷贝
                    });
                });
            }
            #endregion

            #region 字幕
            if (page.ziMuSouce != null)
            {
                //导出字幕文字音
                if (File.Exists(page.BgWordSoundPath))
                {
                    exportObjs.Add(new ExportObject()
                    {
                        orgPath = page.BgWordSoundPath,
                        destPath = Path.Combine(ziMuFolder, "wordAudio" + (Index + 1) + ".mp3"),
                        audioSize = curBook.Settings.AudioSize,
                        type = ExportImageType.音频
                    });
                }
                if (page.ziMuSouce != null)
                {
                    var items = page.ziMuSouce.ZimuItems.Union(page.ziMuSouce.ZiMuFSItems).ToList();
                    DiWenModel dw = null;
                    for (int i = 0; i < items.Count; i++)
                    {
                        dw = items[i].DiWenSouce;
                        if (dw.IsDefault)
                        {
                            exportObjs.Add(new ExportObject()
                            {
                                destPath = Path.Combine(ziMuFolder, "dw" + (Index + 1) + "_" + (i + 1) + ".png"),
                                width = dw.WidthProp,
                                height = dw.HeightProp,
                                cornerRadius = dw.CornerRadius,
                                scale = rate,
                                color = dw.BackgroundColorProperty,
                                type = ExportImageType.圆角矩形,
                                IsFeather = dw.IsFeather
                            });
                        }
                        else
                        {
                            exportObjs.Add(new ExportObject()
                            {
                                orgPath = dw.FilePath,
                                destPath = Path.Combine(ziMuFolder, "dw" + (Index + 1) + "_" + (i + 1) + ".png"),
                                width = dw.WidthProp,
                                height = dw.HeightProp,
                                scale = rate,
                                type = ExportImageType.普通切片
                            });
                        }
                    }
                }

            }
            #endregion
        }

        /// <summary>
        /// 导出一个切片的素材(包含图片、音频、动画素材)
        /// </summary>
        /// <param name="curBook">当前书籍</param>
        /// <param name="page">当前切片</param>
        /// <param name="clinetSize">导出版本</param>
        /// <param name="cutWidth">背景剪切宽度</param>
        /// <param name="rate">缩放系数</param>
        /// <param name="imageFolder">图片文件夹</param>
        /// <param name="soundFolder">音频文件夹</param>
        /// <param name="movieFolder">视频文件夹</param>
        /// <param name="showProgress">是否显示解压文字</param>
        /// <param name="progress">进度条控件</param>
        private static void DoExportOneSprite(List<ExportObject> exportObjs, Book curBook, Sprite onePiece, ClientSize clinetSize, double cutWidth, double rate, string imageFolder, string soundFolder, string movieFolder)
        {
            if (onePiece.ImgName == string.Empty) return;
            string destFilePath = string.Empty;
            string destSoundPath = string.Empty;

            string sizePix = string.Empty;

            //切片
            if (onePiece.haveAnimation(AnimationStyle.帧动画))
            {
                exportObjs.Add(new ExportObject()
                {
                    orgPath = onePiece.SourceImagePath,
                    destPath = Path.Combine(imageFolder, onePiece.ImgName),
                    scale = rate,
                    IsHorizontalFlip = onePiece.IsHorizontalFlip,
                    IsVerticalFlip = onePiece.IsVerticalFlip,
                    type = ExportImageType.帧动画切片
                });
            }
            else if (onePiece.haveAnimation(AnimationStyle.骨骼动画))
            {
                exportObjs.Add(new ExportObject()
                {
                    orgPath = onePiece.TexPngPath,
                    destPath = Path.Combine(imageFolder, onePiece.ImgName + "_tex.png"),
                    scale = rate,
                    type = ExportImageType.骨骼动画切片
                });
            }
            else
            {

                destFilePath = Path.Combine(imageFolder, onePiece.ImgName + ".png");
                #region 切片图片
                if (string.IsNullOrEmpty(onePiece.SourceImagePath))
                {
                    exportObjs.Add(new ExportObject()
                    {
                        destPath = destFilePath,
                        width = onePiece.Width * rate,
                        height = onePiece.Height * rate,
                        type = ExportImageType.空图片
                    });
                }
                else
                {
                    sizePix = "_" + onePiece.Width + "_" + onePiece.Height;
                    if (pathAndImageName.ContainsKey(onePiece.SourceImagePath + sizePix) || onePiece.haveAnimation(AnimationStyle.点击替换))
                    {
                        exportObjs.Add(new ExportObject()
                        {
                            orgPath = onePiece.SourceImagePath,
                            destPath = destFilePath,
                            width = onePiece.Width,
                            height = onePiece.Height,
                            IsHorizontalFlip = onePiece.IsHorizontalFlip,
                            IsVerticalFlip = onePiece.IsVerticalFlip,
                            scale = rate,
                            type = ExportImageType.普通切片
                        });
                    }
                    // 如果是游戏连线游戏 要导出两张切片
                    if (onePiece.GetType() == typeof(OrderLineSprite))
                    {
                        OrderLineSprite ols = onePiece as OrderLineSprite;
                        if (ols != null && ols.IsGameSprite)
                        {
                            destFilePath = Path.Combine(imageFolder, onePiece.ImgName + "_t.png");

                            if (pathAndImageName.ContainsKey(ols.TouchedImagePath + sizePix))
                            {
                                exportObjs.Add(new ExportObject()
                                {
                                    orgPath = ols.TouchedImagePath,
                                    destPath = destFilePath,
                                    width = onePiece.Width,
                                    height = onePiece.Height,
                                    scale = rate,
                                    type = ExportImageType.普通切片
                                });
                            }
                            else
                            {
                                exportObjs.Add(new ExportObject()
                                {
                                    destPath = destFilePath,
                                    width = onePiece.Width * rate,
                                    height = onePiece.Height * rate,
                                    type = ExportImageType.空图片
                                });
                            }
                        }
                    }

                }
                #endregion
            }

            //声音
            if (File.Exists(onePiece.SoundSourcePath))
            {
                if (pathAndImageName.ContainsKey(onePiece.SoundSourcePath))
                {
                    exportObjs.Add(new ExportObject()
                    {
                        orgPath = onePiece.SoundSourcePath,
                        destPath = Path.Combine(soundFolder, onePiece.SoundName + ".mp3"),
                        audioSize = curBook.Settings.AudioSize,
                        type = ExportImageType.音频
                    });
                }
            }

            //动画导出
            if (onePiece.AnimationGroupSet != null)
            {
                //切换动画素材导出
                if (onePiece.haveAnimation(AnimationStyle.点击替换))
                {
                    DoExportRepalceAnims(
                                            exportObjs: exportObjs,
                                            curBook: curBook,          //当前书籍
                                            onePiece: onePiece,        //当前切片
                                            rate: rate,                //缩放系数
                                            imageFolder: imageFolder,  //图片文件夹
                                            soundFolder: soundFolder  //音频文件夹
                                         );
                }

            }
        }
        /// <summary>
        /// 导出切换动画素材
        /// </summary>
        /// <param name="curBook">当前书籍</param>
        /// <param name="page">当前切片</param>
        /// <param name="rate">缩放系数</param>
        /// <param name="imageFolder">图片文件夹</param>
        /// <param name="soundFolder">音频文件夹</param>
        /// <param name="showProgress">是否显示解压文字</param>
        /// <param name="progress">进度条控件</param>
        public static void DoExportRepalceAnims(List<ExportObject> exportObjs, Book curBook, Sprite onePiece, double rate, string imageFolder, string soundFolder)
        {
            string destFilePath = string.Empty;
            string destSoundPath = string.Empty;
            string tempOutPath = string.Empty;
            onePiece.GetReplaceAnims().ForEach(anim =>
            {
                destFilePath = Path.Combine(imageFolder, onePiece.ImgName);
                destSoundPath = Path.Combine(soundFolder, onePiece.SoundName);
                #region 替换动画 使用切片素材作为第一帧
                //替换动画 使用切片素材作为第一帧
                if (File.Exists(onePiece.SourceImagePath))
                {
                    exportObjs.Add(new ExportObject()
                    {
                        orgPath = onePiece.SourceImagePath,
                        destPath = destFilePath + "_1.png",
                        width = onePiece.Width,
                        height = onePiece.Height,
                        scale = rate,
                        type = ExportImageType.普通切片
                    });
                }
                if (File.Exists(onePiece.SoundSourcePath))
                {
                    exportObjs.Add(new ExportObject()
                    {
                        orgPath = onePiece.SoundSourcePath,
                        destPath = destSoundPath + "_1.mp3",
                        audioSize = curBook.Settings.AudioSize,
                        type = ExportImageType.音频
                    });

                }
                #endregion

                #region 替换动画素材列表 作为第一帧以后的素材
                //替换动画素材列表 作为第一帧以后的素材
                var list = (anim as ReplaceAnimation).ImgAudioList;
                if (list != null)
                {
                    for (int j = 0; j < list.Count; j++)
                    {
                        ImageAudioObj obj = list[j];
                        if (File.Exists(obj.ImagePath)) //图片
                        {
                            exportObjs.Add(new ExportObject()
                            {
                                orgPath = obj.ImagePath,
                                destPath = destFilePath + "_" + (j + 2) + ".png",
                                width = onePiece.Width,
                                height = onePiece.Height,
                                scale = rate,
                                type = ExportImageType.普通切片
                            });
                        }
                        if (File.Exists(obj.AudioPath))//音频
                        {

                            exportObjs.Add(new ExportObject()
                            {
                                orgPath = obj.AudioPath,
                                destPath = destSoundPath + "_" + (j + 2) + ".mp3",
                                audioSize = curBook.Settings.AudioSize,
                                type = ExportImageType.音频
                            });

                        }
                    }
                }
                #endregion
            });
        }


        public static void ExportSource(List<ExportObject> exportObjs, bool showProgress, ProgressControl progress, bool compress = true)
        {
            Task[] tasks = new Task[4];

            //普通切片
            tasks[0] = Task.Factory.StartNew(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                var imgs = exportObjs.FindAll(obj => obj.type == ExportImageType.普通切片);
                if (imgs != null && imgs.Count > 0)
                {
                    foreach (var img in imgs)
                    {
                        if (showProgress) progress.ShowFileName(img.destPath);

                        ExportImage(img.orgPath, img.destPath, img.width, img.height, img.scale, compress);
                        if (img.IsHorizontalFlip || img.IsVerticalFlip)
                        {
                            Util.ScaleImage((BitmapSource)Util.Snap2DeviceDpi(img.destPath), img.destPath, img.IsHorizontalFlip ? -1 : 1, img.IsVerticalFlip ? -1 : 1, img.width * 0.5, img.height * 0.5);
                        }
                    }
                    imgs.Clear();
                }

                Console.WriteLine("普通切片用时" + (sw.ElapsedMilliseconds * 0.001) + "秒");
                sw.Restart();

                var imgsStd = exportObjs.FindAll(obj => obj.type == ExportImageType.普通图片);
                if (imgsStd != null && imgsStd.Count > 0)
                {
                    foreach (var img in imgsStd)
                    {
                        if (showProgress) progress.ShowFileName(img.destPath);
                        ExportImage(img.orgPath, img.destPath, img.scale, false);
                    }
                    imgsStd.Clear();
                }

                Console.WriteLine("普通图片用时" + (sw.ElapsedMilliseconds * 0.001) + "秒");
                sw.Restart();

                var objs = exportObjs.FindAll(obj => obj.type == ExportImageType.直接拷贝);
                if (objs != null && objs.Count > 0)
                {
                    foreach (var obj in objs)
                    {
                        if (showProgress) progress.ShowFileName(obj.destPath);
                        File.Copy(obj.orgPath, obj.destPath, true);
                    }
                    objs.Clear();
                }
                Console.WriteLine("直接复制用时" + (sw.ElapsedMilliseconds * 0.001) + "秒");
                sw.Restart();

                var emptys = exportObjs.FindAll(obj => obj.type == ExportImageType.空图片);
                if (emptys != null && emptys.Count > 0)
                {
                    foreach (var empty in emptys)
                    {
                        Util.CreateEmptyPngFile(empty.destPath, (int)(Math.Round(empty.width, 0, MidpointRounding.AwayFromZero)), (int)(Math.Round(empty.height, 0, MidpointRounding.AwayFromZero)), ExportObject.DPI);
                    }
                    emptys.Clear();
                }

                Console.WriteLine("空切片用时" + (sw.ElapsedMilliseconds * 0.001) + "秒");
                sw.Restart();

                var bgs = exportObjs.FindAll(obj => obj.type == ExportImageType.页面背景);
                if (bgs != null && bgs.Count > 0)
                {
                    foreach (var bg in bgs)
                    {
                        if (showProgress) progress.ShowFileName(bg.destPath);
                        ExportBg(bg.orgPath, bg.destPath, bg.width, bg.height, bg.scale, bg.cutWidth);
                    }
                    bgs.Clear();
                }

                Console.WriteLine("背景用时" + (sw.ElapsedMilliseconds * 0.001) + "秒");
                sw.Stop();

                var rects = exportObjs.FindAll(obj => obj.type == ExportImageType.圆角矩形);
                if (rects != null && rects.Count > 0)
                {
                    foreach (var rect in rects)
                    {
                        if (showProgress) progress.ShowFileName(rect.destPath);
                        //var img = ImageHelper.GetFeatherRectangle(rect.width * rect.scale, rect.height * rect.scale, rect.cornerRadius * rect.scale, rect.color, (int)(Math.Min(rect.width,rect.height)*rect.scale*0.3));
                        //Util.SaveImage(rect.destPath,img);
                        // Util.CreateRectangleImage(rect.width, rect.height, rect.cornerRadius, rect.color, rect.scale, rect.destPath);
                        if (rect.IsFeather)
                        {
                            var img = ImageHelper.GetFeatherRectangle(rect.width * rect.scale, rect.height * rect.scale, rect.cornerRadius * rect.scale, new SolidColorBrush(rect.color));
                            Util.SaveImage(rect.destPath, img);
                        }
                        else
                            Util.CreateRectangleImage(rect.width, rect.height, rect.cornerRadius, rect.color, rect.scale, rect.destPath);
                    }
                    rects.Clear();
                }
            });
            //音频
            tasks[1] = Task.Factory.StartNew(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                var audios = exportObjs.FindAll(obj => obj.type == ExportImageType.音频);
                if (audios != null && audios.Count > 0)
                {
                    foreach (var audio in audios)
                    {
                        if (showProgress) progress.ShowFileName(audio.destPath);

                        ExportAudio(audio.orgPath, audio.destPath, audio.audioSize, compress);
                    }
                    audios.Clear();
                }

                Console.WriteLine("音频导出用时" + (sw.ElapsedMilliseconds * 0.001) + "秒");
                sw.Stop();

            });
            //帧动画
            tasks[2] = Task.Factory.StartNew(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                var frames = exportObjs.FindAll(obj => obj.type == ExportImageType.帧动画切片);
                if (frames != null && frames.Count > 0)
                {
                    foreach (var frame in frames)
                    {
                        if (showProgress) progress.ShowFileName(frame.destPath);
                        ExportFrameSprite(frame.orgPath, frame.destPath, frame.scale, frame.IsHorizontalFlip, frame.IsVerticalFlip);

                    }
                    frames.Clear();
                }
                Console.WriteLine("帧动画导出用时" + (sw.ElapsedMilliseconds * 0.001) + "秒");
                sw.Stop();

            });
            //骨骼动画
            tasks[3] = Task.Factory.StartNew(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                var bones = exportObjs.FindAll(obj => obj.type == ExportImageType.骨骼动画切片);
                if (bones != null && bones.Count > 0)
                {
                    foreach (var bone in bones)
                    {
                        if (showProgress) progress.ShowFileName(bone.destPath);
                        ExportBoneSprite(bone.orgPath, bone.destPath, bone.scale);
                    }
                    bones.Clear();
                }
                Console.WriteLine("骨骼动画导出用时" + (sw.ElapsedMilliseconds * 0.001) + "秒");
                sw.Stop();

            });

            Task.WaitAll(tasks);
        }


        #endregion

        /// <summary>
        /// 释放内存
        /// </summary>
        /// <param name="curBook"></param>
        /// <param name="SavePath"></param>
        /// <param name="clinetSize"></param>
        /// <param name="typePix"></param>
        /// <param name="startPageIndex"></param>
        /// <param name="endIndex"></param>
        private static void Dispose()
        {
            //释放压缩图片线程
            if (ImageCompressPro != null)
            {
                ImageCompressPro.Close();
                ImageCompressPro.Dispose();
                ImageCompressPro = null;
            }
            if (ExportAudioProc != null)
            {
                ExportAudioProc.Close();
                ExportAudioProc.Dispose();
                ExportAudioProc = null;
            }
            Util.ClearMemory();
        }

        public static void ExportImage(string destImgPath, BitmapSource imgSource, double rate, bool isCompress = true)
        {

            Util.ScaleRateImage(imgSource, destImgPath, rate);
            if (isCompress) CompressPng(destImgPath);
        }
        public static void ExportImage(string srcImgPath, string destImgPath, double width, double height, double rate, bool isCompress = true)
        {
            if (File.Exists(srcImgPath))
            {
                Util.ScaleRateImage(srcImgPath, destImgPath, width, height, rate);
                if (isCompress) CompressPng(destImgPath);
            }
        }
        public static void ExportBg(string srcImgPath, string destImgPath, double _width, double _height, double rate, double cutWidth)
        {
            Util.ScaleRateImage(srcImgPath, destImgPath, rate);
            //return;
            //if (File.Exists(srcImgPath))
            //{
            //    var bitmap = System.Drawing.Image.FromFile(srcImgPath);
            //    if (cutWidth > 0)
            //    {
            //        int x = (int)Math.Round(cutWidth, 0, MidpointRounding.AwayFromZero);
            //        int y = 0;
            //        int width = (int)Math.Round(_width, 0, MidpointRounding.AwayFromZero);
            //        int height = (int)Math.Round(_height, 0, MidpointRounding.AwayFromZero);
            //        bitmap = ImageHelper.CutImage(bitmap, x, y, width, height);
            //    }

            //    if (bitmap != null)
            //    {
            //        string sizePix = "_2730_1536";
            //        if (pathAndImageName.ContainsKey(srcImgPath + sizePix))
            //        {
            //            ImageHelper.GetPicThumbnail(bitmap, destImgPath, 60, rate);
            //        }
            //        bitmap.Dispose();
            //        bitmap = null;
            //    }
            //}
        }
        public static void CopyImageComp(string path, string newpath, bool doComp)
        {
            if (!File.Exists(path)) return;
            if (Path.GetExtension(path).ToLower() == ".png")
            {
                File.Copy(path, newpath, true);
                if (doComp) CompressPng(newpath);
            }
            else if (Path.GetExtension(path).ToLower() == ".jpg")
            {
                if (doComp)
                    ImageHelper.GetPicThumbnail(path, newpath, 20);
                else
                    File.Copy(path, newpath, true);
            }
        }
        public static void CopyFile(string path, string newpath, bool flag = true)
        {
            if (!File.Exists(path)) return;
            File.Copy(path, newpath, flag);
        }
        /// <summary>
        /// 压缩图片
        /// </summary>
        /// <param name="DirDirectoryPath"></param>
        public static void CompressPng(string filePath)
        {

            if (ImageCompressPro == null) ImageCompressPro = new Process();
            ImageCompressPro.StartInfo = new ProcessStartInfo();
            ImageCompressPro.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            ImageCompressPro.StartInfo.FileName = pngquantEXE;
            ImageCompressPro.StartInfo.Arguments = "--force --verbose --skip-if-larger --speed=1  --ext=.png 256 " + "\"" + filePath + "\"";
            ImageCompressPro.Start();
            ImageCompressPro.WaitForExit();
        }
        private static void ExportAudio(AudioSize type, string orgPath, string newPath)
        {
            if (System.IO.File.GetAttributes(orgPath).ToString().IndexOf("ReadOnly") != -1)
            {
                File.SetAttributes(orgPath, FileAttributes.Normal);
            }
            string lameArgs = string.Empty;
            switch (type)
            {
                case AudioSize.原码率:
                    File.Copy(orgPath, newPath, true);
                    return;
                case AudioSize.Bit32:
                    lameArgs = "-b 32";
                    break;
                case AudioSize.Bit64:
                    lameArgs = "-b 64";
                    break;
                case AudioSize.Bit128:
                    lameArgs = "-b 128";
                    break;
                default:
                    break;
            }


            if (ExportAudioProc == null) ExportAudioProc = new Process();

            ExportAudioProc.StartInfo = new ProcessStartInfo();
            ExportAudioProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            ExportAudioProc.StartInfo.FileName = lameEXE;
            ExportAudioProc.StartInfo.Arguments = string.Format(
                "{0} \"{1}\"  \"{2}\"",
                lameArgs,
                orgPath,
                newPath);

            ExportAudioProc.Start();
            ExportAudioProc.WaitForExit();
        }
        public static void ExportAudio(string orgPath, string newPath, AudioSize type, bool compress = true)
        {
            if (File.Exists(orgPath))
            {
                if (!compress)
                {
                    File.Copy(orgPath, newPath, true);
                    return;
                }
                if (audioPathVisited.ContainsKey(orgPath))
                {
                    if (File.Exists(audioPathVisited[orgPath]))
                    {
                        File.Copy(audioPathVisited[orgPath], newPath, true);
                        return;
                    }
                }
                else
                {
                    audioPathVisited.Add(orgPath, newPath);
                }
                ExportAudio(type, orgPath, newPath);
            }
        }



        /// <summary>
        /// 内部按页码进行导出压缩包
        /// </summary>
        /// <param name="SavePath"></param>
        /// <param name="currBook"></param>
        /// <param name="clinetSize"></param>
        /// <param name="typePix"></param>
        /// <param name="startindex"></param>
        /// <param name="endIndex"></param>
        private static void DoExportByCurrPage(string SavePath, Book currBook, ClientSize clinetSize, string typePix = "", int startindex = 0, int endIndex = -1, bool compress = true)
        {
            if (endIndex == -1) endIndex = currBook.Pages.Count;
            GerPathAndNameMapping(currBook, SavePath, clinetSize, startindex, endIndex);
            DoExportAllFiles(currBook, SavePath, clinetSize, typePix, startindex, endIndex, compress);

        }
        public static string TempPath
        {
            get
            {
                return Path.GetTempPath();
                // return AppDomain.CurrentDomain.BaseDirectory;
            }
        }
        /// <summary>
        /// 外部按页码进行导出压缩包
        /// </summary>
        /// <param name="currBook"></param>
        /// <param name="clinetSize"></param>
        /// <param name="gridWindownContain"></param>
        /// <param name="callback"></param>
        /// <param name="typePix"></param>
        /// <param name="startindex"></param>
        /// <param name="endIndex"></param>
        public static void DoExportByCurrPageAnys(Book currBook, ClientSize clinetSize, Grid gridWindownContain, Action callback = null, string typePix = "", int startindex = 0, int endIndex = -1, bool compress = true)
        {
            if (IsExporting == true)
            {
                return;
            }
            ThreadPool.QueueUserWorkItem(delegate
           {
               IsExporting = true;
               //删除临时文件夹
               string SavePath = Path.Combine(TempPath, "ellabook");

               previewBookLoading loading = null;

               audioPathVisited.Clear();
               //---------------添加进度条-------------------------------------------------------
               if (gridWindownContain != null)
               {
                   MediaTypeNames.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                   {
                       if (loading == null) loading = new previewBookLoading();
                       gridWindownContain.Children.Add(loading);

                   }));
               }
               if (Directory.Exists(SavePath))
               {
                   try
                   {
                       Directory.Delete(SavePath, true);
                   }
                   catch (Exception e)
                   {
                       MediaTypeNames.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                       {
                           if (gridWindownContain != null)
                           {

                               MessageBox.Show(e.ToString());
                               if (loading != null)
                               {
                                   gridWindownContain.Children.Remove(loading);
                                   loading.StopAnim();
                                   loading = null;
                               }

                           }
                       }));
                       IsExporting = false;
                       return;
                   }

               }
               currBook.Settings.SaveExportDir = true;
               //---------------------------------------------------------------------------------
               if (endIndex == -1) endIndex = currBook.Pages.Count;

               GerPathAndNameMapping(currBook, SavePath, clinetSize, startindex, endIndex);
               DoExportAllFiles(currBook, SavePath, clinetSize, typePix, startindex, endIndex, compress);
               //---------------移除进度条-------------------------------------------------------
               if (gridWindownContain != null)
               {
                   MediaTypeNames.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                   {
                       if (loading != null)
                       {
                           gridWindownContain.Children.Remove(loading);
                           loading.StopAnim();
                           loading = null;
                       }
                   }));
               }
               IsExporting = false;
               //---------------------------------------------------------------------------------
               if (callback != null)
                   callback();

           });
        }
        /// <summary>
        /// 书籍资源去重复
        /// </summary>
        /// <param name="curBook"></param>
        /// <param name="SavePath"></param>
        /// <param name="clinetSize"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        private static void GerPathAndNameMapping(Book curBook, string SavePath, ClientSize clinetSize, int startIndex, int endIndex)
        {

            pathAndImageName.Clear();
            string jsonSavePath = SavePath + "\\" + clinetSize.ClinetName + "\\json.txt";
            string defaultFolder = Path.GetDirectoryName(jsonSavePath);
            double rate = PublishHelper.GetRate(clinetSize);
            double cutWidth = PublishHelper.GetBlankWidth(clinetSize);
            //------------------------------------------------------------------------------------------------------
            ClientSize jsonSize = PublishHelper.GetPublishClinet(clinetSize);
            string prefixName = string.Empty;
            if (string.IsNullOrEmpty(curBook.PrefixName))
                prefixName = "b1_";
            //------------------------------------------------------------------------------------------------------
            //切片图
            //先建文件夹
            string imageFolder = defaultFolder + "\\image";
            string soundFolder = defaultFolder + "\\sound";
            string movieFolder = defaultFolder + "\\movie";

            List<Page> pages = new List<Page>();
            pages.AddRange(curBook.Pages.ToArray());
            string sizePix = string.Empty;
            for (int pageIndex = startIndex; pageIndex < endIndex; pageIndex++)
            {
                Page page = pages[pageIndex];
                sizePix = "_2730_1536";
                //背景图
                string destBgPicPath = imageFolder + "\\" + prefixName + page.BgImageName + ".png";
                if (!string.IsNullOrEmpty(page.BgImagePath) && File.Exists(page.BgImagePath))
                {
                    if (!pathAndImageName.ContainsKey(page.BgImagePath + sizePix))
                    {
                        pathAndImageName.Add(page.BgImagePath + sizePix, destBgPicPath);
                    }
                }
                if (!string.IsNullOrEmpty(page.BgSoundPath) && File.Exists(page.BgSoundPath))
                {
                    string destBgSoundPath = soundFolder + "\\" + prefixName + page.BgSoundName + ".mp3";
                    if (!pathAndImageName.ContainsKey(page.BgSoundPath))
                    {
                        pathAndImageName.Add(page.BgSoundPath, destBgSoundPath);
                    }
                }
                List<Sprite> normalSprites = new List<Sprite>();
                List<Particle> particles = new List<Particle>();
                List<Sprite> movies = new List<Sprite>();
                for (int i = 0; i < page.Sprites.Count; i++)
                {
                    Sprite onePiece = page.Sprites[i];
                    if (onePiece.GetType() == typeof(Particle))
                        particles.Add(onePiece as Particle);
                    else
                        if (onePiece.buddyMovie != null)
                    {
                        movies.Add(onePiece);
                    }
                    else if (!string.IsNullOrEmpty(onePiece._sourceImagePath) && onePiece._sourceImagePath.Contains("Stop_Red.png"))
                    { }
                    else
                        normalSprites.Add(onePiece);
                }
                #region 切片
                for (int i = 0; i < page.Sprites.Count; i++)
                {
                    Sprite onePiece = page.Sprites[i];
                    if (onePiece.ImgName == string.Empty) continue;
                    string destFilePath = string.Empty;
                    string destSoundPath = string.Empty;
                    //----------------------------------------------------------------------------------------
                    //切片
                    bool haveFrameAnim = false;

                    //判断是否包含帧动画
                    FrameAnimation frameAnim = null;
                    if (onePiece.AnimationGroupSet.Count != 0)
                    {
                        for (int j = 0; j < onePiece.AnimationGroupSet.Count; j++)
                        {
                            foreach (AnimationGroup group in onePiece.AnimationGroupSet)
                            {
                                foreach (Animation anim in group.AnimationSet)
                                {
                                    if (anim.GetType() == typeof(FrameAnimation))
                                    {
                                        haveFrameAnim = true;
                                        frameAnim = (FrameAnimation)anim;
                                        break;
                                    }
                                }
                                if (haveFrameAnim == true)
                                    break;
                            }
                            if (haveFrameAnim == true)
                                break;
                        }
                    }

                    //如果是帧动画的素材文件，导出总切片和单一切片素材和plist文件
                    if (!haveFrameAnim)
                    {
                        destFilePath = imageFolder + "\\" + prefixName + onePiece.ImgName + ".png";
                        if (!string.IsNullOrEmpty(onePiece.SourceImagePath))
                        {
                            sizePix = "_" + onePiece.Width + "_" + onePiece.Height;
                            BitmapSource curImgsrc = Util.Snap2DeviceDpi(onePiece.SourceImagePath);
                            if (curImgsrc != null && !pathAndImageName.ContainsKey(onePiece.SourceImagePath + sizePix))
                            {
                                pathAndImageName.Add(onePiece.SourceImagePath + sizePix, destFilePath);
                            }
                            #region 如果是游戏连线游戏 要导出两张切片
                            if (onePiece.GetType() == typeof(OrderLineSprite))
                            {
                                OrderLineSprite ols = onePiece as OrderLineSprite;
                                if (ols != null && ols.IsGameSprite)
                                {
                                    destFilePath = imageFolder + "\\" + prefixName + onePiece.ImgName + "_t.png";
                                    if (File.Exists(ols.TouchedImagePath))
                                    {
                                        if (!pathAndImageName.ContainsKey(ols.TouchedImagePath + sizePix))
                                        {
                                            pathAndImageName.Add(ols.TouchedImagePath + sizePix, destFilePath);
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    if (!string.IsNullOrEmpty(onePiece.SoundSourcePath) || File.Exists(onePiece.SoundSourcePath))
                    {
                        destSoundPath = soundFolder + "\\" + prefixName + onePiece.SoundName + ".mp3";
                        if (!string.IsNullOrEmpty(onePiece.SoundSourcePath) && !pathAndImageName.ContainsKey(onePiece.SoundSourcePath))
                        {

                            pathAndImageName.Add(onePiece.SoundSourcePath, destSoundPath);

                        }
                    }
                }
                #endregion
            }
            Util.ClearMemory();
        }

        /// <summary>
        /// 导出帧动画
        /// </summary>
        /// <param name="curBook"></param>
        /// <param name="onePiece"></param>
        /// <param name="imageFolder"></param>
        /// <param name="destResolutionX"></param>
        /// <param name="clinetSize"></param>
        /// <param name="rate"></param>
        private static void ExportFrameSprite(string orgPath, string descPath, double rate, bool IsHorizontalFlip, bool IsVerticalFlip)
        {
            ScaleFrameAnimation(orgPath, descPath, rate, IsHorizontalFlip, IsVerticalFlip, Path.GetFileName(descPath));
            //return;
            //BitmapSource bigBitImage = Util.Snap2DeviceDpi(orgPath);

            ////-----------------------------------------------------------------------------------------
            //string destFilePath = descPath + "_ani.png";
            //string destPlistFilePath = descPath + "_ani.plist";
            //string destFirstPath = descPath + "_0000.png";
            //string orgPlistPath = orgPath.Replace(".png", ".plist");
            ////ExportImage(onePiece.SourceImagePath,destFilePath,rate);
            ////-----------------------------------------------------------------------------------------
            ////导出总切片与plist文件

            //int FlipScaleX = IsHorizontalFlip ? -1 : 1;
            //int FlipScaleY = IsVerticalFlip ? -1 : 1;
            //var list = Util.ParseFramePlistFile(orgPlistPath);
            //List<BitmapSource> bitmapList = new List<BitmapSource>();

            //if (list.Count > 0)
            //{

            //    BitmapSource bitmap = null;
            //    TransformedBitmap transBitmap = null;
            //    int[] rectCoord = null;
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        rectCoord = (int[])list[i];
            //        bitmap = Util.CutImage(bigBitImage, rectCoord[0], rectCoord[1], rectCoord[2], rectCoord[3]);
            //        if (i == 0)
            //        {
            //            if (IsHorizontalFlip || IsVerticalFlip)
            //            {
            //                ExportImage(destFirstPath, Sprite.FlipSource(bitmap, IsHorizontalFlip, IsVerticalFlip), rate);
            //            }
            //            else
            //            {
            //                ExportImage(destFirstPath, bitmap, rate);
            //            }
            //        }
            //        if (bitmap != null)
            //        {
            //            transBitmap = new TransformedBitmap();
            //            transBitmap.BeginInit();
            //            transBitmap.Source = bitmap;
            //            transBitmap.Transform = new ScaleTransform(rate * FlipScaleX, rate * FlipScaleY, 0, 0);
            //            transBitmap.EndInit();
            //            bitmap = transBitmap;
            //        }

            //        bitmapList.Add(bitmap);
            //    }
            //    list.Clear();
            //    list = null;
            //    int ColsCount = 0;
            //    foreach (int[] item in Util.ParseFramePlistFile(orgPlistPath))
            //    {
            //        if (item[1] != 0) break;
            //        ColsCount++;
            //    }
            //    SaveFrameAnimation(destFilePath, Path.GetFileName(descPath), bitmapList, ColsCount);
            //    bitmapList.Clear();
            //    bitmapList = null;

            //}

            //  Util.ClearMemory();
        }
        public static readonly int FrameSplitWidth = 0;

        /// <summary>
        /// 导出骨骼动画
        /// </summary>
        /// <param name="orgPath"></param>
        /// <param name="destFilePath"></param>
        /// <param name="rate"></param>
        private static void ExportBoneSprite(string orgPath, string destFilePath, double rate)
        {
            //拷贝大图
            File.Copy(orgPath, destFilePath);
            //导出切图Json
            string texJsonPath = orgPath.Replace(".png", ".json");
            string skeJsonPath = orgPath.Replace("_tex.png", "_ske.json");
            if (!File.Exists(texJsonPath) || !File.Exists(skeJsonPath)) return;
            dynamic boneObj;
            dynamic ImgObj;
            string tempStr = string.Empty;
            #region  写入图片Json
            using (FileStream fs = new FileStream(texJsonPath, FileMode.Open))
            {

                using (StreamReader sr = new StreamReader(fs, new System.Text.UTF8Encoding(false)))
                {
                    tempStr = sr.ReadToEnd();
                }
                ImgObj = NewtonsoftHelper.DeserializeObject<dynamic>(tempStr);
                ImgObj.name = Path.GetFileNameWithoutExtension(destFilePath);
                ImgObj.imagePath = Path.GetFileName(destFilePath);
                tempStr = NewtonsoftHelper.JsonSerializer(ImgObj);

                using (FileStream fsWrite = new FileStream(destFilePath.Replace(".png", ".json"), FileMode.OpenOrCreate))
                {
                    StreamWriter sw = new StreamWriter(fsWrite);
                    //开始写入
                    sw.Write(tempStr);
                    //清空缓冲区
                    sw.Flush();
                    //关闭流
                    sw.Close();
                }

            }
            #endregion
            #region 写入骨骼Json
            using (FileStream fs = new FileStream(skeJsonPath, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs, new System.Text.UTF8Encoding(false)))
                {
                    tempStr = sr.ReadToEnd();
                }
                #region 修改骨骼Json
                boneObj = NewtonsoftHelper.DeserializeObject<dynamic>(tempStr);
                boneObj.name = Path.GetFileNameWithoutExtension(destFilePath);
                //  double tempCut = PublishHelper.GetBlankWidth(clinetSize);
                int index = 0;
                foreach (var arma in boneObj.armature)
                {
                    arma.name = Path.GetFileNameWithoutExtension(destFilePath) + "_arm" + index;
                    index++;
                    foreach (var bone in arma.bone)
                    {
                        if (bone.parent == null)
                        {
                            if (bone.transform != null)
                            {
                                //缩放
                                bone.transform.scX = bone.transform.scX == null ? rate : (double)bone.transform.scX.Value * rate;
                                bone.transform.scY = bone.transform.scY == null ? rate : (double)bone.transform.scY.Value * rate;
                                double aabbWidth = arma.aabb == null || arma.aabb.width == null ? 0 : arma.aabb.width.Value;
                                double aabbHeight = arma.aabb == null || arma.aabb.height == null ? 0 : arma.aabb.height.Value;
                                double aabbX = arma.aabb == null || arma.aabb.x == null ? 0 : arma.aabb.x.Value;
                                double aabbY = arma.aabb == null || arma.aabb.y == null ? 0 : arma.aabb.y.Value;
                                double transformX = bone.transform.x == null ? 0 : bone.transform.x.Value;
                                double transformY = bone.transform.y == null ? 0 : bone.transform.y.Value;
                                //将中心点手动换算到切片中心
                                double transX = (0.5 - (aabbX * -1 / aabbWidth)) * aabbWidth * -1 * rate;
                                bone.transform.x = bone.transform.x == null ? transX : (double)transformX * rate + transX;
                                double transY = (0.5 - (aabbY * -1 / aabbHeight)) * aabbHeight * -1 * rate;
                                bone.transform.y = bone.transform.y == null ? transY : (double)transformY * rate + transY;
                            }
                        }
                    }
                }

                #endregion
                tempStr = NewtonsoftHelper.JsonSerializer(boneObj);
                using (FileStream fsWrite = new FileStream(destFilePath.Replace("_tex.png", "_ske.json"), FileMode.OpenOrCreate))
                {
                    StreamWriter sw = new StreamWriter(fsWrite);
                    //开始写入
                    sw.Write(tempStr);
                    //清空缓冲区
                    sw.Flush();
                    //关闭流
                    sw.Close();
                }

            }
            #endregion
            tempStr = null;
        }

        /// <summary>
        /// 生成帧动画文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="pixName"></param>
        /// <param name="Frames"></param>
        private static void SaveFrameAnimation(string path, string pixName, List<BitmapSource> Frames, int ColsCount)
        {
            //-----------行列计算-------------------------------------------------------------------------
            int width = (int)(Frames[0].PixelWidth);
            int height = (int)(Frames[0].PixelHeight);
            int count = Frames.Count;
            if (Frames.Count < ColsCount) ColsCount = count;
            //int maxRowCount = Convert.ToInt32(Math.Floor(4060.0 / (height + ExportHelper.FrameSplitWidth)));
            //计算行数
            int rowCount = count % ColsCount == 0 ? (count / ColsCount) : (count / ColsCount) + 1;
            //--------------------------------------------------------------------------------------------

            ImageHelper.MergeImages(Frames, path, ColsCount, width, height);
            PlistHelper.AppendFramePlistFile(count, ColsCount, rowCount, width, height, pixName, path.Replace(".png", ".plist"));
        }
        /// <summary>
        /// 缩放帧动画
        /// </summary>
        public static void ScaleFrameAnimation(string pngPath, string destPath, double scale, bool IsHorizontalFlip, bool IsVerticalFlip, string pix)
        {
            int FlipScaleX = IsHorizontalFlip ? -1 : 1;
            int FlipScaleY = IsVerticalFlip ? -1 : 1;
            string destFilePath = destPath + "_ani.png";
            string destPlistFilePath = destPath + "_ani.plist";
            string destFirstPath = destPath + "_0000.png";
            var list = Util.ParseFramePlistFile(pngPath.ToPlistPath());
            var indexList = new List<int>();
            var distinctList = list.Distinct(new IntListDistinct()).ToList();

            int pixWidth = (int)Math.Ceiling(distinctList[0][2] * scale);
            int pixHeight = (int)Math.Ceiling(distinctList[0][3] * scale);
            int rowCount = 1;
            int colCount = distinctList.Count;
            //多行更新行数列数
            if (distinctList.Count * pixWidth > 4096)
            {
                colCount = (int)Math.Floor(4096 / (double)pixWidth);
                rowCount = (int)Math.Ceiling(distinctList.Count / (double)colCount);
            }
            //按位置给小图排序
            distinctList.Sort((item1, item2) => { return item1[0] + item1[1] * colCount - item2[0] - item2[1] * colCount; });
            //生成小图的动画索引
            list.ForEach(item =>
            {
                indexList.Add(distinctList.FindIndex(disItem => disItem[0] == item[0] && disItem[1] == item[1]));
            });
            #region 图片处理
            var bigBitImage = Util.Snap2DeviceDpi(pngPath);
            var bitmapList = new List<BitmapSource>();
            for (int i = 0; i < distinctList.Count; i++)
            {
                var bitmap = Util.CutImage(bigBitImage, distinctList[i]);
                if (i == 0)
                {
                    if (IsHorizontalFlip || IsVerticalFlip)
                    {
                        ExportImage(destFirstPath, Sprite.FlipSource(bitmap, IsHorizontalFlip, IsVerticalFlip), scale);
                    }
                    else
                    {
                        ExportImage(destFirstPath, bitmap, scale);
                    }
                }
                if (bitmap != null)
                {
                    TransformedBitmap transBitmap = new TransformedBitmap();
                    transBitmap.BeginInit();
                    transBitmap.Source = bitmap;
                    transBitmap.Transform = new ScaleTransform(scale * FlipScaleX, scale * FlipScaleY, 0, 0);
                    transBitmap.EndInit();
                    bitmap = transBitmap;
                }

                bitmapList.Add(bitmap);
            }
            ImageHelper.MergeImages(bitmapList, destFilePath, colCount, pixWidth, pixHeight);
            #endregion

            #region  生成plist文件
            //加载空模板
            XmlDocument doc = new XmlDocument();
            string strxml = @"<?xml version=""1.0"" encoding=""UTF-8""?><plist version=""1.0""><dict> <key>frames</key><dict> </dict> <key>metadata</key> <dict><key>format</key> <integer>3</integer><key>size</key><string>{80,100}</string><key>target</key><dict><key>name</key><string>b6_p1_2_ani-hd.png</string><key>textureFileName</key><string>b6_p1_2_ani-hd</string><key>textureFileExtension</key><string>png</string><key>premultipliedAlpha</key><true /></dict></dict></dict></plist>";
            doc.LoadXml(strxml);

            //获取帧列表节点
            var nodesRoot = doc.SelectSingleNode(@"//dict/key[text()='frames']/following::dict[1]");
            for (int j = 0; j < indexList.Count; j++)
            {

                //通过索引生成坐标
                int x = (int)(indexList[j] % colCount) * pixWidth;
                int y = (int)Math.Floor(indexList[j] / (double)colCount) * pixHeight;

                XmlElement key = doc.CreateElement("key");
                key.InnerText = pix + "_" + j.ToString("0000") + ".png";
                nodesRoot.AppendChild(key);
                var dict = CreatePlistFrameElement(doc, x, y, pixWidth, pixHeight);
                nodesRoot.AppendChild(dict);

            }

            //修改总大小
            string totalSizeStr = "{" + (pixWidth * colCount + 20) + "," + (pixHeight * rowCount + 20) + "}";
            doc.SelectSingleNode(@"//key[text()='size']/following::string[1]").InnerText = totalSizeStr;
            //修改文件名
            doc.SelectSingleNode(@"//key[text()='name']/following::string[1]").InnerText = pix + "_ani.png";
            doc.SelectSingleNode(@"//key[text()='textureFileName']/following::string[1]").InnerText = pix + "_ani";
            doc.Save(destPlistFilePath);
            #endregion
        }

        private static XmlElement CreatePlistFrameElement(XmlDocument doc, int x, int y, int width, int height)
        {
            XmlElement dict = doc.CreateElement("dict");

            var dictkey = doc.CreateElement("key");
            dictkey.InnerText = "aliases";

            var dictkey1 = doc.CreateElement("key");
            dictkey1.InnerText = "sourceColorRect";

            var dictstring1 = doc.CreateElement("string");
            dictstring1.InnerText = "{{0,0},{" + width + "," + height + "}}";

            var dictkey2 = doc.CreateElement("key");
            dictkey2.InnerText = "spriteOffset";

            var dictstring2 = doc.CreateElement("string");
            dictstring2.InnerText = "{0,0}";

            var dictkey3 = doc.CreateElement("key");
            dictkey3.InnerText = "spriteSize";

            var dictstring3 = doc.CreateElement("string");
            dictstring3.InnerText = "{" + width + "," + height + "}";

            var dictkey4 = doc.CreateElement("key");
            dictkey4.InnerText = "spriteSourceSize";

            var dictstring4 = doc.CreateElement("string");
            dictstring4.InnerText = "{" + width + "," + height + "}";

            var dictkey5 = doc.CreateElement("key");
            dictkey5.InnerText = "spriteTrimmed";


            var dictkey6 = doc.CreateElement("key");
            dictkey6.InnerText = "textureRect";

            var dictstring6 = doc.CreateElement("string");
            dictstring6.InnerText = "{{" + x + "," + y + "},{" + width + "," + height + "}}";

            var dictkey7 = doc.CreateElement("key");
            dictkey7.InnerText = "textureRotated";

            dict.AppendChild(dictkey);
            dict.AppendChild(doc.CreateElement("array"));
            dict.AppendChild(dictkey1);
            dict.AppendChild(dictstring1);
            dict.AppendChild(dictkey2);
            dict.AppendChild(dictstring2);
            dict.AppendChild(dictkey3);
            dict.AppendChild(dictstring3);
            dict.AppendChild(dictkey4);
            dict.AppendChild(dictstring4);
            dict.AppendChild(dictkey5);
            dict.AppendChild(doc.CreateElement("false"));
            dict.AppendChild(dictkey6);
            dict.AppendChild(dictstring6);
            dict.AppendChild(dictkey7);
            dict.AppendChild(doc.CreateElement("false"));
            return dict;
        }

        /// <summary>
        /// 复制帧动画
        /// </summary>
        /// <param name="onePiece"></param>
        /// <param name="outPutFileName"></param>
        private static void ExportFrameSprite(Sprite onePiece, string outPutFileName)
        {
            BitmapSource destSrc = Util.GetFrame(onePiece.SourceImagePath, 0);

            //-----------------------------------------------------------------------------------------
            //导出第一帧切片
            string framePngPath = outPutFileName + "_0001.png";
            Util.SaveImage(framePngPath, destSrc);

            //-----------------------------------------------------------------------------------------
            //导出总切片与plist文件
            //总切片
            string destFilePath = string.Empty;
            //plist文件
            string destPlistFilePath = string.Empty;

            destFilePath = outPutFileName + "_ani.png";
            destPlistFilePath = outPutFileName + "_ani.plist";

            //-----------------------------------------------------------------------------------------
            //导出总切片与plist文件
            Util.CopyFile(onePiece.SourceImagePath, destFilePath);
            Util.CopyFile(onePiece.FrameAnimPlistPath, destPlistFilePath);

        }


        /// <summary>
        /// 发布书籍
        /// </summary>
        /// <param name="curBook"></param>
        /// <param name="previewClinetSize"></param>
        /// <param name="gridWindownContain"></param>
        public static void DoExport(Book curBook, ClientSize previewClinetSize, Grid gridWindownContain, List<ClientSize> exportClients = null, bool isPreview = false)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    //添加进度条控件
                    MediaTypeNames.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {

                        progress = new ProgressControl();
                        if (progress != null)
                            gridWindownContain.Children.Add(progress);
                        curBook.progress = progress;
                        progress.ShowWord("正在检查书籍完整性,请等待...");
                    }));

                    //导出路径检查
                    CheckResult checkResult = CheckExportHelper.DoExportAllFilesCheck(curBook, curBook.Settings.ExportPath, previewClinetSize);
                    IsExporting = true;
                    if (checkResult.Result)
                    {
                        //---------------------------------------
                        string exportDir = curBook.Settings.ExportPath;
                        string zipName = curBook.Settings.ZipName;
                        int previewCount = curBook.Settings.PreviewPageCount;
                        if (exportClients == null)
                            exportClients = PublishHelper.GetClients();
                        else
                            exportClients = exportClients.FindAll(delegate (ClientSize cs) { return cs.IsChecked == true; });

                        audioPathVisited.Clear();
                        curBook.RenameAllPages();
                        for (int i = 0; i < exportClients.Count; i++)
                        {
                            if (exportClients[i].IsChecked)
                            {
                                MediaTypeNames.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    progress.progressBar.Maximum = exportClients.Count;
                                    progress.progressBar.Value = i + 1;
                                    progress.ShowWord("正在导出文件" + exportClients[i].ClinetName + "    进度：" + (i + 1).ToString() + "/" + exportClients.Count);
                                }));
                                DoExportByCurrPage(exportDir, curBook, exportClients[i]);
                                if (isPreview)
                                    DoExportByCurrPage(exportDir, curBook, exportClients[i], "_view", 0, previewCount);
                            }
                        }
                    }
                    else
                    {
                        MediaTypeNames.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            MessageBox.Show(checkResult.ErrorMsg, checkResult.ErrorTitle);
                        }));
                    }
                }
                finally
                {
                    IsExporting = false;
                    MediaTypeNames.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        //移除进度条控件
                        if (progress != null)
                        {
                            gridWindownContain.Children.Remove(progress);
                            progress = null;
                        }

                        MessageBoxResult result = MessageBox.Show(MainWin, "是否打开发布文件夹", "信息提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            Process.Start(new ProcessStartInfo() { FileName = "Explorer.exe ", Arguments = curBook.Settings.ExportPath, UseShellExecute = true });
                        }
                    }));
                }


            });
        }

        /// <summary>
        /// 合并书籍
        /// </summary>
        /// <param name="curBook"></param>
        /// <param name="newBookPath"></param>
        /// <returns></returns>
        public static void AppendBook(Book curBook, string newBookPath)
        {

            Book tempBook = Book.Open(newBookPath, MainWin);
            if (tempBook == null)
            {
                MessageBox.Show("无效电子书工程文件");
                return;
            }
            ThreadPool.QueueUserWorkItem(delegate
          {
              IsExporting = true;

              int currPageCount = 0;
              if (curBook.Pages.Count > 0)
                  currPageCount = curBook.Pages.Count;


              string prefixName = curBook.PrefixName;

              //----------创建素材目录--------------------------
              FileInfo tempFile = new FileInfo(curBook.SavePath);
              string dirName = tempFile.DirectoryName;
              string rootName = dirName + "\\" + Path.GetFileName(tempBook.SavePath).Replace(".sfb", " ").Trim();
              rootName = Util.GerNewPath(rootName);
              string imageFolder = rootName + "\\image";
              Directory.CreateDirectory(imageFolder);
              string soundFolder = rootName + "\\sound";
              Directory.CreateDirectory(soundFolder);
              string movieFolder = rootName + "\\movie";
              Directory.CreateDirectory(movieFolder);
              Util.CreateDir(rootName);
              Util.CreateDir(imageFolder);
              Util.CreateDir(soundFolder);
              Util.CreateDir(movieFolder);

              string tempPath = string.Empty;
              //------------------------------------------------

              foreach (Page page in tempBook.Pages)
              {
                  page.ParentBook = curBook;
                  //重置页面编号
                  page.PageIndex = currPageCount++;
                  #region 复制页面素材
                  tempPath = imageFolder + "/" + prefixName + "_" + "p" + page.PageIndex + ".png";
                  //背景图
                  if (File.Exists(page.BgImagePath))
                  {
                      Util.CopyFile(page.BgImagePath, tempPath);
                      page.BgImagePath = tempPath;
                  }
                  else
                  {
                      if (string.IsNullOrEmpty(page.BgImagePath))
                      {
                          Util.CreateEmptyPngFile(tempPath, 2730, 1536, DPI);
                      }
                  }

                  //背景音乐
                  if (!string.IsNullOrEmpty(page.BgSoundPath))
                  {
                      string destBgSoundPath = soundFolder + "/" + prefixName + "_" + "p" + page.PageIndex + ".mp3";
                      if (File.Exists(page.BgSoundPath))
                      {
                          File.Copy(page.BgSoundPath, destBgSoundPath, true);
                          page.BgSoundPath = destBgSoundPath;
                      }

                  }
                  List<Sprite> normalSprites = new List<Sprite>();
                  List<Particle> particles = new List<Particle>();
                  List<Sprite> movies = new List<Sprite>();
                  for (int i = 0; i < page.Sprites.Count; i++)
                  {
                      Sprite onePiece = page.Sprites[i];
                      if (onePiece.GetType() == typeof(Particle))
                          particles.Add(onePiece as Particle);
                      else
                          if (onePiece.buddyMovie != null)
                      {
                          movies.Add(onePiece);
                          normalSprites.Add(onePiece);
                      }
                      else if (!string.IsNullOrEmpty(onePiece._sourceImagePath) && onePiece._sourceImagePath.Contains("Stop_Red.png"))
                      { }
                      else
                          normalSprites.Add(onePiece);
                  }

                  #endregion
                  curBook.Pages.Add(page);
                  for (int i = 0; i < normalSprites.Count; i++)
                  {

                      Sprite onePiece = normalSprites[i];
                      if (onePiece.ImgName == string.Empty) continue;
                      string destFilePath = string.Empty;
                      string destSoundPath = string.Empty;
                      //----------------------------------------------------------------------------------------
                      //切片

                      if (onePiece.haveAnimation(AnimationStyle.帧动画))
                      {
                          destFilePath = imageFolder + "\\" + prefixName + "_" + "p" + page.PageIndex + "_" + onePiece.ID;
                          ExportFrameSprite(onePiece, destFilePath);
                          onePiece.SourceImagePath = destFilePath + "_ani.png";
                      }
                      else if (onePiece.haveAnimation(AnimationStyle.骨骼动画))
                      {
                          destFilePath = imageFolder + "\\" + prefixName + "_" + "p" + page.PageIndex + "_" + onePiece.ID + ".png";
                          if (Util.IsBone(destFilePath)) continue;

                          CopyFile(onePiece.SourceImagePath, destFilePath, true);
                          CopyFile(onePiece.SourceImagePath.ToTexPngPath(), destFilePath.ToTexPngPath(), true);
                          CopyFile(onePiece.SourceImagePath.ToTexJsonPath(), destFilePath.ToTexJsonPath(), true);
                          CopyFile(onePiece.SourceImagePath.ToSkeJsonPath(), destFilePath.ToSkeJsonPath(), true);

                          onePiece.SourceImagePath = destFilePath;
                          onePiece.TexPngPath = destFilePath.ToTexPngPath();
                          onePiece.TexJsonPath = destFilePath.ToTexJsonPath();
                          onePiece.SkeJsonPath = destFilePath.ToSkeJsonPath();

                      }
                      else
                      {
                          destFilePath = imageFolder + "\\" + prefixName + "_" + "p" + page.PageIndex + "_" + onePiece.ID + ".png";
                          if (onePiece.SourceImagePath == string.Empty)
                          {
                              Util.CreateEmptyPngFile(destFilePath, (int)(onePiece.Width), (int)(onePiece.Height), DPI);
                          }
                          else
                          {
                              BitmapSource curImgsrc = Util.Snap2DeviceDpi(onePiece.SourceImagePath);
                              if (curImgsrc != null)
                              {
                                  if (curImgsrc.PixelWidth == (int)onePiece.Width &&
                                      curImgsrc.PixelHeight == (int)onePiece.Height
                                      )
                                  {
                                      Util.CopyFile(onePiece.SourceImagePath, destFilePath);
                                      #region 如果是游戏连线游戏 要导出两张切片
                                      if (onePiece.GetType() == typeof(OrderLineSprite))
                                      {
                                          OrderLineSprite ols = onePiece as OrderLineSprite;
                                          if (ols != null && ols.IsGameSprite)
                                          {
                                              destFilePath = destFilePath.Replace(".png", "_t.png");
                                              if (File.Exists(ols.TouchedImagePath))
                                              {
                                                  Util.CopyFile(ols.TouchedImagePath, destFilePath);
                                                  ols.TouchedImagePath = destFilePath;
                                              }

                                              else
                                              {
                                                  Util.CreateEmptyPngFile(destFilePath, (int)(onePiece.Width), (int)(onePiece.Height), DPI);
                                              }
                                          }
                                      }
                                      #endregion
                                  }
                                  else
                                  {
                                      Util.ScaleImage(onePiece.SourceImagePath, destFilePath, (int)(onePiece.Width), (int)(onePiece.Height), (int)curImgsrc.DpiX);
                                  }
                                  onePiece.SourceImagePath = destFilePath;
                              }
                              else
                              {
                                  Util.RemoveDir(rootName);
                              }
                          }

                      }

                      //----------------------------------------------------------------------------------------
                      //声音
                      if (!string.IsNullOrEmpty(onePiece.SoundSourcePath))
                      {
                          destSoundPath = soundFolder + "/" + prefixName + "_" + "p" + page.PageIndex + "_" + onePiece.ID + ".mp3";
                          if (File.Exists(onePiece.SoundSourcePath))
                          {
                              File.Copy(onePiece.SoundSourcePath, destSoundPath, true);
                              onePiece.SoundSourcePath = destSoundPath;
                          }

                      }
                      #region 动画
                      foreach (AnimationGroup ag in onePiece.AnimationGroupSet)
                      {
                          foreach (Animation anim in ag.AnimationSet)
                          {
                              if (anim.GetType() == typeof(ReplaceAnimation))
                              {
                                  List<ImageAudioObj> list = ((ReplaceAnimation)anim).ImgAudioList;

                                  destFilePath = imageFolder + "/" + prefixName + "_" + "p" + page.PageIndex + "_" + onePiece.ID;
                                  destSoundPath = soundFolder + "/" + prefixName + "_" + "p" + page.PageIndex + "_" + onePiece.ID;

                                  //if (!string.IsNullOrEmpty(onePiece.SourceImagePath) && File.Exists(onePiece.SourceImagePath))
                                  //{
                                  //    Util.CopyFile(onePiece.SourceImagePath, destFilePath + "_1.png");
                                  //}
                                  //if (!string.IsNullOrEmpty(onePiece.SoundSourcePath) && File.Exists(onePiece.SoundSourcePath))
                                  //{
                                  //    Util.CopyFile(onePiece.SoundSourcePath, destSoundPath + "_1.mp3");
                                  //}

                                  for (int j = 0; j < list.Count; j++)
                                  {
                                      ImageAudioObj obj = list[j];
                                      if (!string.IsNullOrEmpty(obj.ImagePath) && File.Exists(obj.ImagePath))
                                      {
                                          Util.CopyFile(obj.ImagePath, destFilePath + "_" + (j + 2).ToString() + ".png");
                                          obj.ImagePath = destFilePath + "_" + (j + 2).ToString() + ".png";
                                      }
                                      if (!string.IsNullOrEmpty(obj.AudioPath) && File.Exists(obj.AudioPath))
                                      {
                                          Util.CopyFile(obj.AudioPath, destSoundPath + "_" + (j + 2).ToString() + ".mp3");
                                          obj.AudioPath = destFilePath + "_" + (j + 2).ToString() + ".png";
                                      }
                                  }
                              }
                          }
                      }
                      #endregion
                  }
                  #region  粒子效果
                  for (int i = 0; i < particles.Count; i++)
                  {
                      Particle onePiece = particles[i];
                      string destFilePath = imageFolder + "/" + prefixName + "_" + "p" + page.PageIndex + "_" + onePiece.ID + ".png";
                      if (File.Exists(onePiece._sourceImagePath))
                          File.Copy(onePiece._sourceImagePath, destFilePath, true);
                      else
                      {
                          MessageBox.Show("找不到粒子效果图片" + onePiece._sourceImagePath);
                          IsExporting = false;
                          return;
                      }
                      onePiece.SourceImagePath = destFilePath;
                  }
                  #endregion

                  #region  视频
                  for (int i = 0; i < movies.Count; i++)
                  {
                      Sprite onePiece = movies[i];
                      Movie movie = onePiece.buddyMovie;
                      if (movie == null) continue;
                      string destFilePath = movieFolder + "/" + movie.VideoName;
                      File.Copy(movie.VideoSourcePath, destFilePath, true);
                      onePiece.buddyMovie.VideoSourcePath = destFilePath;
                  }

                  #endregion


              }
              curBook.RenameAllPages();



              MediaTypeNames.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
              {
                  string newpath = curBook.SavePath.Replace("_merge", "").Trim().Replace(".sfb", "_merge.sfb");
                  curBook.Save(newpath);
                  MainWin.OpenBook(newpath);
                  MessageBox.Show("合并完成");
                  IsExporting = false;
              }));

          });
        }
        public static bool? IsExporting = false;
    }


}
