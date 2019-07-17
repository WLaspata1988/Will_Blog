using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Will_Blog.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser// : at class level means inherits, or implements. In this case, it's inheritance ApplicationUser : comes with all content of IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }

        //Virtual Navigation section always have two pieces, child and parent ApplicationUser = Parent Comments= Child
        public virtual ICollection<Comment> Comments { get; set; }

        public ApplicationUser()//Specific user
        {
            Comments = new HashSet<Comment>();//All Child comments wrote
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>//ApplicationDBContext is arbitrary, name it anything
    {
        public ApplicationDbContext()//DefaultConnection identifies where connection string is
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }
      

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<BlogPost> Posts { get; set; }//Db.BlogPost DbSet of type <BlogPost> Posts
        public DbSet<Comment> Comments { get; set; }//Db.Comment DbSet of type <Comment> Comments

    }
}