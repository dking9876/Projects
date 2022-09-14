using System;
using System.Collections.Generic;
using System.Text;

namespace RestFuncApp
{
    public class Mission
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("n");
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public string MissionDescription { get; set; }
        public bool IsCompleted { get; set; }
    }


    public class MissionCreateModel
    {
        public string MissionDescription { get; set; }
    }



    public class MissionUpdateModel
    {
        public string MissionDescription { get; set; }
        public bool IsCompleted { get; set; }
    }
}
