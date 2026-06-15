using System;
using System.Threading;

//https://learn.microsoft.com/ru-ru/dotnet/api/system.threading.readerwriterlockslim?view=net-10.0


class Program
{
    static void Main()
    {
        Console.WriteLine("Текущее значение: " + Server.GetCount());

        Server.AddToCount(10);

        Server.AddToCount(5);

        Console.WriteLine("Текущее значение после добавления : " + Server.GetCount());
    }
}

static class Server
{
    private static int count = 0;

    private static ReaderWriterLockSlim locker =
        new ReaderWriterLockSlim();

    public static int GetCount()
    {
        locker.EnterReadLock();

        try
        {
            return count;
        }
        finally
        {
            locker.ExitReadLock();
        }
    }

    public static void AddToCount(int value)
    {
        locker.EnterWriteLock();

        try
        {
            count += value;
        }
        finally
        {
            locker.ExitWriteLock();
        }
    }
}