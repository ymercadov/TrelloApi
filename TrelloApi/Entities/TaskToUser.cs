using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloApi.Entities
{
    public class TaskToUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string IdTask { get; set; }
        public string IdUser { get; set; }
        public string IdStatu { get; set; }

        public TaskToUser() { }
        public TaskToUser(string idtask, string iduser, string idstatu)
        {
            this.IdTask = idtask;
            this.IdUser = iduser;
            this.IdStatu = idstatu;
        }
    }
}
