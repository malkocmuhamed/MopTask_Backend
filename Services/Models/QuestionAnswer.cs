using System;
using System.Collections.Generic;

#nullable disable

namespace Services.Models
{
    public partial class QuestionAnswer
    {
        public QuestionAnswer()
        {
            AnswerVotes = new HashSet<AnswerVote>();
        }

        public int Id { get; set; }
        public string AnswerText { get; set; }
        public int? UserId { get; set; }
        public int? QuestionId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Question Question { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<AnswerVote> AnswerVotes { get; set; }
    }
}
