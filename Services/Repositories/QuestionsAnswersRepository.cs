using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Models;
using System.Security.Claims;

namespace Services.Repositories
{
    public interface IQuestionsAnswersRepository
    {
        public Task PostQuestion(Question question);
        public Task<Question> GetQuestionById(int id);

        public Task PostAnswer(QuestionAnswer questionAnswer);
        public Task<QuestionAnswer> GetAnswerById(int id);
        public IEnumerable<Question> GetAllQuestions();
        public IEnumerable<QuestionAnswer> GetAllAnswers();
        public IEnumerable<Question> GetUserQuestions();
        public Task EditAnswer(QuestionAnswer questionAnswer);
        public Task DeleteAnswer(QuestionAnswer questionAnswer);
    }

    public class QuestionsAnswersRepository : IQuestionsAnswersRepository
    {
        moptaskDBContext _context;

        public QuestionsAnswersRepository(moptaskDBContext context)
        {
            this._context = context;
        }
        public async Task PostQuestion(Question question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
        }

        public async Task<Question> GetQuestionById(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            return question;
        }

        public async Task PostAnswer(QuestionAnswer questionAnswer)
        {
            _context.QuestionAnswers.Add(questionAnswer);
            await _context.SaveChangesAsync();
        }

        public async Task<QuestionAnswer> GetAnswerById(int id)
        {
            var answer = await _context.QuestionAnswers.FindAsync(id);
            return answer;
        }

        public IEnumerable<Question> GetAllQuestions()
        {
            return _context.Questions;
        }

        public IEnumerable<QuestionAnswer> GetAllAnswers()
        {
            return _context.QuestionAnswers;
        }
        public IEnumerable<Question> GetUserQuestions()
        {
            var userQuestions = _context.Questions.OrderBy(d => d.CreatedDate).ToList();
            return userQuestions;
        }

        public async Task EditAnswer(QuestionAnswer questionAnswer)
        {
            _context.QuestionAnswers.Update(questionAnswer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAnswer(QuestionAnswer questionAnswer)
        {
            _context.QuestionAnswers.Remove(questionAnswer);
            await _context.SaveChangesAsync();
        }
    }
}
