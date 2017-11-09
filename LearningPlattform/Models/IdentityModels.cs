using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace LearningPlattform.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }
        [Display(Name = "Birth Date"), DataType(DataType.Date), Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public string University { get; set; }
        [ScaffoldColumn(false)]
        public AccountStatus AccountStatus { get; set; }
        [Required]
        public UserLevel UserLevel { get; set; }
        [ScaffoldColumn(false)]
        public ICollection<Course> Courses { get; set; }


    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<CourseComment> CourseComments { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}