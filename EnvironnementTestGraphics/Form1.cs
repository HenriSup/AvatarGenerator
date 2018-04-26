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
        private string Font = "dejavu";
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
            int canvaSize = Convert.ToInt32(numericUpDown1.Value);
            int repetitions = 36;

            for (int i=0; i<repetitions;i++)
            {
                string text = String.IsNullOrWhiteSpace(textBox2.Text) ? RandomString(RandomNumber(1, 3), true) : textBox2.Text;
                Regex hexaRegex = new Regex(@"\d+");
                string color = hexaRegex.Match(textBox1.Text).Success ? textBox1.Text : RandomColor();
                string fileName = "avatar "+ canvaSize.ToString()+"x"+ canvaSize.ToString()+" - " + i.ToString()+".png";

                if (i <= pictureBoxes.Count-1)
                {
                    generateAvatar(text, canvaSize, color, pictureBoxes[i], fileName);
                }
            }
        }

        private void generateAvatar(string text, int canvaSize, string color,PictureBox pictureBox,string fileName)
        {
            pictureBox.Size = new Size(canvaSize, canvaSize);
            this.Controls.Add(pictureBox);

            Bitmap background = new Bitmap(canvaSize, canvaSize);
            Graphics flagGraphics = Graphics.FromImage(background);

            Color BackgroundColor = ColorTranslator.FromHtml(color);
            flagGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;


            if (radioButton1.Checked)
            {
                flagGraphics.FillRectangle(new SolidBrush(BackgroundColor), 0, 0, canvaSize, canvaSize);
            }
            else if (radioButton2.Checked)
            {
                flagGraphics.FillEllipse(new SolidBrush(BackgroundColor), 0, 0, canvaSize, canvaSize);
            }
            else
            {
                Random gen = new Random();
                int prob = gen.Next(100);
                bool randomChoice = prob <= 20;
                if (randomChoice)
                {
                    flagGraphics.FillRectangle(new SolidBrush(BackgroundColor), 0, 0, canvaSize, canvaSize);
                } else
                {
                    flagGraphics.FillEllipse(new SolidBrush(BackgroundColor), 0, 0, canvaSize, canvaSize);
                }
            }

            var font = new Font(Font, 18, FontStyle.Bold);
            var fontColor = Color.WhiteSmoke;
            var adjustedFont = GetAdjustedFont(flagGraphics, text, font, background.Width, 26, 7, true);
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;

            flagGraphics.DrawString(text, adjustedFont, new SolidBrush(fontColor), new RectangleF(0, 0, canvaSize, canvaSize), drawFormat);

            pictureBox.Image = background;
            string folder = string.Concat(canvaSize.ToString(), "x", canvaSize.ToString(), "\\");
            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                folder = string.Concat(canvaSize.ToString(), "x", canvaSize.ToString()," - ", textBox2.Text.ToString(), "\\");
            }
            
            Console.WriteLine(folder);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string file = string.Concat(folder, fileName);
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
                Font = fontDialog1.Font.Name;
                textBox1.Font = fontDialog1.Font;
                textBox1.ForeColor = fontDialog1.Color;
            }
        }
    }
}
