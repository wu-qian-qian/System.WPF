using Panuon.UI.Silver.Core;
using Panuon.UI.Silver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace System.UI.Local.Statics.UI
{
    public  class UIShowTool
    {
        public static MessageBoxXConfigurations messageBoxConfiguration = new MessageBoxXConfigurations
        {
            MinWidth=300,
            MinHeight=150,
            MaxContentHeight=180,
            MaxContentWidth=65
        };
        public static async Task<IPendingHandler> InitLoding(string title, string message, bool cancel = false, Window window = null, int seconds = 2000)
        {
            var handler = PendingBox.Show(message, title, cancel, window);
            await Task.Delay(seconds).ConfigureAwait(false);
            Execute.OnUIThread(() =>
            {
                handler.Close();
            });
            return handler;
        }
        public static async ValueTask UpdateLoding(IPendingHandler handler, string message, int seconds = 300)
        {
            handler.UpdateMessage(message);
            await Task.Delay(seconds) ;
        }
        public static void CloseLoding(IPendingHandler handler)
        {
            handler.Close();
        }

        public static void OpenTips(string title, string message,Window window =null,MessageBoxButton messageBox=MessageBoxButton.OK, int seconds = 5)
        {
            Panuon.UI.Silver.MessageBoxX.Show(message, title, window, messageBox, messageBoxConfiguration);
        }
    }
}
