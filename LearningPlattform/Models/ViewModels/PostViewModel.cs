using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningPlattform.Models.ViewModels
{
    public class PostViewModel
    {
        public Post Post { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public Answer CorrectAnswer { get; set; }
        public Answer Answer { get; set; }
        public AnswerComment Comment { get; set; }
    }
}