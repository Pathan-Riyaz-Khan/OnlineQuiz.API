using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;
using ONLINEEXAMINATION.API.Repositorys.Interface;
using ONLINEEXAMINATION.API.Services.Interface;
using ONLINEEXAMINATION.API.Models.DBModel;

namespace ONLINEEXAMINATION.API.Services
{
    public class OptionService : IOptionService
    {
        private readonly IOptionRepository _optionRepository;
        private readonly IQuestionRepository _QuestionRepository;
        public OptionService(IOptionRepository optionRepository, IQuestionRepository questionRepository)
        {
            _optionRepository = optionRepository;
            _QuestionRepository = questionRepository;
        }

        public int Create(int QuestionId, OptionRequest request)
        {
            ValidateAnswers(QuestionId, request);
            var option = new Option()
            {
                Text = request.Text,
                IsCorrect = request.IsCorrect,
                QuestionId = QuestionId,
            };
            return _optionRepository.Create(QuestionId, option);
        }

        public void Delete(int Id)
        {
            if(Id <= 0)
            {
                throw new ArgumentException("Invalid Id");
            }
            _optionRepository.Delete(Id);
        }

        public IList<OptionResponse> Get(int QuestionId)
        {
            if(QuestionId <= 0)
            {
                throw new ArgumentException("Invalid questionId");
            }
            return _optionRepository.Get(QuestionId).Select(option => new OptionResponse() {
                Id = option.Id,
                Text = option.Text,
            }).ToList();
        }

        public OptionResponse GetById(int Id)
        {
            if(Id <= 0)
            {
                throw new ArgumentException("Invalid id");
            }
            var option = _optionRepository.GetById(Id);
            return new OptionResponse()
            {
                Id = option.Id,
                Text = option.Text,
                
            };
        }

        public void Update(int QuestionId, int id, OptionRequest request)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Invalid id");
            }
            ValidateAnswers(QuestionId, request);
            var option = new Option()
            {
                Text = request.Text,
                IsCorrect = request.IsCorrect,
                QuestionId = QuestionId,
            };
            _optionRepository.Update(QuestionId, id, option);
        }
        
        private void ValidateAnswers(int QuestionId, OptionRequest request)
        {
            if(request == null)
            {
                throw new ArgumentNullException("Invalid object");
            }
            if(string.IsNullOrEmpty(request.Text))
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
