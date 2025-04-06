using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ACMsbEditor.Logging
{
    internal static class ConsoleLogger
    {
        internal static void Write(string message)
        {
            Console.Write(message);
#if DEBUG
            Debug.Write(message);
#endif
        }

        internal static void WriteLine(string message)
        {
            Console.WriteLine(message);
#if DEBUG
            Debug.WriteLine(message);
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void LogInfo(string message)
            => WriteLine($"Info: {message}");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void LogWarn(string message)
            => WriteLine($"Warn: {message}");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void LogError(string message)
            => WriteLine($"Error: {message}");
    }
}
