using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summer_Project
{
    public class Cell
    {
        public bool IsString = false;
        public string String = null;
        public float Value = 0;
    }
    public class GenerateInsert
    {
        List<Cell> sql = new List<Cell>();
        public void Attach(string NewString)
        {
            Cell cell = new Cell();
            cell.String = NewString;
            cell.IsString = true;
            sql.Add(cell);
        }
        public void Attach(float NewValue)
        {
            Cell cell = new Cell();
            cell.Value = NewValue;
            sql.Add(cell);
        }
        public string? GetInsertStatment()
        {
            if (sql.Count == 0) return null;
            string Line = "";
            for (int i = 0; i < sql.Count; i++)
            {
                if (sql[i].IsString) Line += $"'{sql[i].String}'";
                else Line += $"{sql[i].Value}";
                if (i < sql.Count - 1) Line += ", ";
            }
            return Line;
        }
    }
    public class DoubleCell : Cell
    {
        public string ColmnName = "";
    }
    public class GenerateCondition
    {
        List<DoubleCell> condition = new List<DoubleCell>();
        public void Attach(string ColumnName, string NewString)
        {
            DoubleCell cell = new DoubleCell();
            cell.String = NewString;
            cell.IsString = true;
            cell.ColmnName = ColumnName;
            condition.Add(cell);
        }
        public void Attach(string ColumnName, float NewValue)
        {
            DoubleCell cell = new DoubleCell();
            cell.Value = NewValue;
            cell.ColmnName = ColumnName;
            condition.Add(cell);
        }
        public string? GetCondition()
        {
            if (condition.Count == 0) return null;
            string Line = "";
            for (int i = 0; i < condition.Count; i++)
            {
                Line += $"{condition[i].ColmnName}=";
                if (condition[i].IsString) Line += $"'{condition[i].String}'";
                else Line += $"{condition[i].Value}";
                if (i < condition.Count - 1) Line += " and ";
            }
            return Line;
        }
    }
    public class GenerateSet
    {
        List<DoubleCell> condition = new List<DoubleCell>();
        public void Attach(string ColumnName, string NewString)
        {
            DoubleCell cell = new DoubleCell();
            cell.String = NewString;
            cell.IsString = true;
            cell.ColmnName = ColumnName;
            condition.Add(cell);
        }
        public void Attach(string ColumnName, float NewValue)
        {
            DoubleCell cell = new DoubleCell();
            cell.Value = NewValue;
            cell.ColmnName = ColumnName;
            condition.Add(cell);
        }
        public string? GetSetStatment()
        {
            if (condition.Count == 0) return null;
            string Line = "";
            for (int i = 0; i < condition.Count; i++)
            {
                Line += $"{condition[i].ColmnName}=";
                if (condition[i].IsString) Line += $"'{condition[i].String}'";
                else Line += $"{condition[i].Value}";
                if (i < condition.Count - 1) Line += ", ";
            }
            return Line;
        }
    }
    internal class DataBaseManger
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-82NJIQUH;Initial Catalog=Msa;Integrated Security=True;");
        public string TableName;
        public DataBaseManger(string TableName)
        {
            this.TableName = TableName;
        }
        public void Insert(GenerateInsert x)
        {
            string Data = x.GetInsertStatment();
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert Into " + TableName + " Values(" + Data + ")");
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public List<List<string>> Read(GenerateCondition x)
        {
            string Condition = x.GetCondition();
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * From " + TableName + ((Condition != "") ? " Where " + Condition : ""));
            cmd.Connection = con;
            SqlDataReader rdr = cmd.ExecuteReader();
            List<List<string>> Data = new List<List<string>>();
            while (rdr.Read())
            {
                List<string> DataList = new List<string>();
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    DataList.Add(rdr[i].ToString());
                }
                Data.Add(DataList);
            }
            con.Close();
            return Data;
        }
        /// <summary>
        /// Make DataTable for Printing From the information
        /// </summary>
        /// <param name="Headers">The Header of the Table</param>
        /// <param name="Data">the Info of the Table</param>
        /// <returns>The DataTable for Printing</returns>
        public DataTable GetTable(List<string> Headers, List<List<string>> Data)
        {
            DataTable Table = new DataTable();
            foreach (string item in Headers) Table.Columns.Add(item);
            for (int i = 0; i < Data.Count; i++) Table.Rows.Add(Data[i].ToArray());
            return Table;
        }
        public void Delete(GenerateCondition x)
        {
            string Condition = x.GetCondition();
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete " + TableName + " Where " + Condition);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void Update(GenerateCondition x, GenerateSet y)
        {
            string Condition = x.GetCondition();
            string Set = y.GetSetStatment();
            con.Open();
            SqlCommand cmd = new SqlCommand("Update " + TableName + " Set " + Set + " Where " + Condition);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
