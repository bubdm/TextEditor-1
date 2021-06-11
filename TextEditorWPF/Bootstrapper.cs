using Stylet;
using StyletIoC;
using TextEditorWPF.Pages;
using TextEditorWPF.Services;

namespace TextEditorWPF
{
    public class Bootstrapper : Bootstrapper<ShellViewModel>
    {
        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            // Configure the IoC container in here
            builder.Bind<OpenFileDialogService>().ToSelf();
            builder.Bind<FileManagerService>().ToSelf();
        }

        protected override void Configure()
        {
            // Perform any other configuration before the application starts
        }
    }
}
