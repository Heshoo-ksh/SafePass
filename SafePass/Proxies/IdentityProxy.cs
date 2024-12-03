using SafePass.Data;

namespace SafePass.Proxies
{
     public class IdentityProxy
     {
          private Identity _identity;
          public bool IsUnmasked { get; set; } = false;

          public IdentityProxy(Identity identity)
          {
               _identity = identity;
          }

          public Guid Id => _identity.Id;

          public string HolderName => _identity.HolderName;

          public string IDNumber => IsUnmasked ? _identity.IDNumber : MaskString(_identity.IDNumber);

          public string IDType => _identity.IDType;

          public DateOnly? ExpirationDate => _identity.ExpirationDate;

          public DateOnly? IssueDate => _identity.IssueDate;

          public DateOnly? DateOfBirth => _identity.DateOfBirth;

          public string Gender => _identity.Gender;

          public string Address => IsUnmasked ? _identity.Address : MaskString(_identity.Address);

          private string MaskString(string value)
          {
               if (string.IsNullOrEmpty(value))
                    return string.Empty;

               return new string('*', value.Length);
          }
     }
}
