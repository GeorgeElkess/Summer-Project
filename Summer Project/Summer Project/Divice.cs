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

namespace Summer_Project
{
    public partial class Divice : Form
    {
        System.Windows.Forms.Timer tt=new System.Windows.Forms.Timer();   
        Bitmap off;
        int ct = 0,counttick=0;
        List<pic> lpicture = new List<pic>();
        List<pic> llogos = new List<pic>();
        List<TextBox> listtext = new List<TextBox>();
        public List<string> litem = new List<string>();
        public List<int> ldevic_id = new List<int>();
        public Divice()
        {
           
            WindowState = FormWindowState.Maximized;
            InitializeComponent();
            tt.Tick += new EventHandler(tt_Tick);
            tt.Start();
        }
        
        void tt_Tick(object sender, EventArgs e)
        {
            if (counttick == 1)
            {
                DrawDubb(this.CreateGraphics());
            }
        }
        void creatpicture()
        {
            pic pnn;
            pnn = new pic();
            pnn.x = this.ClientSize.Width-250;
            pnn.y = 3;
            pnn.im = new Bitmap("1.png");
        //    pnn.im.MakeTransparent(pnn.im.GetPixel(0, 0));
            lpicture.Add(pnn);
        }
        void creatlogo()
        {
            for (int i = 0; i < 8; i++)
            {
                pic pnn;
                pnn = new pic();
               
                if (ct == 0)
                {
                    pnn.y = this.ClientSize.Height - 160;
                    pnn.x = 5;
                    pnn.im = new Bitmap("LG.jpg");
                }
                if (ct == 1)
                {
                    pnn.y = this.ClientSize.Height - 180;
                    pnn.x = 225;
                    pnn.im = new Bitmap("sam.png");
                }
                if (ct == 2)
                {
                    pnn.y = this.ClientSize.Height - 180;
                    pnn.x = 460;
                    pnn.im = new Bitmap("toshiba.png");
                }
                if(ct == 3)
                {
                    pnn.y = this.ClientSize.Height - 130;
                    pnn.x = 700;
                    pnn.im = new Bitmap("fresh.png");
                }
                if(ct==4)
                {
                    pnn.y = this.ClientSize.Height - 160;
                    pnn.x = 1050;
                    pnn.im = new Bitmap("union.png");
                }
                if(ct==5)
                {
                    pnn.y = this.ClientSize.Height - 100;
                    pnn.x = 1200;
                    pnn.im = new Bitmap("uni.png");
                }
                if(ct==6)
                {
                    pnn.y = this.ClientSize.Height - 150;
                    pnn.x = 1430;
                    pnn.im = new Bitmap("kir.png");
                }
                if (ct == 7)
                {
                    pnn.y = this.ClientSize.Height - 120;
                    pnn.x = 1655;
                    pnn.im = new Bitmap("al.png");
                }

                pnn.im.MakeTransparent(pnn.im.GetPixel(0, 0));
                ct++;
                llogos.Add(pnn);
            }
        }
        void DrawScene(Graphics g)
        {
            g.Clear(BackColor);
            g.DrawImage(lpicture[0].im, lpicture[0].x, lpicture[0].y);
            for (int i = 0; i < llogos.Count; i++)
            {
                g.DrawImage(llogos[i].im, llogos[i].x, llogos[i].y);
            }
        }
        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar)) e.Handled = true;
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar)) e.Handled = true;
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar)) e.Handled = true;
        }
        int Id, LocationNumber;
        float Price;
        string Serial, Company, Type, Color, Details;
        bool GetIntData(TextBox textBox, ref int value)
        {
            if (textBox.Text == "") return false;
            value = int.Parse(textBox.Text);
            return true;
        }
        bool GetFloatData(TextBox textBox, ref float value)
        {
            if (textBox.Text == "") return false;
            value = float.Parse(textBox.Text);
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!GetStringData(textBox2, ref Serial)) { View.Error("Serial is Required"); return; }
            Models.Divice divice = new Models.Divice() { SerialNumber = this.Serial };
            if (GetStringData(textBox3, ref Company)) divice.CompanyName = Company;
            if (GetStringData(textBox4, ref Type)) divice.TypeName = Type;
            if (GetStringData(textBox5, ref Color)) divice.Color = Color;
            if (GetFloatData(textBox6, ref Price)) divice.Price = Price;
            if (GetIntData(textBox7, ref LocationNumber)) divice.LocationNumber = LocationNumber;
            if (GetStringData(richTextBox1, ref Details)) divice.Details = Details;
            if (!divice.Update()) { View.Error("Something went wrong\nPlease Try put a valid values"); return; }
            View.Inform("Updated Sucessfuly");
            InitializeAllInput();
        }
        public void but3()
        {
            Models.Divice divice = new Models.Divice();
            if (!GetIntData(textBox1, ref Id)) divice.Id = Id;
            if (GetStringData(textBox2, ref Serial)) divice.SerialNumber = Serial;
            if (GetStringData(textBox3, ref Company)) divice.CompanyName = Company;
            if (GetStringData(textBox4, ref Type)) divice.TypeName = Type;
            if (GetStringData(textBox5, ref Color)) divice.Color = Color;
            if (GetFloatData(textBox6, ref Price)) divice.Price = Price;
            if (GetIntData(textBox7, ref LocationNumber)) divice.LocationNumber = LocationNumber;
            if (GetStringData(richTextBox1, ref Details)) divice.Details = Details;
            dataGridView1.DataSource = View.GetTable(Models.Divice.FromDivicesToStrings(divice.GetAll()));
        }
        private void button3_Click(object sender, EventArgs e)
        {
            but3();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!GetStringData(textBox2, ref Serial)) { View.Error("Serial is Required"); return; }
            Models.Divice divice = new Models.Divice() { SerialNumber = this.Serial };
            if (!divice.Delete()) { View.Error("Something went wrong\nPlease Try put a valid values"); return; }
            View.Inform("Deleted Sucessfuly");
            InitializeAllInput();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Models.Divice divice = new Models.Divice();
            if (!GetIntData(textBox1, ref Id)) divice.Id = Id;
            if (GetStringData(textBox2, ref Serial)) divice.SerialNumber = Serial;
            if (GetStringData(textBox3, ref Company)) divice.CompanyName = Company;
            if (GetStringData(textBox4, ref Type)) divice.TypeName = Type;
            if (GetStringData(textBox5, ref Color)) divice.Color = Color;
            if (GetFloatData(textBox6, ref Price)) divice.Price = Price;
            if (GetIntData(textBox7, ref LocationNumber)) divice.LocationNumber = LocationNumber;
            if (GetStringData(richTextBox1, ref Details)) divice.Details = Details;
            dataGridView1.DataSource = View.GetTable(divice.GetAllWithSerial());
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
        int counter = 0;
        private void Divice_Load(object sender, EventArgs e)
        {
            but3();         
            creatpicture();
            creatlogo();
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            DrawDubb(this.CreateGraphics());
            counttick = 1;
        }
        private void button8_Click(object sender, EventArgs e)
        {
           // storinlitem("FRIDGE");
           // MessageBox.Show(litem[0].ToString());
           // MessageBox.Show(ldevic_id[0].ToString());
        }
        void storinlitem(string x)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[2].Value.ToString() ==x)
                {
                    for (int j = 0; j < litem.Count; j++)
                    {
                        if (dataGridView1.Rows[i].Cells[1].Value.ToString() == litem[j])
                        {
                            counter++;
                        }
                    }
                    if (counter == 0)
                    {
                        litem.Add(dataGridView1.Rows[i].Cells[1].Value.ToString());
                        ldevic_id.Add(int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString()));
                    }
                    counter = 0;
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void button7_Click(object sender, EventArgs e)
        {
            storinlitem("FRIDGE");
            Search sc=new Search();
            for (int i = 0; i < litem.Count; i++)
            {
                sc.recivedatafromdivice(v: litem[i]);
            }
            this.Hide();
            sc.ShowDialog();
            this.Close();
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string name;
            name=textBox4.Text.ToUpper();
            if (name == "FRIDGE")
            {
                AddNewTextBox(630);
                AddNewLabel(630,"Adam");
            }
            if (name == "MKNSA")
            {
                AddNewTextBox(630);
                AddNewLabel(630, "Watt");
            }
            if (name == "FORN")
            {
                 AddNewTextBox(630);
                 AddNewLabel(630, "Liter");
                 AddNewRadioButton(680,20, "ميكرويف");
                 AddNewRadioButton(680,160, "فرن");
                AddNewCheckBox(720, 20, "شوايه");
            }
        }
        public System.Windows.Forms.RadioButton AddNewRadioButton(int x,int y, string n)
        {
            System.Windows.Forms.RadioButton txt = new System.Windows.Forms.RadioButton();
            this.Controls.Add(txt);
            txt.Top = x;
            txt.Left = y;
            txt.Width = 133;
            txt.Height = 27;
            txt.Text = n;
            return txt;
        }
        public System.Windows.Forms.CheckBox AddNewCheckBox(int x, int y, string n)
        {
            System.Windows.Forms.CheckBox txt = new System.Windows.Forms.CheckBox();
            this.Controls.Add(txt);
            txt.Top = x;
            txt.Left = y;
            txt.Width = 133;
            txt.Height = 27;
            txt.Text = n;
            return txt;
        }
       
        public System.Windows.Forms.TextBox AddNewTextBox(int x)
        {
            System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();
            this.Controls.Add(txt);
            txt.Name = "dtextbox";
            txt.Top = x;
            txt.Left = 62;
            txt.Width = 133;
            txt.Height = 27;
            txt.Text = "";
            listtext.Add(txt);
            return txt;

        }
        public System.Windows.Forms.Label AddNewLabel(int x, string n)
        {
            System.Windows.Forms.Label txt = new System.Windows.Forms.Label();
            this.Controls.Add(txt);
            txt.Top = x;
            txt.Left = 4;
            txt.Width = 62;
           txt.Height = 23;
            txt.Text = n;
            txt.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            return txt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }  
        bool GetStringData(TextBox textBox, ref string value)
        {
            if (textBox.Text == "") return false;
            value = textBox.Text;
            return true;
        }
        bool GetStringData(RichTextBox textBox, ref string value)
        {
            if (textBox.Text == "") return false;
            value = textBox.Text;
            return true;
        }
        bool GetAllData()
        {
            if (!GetStringData(textBox2, ref Serial)) { View.Error("Serial is Required"); return false; }
            if (!GetStringData(textBox3, ref Company)) { View.Error("Company is Required"); return false; }
            if (!GetStringData(textBox4, ref Type)) { View.Error("Type is Required"); return false; }
            if (!GetStringData(textBox5, ref Color)) { View.Error("Color is Required"); return false; }
            if (!GetFloatData(textBox6, ref Price)) { View.Error("Price is Required"); return false; }
            if (!GetIntData(textBox7, ref LocationNumber)) { View.Error("LocationNumber is Required"); return false; }
            if (!GetStringData(richTextBox1, ref Details)) { View.Error("Details is Required"); return false; }
            return true;
        }
        void InitializeAllInput()
        {

                    dataGridView1.DataSource = View.GetTable(Models.Divice.FromDivicesToStrings(new Models.Divice().GetAll()));
                    Id = 0;
                    LocationNumber = 0;
                    Price = 0;
                    Serial = "";
                    Company = "";
                    Type = "";
                    Color = "";
                    Details = "";
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                    for (int i = 0; i < listtext.Count; i++)
                    {
                        listtext[i].Text = "";
                    }
                    richTextBox1.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!GetAllData()) return;
            Models.Divice divice = new Models.Divice();
            divice.SerialNumber = Serial;
            divice.CompanyName = this.Company;
            divice.TypeName = this.Type;
            divice.Color = this.Color;
            divice.Price = this.Price;
            divice.LocationNumber = this.LocationNumber;
            divice.Details = this.Details;
            string n = textBox4.Text.ToUpper();
            if (n== "FRIDGE")
            {
                Fridge1 fridge2=new Fridge1();
                fridge2.Adam = int.Parse(listtext[0].Text);
                fridge2.SerialNumber = Serial;
                fridge2.insert();
                if (listtext[0].Text != null)
                {
                    if (!divice.Add()) { View.Error("Something went wrong\nPlease Try put a valid values"); return; }
                    View.Inform("Added Sucessfuly");
                    InitializeAllInput();
                }
            }
            else
            {
                if (!divice.Add()) { View.Error("Something went wrong\nPlease Try put a valid values"); return; }
                View.Inform("Added Sucessfuly");
                InitializeAllInput();
            }
                    
        }
    }
    class pic
    {
        public int x, y,w,h;
        public int cWidth, cHeight;
        public Bitmap im;
        public Color clr;
    }

}
