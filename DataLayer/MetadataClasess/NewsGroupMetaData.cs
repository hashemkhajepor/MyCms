using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class NewsGroupMetaData
    { 
        public int GroupID { get; set; }
        [Display(Name ="عنوان گروه")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string GroupTitle { get; set; }
        public Nullable<int> ParentID { get; set; }
    }
    [MetadataType(typeof(NewsGroupMetaData))]
    public partial class News_Groups
    {

    }
}
