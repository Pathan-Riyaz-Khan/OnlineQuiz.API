using ONLINEEXAMINATION.API.Models.DBModel;
using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;
using ONLINEEXAMINATION.API.Repositorys;
using ONLINEEXAMINATION.API.Repositorys.Interface;
using ONLINEEXAMINATION.API.Services.Interface;

namespace ONLINEEXAMINATION.API.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuizzRepository _quizzRepository;
        private readonly IOptionRepository _optionRepository;
        private readonly IUserRepository _userRepository;

        public QuestionService(IQuestionRepository questionRepository, 
            IQuizzRepository quizzRepository, IOptionRepository optionRepository, IUserRepository userRepository)
        {
            _questionRepository = questionRepository;
            _quizzRepository = quizzRepository;
            _optionRepository = optionRepository;
            _userRepository = userRepository;
        }

        public int Create(int QuizId, QuestionRequest request)
        {
            ValidateQuestions(QuizId, request);
            
            return _questionRepository.Create(QuizId,request);
        }

        public void Delete(int Id)
        {
            if(Id <=  0)
            {
                throw new ArgumentException("Invalid id");
            }
            if(_questionRepository.Get(Id) == null)
            {
                throw new ArgumentException("Question id not found");
            }
            _questionRepository.Delete(Id);
        }

        public IList<QuestionResponse> Get(int QuizId)
        {
            if(QuizId <= 0)
            {
                throw new ArgumentException("Invalid quizId");
            }
            if (_quizzRepository.GetById(QuizId) == null)
            {
                throw new ArgumentException("Question id not found");
            }

            IList<QuestionResponse> Questions = new List<QuestionResponse>();
            foreach(var question in _questionRepository.Get(QuizId))
            {
                IList<OptionResponse> Options = new List<OptionResponse>();
                IList<Option> optionList =  _optionRepository.Get(question.Id);
                foreach(var option in optionList)
                {
                    Options.Add(new OptionResponse()
                    {
                        Id = option.Id,
                        Text = option.Text,
                        IsCorrect = option.IsCorrect
                    });
                }
                Questions.Add(new QuestionResponse()
                {
                    Id = question.Id,
                    Text=question.Text,
                    QuizId = QuizId,
                    Options = Options
                });
            }

            return Questions;
        }

        public QuestionResponse GetById(int Id)
        {
            if(Id <= 0)
            {
                throw new ArgumentException("Invalid id");
            }

            if (_questionRepository.Get(Id) == null)
            {
                throw new ArgumentException("Question id not found");
            }
            
            var question = _questionRepository.GetById(Id);
            
            IList<OptionResponse>  options = new List<OptionResponse>();
            
            foreach (var option in _optionRepository.Get(question.Id))
            {
                options.Add(new OptionResponse()
                {
                    Id = option.Id,
                    Text = option.Text,
                    IsCorrect = option.IsCorrect,
                });
            }
            
            return new QuestionResponse()
            {
                Id = question.Id,
                Text = question.Text,
                QuizId = question.QuizId,
                CreatedAt = question.CreatedAt,
                UpdatedAt = question.UpdatedAt,
                Options = options,

            };
        }

        public int GetCount(int QuizId)
        {
            return _questionRepository.GetCount(QuizId);
        }

        public void Update(int QuizId, int id, QuestionRequest request)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id");
            }
            if (_questionRepository.Get(id) == null)
            {
                throw new ArgumentException("Question id not found");
            }

            
            
            _questionRepository.Update(QuizId, id, request);
        }

        private void ValidateQuestions(int QuizId, QuestionRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Invalid object");
            }
            if (string.IsNullOrEmpty(request.Text))
            {
                throw new ArgumentException("AnswerText shouldn't be null");
            }
            if (QuizId <= 0)
            {
                throw new ArgumentException("Invalid Id");
            }
            /*if (_quizzRepository.GetById(QuizId) == null)
            {
                throw new ArgumentException("Question Id not found");
            }*/
        }
    }
}
