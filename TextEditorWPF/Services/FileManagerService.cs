using System.IO;
using System.Threading.Tasks;
using TextEditorWPF.Models;

namespace TextEditorWPF.Services
{
    public class FileManagerService
    {
        public async Task Save(Item item)
        {
            await File.WriteAllTextAsync(item.Path, item.Content);
        }
    }
}
