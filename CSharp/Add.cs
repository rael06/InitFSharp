namespace CSharp;

public static class Add
{
    public static void process()
    {
        var arr1 = new List<int> { 1, 2, 3, 4 };
        var arr2 = new List<int> { 5, 6, 7, 8 };

        int Add(int x, int y)
        {
            return x + y;
        }

        List<int> Zip(MyDelegate.Del f, List<int> arr1, List<int> arr2)
        {
            var count = Math.Min(arr1.Count, arr2.Count);
            var result = new List<int>();
            for (var i = 0; i < count; i++)
            {
                result.Add(f(arr1[i], arr2[i]));
            }

            return result;
        }

        Console.WriteLine(string.Join(";", Zip(Add, arr1, arr2)));
    }
}
