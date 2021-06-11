using Stylet;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TextEditorWPF.Models;
using TextEditorWPF.Services;

namespace TextEditorWPF.Pages
{
    public class ShellViewModel : Conductor<Screen>
    {
        public ObservableCollection<Item> OpenedFiles { get; }

        private Item selectedFile;
        public Item SelectedFile
        {
            get => selectedFile;
            set
            {
                SetAndNotify(ref selectedFile, value);
                CanSave = SelectedFile != null;
                Debug.WriteLine(selectedFile.Content == value.Content);

                if (value != null) value.IsSaved = false;
            }
        }

        private bool canSave;
        public bool CanSave { get => canSave; set => SetAndNotify(ref canSave, value); }

        private IWindowManager windowManager;
        private OpenFileDialogService fileDialogService;
        private FileManagerService fileManagerService;
        public ShellViewModel(IWindowManager windowManager, OpenFileDialogService fileDialogService,
                              FileManagerService fileManagerService)
        {
            this.windowManager = windowManager;
            this.fileDialogService = fileDialogService;
            this.fileManagerService = fileManagerService;

            this.OpenedFiles = new ObservableCollection<Item>();
        }

        public async override Task<bool> CanCloseAsync()
        {
            return await SaveAllChanges();
        }

        // Returns whether the application would close or not.
        public async Task<bool> SaveAllChanges()
        {
            if (OpenedFiles.Count < 1)
                return true;

            List<Item> editedFiles = new List<Item>();
            foreach (Item item in OpenedFiles)
            {
                if (!item.IsSaved)
                    editedFiles.Add(item);
            }

            if (editedFiles.Count > 0)
            {
                var result = windowManager.ShowMessageBox("You've unsaved changes, do you want to save all files?",
                                                                       "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        foreach (Item item in editedFiles)
                            await fileManagerService.Save(item);
                        return true;

                    case MessageBoxResult.No:
                        return true;
                }
            }
            return false;
        }

        public async void OpenFile()
        {
            var newItem = await fileDialogService.Open();

            if (newItem != null && !OpenedFiles.Any(item => item.Path == newItem.Path))
                OpenedFiles.Add(newItem);
        }

        public async void SaveFile()
        {
            await fileManagerService.Save(SelectedFile);
            SelectedFile.IsSaved = true;
        }

        public void Exit()
        {
            RequestClose();
        }
    }
}
