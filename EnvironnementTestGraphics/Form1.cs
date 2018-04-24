using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnvironnementTestGraphics
{
    public partial class Form1 : Form
    {
        private static Random random = new Random();
        PictureBox pictureBox1 = new PictureBox();
        public Form1()
        {
            InitializeComponent();
        }

        public Font GetAdjustedFont(Graphics GraphicRef, string GraphicString, Font OriginalFont, int ContainerWidth, int MaxFontSize, int MinFontSize, bool SmallestOnFail)
        {
            // We utilize MeasureString which we get via a control instance           
            for (int AdjustedSize = MaxFontSize; AdjustedSize >= MinFontSize; AdjustedSize = AdjustedSize - 2)
            {
                Font TestFont = new Font(OriginalFont.Name, AdjustedSize, OriginalFont.Style);

                // Test the string with the new size
                SizeF AdjustedSizeNew = GraphicRef.MeasureString(GraphicString, TestFont);
                int round = (int)Math.Truncate(AdjustedSizeNew.Width);

                if ((ContainerWidth > Convert.ToInt32(AdjustedSizeNew.Width)))
                {
                    // Good font, return it
                    return TestFont;
                }
            }

            // If you get here there was no fontsize that worked
            // return MinimumSize or Original?
            if (SmallestOnFail)
            {
                return new Font(OriginalFont.Name, MinFontSize, OriginalFont.Style);
            }
            else
            {
                return OriginalFont;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text = RandomString(RandomNumber(1,3));
            string fileName = "WDF";
            string folder = "WDF";
            int canvaSize = 64;
            string color = RandomColor();
            generateAvatar(text, fileName, folder, canvaSize, color);
        }

        private void generateAvatar(string text, string fileName, string saveLocation, int canvaSize, string color)
        {
            pictureBox2.Size = new Size(canvaSize, canvaSize);
            this.Controls.Add(pictureBox2);

            Bitmap flag = new Bitmap(canvaSize, canvaSize);
            Graphics flagGraphics = Graphics.FromImage(flag);

            Color BackgroundColor = ColorTranslator.FromHtml(color);

            flagGraphics.FillRectangle(new SolidBrush(BackgroundColor), 0, 0, canvaSize, canvaSize);

            var font = new Font("DejaVu", 18, FontStyle.Bold);
            var fontColor = Color.WhiteSmoke;
            var adjustedFont = GetAdjustedFont(flagGraphics, text, font, flag.Width, 26, 7, true);
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;

            flagGraphics.DrawString(text, adjustedFont, new SolidBrush(fontColor), new RectangleF(0, 0, canvaSize, canvaSize), drawFormat);

            pictureBox2.Image = flag;
            flag.Save(@"C:\Users\journel\Desktop\test\64.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string RandomColor()
        {
            String[] colors = { "#BA68C8", "#9575CD", "#81C784", "#AED581", "#DCE775", "#FFD54F", "#FFB74D", "#FF8A64" };

            Random random = new Random();
            int index = random.Next(0, colors.Length);
            return colors[index];
        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            int index = random.Next(min, max+1);
            return index;
        }
    }
}
