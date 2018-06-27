using System;
using System.Collections.Generic;
using System.Linq;
using StudyJWT_on_ASPNETCore.Model;

namespace StudyJWT_on_ASPNETCore.Database
{
    public static class Database
    {
        public static List<User> UserDatabase { get; set; } = new List<User>();
    }
}