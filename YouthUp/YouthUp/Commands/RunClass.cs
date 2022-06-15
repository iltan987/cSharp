using System;
using System.Threading;
using System.Threading.Tasks;

namespace YouthUp.Commands
{
    class RunClass
    {
        public static Task<T> RunAsync<T>(Func<T> func, ApartmentState apartmentState = ApartmentState.Unknown)
        {
            TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();
            Thread thread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    var result = func();
                    tcs.SetResult(result);
                }
                catch (Exception ex) { tcs.SetException(ex); }
            }));
            thread.SetApartmentState(apartmentState);
            thread.Start();
            return tcs.Task;
        }
    }
}