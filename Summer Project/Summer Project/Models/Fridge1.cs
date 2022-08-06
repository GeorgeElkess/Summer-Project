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

namespace Summer_Project.Models
{
    public class Fridge1
    {
        public string SerialNumber;
        public int Adam;
        private DataBaseManger Fridge =new DataBaseManger("Fridge");
        public void insert()
        {
            InsertStatment obj = new InsertStatment();
            obj.Attach(Adam);
            obj.Attach(SerialNumber);
            Fridge.Create(obj);
        }  
        public DataTable viewindatagrid()
        {
            DataBaseManger d = new DataBaseManger("Divice");
            string quary = "select Name,DiviceSerial.SerialNumber,Color,Price,LocationNumber,Details,Adam,Divice.Id,Company.Id " +
                "from Divice,Company,DiviceSerial,Fridge " +
                "Where Company.Id=Divice.CompanyId and DiviceSerial.DiviceId=Divice.Id and DiviceSerial.SerialNumber=Fridge.SerialNumber";
            SqlCommand cmd = new SqlCommand(quary, d.con);
            d.con.Open();
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;

        }
        public void update()
        {
           
        }
    }
}
