using System;
using System.Collections.Generic;
using System.Linq;
using MudBlazor;

public interface IPasswordSubject
{
     void Attach(IPasswordObserver observer);
     void Detach(IPasswordObserver observer);
     void Notify(string password);
}

public interface IPasswordObserver
{
     void Update(string password);
}

public class PasswordStrengthSubject : IPasswordSubject
{
     private List<IPasswordObserver> _observers = new List<IPasswordObserver>();

     public void Attach(IPasswordObserver observer)
     {
          _observers.Add(observer);
     }

     public void Detach(IPasswordObserver observer)
     {
          _observers.Remove(observer);
     }

     public void Notify(string password)
     {
          foreach (var observer in _observers)
          {
               observer.Update(password);
          }
     }
}

public class PasswordStrengthObserver : IPasswordObserver
{
     private readonly Action<string, Severity> _onPasswordEvaluation;

     public PasswordStrengthObserver(Action<string, Severity> onPasswordEvaluation)
     {
          _onPasswordEvaluation = onPasswordEvaluation;
     }

     public void Update(string password)
     {
          var (message, severity) = EvaluatePasswordStrength(password);
          _onPasswordEvaluation(message, severity);
     }

     private (string message, Severity severity) EvaluatePasswordStrength(string password)
     {
          var suggestions = new List<string>();
          if (string.IsNullOrEmpty(password) || password.Length < 12)
          {
               suggestions.Add("Consider using at least 12 characters");
          }
          if (!password.Any(char.IsUpper))
          {
               suggestions.Add("Include uppercase letters");
          }
          if (!password.Any(char.IsLower))
          {
               suggestions.Add("Include lowercase letters");
          }
          if (!password.Any(char.IsDigit))
          {
               suggestions.Add("Include numbers");
          }
          if (!password.Any(c => !char.IsLetterOrDigit(c)))
          {
               suggestions.Add("Include special characters");
          }

          if (suggestions.Count == 0)
          {
               return ("Strong password!", Severity.Success);
          }
          else if (password.Length < 8)
          {
               var sentenceSuggestions = string.Join(", ", suggestions.Take(suggestions.Count - 1))
                           + (suggestions.Count > 1 ? ", and " : "")
                           + suggestions.Last();
               var message = $"Very weak password! {sentenceSuggestions}.";
               return (message, Severity.Error);
          }
          else
          {
               var sentenceSuggestions = string.Join(", ", suggestions.Take(suggestions.Count - 1))
                                          + (suggestions.Count > 1 ? ", and " : "")
                                          + suggestions.Last();
               var message = $"Password strength can be improved: {sentenceSuggestions}.";
               return (message, Severity.Warning);

          }
     }
}

public static class PasswordStrengthNotifier
{
     public static void ShowNotification(ISnackbar snackbar, string message, Severity severity)
     {
          snackbar.Add(message, severity, config =>
          {
               config.VisibleStateDuration = 10000; // Display for 10 seconds
               config.ShowCloseIcon = true;
     });
     }
}