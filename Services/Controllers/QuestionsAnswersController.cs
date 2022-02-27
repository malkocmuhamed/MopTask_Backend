using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Services;
using Services.Models;
using Microsoft.AspNetCore.Authorization;

namespace Services.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("QuestionsAnswers")]
    public class QuestionsAnswersController : Controller
    {
        private readonly IQuestionsAnswersService _questionsAnswersService;

        public QuestionsAnswersController(IQuestionsAnswersService questionsAnswersService)
        {
            this._questionsAnswersService = questionsAnswersService;
        }

        [HttpGet("{id}")]
        [Route("PostQuestion")]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            Question questionInDB = await _questionsAnswersService.GetQuestionById(id);
            if (questionInDB == null)
            {
                return NotFound();
            }
            return Ok(await _questionsAnswersService.GetQuestionById(id));
        }

        [HttpGet("{id}")]
        [Route("PostAnswer")]

        public async Task<IActionResult> GetAnswerById(int id)
        {
            QuestionAnswer answerInDB = await _questionsAnswersService.GetAnswerById(id);
            if (answerInDB == null)
            {
                return NotFound();
            }
            return Ok(await _questionsAnswersService.GetAnswerById(id));
        }

        [HttpPost]
        [Route("PostQuestions")]
        public async Task<IActionResult> PostQuestion(Question question)
        {
            await _questionsAnswersService.PostQuestion(question);
            return CreatedAtAction("GetQuestionById", new { id = question.Id }, question);
        }


        [HttpPost]
        [Route("PostAnswers")]

        public async Task<IActionResult> PostAnswer(QuestionAnswer questionAnswer)
        {
            await _questionsAnswersService.PostAnswer(questionAnswer);
            return CreatedAtAction("GetAnswerById", new { id = questionAnswer.Id }, questionAnswer);
        }

        [HttpGet]
        [Route("GetQuestions")]
        public IActionResult GetAllQuestions()
        {
            return Ok(_questionsAnswersService.GetAllQuestions());
        }


        [HttpGet]
        [Route("GetAnswers")]
        public IActionResult GetAllAnswers()
        {
            return Ok(_questionsAnswersService.GetAllAnswers());
        }

        [HttpGet]
        [Route("GetUserQuestions")]
        public IActionResult GetUserQuestions()
        {
            return Ok(_questionsAnswersService.GetUserQuestions());
        }

        [HttpPut]
        [Route("EditAnswer")]

        public async Task<IActionResult> EditAnswer(QuestionAnswer questionAnswer)
        {
            QuestionAnswer questionInDB = await _questionsAnswersService.GetAnswerById(questionAnswer.Id);
            if (questionInDB == null)
            {
                return NotFound();
            }
            await _questionsAnswersService.EditAnswer(questionInDB, questionAnswer);
            return Ok();
        }

        // DELETE api/<SongsController>/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            QuestionAnswer questionInDb = await _questionsAnswersService.GetAnswerById(id);
            if (questionInDb == null)
            {
                return NotFound();
            }
            await _questionsAnswersService.DeleteAnswer(questionInDb);
            return Ok();
        }




    }
}
