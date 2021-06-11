using Microsoft.Win32;
using System.IO;
using System.Threading.Tasks;
using TextEditorWPF.Models;

namespace TextEditorWPF.Services
{
    public class OpenFileDialogService
    {
        public async Task<Item> Open()
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Open File";

            if (dialog.ShowDialog().Value)
            {
                var contentText = await File.ReadAllTextAsync(dialog.FileName);
                var path = dialog.FileName;

                return new Item()
                {
                    Content = contentText,
                    Name = Path.GetFileName(path),
                    Path = path
                };
            }
            return null;
        }
    }
}
