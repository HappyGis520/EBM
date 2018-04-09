using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using EllaBookMaker.Model;
using EllaBookMaker.Utility;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace EllaBookMaker.ViewModel
{
   public   class MenuToolViewModel: ViewModelBase
   {
       public MenuToolViewModel()
       {
       }

        #region     CmdNewBook       菜单：文件-创建新动画书工程     
        private RelayCommand _CmdNewBook;
       public RelayCommand CmdNewBook
       {
           get
           {
               if (_CmdNewBook == null)
               {
                   _CmdNewBook = new RelayCommand(ExcNewBook, CanExcNewBook);

               }

               return _CmdNewBook;
           }
           set { _CmdNewBook = value; }
       }
       private void ExcNewBook()
       {
            JLog.Instance.Info("文件-创建新动画书工程  ");
       }
       private bool CanExcNewBook()
       {
           return true;
       }
        #endregion
        #region     CmdOpenBook       菜单：文件-打开动画书工程     

        private RelayCommand _CmdOpenBook;

       public RelayCommand CmdOpenBook
       {
           get
           {
               if (_CmdOpenBook == null)
               {
                   _CmdOpenBook = new RelayCommand(ExcOpenBook, CanExcOpenBook);
               }

               return _CmdOpenBook;
           }
           set { _CmdOpenBook = value; }
       }

       private void ExcOpenBook()
       {
           JLog.Instance.Info("执行打开电子 书");
        }

       private bool CanExcOpenBook()
       {
           JLog.Instance.Info("判断是否可执行合并电子 书");
            return false;
       }

        #endregion
        #region     CmdSaveBaook        菜单：文件-保存动画书工程     

        private RelayCommand _CmdSaveBaook;

       public RelayCommand CmdSaveBaook
       {
           get
           {
               if (_CmdSaveBaook == null)
               {
                   _CmdSaveBaook = new RelayCommand(ExcSaveBaook, CanExcSaveBaook);
               }

               return _CmdSaveBaook;
           }
           set { _CmdSaveBaook = value; }
       }

       private void ExcSaveBaook()
       {
       }

       private bool CanExcSaveBaook()
       {
           return false;
       }

        #endregion
        #region     CmdSaveAsBook        菜单：文件-另存为     

        private RelayCommand _CmdSaveAsBook;

       public RelayCommand CmdSaveAsBook
       {
           get
           {
               if (_CmdSaveAsBook == null)
               {
                   _CmdSaveAsBook = new RelayCommand(ExcSaveAsBook, CanExcSaveAsBook);
               }

               return _CmdSaveAsBook;
           }
           set { _CmdSaveAsBook = value; }
       }

       private void ExcSaveAsBook()
       {
       }

       private bool CanExcSaveAsBook()
       {
           return false;
       }

        #endregion
        #region     CmdAppendBook        菜单：文件-合并电子书     

        private RelayCommand _CmdAppendBook;

       public RelayCommand CmdAppendBook
       {
           get
           {
               if (_CmdAppendBook == null)
               {
                   _CmdAppendBook = new RelayCommand(ExcAppendBook, CanExcAppendBook);
               }

               return _CmdAppendBook;
           }
           set { _CmdAppendBook = value; }
       }

       private void ExcAppendBook()
       {
           JLog.Instance.Info("执行合并电子 书");
        }

       private bool CanExcAppendBook()
       {
           JLog.Instance.Info("判断是否可用菜单命令");
           return false;
       }

       #endregion


       

       
   }
}
