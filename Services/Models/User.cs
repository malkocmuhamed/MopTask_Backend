using System;
using System.Collections.Generic;

#nullable disable

namespace Services.Models
{
    public partial class User
    {
        public User()
        {
            QuestionAnswers = new HashSet<QuestionAnswer>();
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
