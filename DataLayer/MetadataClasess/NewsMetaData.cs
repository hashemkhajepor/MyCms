using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataLayer
{
   public class NewsMetaData
    {
        [Key]
        public int NewsID { get; set; }
        [Display(Name ="عنوان")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }
        [Display(Name = "متن خبر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]

        public string Text { get; set; }
        [Display(Name = "تصویر")]
        public string ImageName { get; set; }
        [Display(Name = "تاریخ خبر")]
        public System.DateTime CreateDate { get; set; }
    }
    [MetadataType(typeof(NewsMetaData))]
    public partial class News
    {

    }
}
