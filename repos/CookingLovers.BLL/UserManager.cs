using CookingLovers.DAL;
using CookingLovers.Models;
using System;
using System.Collections.Generic;

namespace CookingLovers.BLL
{
    public class UserManager
    {
        private UserRepository userRepo = new UserRepository();

        // Đăng nhập
        public User GetUserByUsername(string username)
        {
            return userRepo.GetUserByUsername(username);
        }

        // Đăng ký người dùng
        public void AddUser(User user)
        {
            userRepo.AddUser(user);
        }

        // Kiểm tra trùng tên đăng nhập
        public bool UsernameExists(string username)
        {
            return userRepo.UsernameExists(username);
        }

        // Lấy tất cả user (cho admin xem)
        public List<User> GetAllUsers()
        {
            return userRepo.GetAllUsers();
        }

        // Xoá người dùng (chỉ từ admin panel)
        public void DeleteUser(int userId)
        {
            userRepo.DeleteUser(userId);
        }

        // Cập nhật role (admin <-> user)
        public void UpdateUserRole(int userId, string newRole)
        {
            userRepo.UpdateUserRole(userId, newRole);
        }

        public User Login(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}