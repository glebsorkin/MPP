using System;
using System.Runtime.InteropServices;

namespace MPP_lab4
{
    public class OSHandle:IDisposable
    {
        [DllImport("Kernel32.dll")]// так надо библиотека с которой импортирую дескриптор на что-то
        private static extern bool CloseHandle(IntPtr handle);
        private IntPtr Handle { get; set; }//поле класса
        
        private bool _disposed;//проверка на диспоуз

        public OSHandle(IntPtr handle)//конструктор класса передаю дескриптор
        {
            Handle = handle;
        }

        public void Dispose()//принудительное освобождение дескриптора
        {
            if (!_disposed)
            {
                CloseHandle(Handle);//закрываем обработчик
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }

        ~OSHandle()
        {
            Dispose ();
        }
    }
}