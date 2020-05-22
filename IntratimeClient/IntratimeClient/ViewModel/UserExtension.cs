using Xamarin.Essentials;

namespace IntratimeClient.ViewModel
{
    public static class UserExtension
    {
        private static readonly string UserName = "user";
        private static readonly string UserKey = "key";
        private static readonly string UserToken = "token";
        private static readonly string UserRemember = "remember";

        public static void Save(this User user)
        {
            if (user.RememberCredentials)
            {
                SecureStorage.SetAsync(UserName, user.Email);
                SecureStorage.SetAsync(UserKey, user.Password);
                SecureStorage.SetAsync(UserToken, user.Token);
            }
            else
            {
                SecureStorage.SetAsync(UserName, string.Empty);
                SecureStorage.SetAsync(UserKey, string.Empty);
                SecureStorage.SetAsync(UserToken, string.Empty);
            }

            SecureStorage.SetAsync(UserRemember, user.RememberCredentials.ToString().ToLower());
        }

        public static User Get()
        {
            var userName = SecureStorage.GetAsync(UserName).Result;
            var password = SecureStorage.GetAsync(UserKey).Result;
            var token = SecureStorage.GetAsync(UserToken).Result;
            var remember = SecureStorage.GetAsync(UserRemember).Result;

            var rememberBool = !string.IsNullOrEmpty(remember) && remember.ToLower() == "true"; 

            return new User() { Email = userName, Password = password, Token = token, RememberCredentials = rememberBool};
        }
    }
}