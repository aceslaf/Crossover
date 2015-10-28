using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossoverTask.Service.Utility
{
    class CustomBackgroundActionExecutor<T> where T:class
    {        
        private class ActionWithArgument<T2> where T2 : class
        {
            public Action<T2> Action { get; set; }
            public T2 Argument { get; set; }
            public void Execute()
            {
                if (Action != null)
                {
                    Action(Argument);
                }
            }
        }        

        /// <summary>
        /// Calls an action in the background passing the argument of type T as parameter
        /// </summary>
        /// <param name="action">Must not be null.</param>
        /// <param name="argument">Argument to pass to the bacground action</param>
        public void Execute(Action<T> action,T argument)
        {
             if (action == null)
            {
                throw new ArgumentNullException("The custom background action must not be null");
            }

            var actionAndArgument = new ActionWithArgument<T>
            {
                Action = action,
                Argument = argument
            };

            var backgroundWorker = new BackgroundWorker();           
            backgroundWorker.DoWork += DoWork;
            backgroundWorker.RunWorkerCompleted += RunWorkerCompleted;
            backgroundWorker.RunWorkerAsync(actionAndArgument);
        }

        private static void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var backgroundWorker = sender as BackgroundWorker;
            if (backgroundWorker == null)
            {
                return;
            }
            backgroundWorker.DoWork -= DoWork;
            backgroundWorker.RunWorkerCompleted -= RunWorkerCompleted;                
        }

        private static void DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ActionWithArgument<T> actionAndArgument = e.Argument as ActionWithArgument<T>;
                actionAndArgument.Execute();
                e.Result = true;
            }
            catch (Exception ex)
            {               
                //Idealy the exception should be logged or left for the exception handling layer
                //This is failing a bit silently 
                Debug.Write(ex);
                e.Result = false;
            }            
        }
       
    }
}
