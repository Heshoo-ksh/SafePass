
namespace SafePass.Services
{
    public class AuthState
    {
        public bool IsAuthenticated { get; private set; }
        public string? UserName { get; private set; }

        public event Action? OnAuthStateChanged;

        public void LogIn(string userName)
        {
            IsAuthenticated = true;
            UserName = userName;
            NotifyAuthStateChanged();
        }

        public void LogOut()
        {
            IsAuthenticated = false;
            UserName = null;
            NotifyAuthStateChanged();
        }

        private void NotifyAuthStateChanged() => OnAuthStateChanged?.Invoke();
    }
}

