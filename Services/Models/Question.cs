using System;
using System.Collections.Generic;

#nullable disable

namespace Services.Models
{
    public partial class Question
    {
        public Question()
        {
            QuestionAnswers = new HashSet<QuestionAnswer>();
            QuestionVotes = new HashSet<QuestionVote>();
        }

        public int Id { get; set; }
        public string QuestionText { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }
        public virtual ICollection<QuestionVote> QuestionVotes { get; set; }
    }
}
