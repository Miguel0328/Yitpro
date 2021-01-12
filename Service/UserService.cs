using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Persistence.Models;
using Resources.Constants;
using Persistence;
using Repository.Interfaces;
using Resources.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Resources.Extension;
using Resources.Reports;

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

        public async Task<List<UserDTO>> Get(UserFilterDTO filter)
        {
            var users = await _user_.Get(filter);
            return _mapper.Map<List<UserDTO>>(users);
        }       
        
        public async Task<UserDTO> Get(long id)
        {
            var user = await _user_.GetDetail(id);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDetailDTO> GetDetail(long id)
        {
            var user = await _user_.GetDetail(id);
            return _mapper.Map<UserDetailDTO>(user);
        }

        public async Task<long> Post(UserDetailDTO _user)
        {
            if (_user.Photo != null)
                _user.PhotoUrl = await SavePhoto(_user.Photo, _user.EmployeeNumber);
            var user = _mapper.Map<UserModel>(_user);
            return await _user_.Post(user);
        }

        public async Task<bool> Put(UserDetailDTO _user)
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
            var permissions = _mapper.Map<List<UserPermissionModel>>(_permissions);
            return await _user_.PutPermissions(permissions);
        }

        private async Task<string> SavePhoto(IFormFile file, string fileName)
        {
            var path = _service.GetBasePath();
            Directory.CreateDirectory(System.IO.Path.Combine(path, Resources.Constants.Path.UserPhotos));
            var name = System.IO.Path.Combine(Resources.Constants.Path.UserPhotos, $"{fileName}.jpg");
            var url = System.IO.Path.Combine(path, name);

            using Stream stream = new FileStream(url, FileMode.Create);
            await file.CopyToAsync(stream);

            return name;
        }

        public async Task<byte[]> Download()
        {
            var users = await _user_.Download();
            var file = users.ToTable("Usuarios").ToExcel();

            return file;
        }
    }
}
