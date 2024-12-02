using SafePass.Data;

namespace SafePass.Proxies
{
     public class CreditCardProxy
     {
          private CreditCard _creditCard;
          public bool IsUnmasked { get; set; } = false;

          public CreditCardProxy(CreditCard creditCard)
          {
               _creditCard = creditCard;
          }

          public Guid Id => _creditCard.Id;

          public string? Name => _creditCard.Name;

          public string? CardHolderName => IsUnmasked ? _creditCard.CardHolderName : new string('*', _creditCard.CardHolderName.Length);

          public string? Number => IsUnmasked ? _creditCard.Number : MaskCardNumber(_creditCard.Number);

          public string? CVV => IsUnmasked ? _creditCard.CVV : new string('*', _creditCard.CVV.Length);

          public string? ZipCode => IsUnmasked ? _creditCard.ZipCode : new string('*', _creditCard.ZipCode.Length);

          public DateTime? ExpirationDate => _creditCard.ExpirationDate;

          // Helper methods to mask sensitive data
          private string MaskCardNumber(string? number)
          {
               if (string.IsNullOrEmpty(number))
                    return string.Empty;

               if (number.Length <= 4)
                    return new string('*', number.Length);

               // Show only last 4 digits
               return new string('*', number.Length - 4) + number.Substring(number.Length - 4);
          }
     }
}
