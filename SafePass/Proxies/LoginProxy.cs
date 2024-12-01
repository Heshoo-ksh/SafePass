using SafePass.Data;

namespace SafePass.Proxies
{
     public class LoginProxy
     {
          private Login _login;
          public bool IsUnmasked { get; set; } = false;

          public LoginProxy(Login login)
          {
               _login = login;
          }

          public string? Name => _login.Name;

          public string? Username => IsUnmasked ? _login.Username : new string('*', _login.Username.Length);

          public string? Password => IsUnmasked ? _login.Password : new string('*', _login.Password.Length);

          public string? URL => _login.URL;

          public Guid Id => _login.Id;
     }
}
