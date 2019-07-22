using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Will_Blog.Models
{
    public class BlogPost
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }//Physically type in value of this "abstract"
        public string Slug { get; set; }//Value set using helper method
        [AllowHtml]
        public string Body { get; set; }
        public string MediaUrl { get; set; }//Record a string that is pointing to a path in project, or server containing media(gif, mp4, image)
        public bool Published { get; set; }//Everyone can see it true/false
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }

        //Virtual Navigation section
        public virtual ICollection<Comment> Comments { get; set; }//Icollection List/HashSet = Interface can be satisfied with more than 1 type// collection--Collection of a certain type <Comment> Comments section
        public BlogPost()
        {
            Comments = new HashSet<Comment>();//Constructor = method that runs automatically, same name as the class or "BlogPost" "this." optional
        }

        
        
    }

}