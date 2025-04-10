﻿using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;

namespace ONLINEEXAMINATION.API.Services.Interface
{
    public interface IQuizzService
    {
        IList<QuizzResponse> Get();
        QuizzResponse GetById(int Id);
        IList<AdminQuizResponse> GetByAdminId(int AdminId);
        int Create(QuizzRequest quizzRquest);
        int QuizsAttemptedByUser(int id,  int userId);
        void Update(int id, QuizzRequest quizzRquest);
        void Delete(int id);
    }
}
