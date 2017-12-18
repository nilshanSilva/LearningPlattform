using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LearningPlattform.Models
{
    public class Video
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [ScaffoldColumn(false)]
        public string Path { get; set; }
        [ScaffoldColumn(false)]
        public Course Course { get; set; }
    }

    public class Document
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [ScaffoldColumn(false)]
        public string Path { get; set; }
        [ScaffoldColumn(false)]
        public Course Course { get; set; }
    }

    public class CourseComment
    {
        public int Id { get; set; }
        [Required]
        public string Comment { get; set; }
        [ScaffoldColumn(false)]
        public Course Course { get; set; }
        [ScaffoldColumn(false)]
        public ApplicationUser Commenter { get; set; }
    }
}