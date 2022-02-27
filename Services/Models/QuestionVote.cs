using System;
using System.Collections.Generic;

#nullable disable

namespace Services.Models
{
    public partial class QuestionVote
    {
        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public int? UserId { get; set; }
        public string OperationCode { get; set; }

        public virtual Question Question { get; set; }
    }
}
