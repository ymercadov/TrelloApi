using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloApi.Entities
{
    public class TaskToUserDetail
    {
        public string Id { get; set; }
        public string TaskName { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
    }
}
