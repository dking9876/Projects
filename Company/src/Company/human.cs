
namespace Company
{
    class human
    {
        private string Worker_id;
        private readonly string first_name;
        protected string last_name;
        private string address;

        public human(string worker_id, string first_name, string last_name, string address)
        {
            Worker_id = worker_id;
            this.first_name = first_name;
            this.last_name = last_name;
            this.address = address;
        }

        public void  SetWorkerId(string Worker_id)
        {
            this.Worker_id = Worker_id;
        }
  
        public void SetLastName(string last_name)
        {
            this.last_name = last_name;
        }
        public void SetAddress(string address)
        {
            this.address = address;
        }
        public string GetWorkerId()
        {
            return this.Worker_id;
        }

        public string GetFirst_Name()
        {
            return this.first_name;
        }
        public string GetLastName()
        {
            return this.last_name;
        }
        public string GetAddress()
        {
            return this.address;
        }
        public override string ToString()
        {
            return " workerId: " + Worker_id + " first name: " + first_name + 
                " LastName: " + last_name + " ADDRESS: " + address;

        }

        public virtual double CalcSalary()
        {
            return 5300;
        }
    }
}
