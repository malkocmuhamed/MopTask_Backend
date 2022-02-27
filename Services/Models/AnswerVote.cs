using System;
using System.Collections.Generic;

#nullable disable

namespace Services.Models
{
    public partial class AnswerVote
    {
        public int Id { get; set; }
        public int? AnswerId { get; set; }
        public int? UserId { get; set; }
        public string OperationCode { get; set; }

        public virtual QuestionAnswer Answer { get; set; }
    }
}
