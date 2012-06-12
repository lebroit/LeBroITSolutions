using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using LebroITSolutions.ChatModel;
using WPFChatProgram.Components;

namespace WPFChatProgram
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public NotifyIconWrapper Component;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ShutdownMode = ShutdownMode.OnExplicitShutdown;
            EnsureApplicationResources();
            Component = new NotifyIconWrapper();
            Properties["Component"] = Component;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            var messages = new ErrorMessages();
            
            if (Globals.CurrentExceptionsCollection.Count > 0)
            {
                messages.AddRange(
                    Globals.CurrentExceptionsCollection.Select(exception => new ErrorMessage
                        (exception, Assembly.GetExecutingAssembly())
                        {
                            CurrentUser = Globals.MainUser.UserName,
                            UsesWebservice = Globals.MainUser.WebserviceUsed
                        }));
                messages.WriteExceptions2File(null, true);
            }

            base.OnExit(e);
            Component.Dispose();
        }


        /// <summary>
        /// Because of the use of the NotifyIcon component de App.Resources aren't created
        /// By adding the resources through a resource dictionary this is solved
        /// for the entire application
        /// detailled information can be found here: 
        /// http://drwpf.com/blog/2007/10/05/managing-application-resources-when-wpf-is-hosted/
        /// </summary>
        public static void EnsureApplicationResources()
        {
            // merge in your application resources
            if (Current != null)
                Current.Resources.MergedDictionaries.Add(
                    LoadComponent(new Uri("Resources/ChatGUIDictionary.xaml",
                                  UriKind.Relative)) as ResourceDictionary);
          
        }
    }
}
