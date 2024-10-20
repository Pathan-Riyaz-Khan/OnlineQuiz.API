using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;
using ONLINEEXAMINATION.API.Repositorys.Interface;
using ONLINEEXAMINATION.API.Services.Interface;

namespace ONLINEEXAMINATION.API.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuestionRepository _QuestionRepository;
        public AnswerService(IAnswerRepository answerRepository, IQuestionRepository questionRepository)
        {
            _answerRepository = answerRepository;
            _QuestionRepository = questionRepository;
        }

        public int Create(int QuestionId, AnswerRequest request)
        {
            ValidateAnswers(QuestionId, request);
            return _answerRepository.Create(QuestionId, request);
        }

        public void Delete(int Id)
        {
            if(Id <= 0)
            {
                throw new ArgumentException("Invalid Id");
            }
            _answerRepository.Delete(Id);
        }

        public IList<AnswerResponse> Get(int QuestionId)
        {
            if(QuestionId <= 0)
            {
                throw new ArgumentException("Invalid questionId");
            }
            return _answerRepository.Get(QuestionId);
        }

        public AnswerResponse GetById(int Id)
        {
            if(Id <= 0)
            {
                throw new ArgumentException("Invalid id");
            }
            return _answerRepository.GetById(Id);
        }

        public void Update(int QuestionId, int id, AnswerRequest request)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Invalid id");
            }
            ValidateAnswers(QuestionId, request);
            _answerRepository.Update(QuestionId, id, request);
        }
        
        private void ValidateAnswers(int QuestionId, AnswerRequest request)
        {
            if(request == null)
            {
                throw new ArgumentNullException("Invalid object");
            }
            if(string.IsNullOrEmpty(request.AnswerText))
            {
                throw new ArgumentException("AnswerText shouldn't be null");
            }
            if(QuestionId <= 0)
            {
                throw new ArgumentException("Invalid Id");
            }
            if(_QuestionRepository.GetById(QuestionId) == null)
            {
                throw new ArgumentException("Question Id not found");
            }
        }
    }
}
