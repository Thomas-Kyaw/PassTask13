using System;
using SplashKitSDK;

namespace PassTask13
{
    public abstract class User
    {
        internal string username;
        internal string email;
        internal string password;
        
        protected User() { }
        public User(string Username, string Email, string Password)
        {
            username = Username;
            email = Email;
            password = Password;
        }

        public virtual bool Login()
        {
            // Add logic here or in the derived classes.
            return false;
        }

        public void SetDetails(string Username, string Email, string Password)
        {
            username = Username;
            email = Email;
            password = Password;
        }
    }
}
