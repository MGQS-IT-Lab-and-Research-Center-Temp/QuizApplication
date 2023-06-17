﻿using QuizApplication.Context;
using QuizApplication.Entities;
using QuizApplication.Repositories.Interface;
using QuizApplication.Service.Interface;

namespace QuizApplication.Repositories.Implementation
{
    public class AnswerRepository : BaseRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(QuizApplicationContext context) : base(context)
        {
        }
    }
}
