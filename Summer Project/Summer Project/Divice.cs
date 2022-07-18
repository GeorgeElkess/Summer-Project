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

namespace Summer_Project
{
    public partial class Divice : Form
    {
        public Divice()
        {
            InitializeComponent();
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

        private void button3_Click(object sender, EventArgs e)
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
            if (!divice.Add()) { View.Error("Something went wrong\nPlease Try put a valid values"); return; }
            View.Inform("Added Sucessfuly");
            InitializeAllInput();
        }
    }
}
