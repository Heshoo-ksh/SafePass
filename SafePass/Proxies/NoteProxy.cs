using SafePass.Data;

namespace SafePass.Proxies
{
     public class NoteProxy
     {
          private Note _note;
          public bool IsUnmasked { get; set; } = false;

          public NoteProxy(Note note)
          {
               _note = note;
          }

          public Guid Id => _note.Id;

          public string Name => _note.Name;

          public string Content => IsUnmasked ? _note.Content : MaskContent(_note.Content);

          private string MaskContent(string content)
          {
               if (string.IsNullOrEmpty(content))
                    return string.Empty;

               return new string('*', content.Length);
          }
     }
}