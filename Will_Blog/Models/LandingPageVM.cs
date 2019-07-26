using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Will_Blog.Models;

namespace Will_Blog.Models
{
    public class LandingPageVM
    {
        public BlogPost TopLeft { get; set; }
        public BlogPost TopMiddle { get; set; }
        public BlogPost TopRight { get; set; }
        public BlogPost BottomLeft { get; set; }
        public BlogPost BottomMiddle { get; set; }
        public BlogPost BottomRight { get; set; }
    }
}