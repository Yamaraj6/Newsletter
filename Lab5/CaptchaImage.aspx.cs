using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab5
{
    public partial class CaptchaImage : System.Web.UI.Page
    {
        private string GenerateRandomCode()
        {
            var random = new Random();
            var code = string.Empty;
            for (var i = 0; i < 5; i++)
            {
                var j = random.Next(3);
                int ch;
                switch (j)
                {
                    case 0:
                        ch = random.Next(0, 9);
                        code = code + ch;
                        break;
                    case 1:
                        ch = random.Next(65, 90);
                        code = code + Convert.ToChar(ch);
                        break;
                    case 2:
                        ch = random.Next(97, 122);
                        code = code + Convert.ToChar(ch);
                        break;
                }
            }
            return code;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CaptchaImageText"] = GenerateRandomCode();
            var captchaImage = new RandomImage(Session["CaptchaImageText"].ToString(), 280, 100);
            Response.Clear();
            Response.ContentType = "image/jpeg";
            using (var memoryStream = new MemoryStream())
            {
                captchaImage.Image.Save(memoryStream, ImageFormat.Jpeg);
                memoryStream.WriteTo(Response.OutputStream);
            }
        }
    }
}