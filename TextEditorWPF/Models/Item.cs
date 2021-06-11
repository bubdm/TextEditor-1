using Stylet;

namespace TextEditorWPF.Models
{
    // Represents a file.
    public class Item : PropertyChangedBase
    {
        public string Path { get; init; }
        public string Name { get; init; }

        private string content;
        public string Content { get => content; set => SetAndNotify(ref content, value); }

        private bool isSaved = true;
        public bool IsSaved { get => isSaved; set => SetAndNotify(ref isSaved, value); }
    }
}
