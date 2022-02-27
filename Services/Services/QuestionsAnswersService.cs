using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Repositories;
using Services.Models;

namespace Services.Services
{
    public interface IQuestionsAnswersService
    {
        public Task PostQuestion(Question question);
        public Task<Question> GetQuestionById(int id);
        public Task PostAnswer(QuestionAnswer questionAnswer);
        public Task<QuestionAnswer> GetAnswerById(int id);
        public IEnumerable<Question> GetAllQuestions();
        public IEnumerable<QuestionAnswer> GetAllAnswers();
        public IEnumerable<Question> GetUserQuestions();
        public Task EditAnswer(QuestionAnswer answerInDb, QuestionAnswer questionAnswer);
        public Task DeleteAnswer(QuestionAnswer questionAnswer);

    }

    public class QuestionsAnswersService : IQuestionsAnswersService
    {
        private readonly IQuestionsAnswersRepository _questionsAnswersRepository;

        public QuestionsAnswersService(IQuestionsAnswersRepository questionsAnswersRepository)
        {
            this._questionsAnswersRepository = questionsAnswersRepository;
        }

        public Task PostQuestion(Question question)
        {
            return _questionsAnswersRepository.PostQuestion(question);
        }

        public Task<Question> GetQuestionById(int id)
        {
            return _questionsAnswersRepository.GetQuestionById(id);
        }

        public Task PostAnswer(QuestionAnswer questionAnswer)
        {
            return _questionsAnswersRepository.PostAnswer(questionAnswer);
        }

        public Task<QuestionAnswer> GetAnswerById(int id)
        {
            return _questionsAnswersRepository.GetAnswerById(id);
        }


        public IEnumerable<Question> GetAllQuestions()
        {
            return _questionsAnswersRepository.GetAllQuestions();
        }

        public IEnumerable<QuestionAnswer> GetAllAnswers()
        {
            return _questionsAnswersRepository.GetAllAnswers();
        }
        public IEnumerable<Question> GetUserQuestions()
        {
            return _questionsAnswersRepository.GetUserQuestions();
        }

        public Task EditAnswer(QuestionAnswer answerInDB, QuestionAnswer questionAnswer)
        {
            answerInDB.AnswerText = questionAnswer.AnswerText;
            answerInDB.CreatedDate = questionAnswer.CreatedDate;
            return _questionsAnswersRepository.EditAnswer(questionAnswer);
        }

        public Task DeleteAnswer(QuestionAnswer questionAnswer)
        {
            return _questionsAnswersRepository.DeleteAnswer(questionAnswer);
        }
    }
}
