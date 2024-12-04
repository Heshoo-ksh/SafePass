namespace SafePass.Services
{
    public class SessionManager
    {
        private static SessionManager? _instance;
        private static readonly object _lock = new();

        // Properties to store user session data
        public string? UserId { get; private set; }
        public string? AuthToken { get; private set; }

        // Private constructor to prevent external instantiation
        private SessionManager() { }

        // Public method to get the Singleton instance
        public static SessionManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SessionManager();
                    }
                    return _instance;
                }
            }
        }

        // Initialize the session
        public void InitializeSession(string userId, string authToken)
        {
            UserId = userId;
            AuthToken = authToken;
        }

        // Clear the session
        public void ClearSession()
        {
            UserId = null;
            AuthToken = null;
        }

        // Check if a session is active
        public bool IsActive => !string.IsNullOrEmpty(UserId);
    }
}
