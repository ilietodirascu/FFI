using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;

class Program
{
    // Import the Rust function from the shared library (DLL)
    [DllImport("fast_factorials", EntryPoint = "factorial_big", CallingConvention = CallingConvention.Cdecl)]
    public static extern string Factorial(int n);

    public static BigInteger FactorialDotNet(int n)
    {
        BigInteger result = 1;
        for (BigInteger i = 1; i <= n; i++)
        {
            result *= i;
        }
        return result;
    }

    static void Main(string[] args)
    {
        Stopwatch stopwatch = new();
        var rnd = new Random();

        // Run 1000 loops for Rust function
        for (int i = 0; i < 1000; i++)
        {
            var randomNum = rnd.Next(15_000, 50_000);
            stopwatch.Start();
            Factorial(randomNum); // Call Rust function
            stopwatch.Stop();

            Console.WriteLine($"Number:{randomNum} Iteration {i + 1}: Time taken by Rust with Marshalling: {stopwatch.Elapsed.TotalMilliseconds} ms");

            stopwatch.Restart();
            FactorialDotNet(randomNum); // Call .NET function
            stopwatch.Stop();

            // Log the execution time for .NET
            Console.WriteLine($"Number:{randomNum} Iteration {i + 1}: Time taken by .NET: {stopwatch.Elapsed.TotalMilliseconds} ms");
        }
    }
}
