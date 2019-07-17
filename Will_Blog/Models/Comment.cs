using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Will_Blog.Models
{
    public class Comment
    {
        public int Id { get; set; } //Primary Key Identifier or "Parent"
        public int BlogPostId { get; set; }//Family Key Identifier or "Child"
        public string AuthorId { get; set; }//Username of the person who posted the comment to this specific blog
        public string Body { get; set; }//Text body in which comment is contained
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }//? = null, or Nullable; example "Nullable<DateTimeOffset>" meaning I do not need a valid updated date
        public string UpdateReason { get; set; }//Reason for updating post/comment
        //Virtual Navigation section
        public virtual BlogPost BlogPost { get;set; }//Class of BlogPost looking for Family Key identifier(or child) of "BlogPost"
        public virtual ApplicationUser Author { get; set; }//AuthorId is of type "ApplicationUser"
    }

}
