using ONLINEEXAMINATION.API.Models.DBModel;
using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;
using ONLINEEXAMINATION.API.Repositorys.Interface;
using ONLINEEXAMINATION.API.Services.Interface;

namespace ONLINEEXAMINATION.API.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public int Create(AdminRequest request)
        {
            ValidateAdmin(request);
            var admin = new Admin()
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            return _adminRepository.Create(admin);
        }

        public void Delete(int Id)
        {
            if(Id <= 0)
            {
                throw new ArgumentException("id shouldn't be null");
            }
            _adminRepository.Delete(Id);
        }

        public IList<AdminResponse> Get()
        {
            var admins =  _adminRepository.Get();
            return _adminRepository.Get().Select(admin => new AdminResponse()
            {
                Id = admin.Id,
                Name = admin.Name,
                Email = admin.Email,
                CreatedDate = admin.CreatedAt,
                UpdatedDate = admin.UpdatedAt,
            }).ToList();
        }

        public AdminResponse GetById(int id)
        {
            if( id <= 0)
            {
                throw new ArgumentException("id shouldn't be null");
            }
            var admin = _adminRepository.GetById(id);
            if( admin == null )
            {
                throw new EntryPointNotFoundException("admin doesn't exit with given Id");
            }
            return new AdminResponse()
            {
                Id = admin.Id,
                Name = admin.Name,
                Email = admin.Email,
                CreatedDate = admin.CreatedAt,
                UpdatedDate = admin.UpdatedAt,
            };
        }

        public void Update(int Id, AdminRequest request)
        {
            if (request == null) { throw new ArgumentException("request should not be null"); }

            if (Id  <= 0)
            {
                throw new ArgumentException("id shouldn't be null");
            }
            var admin = _adminRepository.GetById(Id);
            if (admin == null)
            {
                throw new ArgumentException("object shouldn't be null");
            }
            
            if(!string.IsNullOrEmpty(request.Name)){
                admin.Name = request.Name;
            }
            if (!string.IsNullOrEmpty(request.Email))
            {
                admin.Email = request.Email;
            }
            if (!string.IsNullOrEmpty(request.Password))
            {
                admin.Password = request.Password;
            }
            admin.UpdatedAt = DateTime.Now;
            _adminRepository.Update(Id, admin);
        }

        public void ValidateAdmin(AdminRequest request)
        {
            if(request == null)
            {
                throw new ArgumentException("Admin shouldn't be null");
            }
            if(request.Name == null)
            {
                throw new ArgumentException("Name shouldnt be null");
            }
            if(string.IsNullOrEmpty(request.Email))
            {
                throw new ArgumentException("Email shouldnt be null");
            }
            if(string.IsNullOrEmpty(request.Password))
            {
                throw new ArgumentException("Password shouldnt be null");
            }
        }
    }
}
