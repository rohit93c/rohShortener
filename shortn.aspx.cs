using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;

public partial class shortn : System.Web.UI.Page
{
    private const string key = "AIzaSyA0Ip84UyJtXsXUmWJD5t76DQ2TZFRor8A";
    static List<string> shortUrlList = new List<string>();

    private static String ALPHABET = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private static int BASE = 62;

    private static Random random = new Random();

    protected void Page_Load(object sender, EventArgs e)
    {
        //btnCopy2.Visible = false;
        btnCopy.Visible = false;
    }


    #region Old section
    /// <summary>
    /// using API
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void btnShorten_Click(object sender, EventArgs e)
    {
        string longUrl = txtLongUrl.Text;
        string finalURL = "";
        string post = "{\"longUrl\": \"" + longUrl + "\"}";
        string shortUrl = longUrl;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/urlshortener/v1/url?key=" + key);
        try
        {
            request.ServicePoint.Expect100Continue = false;
            request.Method = "POST";
            request.ContentLength = post.Length;
            request.ContentType = "application/json";
            request.Headers.Add("Cache-Control", "no-cache");
            using (Stream requestStream = request.GetRequestStream())
            {
                byte[] postBuffer = Encoding.ASCII.GetBytes(post);
                requestStream.Write(postBuffer, 0, postBuffer.Length);
            }
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader responseReader = new StreamReader(responseStream))
                    {
                        string json = responseReader.ReadToEnd();
                        JObject jsonResult = JObject.Parse(json);
                        finalURL = jsonResult["id"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(" Success -- > " + ex.StackTrace);
            System.Diagnostics.Debug.WriteLine(ex.Message);
            System.Diagnostics.Debug.WriteLine(ex.StackTrace);
        }
        //return finalURL;
        Console.Write(finalURL);
        lblShortenedUrl.InnerText = finalURL;
    }

    public static String encode(int num)
    {
        StringBuilder sb = new StringBuilder();
        while (num > 0)
        {
            sb.Append(ALPHABET[(num % BASE)]);
            num /= BASE;
        }

        StringBuilder builder = new StringBuilder();
        for (int i = sb.Length - 1; i >= 0; i--)
        {
            builder.Append(sb[i]);
        }
        return builder.ToString();
    }

    public static int decode(String str)
    {
        int num = 0;
        for (int i = 0, len = str.Length; i < len; i++)
        {
            num = num * BASE + ALPHABET.IndexOf(str[(i)]);
        }
        return num;
    }

    #endregion

    public void btnShorten2_Click(object sender, EventArgs e)
    {
        if (txtLongUrl.Text != "" && IsValidUri(txtLongUrl.Text)!=false)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            string shortenKey = RandomString(6); 
            string finalURL = baseUrl + shortenKey;

            LearnEFEntities db = new LearnEFEntities(); 
            tbl_shortenedUrl shortnDetails = new tbl_shortenedUrl();
            shortnDetails.longUrl = txtLongUrl.Text;
            shortnDetails.shortUrl = finalURL;
            shortnDetails.urlKey = shortenKey;
            shortnDetails.addedOn = DateTime.Now;

            db.tbl_shortenedUrl.Add(shortnDetails);
            db.SaveChanges(); 
            //Console.Write(finalURL);
            lblShortenedUrl.InnerText = finalURL;
            btnCopy.Visible = true;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please specify a valid url!')", true);
        }
    }


    public void btnShorten3_Click(object sender, EventArgs e)
    {
        string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        string customKey = txtShortCustom.Text;
        if (txtLongUrl.Text != "")
        {
            if (customKey != "" && customKey.Length <= 6 && System.Text.RegularExpressions.Regex.IsMatch(customKey, @"^[a-zA-Z0-9]+$"))
            {
                if(!IsExists(customKey))
                { 
                    string finalURL = baseUrl + customKey;
                    LearnEFEntities db = new LearnEFEntities();
                    tbl_shortenedUrl shortnDetails = new tbl_shortenedUrl();
                    shortnDetails.longUrl = txtLongUrl.Text;
                    shortnDetails.shortUrl = finalURL;
                    shortnDetails.urlKey = customKey;
                    shortnDetails.addedOn = DateTime.Now;
                    db.tbl_shortenedUrl.Add(shortnDetails);
                    db.SaveChanges();
                    //Console.Write(finalURL);
                    lblShortenedUrl.InnerText = finalURL;
                    //btnCopy2.Visible = true;
                    btnCopy.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Name has already been taken!')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please specify valid custom url!')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please specify a valid url to shorten!')", true);
        }
    }

    /// <summary>
    /// To generate random alphaneumeric string
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string RandomString(int length)
    {
        char[] chars = new char[length];
        for (int i = 0; i < length; i++)
        {
            chars[i] = ALPHABET[random.Next(ALPHABET.Length)];
        }
        string uniqueKey = new string(chars);

        if (IsExists(uniqueKey))
        {
            return RandomString(6);
        }
        else
        {
            return uniqueKey;
        }
        //return new string(chars);
    }

    public static bool IsExists(string chkMatch)
    {
        LearnEFEntities db = new LearnEFEntities();
        if (db.tbl_shortenedUrl.Any(o => o.urlKey == chkMatch.ToString()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsValidUri(string url)
    {
        Uri validatedUri; 
        if (Uri.TryCreate(url, UriKind.Absolute, out validatedUri)) 
        { 
            return (validatedUri.Scheme == Uri.UriSchemeHttp || validatedUri.Scheme == Uri.UriSchemeHttps);
        }
        return false;
    }

}