namespace DataBaseModles;

public class JwtModel
{
    public string token { get; set; }
    public string refresh  { get; set; }
    public bool valid { get; set; }
}