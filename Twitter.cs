using CoreTweet;

public class Twitter
{
    private string? consumerKey { get; set; }
    private string? consumerKeySecret { get; set; }
    private string? accessToken { get; set; }
    private string? accessTokenSecret { get; set; }
    private Tokens? Token { get; set; }

    public void getTwitterInformation()
    {
        var a = new Twitter();
        a.SetKey();
        a.Createoken();
        a.SearchTweet(5, "セザンヌ新商品情報");
    }

    private void SetKey()
    {
        DotNetEnv.Env.Load("./.env");
        //Twitter認証用セッション変数
        consumerKey = DotNetEnv.Env.GetString("CONSUMER_KEY");
        consumerKeySecret = DotNetEnv.Env.GetString("CONSUMER_KEY_SECRET");
        accessToken = DotNetEnv.Env.GetString("ACCESS_TOKEN");
        accessTokenSecret = DotNetEnv.Env.GetString("ACCESS_TOKEN_SECRET");
    }
    private void Createoken()
    {
        //トークンの生成
        Token = CoreTweet.Tokens.Create(consumerKey, consumerKeySecret, accessToken, accessTokenSecret);
    }

    public void SearchTweet(int cnt, string keyword)
    {
        try {
            var tweets = Token!.Search.Tweets(count => cnt, q => keyword);
            foreach (var tweet in tweets)
            {
                Console.WriteLine($"ユーザID: {tweet.User.ScreenName}");
                Console.WriteLine($"ユーザ名: {tweet.User.Name}");
                Console.WriteLine($"ツイート: {tweet.Text}\n");
            }
        }
        catch (TwitterException e)
        {
            // recover from exception
            Console.WriteLine(e);
        }
    }
}