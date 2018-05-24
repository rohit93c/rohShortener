using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class shrtn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string urlKey = Request.QueryString["id"];
        //Response.Write(urlKey);
        string originalUrl = getLongUrlFromKey(urlKey);
        if (originalUrl != null)
        {
            Response.Redirect(originalUrl);
        }
        else
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No such Url')", true);
            Response.Redirect("~/shortn.aspx");
        }
    }

    private string getLongUrlFromKey(string key)
    {
        LearnEFEntities db = new LearnEFEntities();
        var longUrl = db.tbl_shortenedUrl.Where(b => b.urlKey == key)
                      .Select(b => b.longUrl).FirstOrDefault();
        if (longUrl != null)
        {
            return longUrl.ToString();
        }
        else
        {
            return null;
        }
    }
}