using System.Collections.Generic;
using System.Linq;
using JwT.Models;

namespace JwT.Repositories
{
    public static class SpyRepository
    {
        public static Spy Get(string username, string password)
        {
            var spy = new List<Spy>();
            spy.Add(new Spy { 
                              Id = 1, 
                              Username = "Secret#1", 
                              Password = "1536764", 
                              Role = "encryptor" 
                            });
            spy.Add(new Spy { 
                              Id = 2, 
                              Username = "Secret#2", 
                              Password = "8294u73", 
                              Role = "decryptor" 
                            });
            return spy.Where(
                x => x.Username.ToLower() == username.ToLower() &&
                     x.Password == password).FirstOrDefault();
        }
    }
}