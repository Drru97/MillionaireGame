﻿using MillionaireGame.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using MillionaireGame.Entities;
using MillionaireGame.Repositories.Abstract;
using MillionaireGame.Frontend.Models;
using Newtonsoft.Json;

namespace MillionaireGame.Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IGameStepRepository _gameStepRepository;

        public HomeController(IQuestionRepository questionRepo, IGameStepRepository gameStepRepo)
        {
            _questionRepository = questionRepo;
            _gameStepRepository = gameStepRepo;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Game(string PlayerName)
        {
            Session["name"] = PlayerName;
            var game = new GameViewModel
            {
                EndOfGame = false,
                QuestionIndex = 0,
                Question = _questionRepository.Questions.ElementAt(0),
                GameSteps = _gameStepRepository.GameSteps.OrderByDescending(i => i.Reward)
            };
            return View(game);
        }

        [HttpPost]
        public ActionResult PlayerGame(PlayerAnswerViewModel answer)
        {
            int index = answer.QuestionIndex;
            var currentQuestion = _questionRepository.Questions.ElementAt(0); //see lower
            bool endOfGame = true;
            if (currentQuestion.IsAnswerCorrect(answer.PlayerAnswer))
            {
                if(index == 14)
                {
                    //return winning page here
                }
                endOfGame = false;
            }
            int newIndex = index + 1;
            var game = new GameViewModel
            {
                EndOfGame = endOfGame,
                QuestionIndex = newIndex,                              //uncomment when all quests will be able
                Question = _questionRepository.Questions.ElementAt(0), //ElementAt(newIndex)  
                GameSteps = null
            };
            string response = JsonConvert.SerializeObject(game);
            return Json(response);
        }

        //might be improoved. Its bad idea to use get for this thing i guess :)
        [HttpGet]
        public ActionResult GameResult(int step) //step need for win sum of money
        {
            return View();
        } 
    }
}