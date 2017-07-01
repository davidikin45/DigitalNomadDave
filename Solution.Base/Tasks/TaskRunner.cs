using Ninject;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Base.Tasks
{
    public class TaskRunner
    {
        [Inject]
        public IEnumerable<IRunAtInit> RunAtInit { get; set; }
        [Inject]
        public IEnumerable<IRunAtStartup> RunAtStartup { get; set; }
        [Inject]
        public IEnumerable<IRunAtOwinStartup> RunAtOwinStartup { get; set; }
        [Inject]
        public IEnumerable<IRunOnEachRequest> RunOnEachRequest { get; set; }
        [Inject]
        public IEnumerable<IRunAfterEachRequest> RunAfterEachRequest { get; set; }
        [Inject]
        public IEnumerable<IRunOnError> RunOnError { get; set; }

        public void RunTasksAtInit()
        {
            foreach (IRunAtInit task in RunAtInit)
            {
                task.Execute();
            }
        }

        public void RunTasksAtStartup()
        {
            foreach (IRunAtStartup task in RunAtStartup)
            {
                task.Execute();
            }
        }

        public void RunTasksAtOwinStartup(IAppBuilder app, string nameOrConnectionString)
        {
            foreach (IRunAtOwinStartup task in RunAtOwinStartup)
            {
                task.Execute(app, nameOrConnectionString);
            }
        }

        public void RunTasksOnEachRequest()
        {
            foreach (IRunOnEachRequest task in RunOnEachRequest)
             {
                task.Execute();
            }
        }

        public void RunTasksAfterEachRequest()
        {      
            foreach (IRunAfterEachRequest task in RunAfterEachRequest)
            {
                task.Execute();
            }
        }

        public void RunTasksOnError()
        {
            foreach (IRunOnError task in RunOnError)
            {
                task.Execute();
            }
        }

    }
}
