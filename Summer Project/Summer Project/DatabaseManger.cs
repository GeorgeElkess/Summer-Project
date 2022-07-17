using Microsoft.Data.SqlClient;

namespace Remon_Database_Core_System.Models
{
    public class Cell
    {
        public bool IsString = false;
        public string String = null;
        public float Value = 0;
    }
    public class InsertStatment
    {
        List<Cell> sql = new List<Cell>();
        public InsertStatment() { }
        public InsertStatment(string NewString) => Attach(NewString);
        public InsertStatment(float NewValue) => Attach(NewValue);
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
        public string? GenerateInsertStatment()
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
    public class Condition
    {
        List<DoubleCell> condition = new List<DoubleCell>();
        public Condition() { }
        public Condition(string ColumnName, float value) => Attach(ColumnName, value);
        public Condition(string ColumnName, string value) => Attach(ColumnName, value);
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
        public string? GenerateCondition()
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
    public class SetStatment
    {
        List<DoubleCell> condition = new List<DoubleCell>();
        public SetStatment() { }
        public SetStatment(string ColumnName, string NewString) => Attach(ColumnName, NewString);
        public SetStatment(string ColumnName, float NewValue) => Attach(ColumnName, NewValue);
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
        public string? GenerateSetStatment()
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
    public class DataBaseManger
    {
        string TableName;
        public DataBaseManger(string TableName) => this.TableName = TableName;
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-82NJIQUH;Initial Catalog=Remon's Database;Integrated Security=True;");
        public void Create(InsertStatment insert)
        {
            string Data = insert.GenerateInsertStatment();
            con.Open();
            SqlCommand cmd = new SqlCommand($"Insert Into {TableName} Values({Data})");
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public List<List<string>> GetAll(Condition x = null)
        {
            string Condition = x is null ? "" : x.GenerateCondition();
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
        public void Delete(Condition x)
        {
            string Condition = x.GenerateCondition();
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete " + TableName + " Where " + Condition);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void Edit(Condition x, SetStatment y)
        {
            string Condition = x.GenerateCondition();
            string Set = y.GenerateSetStatment();
            con.Open();
            SqlCommand cmd = new SqlCommand("Update " + TableName + " Set " + Set + " Where " + Condition);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
