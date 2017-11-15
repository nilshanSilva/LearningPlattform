using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LearningPlattform.Models
{
    public enum CourseLevel { Beginner = 1, Intermediate, Advanced}
    public enum Language { English = 1, Spanish, Tamil}
    public enum Category { Science = 1, Technology, Engineering, Mathematics }
    public enum UserLevel { Student = 1, Instructor}
    public enum Gender { Male = 1, Female}
    public enum AccountStatus { Active = 1, Inactive}


    public class Course
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public Language Language { get; set; }
        [Required]
        public CourseLevel CourseLevel { get; set; }
        [DataType(DataType.Currency), Range(0,300)]
        public Decimal Price { get; set; }
       // [ScaffoldColumn(false)]
      //  public string ImagePath { get; set; }
        [ScaffoldColumn(false)]
        public ICollection<Video> Videos { get; set; }
        [ScaffoldColumn(false)]
        public ICollection<Document> Documents { get; set; }
        [ScaffoldColumn(false)]
        public ICollection<CourseComment> CourseComments { get; set; }
        [ScaffoldColumn(false)]
        public ICollection<ApplicationUser> Users { get; set; }

    }
}