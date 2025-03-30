using ONLINEEXAMINATION.API.Models.DBModel;
using ONLINEEXAMINATION.API.Models.DTO;
using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;
using ONLINEEXAMINATION.API.Repositorys;
using ONLINEEXAMINATION.API.Repositorys.Interface;
using ONLINEEXAMINATION.API.Services.Interface;
using System.Collections.Generic;

namespace ONLINEEXAMINATION.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IQuizzRepository _quizRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IOptionRepository _optionRepository;
        private readonly IUserOptionRepository _userOptionRepository;
        private readonly IQuestionService _questionService;
        private readonly IOptionService _optionService;
        private readonly IUserQuizRepository _userQuizRepository;
        public UserService(IUserRepository userRepository, IQuizzRepository quizRepository,
            IOptionRepository optionRepository, IQuestionRepository questionRepository, 
            IUserOptionRepository userOptionRepository, IQuestionService questionService, 
            IOptionService optionService, IUserQuizRepository userQuizRepository)
        {
            _userRepository = userRepository;
            _quizRepository = quizRepository;
            _questionRepository = questionRepository;
            _optionRepository = optionRepository;
            _userOptionRepository = userOptionRepository;
            _optionService = optionService;
            _questionService = questionService;
            _userQuizRepository = userQuizRepository;
        }

        public int Create(UserRequest request)
        {
            ValidateUser(request);
            var user = new User()
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            return _userRepository.Create(user);
        }

        public void Delete(int Id)
        {
            if(Id == 0)
            {
                throw new ArgumentException("Id shouldn't be null");
            }
            _userRepository.Delete(Id);
        }

        public IList<UserResponse> Get()
        {
            var users = _userRepository.Get();
            return users.Select(user => new UserResponse() {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            }).ToList();
        }

        public UserResponse GetById(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Id shouldn't");
            }

            var user = _userRepository.GetById(id);

            if(user == null)
            {
                throw new EntryPointNotFoundException("user not Found");
            }

            IList<QuizzResponse> quizzes = new List<QuizzResponse>();
            foreach (var quizz in _quizRepository.GetQuizzsByUserId(user.Id))
            {
                int questionCount = _questionService.GetCount(quizz.Id);
                int score = _userQuizRepository.GetScore(quizz.Id, user.Id);
                int accuracy = (int)(((double)score / questionCount) * 100);
                quizzes.Add(new QuizzResponse()
                {
                    Id = quizz.Id,
                    Title = quizz.Title,
                    Description = quizz.Description,
                    QuestionCount = questionCount,
                    StartTime = quizz.StartTime,
                    EndTime = quizz.EndTime,
                    Accuracy = accuracy
                });
            }
            return new UserResponse()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Quizzes = quizzes,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
            };
        }

        public void Update(int Id, UserRequest request)
        {
            if (request == null) { throw new ArgumentException("request should not be null"); }

            if (Id <= 0)
            {
                throw new ArgumentException("id shouldn't be null");
            }
            var user = _userRepository.GetById(Id);
            if (user == null)
            {
                throw new ArgumentException("object shouldn't be null");
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                user.Name = request.Name;
            }
            if (!string.IsNullOrEmpty(request.Email))
            {
                user.Email = request.Email;
            }
            if (!string.IsNullOrEmpty(request.Password))
            {
                user.Password = request.Password;
            }
            user.UpdatedAt = DateTime.Now;
            _userRepository.Update(Id, user);
        }

        private void ValidateUser(UserRequest request)
        {
            if(request == null)
            {
                throw new ArgumentException("User shouldn't be null");
            }
            if(string.IsNullOrEmpty(request.Name))
            {
                throw new ArgumentException("Name shouln't be null");
            }
            if(string.IsNullOrEmpty(request.Email))
            {
                throw new ArgumentException("Email shouldn't be null");
            }
            if(string.IsNullOrEmpty(request.Password))
            {
                throw new ArgumentException("Password shouldn't be null");
            }
        }
        public int UserQuestionOption(int id, int questionId, int optionId, int quizId)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Question Id");
            }
            var question = _questionRepository.GetById(questionId);
            if (question == null)
            {
                throw new ArgumentException("Invalid Quiz Id");
            }
            var option = _optionRepository.GetById(optionId);
            if (option == null)
            {
                throw new ArgumentException("Invalid User Id");
            }
            return _userOptionRepository.UserQuestionOption(id, questionId, optionId, quizId);
        }

        public void CheckUser(LoginDTO user)
        {
            int id = _userRepository.checkUser(user);
            if (id <= 0)
            {
                throw new ArgumentNullException("login not found");
            }
            
        }

        public IList<UserQuestionResponse> GetUserQuestionOption(int id, int quizId)
        {
            IList<UserOption> userOptions = _userOptionRepository.GetUserQuestionOptions(id, quizId);

            if(quizId <= 0)
            {
                throw new EntryPointNotFoundException("quizId should not be zero or less than zero");   
            }
            Quizz quizz = _quizRepository.GetById(quizId);
            string quizName = quizz.Title;
            IList<UserQuestionResponse> userQuestionResponse = new List<UserQuestionResponse>();
            foreach (var userOption in userOptions )
            {
                QuestionResponse questionResponse = _questionService.GetById(userOption.QuestionId);
                OptionResponse optionResponse = _optionService.GetById(userOption.OptionId);
                
                userQuestionResponse.Add(new UserQuestionResponse()
                {
                    Id = questionResponse.Id,
                    text = questionResponse.Text,
                    Options = questionResponse.Options,
                    selectedOption = optionResponse.Text,
                });

            }
            return userQuestionResponse;
        }

        public int GetQuizForUser(int Id, string Password)
        {
            if(Id <= 0)
            {
                throw new ArgumentException("Password shouldn't be null");
            }
            if (string.IsNullOrEmpty(Password))
            {
                throw new ArgumentException("Password shouldn't be null");
            }
            int id = _quizRepository.CheckQuizCredintals(Id, Password);
            if(id <= 0)
            {
                throw new ArgumentException("Wrong Credentials");
            }
            return id;
        }
    }
}
