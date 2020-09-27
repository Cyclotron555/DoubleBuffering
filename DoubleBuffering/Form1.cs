using DoubleBuffering.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoubleBuffering
{
    public partial class mainWindow : Form
    {
        public Color BackgroundColor { get; set; } = Color.FromArgb(255, 17, 148, 244);
        DateTime startTime;
        TimeSpan fps = TimeSpan.FromSeconds(1);
        Timer timer;
        Image ufoImg;
        StringFormat stringFormat;
        int ufoH;
        int ufoW;
        int w;
        int h;
        Font f;
        Graphics g;//on screen gfx
        Graphics g2;//buffered gfx
        Bitmap btmp;//memory buffered storage | g2 writes on this
        Brush b;
        Pen p;
        int mouseX;
        int mouseY;
        int counter;
        public mainWindow()
        {
            InitializeComponent();
        }
        private void mainWindow_Load(object sender, EventArgs e)
        {
            w = Width;
            h = Height;
            g = CreateGraphics();
            g.Clear(BackgroundColor);
            btmp = new Bitmap(w, h);
            g2 = Graphics.FromImage(btmp);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g2.SmoothingMode = SmoothingMode.AntiAlias;
            g2.Clear(BackgroundColor);
            g.DrawImage(btmp, 0, 0, w, h);
        }
        private void mainWindow_Shown(object sender, EventArgs e)
        {
            b = new SolidBrush(Color.FromArgb(255, 6, 255,104));
            f = new Font("ArcadeClassic", 14);
            stringFormat = new StringFormat();
            ufoImg = Resources.ufo1;
            ufoH = ufoImg.Size.Height;
            ufoW = ufoImg.Size.Width;
            p = new Pen(Color.Red, 5.0f);
            timer = new Timer();
            timer.Enabled = true;
            Paint += MainWindow_Paint;
            timer.Interval = 2;
            timer.Tick += Timer_Tick;
        }

        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            g.DrawImage(btmp, 0, 0, w, h);
            SizeChanged += MainWindow_SizeChanged;
        }

        private void MainWindow_SizeChanged(object sender, EventArgs e)
        {
            w = Width;
            h = Height;
            g = null;
            g2 = null;
            btmp = null;
            g = CreateGraphics();
            btmp = new Bitmap(w, h);
            g2 = Graphics.FromImage(btmp);
            g2.Clear(BackgroundColor);
            g.DrawImage(btmp, 0, 0, w, h);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Game();
        }

        private void Game()
        {
            w= Width;
            h= Height;
            g2.DrawLine(p, 0f, 0f,  mouseX, mouseY);
            g2.DrawLine(p, w, h, mouseX, mouseY);
            g2.DrawImage(ufoImg, new Point(mouseX - (ufoW/2), mouseY-(ufoH/2)));
            g2.DrawString(" |  " + counter.ToString() + "  | ", f, b, 1400, 25, stringFormat);
            g2.DrawString("Mouse X - " + mouseX.ToString() + "  Mouse Y - " + mouseY.ToString(), f, b, 1000, 25, stringFormat) ;
            g2.DrawString("Bitmap Width - " + btmp.Size.Width.ToString() + "  Bitmap Height - " + btmp.Size.Height.ToString(), f, b, 600, 25, stringFormat);
            g.DrawImage(btmp, 0, 0, w, h);
            g2.Clear(BackgroundColor);
            counter++;
        }

        private void mainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            mouseX = e.Location.X;
            mouseY = e.Location.Y;
        }
    }
}
