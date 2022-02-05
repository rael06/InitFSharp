namespace CSharp;

public static class Currying
{
    public static Func<int, int> Add(int x)
    {
        return y  => y + x;
    }

    public static void Process()
    {
        var f = Add(5);
        var result = f(10);
        Console.WriteLine(result);
    }
}
