namespace TaskManager.Model
{
    public interface ITasks
    {
        public List<TaskBody> GetTasks();
        public TaskBody GetTaskById(int taskId);
        public TaskBody GetTaskByName(string name);
        public void AddTask(TaskBody task);

        public void UpdateTask(int id ,TaskBody task);
        public void RemoveTask(int taskId);
    }

    public class Tasks: ITasks
    {
        public List<TaskBody> taskbody = new List<TaskBody>
        {
            new TaskBody {TaskId=1,TaskName="Learn",TaskDescription="Learn about Dependancy injection"},
            new TaskBody {TaskId=2,TaskName="Do",TaskDescription="Create a crud operation using DI"}
        };

        public List<TaskBody> GetTasks()
        {
            return taskbody;
        }

        public TaskBody GetTaskById(int taskId)
        {
            var idcheck = taskbody.FirstOrDefault(x => x.TaskId == taskId);
            return idcheck;
        }
        
        public void AddTask(TaskBody task)
        {
            task.TaskId = taskbody.Count + 1;
            taskbody.Add(task);
        }

        public void UpdateTask(int id, TaskBody task)
        {
            var updtsk = taskbody.FirstOrDefault(d=>d.TaskId==id);
            updtsk.TaskName = task.TaskName;
            updtsk.TaskDescription = task.TaskDescription;
        }

        public void RemoveTask(int taskId)
        {
            var dlt = taskbody.FirstOrDefault(s=>s.TaskId==taskId);
            taskbody.Remove(dlt);
        }
    }

}
