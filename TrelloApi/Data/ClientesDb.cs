using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using TrelloApi.Data.Configuration;
using TrelloApi.Entities;

namespace TrelloApi.Data
{
    public class ClientesDb
    {

        private readonly IMongoCollection<Task> taskCollecion;
        private readonly IMongoCollection<User> userCollecion;
        private readonly IMongoCollection<TaskToUser> TaskToUserCollecion; 
        private readonly IMongoCollection<Statu> StatuCollecion;

        public ClientesDb(IClientesStoreDatabaseSettings settings)
        {
            var mdbClient = new MongoClient(settings.ConnectionString);
            var trellodb = mdbClient.GetDatabase(settings.DatabaseName);

            taskCollecion = trellodb.GetCollection<Task>("Task");
            userCollecion = trellodb.GetCollection<User>("User");
            TaskToUserCollecion = trellodb.GetCollection<TaskToUser>("TaskToUser");
            StatuCollecion = trellodb.GetCollection<Statu>("Statu");

        }

        #region -Task-
        public List<Task> Get()
        {
            return taskCollecion.Find(cli => true).ToList();
        }

        public Task GetById(string id)
        {
            return taskCollecion.Find<Task>(task => task.Id == id).FirstOrDefault();
        }
        
        public Task CreateTask(Task task)
        {
            Task _task = new Task(task.TaskName);
            taskCollecion.InsertOne(task);
            return task;
        }

        public void Update(string id, Task task)
        {
            taskCollecion.ReplaceOne(upd => upd.Id == id, task);
        }
        #endregion

        #region -User-
        public List<User> GetAllUser()
        {
            return userCollecion.Find(cli => true).ToList();
        }

        public User GetByIdUser(string id)
        {
            return userCollecion.Find<User>(user => user.Id == id).FirstOrDefault();
        }
        public User CreateUser(User user)
        {
            userCollecion.InsertOne(user);
            return user;
        }
        #endregion

        #region -Task to User-

        public List<TaskToUser> GetAllTasktoUser()
        {
            return TaskToUserCollecion.Find(cli => true).ToList();
        }
        public TaskToUser GetByIdTaskToUser(string id)
        {
            return TaskToUserCollecion.Find<TaskToUser>(ttu => ttu.Id == id).FirstOrDefault();
        }
        public TaskToUser CreateTasktoUser(TaskToUser taskToUser)
        {
            Statu statu = new Statu();
            //Create with statu = Open by default
            statu = GetByName();
            taskToUser.IdStatu = statu.Id;
            TaskToUserCollecion.InsertOne(taskToUser);
            return taskToUser;
        }
               
        public void DeleteById(string id)
        {
            TaskToUserCollecion.DeleteOne(ttu => ttu.Id == id);
        }

        public void Update(string id, TaskToUser taskToUser)
        {
            TaskToUserCollecion.ReplaceOne(upd => upd.Id == id, taskToUser);
        }


        public List<TaskToUserDetail> ConsultDetail()
        {

            var tasks =  taskCollecion.Find(cli => true).ToList();
            var users = userCollecion.Find(cli => true).ToList();
            var status = StatuCollecion.Find(cli => true).ToList();
            var tasktouser = TaskToUserCollecion.Find(cli => true).ToList();

            var result = from tu in tasktouser
                         join t in tasks on tu.IdTask.ToString() equals t.Id into jointut
                         join u in users on tu.IdUser.ToString() equals u.Id into jointaus
                         join s in status on tu.IdStatu equals s.Id into joinsta
                         from tut in jointut.DefaultIfEmpty()
                         from taus in jointaus.DefaultIfEmpty()
                         from sta in joinsta.DefaultIfEmpty()
                         select new { tu.Id, tut.TaskName, taus.UserName, sta.Name };


            List<TaskToUserDetail> lstatasktouserdetail = new List<TaskToUserDetail>();
            TaskToUserDetail atasktouserdetail = new TaskToUserDetail();

           foreach (var item in result)
           {
                atasktouserdetail = new TaskToUserDetail();
                atasktouserdetail.Id = item.Id;
                atasktouserdetail.TaskName = item.TaskName;
                atasktouserdetail.UserName = item.UserName;
                atasktouserdetail.Name = item.Name;

                lstatasktouserdetail.Add(atasktouserdetail);

               //Console.WriteLine(item.ToJson());
           }

            return lstatasktouserdetail.ToList();
            
        }



        #endregion

        #region -Statu-

        public Statu GetByName()
        {
            return StatuCollecion.Find<Statu>(statu => statu.Name == "Open").FirstOrDefault();
        }

        public List<Statu> GetAllStatu()
        {
            return StatuCollecion.Find(cli => true).ToList();
        }

        public Statu CreateStatu(Statu statu)
        {            
            StatuCollecion.InsertOne(statu);
            return statu;
        }
        #endregion
    }
}
