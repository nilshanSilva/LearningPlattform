using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningPlattform.Models.ViewModels
{
    public class VideoCourseViewModel
    {
        public VideoCourseViewModel()
        {
            Videos = new List<Video>();
            Course = new Course();
        }
        
        public List<Video> Videos { get; set; }
        public Course Course { get; set; }
    }

}