namespace Company
{
    class employer: human
    {
        private string EmployerId;
        private string department;
        public employer(string worker_id, string first_name, string last_name, string address, 
            string EmployerId, string department)
            : base (worker_id, first_name, last_name, address)
        {
            this.EmployerId = EmployerId;
            this.department = department;
        }
        public employer(employer e) :base(e.GetWorkerId(), e.GetFirst_Name(), e.GetLastName(), e.GetAddress())
        {
            this.EmployerId = e.EmployerId;
            this.department = e.department;
        }
        public void SetEmployerrId(string EmployerId)
        {
            this.EmployerId = EmployerId;
        }
        public void SetDepartment(string department)
        {
            this.department = department;
        }
        public string GetEmployerId()
        {
            return this.EmployerId;
        }
        public string GetDepartment()
        {
            return this.department;
        }
        public override string ToString()
        {
            return base.ToString() + "Employer Id " + EmployerId + "Department " + department;
        }
        
    }
}
