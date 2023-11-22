<%@ WebHandler Language="C#" Class="captcha" %>

using System;
using System.Web;

using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
public class captcha : IHttpHandler, System.Web.SessionState.IRequiresSessionState,System.Web.SessionState.IReadOnlySessionState
{

    //public void ProcessRequest(HttpContext context)
    //{

    //    Bitmap objBMP = new System.Drawing.Bitmap(170, 50);

    //    //Bitmap objBMP = new System.Drawing.Bitmap(1, 1);
    //    Graphics objGraphics = System.Drawing.Graphics.FromImage(objBMP);
    //    objGraphics.Clear(Color.White);
    //    objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
    //    objGraphics.CompositingQuality = CompositingQuality.HighQuality;
    //    objGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
    //    //' Configure font to use for text
    //    //Font objFont = new Font("Georgia", 26, FontStyle.Regular);
    //    Font objFont = new Font("Times New Roman", 26, FontStyle.Italic);

    //    //Font objFont = new Font("Georgia", 2, FontStyle.Regular);
    //    string randomStr = GenerateRandomString(5);
    //    //string randomStr = context.Request.QueryString["arg"];
    //    //This is to add the string to session cookie, to be compared later
    //    HttpContext.Current.Session.Add("captcha", randomStr);
    //    //' Write out the text
    //    objGraphics.DrawString(randomStr, objFont, Brushes.Green, 3, 3);
    //    //objGraphics.DrawString(randomStr, objFont, Brushes.Black, 1, 1);
    //    //' Set the content type and return the image
    //    context.Response.ContentType = "image/JPEG";
    //    objBMP.Save(context.Response.OutputStream, ImageFormat.Jpeg);
    //    objFont.Dispose();
    //    objGraphics.Dispose();
    //    objBMP.Dispose();
    //}
    private Random rand = new Random();
    public void ProcessRequest(HttpContext context)
    {
        //using (Bitmap b = new Bitmap(70, 25, PixelFormat.Format32bppArgb))
        using (Bitmap b = new Bitmap(150, 40, PixelFormat.Format32bppArgb))
        {
            using (Graphics g = Graphics.FromImage(b))
            {
               
                //Rectangle rect = new Rectangle(0, 0, 70, 25);
                Rectangle rect = new Rectangle(0, 0, 149, 39);
                g.FillRectangle(Brushes.White, rect);
                //Random r = new Random();
                //int startIndex = r.Next(1, 5);
                //int length = r.Next(4, 7);
                //String drawString = Guid.NewGuid().ToString().Replace("-", "0").Substring(startIndex, length);
                string drawString ;
                string randomStr = context.Request.QueryString["arg"];
                if (randomStr==null || randomStr=="")
                {
                    drawString = GenerateRandomString(6);
                }
                else
                {
                    drawString = randomStr;
                }

                //Font drawFont = new Font("Times New Roman", 14, FontStyle.Italic | FontStyle.Strikeout);
                Font drawFont = new Font("Arial", 24, FontStyle.Italic | FontStyle.Strikeout);
                HttpContext.Current.Session.Add("captcha", drawString);
                using (SolidBrush drawBrush = new SolidBrush(Color.Black))
                {
                    g.DrawRectangle(new Pen(Color.CornflowerBlue, 0), rect);
                    PointF drawPoint = new PointF(15, 8);
                    g.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                DrawRandomLines(g);   
                b.Save(context.Response.OutputStream, ImageFormat.Jpeg);
                context.Response.ContentType = "image/jpeg";
                context.Response.End();
            }
        }
    }
    private void DrawRandomLines(Graphics g)
    {
        SolidBrush yellow = new SolidBrush(Color.Black);
        for (int i = 0; i < 20; i++)
        {
            g.DrawLines(new Pen(yellow, 1), GetRandomPoints());
        }

    }
    private Point[] GetRandomPoints()
    {
        
        Point[] points = { new Point(rand.Next(0, 150), rand.Next(1, 150)), new Point(rand.Next(0, 200), rand.Next(1, 190)) };
        return points;
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    public static string GenerateRandomString(int length)
    {
        //string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz";
        string chars = "0123456789";
        string result = "";
        Random rand = new Random();
        for (int i = 0; i < length; i++)
        {
            result += chars[rand.Next(chars.Length)];
        }
        return result;
    }

}