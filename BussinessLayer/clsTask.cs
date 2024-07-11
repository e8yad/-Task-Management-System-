using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataAccessLayer;
using Newtonsoft.Json;
using System.IO;
using System.Threading;

namespace BusinessLayer
{
    public class clsTask
    {
        private enum enMode { AddNew, Update }
        public enum enStatus { Normal = 1, Overdue = 2, Completed = 3 }
        private enMode _mode { get; set; }
        private Dictionary<enMode, Func<Task<bool>>> _DoBaseOnMode {  get; } = new Dictionary<enMode, Func<Task<bool>>>();
        public long? TaskID { get; set; }
        public string TaskName { get; set; }
        public DateTime CreationDate { get; set; }
        // very important for check is there in Coming notification for latter 
        public DateTime? DueDate { get; set; }
        public DateTime LastTaskModificationDate { get; set; }
        public string Notes { get; set; }
        public DateTime LastNoteModificationDate { get; set; }
        public long? PersonID { set; get; }

        [JsonIgnore]
        private static List<clsTask> _Tasks = new List<clsTask>();

        //public static List<clsTask>
        public enStatus status { get; set; }

        private Task<List<string>> _Categories;
        public Task<List<string>> Categories
        {
            get
            {
                return _Categories;
            }
        }
        /// <summary>
        /// public constructor to initiate parameters and properties 
        /// </summary>
        public clsTask()
        {
            InitializeDefaults();
            _mode = enMode.AddNew;
            // allocate Dictionary in memory
            _DoBaseOnMode.Add(_mode,AddNewTaskAsync);
        }
        private clsTask(long taskID, string taskName,
            DateTime creationDate, DateTime? dueDate, DateTime lastTaskModificationDate, string notes, enStatus state,DateTime lastNoteModificationDate, long personID)
        {
            TaskID = taskID;
            TaskName = taskName;
            CreationDate = creationDate;
            DueDate = dueDate;
            LastTaskModificationDate = lastTaskModificationDate;
            Notes = notes;
            LastNoteModificationDate = lastNoteModificationDate;
            PersonID = personID;
            this.status = state;
            _Categories = clsCategory.GetGategoriesListByTaskIDAsync((long)TaskID);
            //   Person = clsPerson.Find((long)this.PersonID);
            _mode = enMode.Update;

            _DoBaseOnMode.Add(_mode, UpdateTaskAsync);
        }


        private void InitializeDefaults()
        {
            TaskID = null;
            TaskName = null;
            CreationDate = DateTime.Now;
            DueDate = DateTime.Now;
            LastTaskModificationDate = DateTime.Now;
            Notes = null;
            LastNoteModificationDate = DateTime.Now;
            PersonID = null;
            _Categories = null;
        }


        private static void SerializeTaskData(clsTask Task)
        {
            string json=JsonConvert.SerializeObject(Task);
            try
            {
                clsUtil.SaveSerializationOnFile(json);
            }
            catch (Exception e)
            {
                // it will return control for caller 
                throw new Exception("Can not serialize ",e);
            
            }

        }
        /// <summary>
        /// to serialize task and save it into APPData file 
        /// </summary>
        public void Serialize()
        {
            SerializeTask(this);
        }
        /// <summary>
        ///  to static serialize task and save it into APPData file 
        /// </summary>
        /// <param name="Task">TaskClass you want to serialize</param>
        /// <exception cref="Exception"></exception>
        public static void SerializeTask(clsTask Task)
        {
            try
            {
                SerializeTaskData(Task);
            }
            catch (Exception e )
            {
                throw new Exception("Can not serialize task",e);
            }
        }
        private async Task<bool> AddNewTaskAsync()
        {
            // if i call this function from ui thread 
            // it will execute this line and back to caller to handel other operation 
           long? TasksIDValue = await clsTasksData.AddTaskAsync(this.TaskName, (DateTime)this.DueDate,this.Notes,(long)this.PersonID);

            if (TasksIDValue != null)
            {
                this.TaskID = TasksIDValue;
                return true;

            }
            else
                return false;

        }

        // i will make dictionary latter 

        // update task status async to avoid thread block
        public async Task<bool> UpdateTaskStateAsync()
        {
            try
            {
                bool Result = await clsTasksData.UpdateStatusAsync((byte)this.status, (long)this.TaskID);
               return Result;

            }
            catch (Exception e)
            {

                throw new Exception("Can not Update status",e);
            }


        }

        public async Task<bool> UpdateTaskNoteAsync()
        {
            try
            {
                bool Result = await clsTasksData.UpdateTaskNoteAsync(this.Notes, (long)this.TaskID);
                return Result;

            }
            catch (Exception e)
            {

                throw new Exception("Can not Update Task", e);
            }


        }

        private async Task<bool> AddCategoryToTaskAsync(long CatID)
        {
            try
            {
                bool Result=await clsTasksData.AddCategoryToTask(CatID, (long)this.TaskID);
                if (!Result)
                    return false;
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {

                throw new Exception("Can not add category to task ",e);
            }
        }

        public async Task<bool> AddCategoryToTask(clsCategory category)
        {
            return await AddCategoryToTaskAsync((long)category.CatID);
        }
        /// <summary>
        /// To add category to task 
        /// </summary>
        /// <param name="CatID">categoryID in database</param>
        /// <returns></returns>
        public async Task<bool> AddCategoryToTask(long CatID)
        {
            return await AddCategoryToTaskAsync(CatID);
        }
        /// <summary>
        /// to check value form database is null or not  incase of null return null else return  object
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        private static object CheckISObjectNull(object ob)
        {
            // if DBNUll
            if (ob==null||ob==DBNull.Value)
                return null;
            return ob;


        }

        private static async Task GetTasksForPeronIntoList(long PersonID)
        {
            Thread.CurrentThread.Name = "Get  Tasks Thread";
            // to clear task 
            _Tasks.Clear();
            DataTable dt=await clsTasksData.TasksByPersonIDAsync(PersonID);
            foreach(DataRow row in dt.Rows)
            {
                var task = new clsTask(
                    (long)row["TaskID"],
                    (string)row["TaskName"],
                    (DateTime)row["CreationDate"],
                    (DateTime?)CheckISObjectNull(row["DueDate"]),
                    (DateTime)row["LastTaskModificationDate"],
                    (string)CheckISObjectNull(row["Notes"]),
                     (enStatus)((byte)row["State"]), 
                    (DateTime)row["LastNoteModificationDate"],
                      PersonID);
                _Tasks.Add(task);
            }
        }
        /// <summary>
        /// Get all tasks related to person form database 
        /// will be executed on a new ThreadPool thread
        /// </summary>
        /// <param name="PersonID"></param>
        /// <returns></returns>
        public static async Task<List<clsTask>> GetTasks(long PersonID)
        {
            Task T=  Task.Run(() => GetTasksForPeronIntoList(PersonID));
           await Task.WhenAll(T);
            return _Tasks;
        }
        public Task<bool> UpdateTaskAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// To save change in class in data base 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveAsync()
        {
            if(_DoBaseOnMode.ContainsKey(_mode))
            {
                bool Result=await _DoBaseOnMode[_mode].Invoke();
                if(Result)
                    return true;
                else
                    return false;
            }
            return false;
        }
        /// <summary>
        /// get tasks from APPDATA 
        /// </summary>
        /// <returns></returns>
        public static async Task<List<clsTask>> DeserializeTasks()
        {
            List<string> lines = new List<string>();
            lines = await clsUtil.ReadFormSavedFileAsync(clsUtil.SerializedObjectPath);
            if (lines == null)
                return null ;
            List < clsTask > t=new List<clsTask>();
            try
            {
                foreach (var line in lines)
                {
                    clsTask task = JsonConvert.DeserializeObject<clsTask>(line);
                    t.Add(task);

                }
            }
            catch (Exception)
            {

                throw;
            }
         //   File.Delete(clsUtil.SerializedObjectPath);
            
            return t ;
 
        }






    }
}
