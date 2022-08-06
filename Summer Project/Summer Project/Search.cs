using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Summer_Project
{
    public partial class Search : Form
    {
        System.Windows.Forms.Timer tt = new System.Windows.Forms.Timer();
        Bitmap off;
        int ct = 0;
        int k = 1, k2 = 1;
        List<pic> ldevice = new List<pic>();
        List<pic> lreq = new List<pic>();
        List<pic> lline = new List<pic>();
        List<pic> lbutton = new List<pic>();
        List<string> list = new List<string>();
        public Search()
        {
            WindowState = FormWindowState.Maximized;
            InitializeComponent();
            tt.Tick += new EventHandler(tt_Tick);
            tt.Start();
            MouseDown += Form1_MouseDown;
            MouseUp += Form1_MouseUp;
        }
        internal void recivedatafromdivice(string v)
        {
            list.Add(v);
           // MessageBox.Show(list[0]);
           // MessageBox.Show(list.Count.ToString());
            // throw new NotImplementedException();
        }
        void tt_Tick(object sender, EventArgs e)
        {
            DrawDubb(this.CreateGraphics());
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            this.Text = e.X + "  " + e.Y;
            if (e.X <this.ClientSize.Width/5*1 )
            {
                if (e.Y>= 3 && e.Y < 60 && e.X >= 3 && e.X < 63)
                {
                    Divice dv = new Divice();
                    this.Hide();
                    dv.ShowDialog();
                    this.Close();
                }
                else if (e.Y < this.ClientSize.Height / 2 )
                {
                    MessageBox.Show("1");
                }
                else if (e.Y > this.ClientSize.Height / 2 )
                {
                    MessageBox.Show("11");
                }

            }
            if (e.X > this.ClientSize.Width / 5 * 1&& e.X < this.ClientSize.Width / 5 * 2)
            {
                if (e.Y < this.ClientSize.Height / 3*1)
                {
                    MessageBox.Show("2");
                }
                if (e.Y >this.ClientSize.Height / 3 * 1 && e.Y < this.ClientSize.Height / 3 * 2)
                {
                    MessageBox.Show("22");
                }
                if (e.Y > this.ClientSize.Height / 3 * 2)
                {
                    MessageBox.Show("222");
                }
            }
            if (e.X > this.ClientSize.Width / 5 *2 && e.X < this.ClientSize.Width / 5 *3)
            {
                fridge fr = new fridge();
                for (int i = 0; i < list.Count; i++)
                {
                    fr.recivedatafromdivice(v: list[i]);
                }
                this.Hide();
                fr.ShowDialog();
                this.Close();
            }
            if (e.X > this.ClientSize.Width / 5 * 3 && e.X < this.ClientSize.Width / 5 * 4)
            {
                if (e.Y < this.ClientSize.Height / 3 * 1)
                {
                    MessageBox.Show("4");
                }
                if (e.Y > this.ClientSize.Height / 3 * 1 && e.Y < this.ClientSize.Height / 3 * 2)
                {
                    MessageBox.Show("44");
                }
                if (e.Y > this.ClientSize.Height / 3 * 2)
                {
                    MessageBox.Show("444");
                }
            }
            if (e.X > this.ClientSize.Width / 5 * 4 && e.X < this.ClientSize.Width / 5 * 5)
            {
                if (e.Y < this.ClientSize.Height / 2)
                {
                    MessageBox.Show("5");
                }
                else if (e.Y > this.ClientSize.Height / 2)
                {
                    MessageBox.Show("55");
                }
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
           
        }
        void creatbutton()
        {
            pic pnn;
            pnn = new pic();
            pnn.y = 3;
            pnn.x = 3;
            pnn.im = new Bitmap("back.png");
            pnn.cWidth = (pnn.im.Width);
            pnn.cHeight = (pnn.im.Height);
            //  pnn.im.MakeTransparent(pnn.im.GetPixel(0, 0));
            lbutton.Add(pnn);
        }
       void creatlines()
        {

            for (int i = 1; i < 6; i++)
            {
                pic pnn;
                pnn = new pic();
                pnn.w = 4;
                pnn.h = this.ClientSize.Height;
                pnn.x = (this.ClientSize.Width / 5)*i;
                pnn.y = 0;
                pnn.clr = Color.White;
                lline.Add(pnn);
            }
            for (int i = 1; i < 7; i++)
            {
                if (i == 3)
                {
                    k = 1;
                    k2 = 2;
                }
                if(i==5)
                {
                    k = 0;
                    k2 = 1;
                }
                if(i==6)
                {
                    k = 4;
                }
                pic pnn;
                pnn = new pic();
                pnn.w = (this.ClientSize.Width / 5);
                pnn.h = 4;
                if (i < 5)
                {
                    pnn.x = (this.ClientSize.Width / 5) * k;
                    pnn.y = (this.ClientSize.Height / 3) * k2;
                }
                else
                {
                    pnn.x = (this.ClientSize.Width / 5) * k;
                    pnn.y = (this.ClientSize.Height / 2) * k2;
                }
                pnn.clr = Color.White;
                lline.Add(pnn);
                k += 2;
            }
        }
        void creatreq()
        {
            for (int i = 0; i < 8; i++)
            {
                pic pnn;
                pnn = new pic();
                pnn.w = (this.ClientSize.Width / 5)-3;
                if (i == 0)
                {
                    pnn.h = this.ClientSize.Height;
                    pnn.x = ((this.ClientSize.Width / 5) * 2) + 3;
                    pnn.y = 0;
                    pnn.clr = Color.FromArgb(255, 215, 0);
                }
                if (i > 0 && i < 5)
                {
                    pnn.h = (this.ClientSize.Height / 2)-3;
                    if (i < 3)
                    {
                        pnn.y = 0;
                    }
                    else
                    {
                        pnn.y = (this.ClientSize.Height / 2)+4;
                    }
                    if (i == 1||i==3)
                    {
                        pnn.x = ((this.ClientSize.Width / 5) * 0) + 3;
                        if (i == 1)
                        {
                            pnn.clr = Color.FromArgb(119, 136, 153);
                        }
                        if(i==3)
                        {
                            pnn.clr = Color.FromArgb(152, 251, 152);
                        }
                    }
                    if(i == 2||i==4)
                    {                    
                        pnn.x = ((this.ClientSize.Width / 5) * 4) + 3;
                        if (i == 2)
                        {
                            pnn.clr = Color.FromArgb(135, 206, 235);
                        }
                        if(i==4)
                        {
                            pnn.clr = Color.FromArgb(240, 248, 255);
                        }
                    }
                }
                if(i>=5)
                {
                    pnn.h = (this.ClientSize.Height / 3) - 3;
                    pnn.x = ((this.ClientSize.Width / 5) * 1) + 3;
                    if (i == 5)
                    {
                        pnn.y = 0;
                        pnn.clr = Color.FromArgb(255, 228, 225);
                    }
                    if (i == 6)
                    {
                        pnn.y = (this.ClientSize.Height / 3) +3;
                        pnn.clr = Color.FromArgb(211, 211, 211);
                    }
                }
                if(i==7)
                {
                    pnn.h = (this.ClientSize.Height / 3) - 3;
                    pnn.x = ((this.ClientSize.Width / 5) *3) + 3;
                    pnn.y = (this.ClientSize.Height / 2) + 170;
                    pnn.clr = Color.FromArgb(245, 222, 179);
                }
                lreq.Add(pnn);
            }
        }
        void creatdevice()
        {
            for (int i = 0; i <11; i++)
            {
                pic pnn;
                pnn = new pic();
                if (ct == 0)
                {
                    pnn.y = (this.ClientSize.Height/5)-100;
                    pnn.x = (this.ClientSize.Width / 5) * 2;
                    pnn.im = new Bitmap("fridge.png");
                }
                if (ct == 1)
                {
                    pnn.y = (this.ClientSize.Height / 2)+50;
                    pnn.x = (this.ClientSize.Width / 5) * 0;
                    pnn.im = new Bitmap("wash.png");
                }
                if (ct == 2)
                {
                    pnn.y = -50;
                    pnn.x = (this.ClientSize.Width / 5) * 0;
                    pnn.im = new Bitmap("cooker.png");
                }
                if (ct == 3)
                {
                    pnn.y = -10;
                    pnn.x = (this.ClientSize.Width / 5) * 4;
                    pnn.im = new Bitmap("tv.png");
                    pnn.cWidth = (pnn.im.Width);
                    pnn.cHeight = (pnn.im.Height);
                }
                if (ct == 4)
                {
                    pnn.y = (this.ClientSize.Height / 2) + 50;
                    pnn.x =( (this.ClientSize.Width / 5) * 4)+5;
                    pnn.im = new Bitmap("fre.jpg");
                }
                if (ct == 5)
                {
                    pnn.y =-40;
                    pnn.x = ((this.ClientSize.Width / 5) * 1)-20;
                    pnn.im = new Bitmap("air.png");
                    pnn.cWidth = (pnn.im.Width) ;
                    pnn.cHeight = (pnn.im.Height) ;
                }
                if (ct == 6)
                {
                    pnn.y = (this.ClientSize.Height / 3) + 50;
                    pnn.x = ((this.ClientSize.Width / 5) * 1) ;
                    pnn.im = new Bitmap("micro.png"); 
                }
                if (ct == 7)
                {
                    pnn.y = (this.ClientSize.Height / 2) + 155;
                    pnn.x = ((this.ClientSize.Width / 5) * 1)-80;
                    pnn.im = new Bitmap("fan2.jpg");
                    pnn.cWidth = (pnn.im.Width);
                    pnn.cHeight = (pnn.im.Height);
                }
                if (ct == 8)
                {
                    pnn.y = 0;
                    pnn.x = ((this.ClientSize.Width / 5) * 3);
                    pnn.im = new Bitmap("mak.png"); 
                }
                if (ct == 9)
                {
                    pnn.y = (this.ClientSize.Height / 3) + 50;
                    pnn.x = ((this.ClientSize.Width / 5) * 3)+100;
                    pnn.im = new Bitmap("water.png");
                }
                if (ct == 10)
                {
                    pnn.y = (this.ClientSize.Height / 2) + 250;
                    pnn.x = ((this.ClientSize.Width / 5) * 3) + 120;
                    pnn.im = new Bitmap("other.png");
                }
                pnn.im.MakeTransparent(pnn.im.GetPixel(0, 0));
                ct++;
                ldevice.Add(pnn);
            }
        }
        void DrawScene(Graphics g)
        {
            g.Clear(BackColor);          
            for (int i = 0; i < lreq.Count; i++)
            {
                SolidBrush brs = new SolidBrush(lreq[i].clr);
                g.FillRectangle(brs, lreq[i].x, lreq[i].y, lreq[i].w, lreq[i].h);
            }
            for (int i = 0; i < lline.Count; i++)
            {
                SolidBrush brs = new SolidBrush(lline[i].clr);
                g.FillRectangle(brs, lline[i].x, lline[i].y, lline[i].w, lline[i].h);
            }
            for (int i = 0; i < ldevice.Count; i++)
            {
                if (i == 5||i==3)
                {
                    Rectangle rDst = new Rectangle(ldevice[i].x, ldevice[i].y, ldevice[i].cWidth - 160, ldevice[i].cHeight);
                    Rectangle rSrc = new Rectangle(0, 0, ldevice[i].im.Width, ldevice[i].im.Height);
                    g.DrawImage(ldevice[i].im, rDst, rSrc, GraphicsUnit.Pixel);
                }
                else if(i==7)
                {
                    Rectangle rDst = new Rectangle(ldevice[i].x, ldevice[i].y, ldevice[i].cWidth, ldevice[i].cHeight-220);
                    Rectangle rSrc = new Rectangle(0, 0, ldevice[i].im.Width, ldevice[i].im.Height);
                    g.DrawImage(ldevice[i].im, rDst, rSrc, GraphicsUnit.Pixel);
                }
                else
                {
                    g.DrawImage(ldevice[i].im, ldevice[i].x, ldevice[i].y);
                }
            }
            for (int i = 0; i < lbutton.Count; i++)
            {
                Rectangle rDst = new Rectangle(lbutton[i].x, lbutton[i].y, lbutton[i].cWidth/2, lbutton[i].cHeight/2);
                Rectangle rSrc = new Rectangle(0, 0, lbutton[i].im.Width, lbutton[i].im.Height);
                g.DrawImage(lbutton[i].im, rDst, rSrc, GraphicsUnit.Pixel);
            }
        }
        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }

        private void Search_Load(object sender, EventArgs e)
        {
            creatbutton();
            creatreq();
            creatdevice();
            creatlines();
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
        }
    }
}
