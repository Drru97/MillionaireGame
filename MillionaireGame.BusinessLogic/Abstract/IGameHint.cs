﻿using MillionaireGame.Entities;

namespace MillionaireGame.BusinessLogic.Abstract
{
    public interface IGameHint
    {
        /// <summary>
        /// Implements fifty on fifty hint.
        /// </summary>
        /// <param name="question">Initial question</param>
        /// <returns>Question that contains only two titled answers, other should be empty string</returns>
        Question FiftyPercentsHint(Question question);

        /// <summary>
        /// Implements friend call hint
        /// </summary>
        /// <param name="question">Initial question</param>
        /// <param name="playerName">Current player name</param>
        /// <param name="recipient">Email, phone or other information to contact with friend</param>
        void FriendCallHint(Question question, string playerName, string recipient);

        // TODO: Add hall help hint
    }
}
