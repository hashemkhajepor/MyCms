//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class News_Groups
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public News_Groups()
        {
            this.News_Groups1 = new HashSet<News_Groups>();
            this.News_Selected_Groups = new HashSet<News_Selected_Groups>();
        }
    
        public int GroupID { get; set; }
        public string GroupTitle { get; set; }
        public Nullable<int> ParentID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<News_Groups> News_Groups1 { get; set; }
        public virtual News_Groups News_Groups2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<News_Selected_Groups> News_Selected_Groups { get; set; }
    }
}
