using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningPlattform.Models.ViewModels
{
    public class SingleVideoCourseVM
    {
        public SingleVideoCourseVM()
        {
            Video = new Video();
            Course = new Course();
        }

        public Video Video { get; set; }
        public Course Course { get; set; }
    }
}