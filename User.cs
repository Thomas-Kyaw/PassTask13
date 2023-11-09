using System;
using SplashKitSDK;

namespace PassTask13
{
    public abstract class User
    {
        /// <summary>
        /// Name of the User
        /// </summary>
        internal string username;
        /// <summary>
        /// Unique Email of the user
        /// </summary>
        internal string email;
        /// <summary>
        /// authentication password of the user
        /// </summary>
        internal string password;
        /// <summary>
        /// empty constructor for the User
        /// </summary>
        protected User() { }
        /// <summary>
        /// normal constructor for the User
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        public User(string Username, string Email, string Password)
        {
            username = Username;
            email = Email;
            password = Password;
        }
        /// <summary>
        /// this is for testing purposes. works like a constructor
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        public void SetDetails(string Username, string Email, string Password)
        {
            username = Username;
            email = Email;
            password = Password;
        }
    }
}
