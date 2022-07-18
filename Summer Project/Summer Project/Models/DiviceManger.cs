using Remon_Database_Core_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summer_Project.Models
{
    public class Company
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { if (value > 0) id = value; }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value.ToUpper();
            }
        }
        private DataBaseManger manger = new DataBaseManger("Company");
        public bool Add()
        {
            if (this.name is null || this.name == "") return false;
            List<Company>? Temp = (new Company() { name = this.name }).GetAll();
            if (Temp is null || Temp.Count > 0) return false;
            int LastId = new Company().GetAll().Count;
            this.Id = LastId + 1;
            InsertStatment insertStatment = new InsertStatment();
            insertStatment.Attach(this.Id);
            insertStatment.Attach(name);
            manger.Create(insertStatment);
            return true;
        }
        public bool Update()
        {
            if (this.Id == 0) return false;
            if (this.name is null || this.name == "") return false;
            List<Company>? Temp = (new Company() { name = this.name }).GetAll();
            if (Temp is null || Temp.Count > 0) return false;
            Condition condition = new Condition("Id", this.Id);
            SetStatment Set = new SetStatment("Name", this.name);
            manger.Edit(condition, Set);
            return true;
        }
        public static Company? FromStringsToObject(List<string> Strings)
        {
            if (Strings.Count != 2) return null;
            return new Company() { Id = int.Parse(Strings[0]), name = Strings[1] };
        }
        public List<Company>? GetAll()
        {
            Condition condition = new Condition();
            if (this.Id != 0) condition.Attach("Id", this.Id);
            if (this.name != null && this.name != "") condition.Attach("Name", this.name);
            List<List<string>> AllRows = manger.GetAll(condition);
            List<Company> CompanyList = new List<Company>();
            foreach (var item in AllRows) CompanyList.Add(Company.FromStringsToObject(item));
            return CompanyList;
        }
        public bool Delete()
        {
            if (this.Id == 0) return false;
            manger.Delete(new Condition("Id", this.Id));
            return true;
        }
    }
    public class Type
    {

        public int id;
        private string name;
        public int Id
        {
            get { return id; }
            set { if (value > 0) id = value; }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value.ToUpper();
            }
        }
        private DataBaseManger manger = new DataBaseManger("Type");
        public bool Add()
        {
            if (this.Name is null || this.Name == "") return false;
            List<Type>? Temp = (new Type() { Name = this.Name }).GetAll();
            if (Temp is null || Temp.Count > 0) return false;
            int LastId = new Type().GetAll().Count;
            this.Id = LastId + 1;
            InsertStatment insertStatment = new InsertStatment();
            insertStatment.Attach(this.Id);
            insertStatment.Attach(name);
            manger.Create(insertStatment);
            return true;
        }
        public bool Update()
        {
            if (this.Id == 0) return false;
            if (this.Name is null || this.Name == "") return false;
            List<Type>? Temp = (new Type() { Name = this.Name }).GetAll();
            if (Temp is null || Temp.Count > 0) return false;
            Condition condition = new Condition("Id", this.Id);
            SetStatment Set = new SetStatment("Name", this.Name);
            manger.Edit(condition, Set);
            return true;
        }
        public static Type? FromStringsToObject(List<string> Strings)
        {
            if (Strings.Count != 2) return null;
            return new Type() { Id = int.Parse(Strings[0]), Name = Strings[1] };
        }
        public List<Type>? GetAll()
        {
            Condition condition = new Condition();
            if (this.Id != 0) condition.Attach("Id", this.Id);
            if (this.Name != null && this.Name != "") condition.Attach("Name", this.Name);
            List<List<string>> AllRows = manger.GetAll(condition);
            List<Type> TypeList = new List<Type>();
            foreach (var item in AllRows) TypeList.Add(Type.FromStringsToObject(item));
            return TypeList;
        }
        public bool Delete()
        {
            if (this.Id == 0) return false;
            manger.Delete(new Condition("Id", this.Id));
            return true;
        }
    }
    public class Divice
    {
        private int id;
        private int companyId;
        private int typeId;
        public string Color;
        private float price;
        private int locationNumber;
        public string Details;
        public string SerialNumber;
        public int Id
        {
            get { return id; }
            set { if (value > 0) id = value; }
        }
        private string companyName = "";
        public string? CompanyName
        {
            get
            {
                if (CompanyId == 0) return null;
                Company company = new Company() { Id = this.CompanyId };
                List<Company> x = company.GetAll();
                return x.Count > 0 ? x[0].Name : null;
            }
            set
            {
                Company company = new Company() { Name = value };
                List<Company> x = company.GetAll();
                if (x.Count > 0) CompanyId = x[0].Id;
                companyName = value;
            }
        }
        private int CompanyId
        {
            get { return companyId; }
            set
            {
                companyId = value;
            }
        }
        private string typeName = "";
        public string? TypeName
        {
            get
            {
                if (TypeId == 0) return null;
                Type Type = new Type() { Id = this.TypeId };
                List<Type> x = Type.GetAll();
                return x.Count > 0 ? x[0].Name : null;
            }
            set
            {
                Type Type = new Type() { Name = value };
                List<Type> x = Type.GetAll();
                if (x.Count > 0) TypeId = x[0].Id;
                typeName = value;
            }
        }
        private int TypeId
        {
            get { return typeId; }
            set { typeId = value; }
        }
        public float Price
        {
            get { return price; }
            set { if (value > 0) price = value; }
        }
        public int LocationNumber
        {
            get { return locationNumber; }
            set { if (value >= 1 && value <= 3) locationNumber = value; }
        }
        public bool AllIsSet() =>
            CompanyName != "" && TypeName != "" && Color != null && Color != "" && Price != 0 && LocationNumber != 0 && Details != null && Details != "";
        private static DataBaseManger DiviceManger = new DataBaseManger("Divice");
        private static DataBaseManger SerialManger = new DataBaseManger("DiviceSerial");
        public static Divice? FromStringsToDivice(List<string> Strings)
        {
            if (Strings.Count != 7) return null;
            Divice? divice = new Divice()
            {
                Id = int.Parse(Strings[0]),
                CompanyId = int.Parse(Strings[1]),
                TypeId = int.Parse(Strings[2]),
                Color = Strings[3],
                Price = float.Parse(Strings[4]),
                LocationNumber = int.Parse(Strings[5]),
                Details = Strings[6]
            };
            return divice;
        }
        public bool Add()
        {
            if (!AllIsSet()) return false;
            Divice? divice = new Divice()
            {
                CompanyId = this.CompanyId,
                TypeId = this.TypeId,
                Color = this.Color,
                Price = this.Price,
                LocationNumber = this.LocationNumber,
            };
            List<Divice> devices = divice.GetAll();
            if (devices.Count == 0)
            {
                if (CompanyId == 0)
                {
                    Company company = new Company() { Name = companyName };
                    company.Add();
                    this.CompanyId = company.Id;
                }
                if (TypeId == 0)
                {
                    Type Type = new Type() { Name = typeName };
                    Type.Add();
                    this.TypeId = Type.Id;
                }
                int LastId = DiviceManger.GetAll().Count;
                this.Id = LastId + 1;
                InsertStatment insertStatment = new InsertStatment(Id);
                insertStatment.Attach(CompanyId);
                insertStatment.Attach(TypeId);
                insertStatment.Attach(Color);
                insertStatment.Attach(Price);
                insertStatment.Attach(LocationNumber);
                insertStatment.Attach(Details);
                DiviceManger.Create(insertStatment);
            }
            if (SerialNumber != null && SerialNumber != "")
            {
                divice = new Divice()
                {
                    CompanyId = this.CompanyId,
                    TypeId = this.TypeId,
                    Color = this.Color,
                    Price = this.Price,
                    LocationNumber = this.LocationNumber,
                    Details = this.Details
                };
                devices = divice.GetAll();
                if (devices.Count > 0)
                {
                    divice = new Divice() { SerialNumber = this.SerialNumber };
                    if (divice.GetAll().Count > 0) return false;
                    InsertStatment insertStatment = new InsertStatment(devices[0].Id);
                    insertStatment.Attach(this.SerialNumber);
                    SerialManger.Create(insertStatment);
                }
            }
            return true;
        }
        public bool Update()
        {
            if (SerialNumber == null || SerialNumber == "") return false;
            List<List<string>> x = SerialManger.GetAll(new Condition("SerialNumber", SerialNumber));
            if (x.Count == 0) return false;
            int OldId = int.Parse(x[0][0]);
            Divice OldDivice =  new Divice() { Id = OldId };
            OldDivice = OldDivice.GetAll()[0];
            if(this.CompanyId == 0) CompanyId = OldDivice.CompanyId;
            if(this.TypeId == 0) TypeId = OldDivice.TypeId;
            if (this.Color == null || this.Color == "") this.Color = OldDivice.Color;
            if(this.price == 0) this.price = OldDivice.Price;
            if(this.LocationNumber == 0) this.LocationNumber = OldDivice.LocationNumber;
            if(this.Details == null || this.Details == "") this.Details = OldDivice.Details;
            OldDivice.SerialNumber = SerialNumber;
            OldDivice.Delete();
            this.Add();
            return true;
        }
        public List<Divice> GetAll()
        {
            List<Divice> result = new List<Divice>();
            if (SerialNumber != null && SerialNumber != "")
            {
                List<List<string>> temp = SerialManger.GetAll(new Condition("SerialNumber", SerialNumber));
                if (temp.Count == 0) return result;
                this.Id = int.Parse(temp[0][0]);
            }
            Condition condition = new Condition();
            if (Id != 0) condition.Attach("Id", Id);
            if (CompanyId != 0) condition.Attach("CompanyId", CompanyId);
            if (TypeId != 0) condition.Attach("TypeId", TypeId);
            if (Color != null && Color != "") condition.Attach("color", Color);
            if (Price != 0) condition.Attach("Price", Price);
            if (LocationNumber != 0) condition.Attach("LocationNumber", LocationNumber);
            List<List<string>> x = DiviceManger.GetAll(condition);
            foreach (var item in x)
            {
                result.Add(Divice.FromStringsToDivice(item));
            }
            return result;
        }
        public List<List<string>> GetAllWithSerial()
        {
            Condition condition = new Condition();
            if (Id != 0) condition.Attach("Id", Id);
            if (CompanyId != 0) condition.Attach("CompanyId", CompanyId);
            if (TypeId != 0) condition.Attach("TypeId", TypeId);
            if (Color != null && Color != "") condition.Attach("color", Color);
            if (Price != 0) condition.Attach("Price", Price);
            if (LocationNumber != 0) condition.Attach("LocationNumber", LocationNumber);
            List<List<string>> x = DiviceManger.GetAll(condition);
            List<Divice> Matches = new List<Divice>();
            foreach (var item in x)
            {
                Matches.Add(Divice.FromStringsToDivice(item));
            }
            List<List<string>> result = new List<List<string>>() { new List<string>() { "Divice Id", "Serial Numbers"} };
            foreach (var item in Matches)
            {
                List<List<string>> Serials = SerialManger.GetAll(new Condition("DiviceId", item.Id));
                foreach (var Serial in Serials)
                {
                    result.Add(Serial);
                }
            }
            return result;
        }
        public bool Delete()
        {
            if (SerialNumber == "") return false;
            SerialManger.Delete(new Condition("SerialNumber", SerialNumber));
            return true;
        }
        public static List<List<string>> FromDivicesToStrings(List<Divice> divices)
        {
            List<List<string>> vs = new List<List<string>>();
            vs.Add(new List<string> { "Id", "Company Name", "Type Name", "Color", "Price", "Details", "LocationNumber", "Number of Divice" });
            foreach (Divice divice in divices)
            {
                List<string> list = new List<string>();
                list.Add(divice.Id.ToString());
                list.Add(divice.CompanyName.ToString());
                list.Add(divice.TypeName.ToString());
                list.Add(divice.Color.ToString());
                list.Add(divice.Price.ToString());
                list.Add(divice.Details.ToString());
                list.Add(divice.LocationNumber.ToString());
                list.Add(divice.GetDiviceNumber().ToString());
                vs.Add(list);
            }
            return vs;
        }
        int GetDiviceNumber()
        {
            return SerialManger.GetAll(new Condition("DiviceId", this.Id)).Count;
        }
    }
}
