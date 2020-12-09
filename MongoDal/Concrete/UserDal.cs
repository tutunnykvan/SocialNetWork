using MongoDal.Interface;
using MongoDB.Driver;
using MongoDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MongoDal.Concrete
{
    public class UserDal : IUserDal
    {
        private readonly string _conn;
        private readonly string DataName;

        public UserDal(string conn,string Name)
        {
            this._conn = conn;
            this.DataName = Name;
        }

        public UserDTO Create(UserDTO user)
        {
            try
            {
                var Users = DB.GetCollection<UserDTO>("users");
                var client = new MongoClient(_conn);
                var DB = client.GetDatabase(DataName);
                

                user.UserPassword = Convert.ToBase64String(hash(user.UserPassword, "qwerty"));

                var count_id = Users.CountDocuments(p => p.UserId >= 0);
                user.UserId = (int)count_id + 1;

                Users.InsertOne(user);
                return user;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public List<UserDTO> GetAllUsers()
        {
            try
            {
                var client = new MongoClient(_conn);
                var db = client.GetDatabase(DataName);
                var users = db.GetCollection<UserDTO>("Users");

                var all_users = users.Find(p => p.UserId >= 0).ToList();
                return all_users;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


        private byte[] hash(string password, string salt)
        {
            var alg = SHA512.Create();
            return alg.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
        }

        public void Delete(int id)
        {
            try
            {
                var client = new MongoClient(_conn);
                var db = client.GetDatabase(DataName);
                var users = db.GetCollection<UserDTO>("clients");
                users.DeleteOne(p => p.UserId == id);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }



        public bool Login(string Login, string Password)
        {
            try
            {
                var user = this.GetByLogin(Login);
                if (Convert.ToBase64String(hash(Password, "1jdkskjns")) == user.UserPassword)
                {
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (Exception exp)
            {
                throw exp;

            }
        }

        public UserDTO GetById(int id)
        {
            try
            {
                var client = new MongoClient(_conn);
                var db = client.GetDatabase(DataName);
                var users = db.GetCollection<UserDTO>("Users");

                
                var founded = users.Find(p => p.UserId == id).Single();
                return founded;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public UserDTO GetByLogin(string login)
        {
            try
            {
                var client = new MongoClient(_conn);
                var db = client.GetDatabase(DataName);
                var users = db.GetCollection<UserDTO>("Users");
                var founded = users.Find(p => p.UserLogin == login).Single();
                return founded;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


        public UserDTO UpdateUser(UserDTO user)
        {
            try
            {
                var client = new MongoClient(_conn);
                var db = client.GetDatabase(DataName);
                var users = db.GetCollection<UserDTO>("Users");

                var UpdateFilter = Builders<UserDTO>.Update.Set("UserId", user.UserId);
                UpdateFilter = UpdateFilter.Set("Interests", user.Interests);
                UpdateFilter = UpdateFilter.Set("Email", user.Email);
                UpdateFilter = UpdateFilter.Set("UserLogin", user.UserLogin); 
                UpdateFilter = UpdateFilter.Set("UserPassword", user.UserPassword); 
                UpdateFilter = UpdateFilter.Set("UserName", user.UserName); 
                UpdateFilter = UpdateFilter.Set("UserLastName", user.UserLastName); 


                users.UpdateOne(g => g.UserId == user.UserId, UpdateFilter);
                var res = users.Find(p => p.UserId == user.UserId).Single();
                return res;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}
