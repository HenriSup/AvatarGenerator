using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnvironnementTestGraphics
{
    public partial class Form1 : Form
    {
        private static Random random = new Random();
        private static List<PictureBox> pictureBoxes = new List<PictureBox>();
        private Font Font;
        private String[] FontLists;
        public Form1()
        {
            InitializeComponent();
            pictureBoxes.Add(pictureBox1);
            pictureBoxes.Add(pictureBox2);
            pictureBoxes.Add(pictureBox3);
            pictureBoxes.Add(pictureBox4);
            pictureBoxes.Add(pictureBox5);
            pictureBoxes.Add(pictureBox6);
            pictureBoxes.Add(pictureBox7);
            pictureBoxes.Add(pictureBox8);
            pictureBoxes.Add(pictureBox9);
            pictureBoxes.Add(pictureBox10);
            pictureBoxes.Add(pictureBox11);
            pictureBoxes.Add(pictureBox12);
            pictureBoxes.Add(pictureBox13);
            pictureBoxes.Add(pictureBox14);
            pictureBoxes.Add(pictureBox15);
            pictureBoxes.Add(pictureBox16);
            pictureBoxes.Add(pictureBox17);
            pictureBoxes.Add(pictureBox18);
            pictureBoxes.Add(pictureBox19);
            pictureBoxes.Add(pictureBox20);
            pictureBoxes.Add(pictureBox21);
            pictureBoxes.Add(pictureBox22);
            pictureBoxes.Add(pictureBox23);
            pictureBoxes.Add(pictureBox24);
            pictureBoxes.Add(pictureBox25);
            pictureBoxes.Add(pictureBox26);
            pictureBoxes.Add(pictureBox27);
            pictureBoxes.Add(pictureBox28);
            pictureBoxes.Add(pictureBox29);
            pictureBoxes.Add(pictureBox30);
            pictureBoxes.Add(pictureBox31);
            pictureBoxes.Add(pictureBox32);
            pictureBoxes.Add(pictureBox33);
            pictureBoxes.Add(pictureBox34);
            pictureBoxes.Add(pictureBox35);
            pictureBoxes.Add(pictureBox36);
            pictureBoxes.Add(pictureBox37);
            pictureBoxes.Add(pictureBox38);
            pictureBoxes.Add(pictureBox39);
            pictureBoxes.Add(pictureBox40);
            pictureBoxes.Add(pictureBox41);
            pictureBoxes.Add(pictureBox42);
            pictureBoxes.Add(pictureBox43);
            pictureBoxes.Add(pictureBox44);
            pictureBoxes.Add(pictureBox45);
            Font = fontDialog1.Font;
        }


        private void generateButtonClick(object sender, EventArgs e)
        {
            int canvaSize = Convert.ToInt32(numericUpDown1.Value);
            int repetitions = pictureBoxes.Count();

            for (int i=0; i<repetitions;i++)
            {
                string text = String.IsNullOrWhiteSpace(textBox2.Text) ? RandomString(RandomNumber(1, 3), true) : textBox2.Text;
                Regex hexaRegex = new Regex(@"\d+");
                string color = hexaRegex.Match(textBox1.Text).Success ? textBox1.Text : RandomColor();
                string fileName = "avatar "+ canvaSize.ToString()+"x"+ canvaSize.ToString()+" - " + i.ToString()+".png";

                if (i <= pictureBoxes.Count)
                {
                    generateAvatar(text, canvaSize, color, pictureBoxes[i], fileName);
                }
            }
        }

        private void generateAvatar(string text, int canvaSize, string color,PictureBox pictureBox,string fileName)
        {
            Bitmap background = new Bitmap(canvaSize, canvaSize);
            Graphics avatar = Graphics.FromImage(background);
            Color BackgroundColor = ColorTranslator.FromHtml(color);
            Font font = Font;
            Color fontColor = Color.WhiteSmoke;
            Font adjustedFont = GetAdjustedFont(avatar, text, font, background.Width, 26, 7, true);
            StringFormat drawFormat = new StringFormat();
            string folder = string.Concat(canvaSize.ToString(), "x", canvaSize.ToString(), "\\");
            string file = string.Concat(folder, fileName);

            avatar.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pictureBox.Size = new Size(canvaSize, canvaSize);
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;
            avatar.TextRenderingHint = TextRenderingHint.AntiAlias;
            pictureBox.Image = background;

            if (radioButton1.Checked)
            {
                avatar.FillRectangle(new SolidBrush(BackgroundColor), 0, 0, canvaSize, canvaSize);
            }
            else if (radioButton2.Checked)
            {
                avatar.FillEllipse(new SolidBrush(BackgroundColor), 0, 0, canvaSize, canvaSize);
            }
            else
            {
                Random gen = new Random();
                int prob = gen.Next(100);
                bool randomChoice = prob <= 20;
                if (randomChoice)
                {
                    avatar.FillRectangle(new SolidBrush(BackgroundColor), 0, 0, canvaSize, canvaSize);
                } else
                {
                    avatar.FillEllipse(new SolidBrush(BackgroundColor), 0, 0, canvaSize, canvaSize);
                }
            }

            avatar.DrawString(text, adjustedFont, new SolidBrush(fontColor), new RectangleF(0, 0, canvaSize, canvaSize), drawFormat);

            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                folder = string.Concat(canvaSize.ToString(), "x", canvaSize.ToString()," - ", textBox2.Text.ToString(), "\\");
            }
            
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            background.Save(@file, System.Drawing.Imaging.ImageFormat.Png);
        }

        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string RandomString(int length,bool onlyLetters)
        {
            string chars = (onlyLetters ? "ABCDEFGHIJKLMNOPQRSTUVWXYZ" : "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string RandomColor()
        {
            String[] colors = { "#BA68C8", "#9575CD", "#81C784", "#AED581", "#DCE775", "#FFD54F", "#FFB74D", "#FF8A64" };
            Random random1 = new Random();
            int randomSeed = random1.Next(0, colors.Length);
            Random random = new Random(randomSeed);
            int index = random.Next(0, colors.Length);
            return colors[index];
        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            int index = random.Next(min, max+1);
            return index;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = true;
            fontDialog1.Color = textBox1.ForeColor;

            if (fontDialog1.ShowDialog() != DialogResult.Cancel)
            {
                label4.Text = fontDialog1.Font.Name;
                Font = fontDialog1.Font;
            }
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
    }
}
