using System.Collections.Generic;
using System.Linq;
using DatingApp.API.Data;
using DatingApp.API.Models;
using Newtonsoft.Json;

namespace DatingApp_API.Data
{
    public class Seed
    {
        public static void SeedUsers(DataContext context){
            if(!context.Users.Any()){
                var userData= System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users= JsonConvert.DeserializeObject<List<User>>(userData);
                foreach (var user in users)
                {
                    byte[] passwordhash, passwordSalt;
                    CreatePasswordHas("password",out passwordhash, out passwordSalt);
                    user.PasswordHash=passwordhash;
                    user.PasswrodSalt=passwordSalt;
                    user.Username= user.Username.ToLower();
                    context.Users.Add(user);
                }
                context.SaveChanges();
            }
        }
         private static void CreatePasswordHas(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }
    }
}