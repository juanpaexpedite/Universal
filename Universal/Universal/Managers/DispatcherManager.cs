using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using static Universal.Managers.ExceptionManager;

namespace Universal.Managers
{
    public class DispatcherManager
    {
        public static async Task InvokeCoreDispatcher(Action action)
        {
            try
            {
                if (action != null)
                {
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                        CoreDispatcherPriority.Normal, () => { action.Invoke(); });
                }
            }
            catch (Exception ex)
            {
                Launch(ex);
            }
        }
    }

    //    public static CoreDispatcher CurrentDispatcher { get; set; }

    //    public static async Task InvokeCurrentDispatcher(Action action)
    //    {
    //        try
    //        {
    //            if (action != null && CurrentDispatcher != null)
    //            {
    //                await CurrentDispatcher.RunAsync(
    //                    CoreDispatcherPriority.Normal, () => { action.Invoke(); });
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Launch(ex);
    //        }
    //    }
    //}
}
