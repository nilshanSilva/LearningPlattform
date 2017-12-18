using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LearningPlattform.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string Question { get; set; }

        [Required, DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [ScaffoldColumn(false)]
        public Answer CorrectAnswer { get; set; }

        [ScaffoldColumn(false)]
        public ICollection<Answer> Answers { get; set; }

        [ScaffoldColumn(false), Required]
        public string  RaiserId { get; set; }

        [Display(Name = "Posted Date"), DataType(DataType.Date), Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PostedDate { get; set; }


    }

    public class Answer
    {
        public int Id { get; set; }

        [Required, Display(Name ="Answer"), DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [ScaffoldColumn(false)]
        public ApplicationUser Answerer { get; set; }

        [Required, ScaffoldColumn(false)]
        public Post ParentPost { get; set; }

        [ScaffoldColumn(false)]
        public bool CorrectAnswer { get; set; }

        [ScaffoldColumn(false)]
        public ICollection<AnswerComment> Comments { get; set; }

        [ScaffoldColumn(false)]
        public ICollection<Like> Likes { get; set; }
    }


    public class AnswerComment
    {
        public int Id { get; set; }

        [Required, Display(Name ="Comment")]
        public string Body { get; set; }

        [Required, ScaffoldColumn(false)]
        public ApplicationUser Commenter { get; set; }

        [ScaffoldColumn(false)]
        public string CommenterName { get; set; }

        [Required, ScaffoldColumn(false)]
        public Answer ParentAnswer { get; set; }
    }

    public class Like
    {
        public int Id { get; set; }
        public ApplicationUser Liker { get; set; }
        public Answer LikedAnswer { get; set; }
    }
}