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
        Console.Write("ツイートを取得したいユーザのIDを入力: ");
        var userId = Console.ReadLine() ?? "";
        var tweet = new Twitter();

        tweet.SetKey();
        tweet.Createoken();
        tweet.SearchTweet(100, userId);
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

    public void SearchTweet(int cnt, string key)
    {
        var keyword = "from:" + key + " exclude:retweets"; 
        try {
            var tweets = Token!.Search.Tweets(count => cnt, q => keyword);
            if (tweets.Count == 0) {
                Console.WriteLine("該当するユーザまたはツイートが見つかりません。");
            }
            else {
                foreach (var tweet in tweets)
                {
                    Console.WriteLine($"ユーザID: {tweet.User.ScreenName}");
                    Console.WriteLine($"ユーザ名: {tweet.User.Name}");
                    Console.WriteLine($"ツイート日時: {tweet.CreatedAt}");
                    Console.WriteLine($"ツイート: {tweet.Text}\n");
                }
            }
        }
        catch (TwitterException e)
        {
            Console.WriteLine(e);
        }
    }
}