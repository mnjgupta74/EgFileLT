<%@ WebHandler Language="C#" Class="captcha" %>

using System.Web;

using System.Drawing;
using System.Drawing.Imaging;
public class captcha : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        using (Bitmap b = new Bitmap(200, 20, PixelFormat.Format32bppArgb))
        {
            using (Graphics g = Graphics.FromImage(b))
            {
                Rectangle rect = new Rectangle(0, 0, 199, 19);
                g.FillRectangle(Brushes.White, rect);
                string randomStr = context.Request.QueryString["arg"];
                Font drawFont = new Font("Arial", 8, FontStyle.Italic | FontStyle.Regular);
                using (SolidBrush drawBrush = new SolidBrush(Color.Black))
                {
                    g.DrawRectangle(new Pen(Color.CornflowerBlue, 0), rect);
                    PointF drawPoint = new PointF(5, 3);
                    g.DrawString(randomStr, drawFont, drawBrush, drawPoint);
                }
                b.Save(context.Response.OutputStream, ImageFormat.Jpeg);
                context.Response.ContentType = "image/jpeg";
                context.Response.End();
            }
        }
    }
   
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
 

}