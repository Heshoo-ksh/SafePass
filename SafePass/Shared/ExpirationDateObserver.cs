using System;
using MudBlazor;

public interface IExpirationObserver
{
     void Update(string name, string expirationDate);
}


public class ExpirationDateObserver : IExpirationObserver
{
     private readonly Action<string, Severity> _onExpirationEvaluation;

     public ExpirationDateObserver(Action<string, Severity> onExpirationEvaluation)
     {
          _onExpirationEvaluation = onExpirationEvaluation;
     }

     public void Update(string name, string expirationDate)
     {
          var (message, severity) = EvaluateExpirationDate(expirationDate, name);
          _onExpirationEvaluation(message, severity);
     }

     private (string message, Severity severity) EvaluateExpirationDate(string expirationDateStr, string itemName)
     {
          if (!DateTime.TryParse(expirationDateStr, out DateTime expirationDate))
          {
               var message = $"Item '{itemName}': Invalid expiration date format.";
               return (message, Severity.Error);
          }

          var now = DateTime.Now.Date;
          if (expirationDate.Date < now)
          {
               var message = $"Item '{itemName}': Has expired!";
               return (message, Severity.Error);
          }
          else if (expirationDate.Date <= now.AddMonths(1))
          {
               var daysLeft = (expirationDate.Date - now).Days;
               var message = $"Item '{itemName}': Expires in {daysLeft} day(s).";
               return (message, Severity.Warning);
          }
          else
          {
               // You can choose not to show a notification if the expiration date is okay
               return (string.Empty, Severity.Normal);
          }
     }
}

public interface IExpirationSubject
{
     void Attach(IExpirationObserver observer);
     void Detach(IExpirationObserver observer);
     void Notify(string name, string expirationDate);
}

public class ExpirationDateSubject : IExpirationSubject
{
     private List<IExpirationObserver> _observers = new List<IExpirationObserver>();

     public void Attach(IExpirationObserver observer)
     {
          _observers.Add(observer);
     }

     public void Detach(IExpirationObserver observer)
     {
          _observers.Remove(observer);
     }

     public void Notify(string name, string expirationDate)
     {
          foreach (var observer in _observers)
          {
               observer.Update(name, expirationDate);
          }
     }
}

public static class NotificationHelper
{
     public static void ShowNotification(ISnackbar snackbar, string message, Severity severity)
     {
          if (string.IsNullOrEmpty(message))
               return; // Do not show notification if message is empty

          snackbar.Add(message, severity, config =>
          {
               config.VisibleStateDuration = 10000; // Display for 10 seconds
               config.ShowCloseIcon = true;
          });
     }
}