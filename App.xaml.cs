using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HelloStickyNotes
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        private const string MutexName = "Global\\MyAppMutex"; // 确保这个名称是唯一的就行
        private Mutex mutex;
        private bool existOthers;

        protected override void OnStartup(StartupEventArgs e)
        {
            bool isOwned;
            mutex = new Mutex(true, MutexName, out isOwned);

            if (!isOwned)
            {
                // 如果应用已经存在一个实例，则关闭当前实例
                //MessageBox.Show("Another instance of the application is already running.", "Application Running", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.Shutdown();
                this.existOthers = true;
                return;
            }

            base.OnStartup(e);
        }

        public bool ExistOthers()
        {
            return this.existOthers;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (mutex != null)
            {
                mutex.ReleaseMutex();
                mutex.Dispose();
            }
            base.OnExit(e);
        }

    }
}
