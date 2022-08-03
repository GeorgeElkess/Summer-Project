using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Remon_Database_Core_System.Models;
using Summer_Project.Models;
using View = Remon_Database_Core_System.Models.View;
using System.Data.SqlClient;
using System.Configuration;


namespace Summer_Project
{
    public partial class fridge : Form
    {
        System.Windows.Forms.Timer tt = new System.Windows.Forms.Timer();
        Bitmap off;
        int ct = 0, counttick = 0, flag = 0, counter = 0;
        List<pic> lpicture = new List<pic>();
        List<pic> lfridge = new List<pic>();
        List<string> list = new List<string>();
        List<string> listadam = new List<string>();
        List<pic> lbutton = new List<pic>();
        public fridge()
        {

            WindowState = FormWindowState.Maximized;
            tt.Tick += new EventHandler(tt_Tick);
            MouseDown += Form1_MouseDown;
            InitializeComponent();
        }
        void tt_Tick(object sender, EventArgs e)
        {
            if (counttick == 1)
            {
                DrawDubb(this.CreateGraphics());
            }
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y >= 3 && e.Y < 60 && e.X >= 3 && e.X < 63)
            {
                Search dv = new Search();
                this.Hide();
                dv.ShowDialog();
                this.Close();
            }
        }
        void creatbutton()
        {
            pic pnn;
            pnn = new pic();
            pnn.y = 3;
            pnn.x = 3;
            pnn.im = new Bitmap("back3.png");
            pnn.cWidth = (pnn.im.Width);
            pnn.cHeight = (pnn.im.Height);
            //  pnn.im.MakeTransparent(pnn.im.GetPixel(0, 0));
            lbutton.Add(pnn);
        }
        void creatpicture()
        {
            pic pnn;
            pnn = new pic();
            pnn.x = this.ClientSize.Width - 250;
            pnn.y = 3;
            pnn.im = new Bitmap("1.png");
            lpicture.Add(pnn);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string x = comboBox1.SelectedItem.ToString();
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString() ==x)
                {
                    dataGridView1.Rows[i].Visible = true;
                }
                else
                {
                    if (i == 0)
                    {
                        dataGridView1.Rows[0].DefaultCellStyle.ForeColor = Color.Gray; 
                        dataGridView1.Rows[0].DefaultCellStyle.BackColor = Color.Gray;
                        dataGridView1.Rows[0].DefaultCellStyle.SelectionForeColor= Color.Gray;
                        dataGridView1.Rows[0].DefaultCellStyle.SelectionBackColor = Color.Gray;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Visible = false;
                    }
                }
            }
        }
        void storinadam()
        {
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                for (int j = 0; j < listadam.Count; j++)
                {

                    if (dataGridView1.Rows[i].Cells[6].Value.ToString() == listadam[j])
                    {
                       counter++;
                    }
                }
                    if (counter == 0)
                    {               
                     listadam.Add(dataGridView1.Rows[i].Cells[6].Value.ToString());
                    }
                     counter = 0;
            }
        }
        internal void recivedatafromdivice(string v)
        {
            
            list.Add(v);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.Yellow;
            if (flag == 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    comboBox1.Items.Add(list[i]);
                }
                for (int i = 0; i < listadam.Count; i++)
                {
                    comboBox2.Items.Add(listadam[i]);
                }
                flag = 1;
            }
        }
        void DrawScene(Graphics g)
        {
            g.Clear(BackColor);
            g.DrawImage(lpicture[0].im, lpicture[0].x, lpicture[0].y);
            for (int i = 0; i < lfridge.Count; i++)
            {
                g.DrawImage(lfridge[i].im, lfridge[i].x, lfridge[i].y);
            }
            for (int i = 0; i < lbutton.Count; i++)
            {
                Rectangle rDst = new Rectangle(lbutton[i].x, lbutton[i].y, lbutton[i].cWidth / 2, lbutton[i].cHeight / 2);
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

        private void button2_Click(object sender, EventArgs e)
        {
            Fridge1 f = new Fridge1();
            dataGridView1.DataSource = f.viewindatagrid();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string x = comboBox2.SelectedItem.ToString();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells[6].Value.ToString() == x)
                {
                    dataGridView1.Rows[i].Visible = true;
                }
                else
                {
                    if (i == 0)
                    {
                        dataGridView1.Rows[0].DefaultCellStyle.ForeColor = Color.Gray;
                        dataGridView1.Rows[0].DefaultCellStyle.BackColor = Color.Gray;
                        dataGridView1.Rows[0].DefaultCellStyle.SelectionForeColor = Color.Gray;
                        dataGridView1.Rows[0].DefaultCellStyle.SelectionBackColor = Color.Gray;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Visible = false;
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {    
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Fridge1 f = new Fridge1();
            dataGridView1.DataSource = f.viewindatagrid();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            DataBaseManger d = new DataBaseManger("Divice");
            for(int i=0;i< dataGridView1.Rows.Count-1; i++)
            {
                    
                    //SqlCommand cmd = new SqlCommand("Update Divice,Company,DiviceSerial,Fridge set Name=@Name,DiviceSerial.SerialNumber=@DiviceSerial.SerialNumber," +
                    //    "Color=@Color,Price=@Price,LocationNumber=@LocationNumber,Details=@Details,Adam=@Adam where Company.Id=@Company.Id,DiviceSerial.SerialNumber=@DiviceSerial.SerialNumber" +
                    //    "Divice.I@Divice.Id,Fridge.SerialNumber=@Fridge.SerialNumber", d.con);
                    SqlCommand cmd = new SqlCommand("Update Divice set Color=@Color,Price=@Price,LocationNumber=@LocationNumber,Details=@Details where Id=@Id",d.con);
                    SqlCommand cmd2 = new SqlCommand("Update Company set Name=@Name where Id=@Id1",d.con);

                    //  cmd.Parameters.AddWithValue("@DiviceSerial.SerialNumber", dataGridView1.Rows[i].Cells[1].Value);
                    cmd.Parameters.AddWithValue("@Color",dataGridView1.Rows[i].Cells[2].Value);
                    cmd.Parameters.AddWithValue("@Id",dataGridView1.Rows[i].Cells[7].Value);
                    cmd2.Parameters.AddWithValue("@Name", dataGridView1.Rows[i].Cells[0].Value);
                    cmd2.Parameters.AddWithValue("@Id1", dataGridView1.Rows[i].Cells[8].Value);
                    cmd.Parameters.AddWithValue("@Price", dataGridView1.Rows[i].Cells[3].Value);
                    cmd.Parameters.AddWithValue("@LocationNumber", dataGridView1.Rows[i].Cells[4].Value);
                    cmd.Parameters.AddWithValue("@Details", dataGridView1.Rows[i].Cells[5].Value);
                //cmd.Parameters.AddWithValue("@Adam", dataGridView1.Rows[i].Cells[6].Value);
                    d.con.Open();
                    cmd.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    d.con.Close();
            }
            MessageBox.Show("Successfully Updated");
        }

        public void check()
        {

        }
        void creatdevice()
        {

            pic pnn;
            pnn = new pic();
            if (ct == 0)
            {
                pnn.y = (this.ClientSize.Height/6);
                pnn.x = (this.ClientSize.Width-400) ;
                pnn.im = new Bitmap("fridge.png");
                pnn.im.MakeTransparent(pnn.im.GetPixel(0, 0));
                lfridge.Add(pnn);
            }
        }
        private void fridge_Load(object sender, EventArgs e)
        {
            
            Fridge1 f = new Fridge1();        
            dataGridView1.DataSource = f.viewindatagrid();
            storinadam();
            creatdevice();
            creatbutton();
            creatpicture();
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            DrawDubb(this.CreateGraphics());
            counttick = 1;
        }
    }
}
