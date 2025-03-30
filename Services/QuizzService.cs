using ONLINEEXAMINATION.API.Models.DBModel;
using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;
using ONLINEEXAMINATION.API.Repositorys.Interface;
using ONLINEEXAMINATION.API.Services.Interface;

namespace ONLINEEXAMINATION.API.Services
{
    public class QuizzService : IQuizzService
    {
        private readonly IQuizzRepository _quizzRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserQuizRepository _userQuizRepository;
        private readonly IQuestionService _questionService;
        public QuizzService(IQuizzRepository quizzRepository, IAdminRepository adminRepository, 
            IUserRepository userRepository , IUserQuizRepository userQuizRepository
           , IQuestionService questionService)
        {
            _quizzRepository = quizzRepository;
            _adminRepository = adminRepository;
            _userRepository = userRepository;
            _userQuizRepository = userQuizRepository;
            _questionService = questionService;
        }

        public int Create(QuizzRequest quizzRequest)
        {
            ValidateQuizz(quizzRequest);
            var quizz = new Quizz()
            {
                Title = quizzRequest.Title,
                Description = quizzRequest.Description,
                StartTime = quizzRequest.StartTime,
                EndTime = quizzRequest.EndTime,
                AdminId = quizzRequest.AdminId,
                Password = quizzRequest.Password,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            return _quizzRepository.Create(quizz);
        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id");
            }
            if(_quizzRepository.GetById(id) == null)
            {
                throw new ArgumentException("quizz not found");
            }
            _quizzRepository.Delete(id);
        }
        public int QuizsAttemptedByUser(int id, int userId)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Invalid id");
            }
            var quizz = _quizzRepository.GetById(id);
            if (quizz == null)
            {
                throw new ArgumentException("quizz not found");
            }
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid id");
            }
            var user = _userRepository.GetById(userId);
            int score = _userQuizRepository.GetScore(id, userId);
            if(user == null)
            {
                throw new ArgumentException("Invalid user");
            }
            return _userQuizRepository.QuizsAttemptedByUser(id, userId, score);

        }

        public IList<QuizzResponse> Get()
        {
            var quizzes = new List<QuizzResponse>();
            foreach(var quizz in _quizzRepository.Get())
            {
                var admin = _adminRepository.GetById(quizz.AdminId);
                quizzes.Add(new QuizzResponse()
                {
                    Id = quizz.Id,
                    Title = quizz.Title,
                    Description = quizz.Description,
                    Password = quizz.Password,
                    StartTime = quizz.StartTime,
                    EndTime = quizz.EndTime,
                    CreatedAt = quizz.CreatedAt,
                    UpdatedAt = quizz.UpdatedAt,

                });
            }
            return quizzes;
        }

        public IList<AdminQuizResponse> GetByAdminId(int AdminId)
        {
            if(AdminId <= 0)
            {
                throw new ArgumentException("AdminId Invalid");
            }
            IList<Quizz> quizzes = _quizzRepository.GetByAdminId(AdminId);
            IList<AdminQuizResponse> adminQuizzes = new List<AdminQuizResponse>();
            foreach(var quiz in quizzes)
            {
                QuizzResponse quizzResponse = GetById(quiz.Id);

                int passedUsers = 0;
                foreach(var user in quizzResponse.Users)
                {
                    int value = (int)(((double)user.Score / quizzResponse.QuestionCount) * 100);
                    if (value >= 50)
                    {
                        passedUsers++;
                    }
                }

                int accuracy = 0;
                if(quizzResponse.Users.Count > 0)
                    accuracy = (int)(((double)passedUsers / quizzResponse.Users.Count) * 100);
                adminQuizzes.Add(new AdminQuizResponse()
                {
                    Id = quiz.Id,
                    Title = quiz.Title,
                    Description = quiz.Description,
                    Password = quiz.Password,
                    StartTime = quiz.StartTime,
                    EndTime = quiz.EndTime,
                    CreatedAt = quiz.CreatedAt,
                    Users = quizzResponse.Users,
                    questionCount = quizzResponse.QuestionCount,
                    UpdatedAt = quiz.UpdatedAt,
                    UserCount = quizzResponse.Users.Count,
                    accuracy = accuracy,
                });
            }

            return adminQuizzes;
        }

        public QuizzResponse GetById(int Id)
        {
            if(Id <= 0)
            {
                throw new ArgumentException("id is invalid");
            }
            var quizz = _quizzRepository.GetById(Id);
            if(quizz == null)
            {
                throw new ArgumentException("quizz not found");
            }
            var admin = _adminRepository.GetById(quizz.AdminId);
            int questionCount = _questionService.GetCount(Id);

            IList<UserQuiz> userQuizzes = _userQuizRepository.GetQuizUsers(quizz.Id);
            IList<UserQuizResponse> QuizUsers = new List<UserQuizResponse>();
            foreach(var uq in userQuizzes)
            {
                User user = _userRepository.GetById(uq.UserId);
                UserQuizResponse userQuizResponse = new UserQuizResponse()
                {
                    Name = user.Name,
                    Email = user.Email,
                    Score = uq.Score,
                };
                QuizUsers.Add(userQuizResponse);

            }
            return new QuizzResponse()
            {
                Id = Id,
                Title = quizz.Title,
                Description = quizz.Description,
                QuestionCount = questionCount,
                StartTime = quizz.StartTime,
                EndTime = quizz.EndTime,
                Users = QuizUsers,
                CreatedAt = quizz.CreatedAt,
                UpdatedAt = quizz.UpdatedAt,
            };
        }

        public void Update(int id, QuizzRequest quizzRequest)
        {
            
            if(id <= 0)
            {
                throw new ArgumentException("Quiz id is Invalid");
            }
            if (quizzRequest == null)
            {
                throw new ArgumentException("quiz should not be empty");
            }
            var quizz = _quizzRepository.GetById(id);
            _quizzRepository.Update(id, quizz);
        }
        private void ValidateQuizz(QuizzRequest quizzRequest)
        {
            if(quizzRequest.AdminId <= 0)
            {
                throw new ArgumentException("Invalid id");
            }
            if(string.IsNullOrEmpty(quizzRequest.Title))
            {
                throw new ArgumentException("Title shouldn't be null");
            }
            if(string.IsNullOrEmpty(quizzRequest.Description))
            {
                throw new ArgumentException("Description shouldn't be null");
            }
            if(_adminRepository.GetById(quizzRequest.AdminId) == null)
            {
                throw new ArgumentException("Admin not found");
            }
            if(string.IsNullOrEmpty(quizzRequest.Password))
            {
                throw new ArgumentException("Password shouldn't be empty");
            }
        }
    }
}
