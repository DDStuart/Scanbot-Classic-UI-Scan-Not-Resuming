using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using System;

namespace Scanbot_Classic_UI_Scan_Not_Resuming
{
    internal class Program : MauiApplication
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
