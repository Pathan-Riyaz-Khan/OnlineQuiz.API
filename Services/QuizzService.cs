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
        public QuizzService(IQuizzRepository quizzRepository, IAdminRepository adminRepository, IUserRepository userRepository , IUserQuizRepository userQuizRepository)
        {
            _quizzRepository = quizzRepository;
            _adminRepository = adminRepository;
            _userRepository = userRepository;
            _userQuizRepository = userQuizRepository;
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
                    StartTime = quizz.StartTime,
                    EndTime = quizz.EndTime,
                    Admin = new AdminResponse()
                    {
                        Id = admin.Id,
                        Name = admin.Name,
                        Email = admin.Email,
                    },
                    CreatedAt = quizz.CreatedAt,
                    UpdatedAt = quizz.UpdatedAt,

                });
            }
            return quizzes;
        }

        public IList<QuizzResponse> GetByAdminId(int AdminId)
        {
            if(AdminId <= 0)
            {
                throw new ArgumentException("AdminId Invalid");
            }
            return _quizzRepository.GetByAdminId(AdminId).Select(quiz => new QuizzResponse()
            {
                Id = quiz.Id,
                Title = quiz.Title,
                Description = quiz.Description,
                StartTime = quiz.StartTime,
                EndTime = quiz.EndTime,
                CreatedAt = quiz.CreatedAt,
                UpdatedAt = quiz.UpdatedAt,
            }).ToList();
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
                StartTime = quizz.StartTime,
                EndTime = quizz.EndTime,
                Admin = new AdminResponse()
                {
                    Id = admin.Id,
                    Name = admin.Name,
                    Email = admin.Email,
                },
                Users = QuizUsers,
                CreatedAt = quizz.CreatedAt,
                UpdatedAt = quizz.UpdatedAt,
            };
        }

        public void Update(int AdminId, int id, QuizzRequest quizzRequest)
        {
            if(AdminId <= 0)
            {
                throw new ArgumentException("AdminId should not be less or equal to zero");
            }
            if(id <= 0)
            {
                throw new ArgumentException("Quiz id is Invalid");
            }
            if (quizzRequest == null)
            {
                throw new ArgumentException("quiz should not be empty");
            }
            var quizz = _quizzRepository.GetById(id);
            _quizzRepository.Update(AdminId, id, quizz);
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
