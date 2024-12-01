namespace SafePass.Shared
{
     public class PasswordBuilder
     {
          private int length;
          private bool includeUppercase;
          private bool includeLowercase;
          private bool includeNumbers;
          private bool includeSpecialChars;

          public PasswordBuilder SetLength(int length)
          {
               this.length = length;
               return this;
          }

          public PasswordBuilder IncludeUppercase()
          {
               includeUppercase = true;
               return this;
          }

          public PasswordBuilder IncludeLowercase()
          {
               includeLowercase = true;
               return this;
          }

          public PasswordBuilder IncludeNumbers()
          {
               includeNumbers = true;
               return this;
          }

          public PasswordBuilder IncludeSpecialChars()
          {
               includeSpecialChars = true;
               return this;
          }

          public string Build()
          {
               var charPool = new List<char>();
               var requiredChars = new List<char>();
               var random = new Random();

               if (includeUppercase)
               {
                    var uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    charPool.AddRange(uppercase);
                    requiredChars.Add(uppercase[random.Next(uppercase.Length)]);
               }
               if (includeLowercase)
               {
                    var lowercase = "abcdefghijklmnopqrstuvwxyz";
                    charPool.AddRange(lowercase);
                    requiredChars.Add(lowercase[random.Next(lowercase.Length)]);
               }
               if (includeNumbers)
               {
                    var numbers = "0123456789";
                    charPool.AddRange(numbers);
                    requiredChars.Add(numbers[random.Next(numbers.Length)]);
               }
               if (includeSpecialChars)
               {
                    var specialChars = "!@#$%^&*()-_=+[]{}|;:,.<>?";
                    charPool.AddRange(specialChars);
                    requiredChars.Add(specialChars[random.Next(specialChars.Length)]);
               }

               if (charPool.Count == 0)
                    throw new InvalidOperationException("At least one character set must be included.");

               int remainingLength = length - requiredChars.Count;
               if (remainingLength < 0)
                    throw new ArgumentException("Length is too short for the selected character sets.");

               var passwordChars = new List<char>(length);
               passwordChars.AddRange(requiredChars);

               using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
               var bytes = new byte[sizeof(uint)];

               // Fill the remaining length with random characters from the full pool
               for (int i = 0; i < remainingLength; i++)
               {
                    rng.GetBytes(bytes);
                    uint num = BitConverter.ToUInt32(bytes, 0);
                    passwordChars.Add(charPool[(int)(num % (uint)charPool.Count)]);
               }

               // Shuffle the password characters
               var shuffledPassword = passwordChars.OrderBy(x => random.Next()).ToArray();

               return new string(shuffledPassword);
          }

     }
}
