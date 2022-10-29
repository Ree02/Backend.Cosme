
public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Twitterの情報を取得");
        var test = new Twitter();
        test.getTwitterInformation();
    }
}