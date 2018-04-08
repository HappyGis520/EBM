using System;
using System.Collections.Generic;
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
   public   class MenuViewModel: ViewModelBase
   {
       private Book _CurBook;

       public Book CurBook
       {
           get { return CurBook; }
           set { _CurBook = value; RaisePropertyChanged(()=>CurBook); }
       }
       public MenuViewModel()
       {
           _CurBook = new Book()
           {
               Name = "雪孩子"
           };
       }
       private RelayCommand _MenuCmdPublish;
        public RelayCommand MenuCmdPublish
        {
            get
            {
                if (_MenuCmdPublish == null)
                {
                    
                    return new RelayCommand(() =>
                    {
                        JLog.Instance.Error("fdsafsafdsafdsaf");
                    }, CanExcute);
                }
                return _MenuCmdPublish;
            }
            set { _MenuCmdPublish  = value; }

        }
       private bool CanExcute()
       {
           if(_CurBook!=null)
            return true;
           return false;    
       }

     

    }
}
