using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Persistence.Models;
using Repository.Interfaces;
using Service.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUser _user_;
        private readonly IBaseService _service;

        public UserService(IMapper mapper, IUser user, IBaseService service)
        {
            _mapper = mapper;
            _service = service;
            _user_ = user;
        }

        public async Task<List<UserDTO>> Get()
        {
            var users = await _user_.Get();
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<UserDetailsDTO> Get(long id)
        {
            var user = await _user_.Get(id);
            var a = _mapper.Map<UserDetailsDTO>(user);
            return a;
        }

        public async Task<bool> Post(UserDetailsDTO _user)
        {
            if (_user.Photo != null)
                _user.PhotoUrl = await SavePhoto(_user.Photo, _user.EmployeeNumber);
            var user = _mapper.Map<UserModel>(_user);
            return await _user_.Post(user);
        } 
        
        public async Task<bool> Put(UserDetailsDTO _user)
        {
            if (_user.Photo != null)
                _user.PhotoUrl = await SavePhoto(_user.Photo, _user.EmployeeNumber);
            var user = _mapper.Map<UserModel>(_user);
            return await _user_.Put(user);
        }        
        
        public async Task<bool> PutEnabled(UserDTO _user)
        {
            var user = _mapper.Map<UserModel>(_user);
            return await _user_.PutEnabled(user);
        }

        public async Task<List<UserPermisssionDTO>> GetPermissions(long id)
        {
            var permissions = await _user_.GetPermissions(id);
            return _mapper.Map<List<UserPermisssionDTO>>(permissions);
        }

        public async Task<bool> PutPermissions(List<UserPermisssionDTO> _permissions)
        {
            var permissions = _mapper.Map<List<UserPermissionsModel>>(_permissions);
            return await _user_.PutPermissions(permissions);
        }

        private async Task<string> SavePhoto(IFormFile file, string fileName)
        {
            var path = _service.GetFilesPath();
            Directory.CreateDirectory(Path.Combine(path, Constants.Path.UserPhotos));
            var name = Path.Combine(Constants.Path.UserPhotos, $"{fileName}.jpg");
            var url = Path.Combine(path, name);

            using Stream stream = new FileStream(url, FileMode.Create);
            await file.CopyToAsync(stream);

            return name;
        }
    }
}
