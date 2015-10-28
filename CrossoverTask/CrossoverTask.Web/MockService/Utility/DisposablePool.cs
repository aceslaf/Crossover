using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossoverTask.Service.Utility
{
    /// <summary>
    /// Used to handle multiple disposable resources within the same using scope
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDisposableResourceTracker<in T> : IDisposable where T : IDisposable
    {
        /// <summary>
        /// Starts tracking the managed resource and disposes of it when the using scope ends
        /// </summary>
        /// <param name="newObj"></param>
       void Track(T newObj);
    }

    /// <summary>
    /// A class responsible for managing multiple disposable resources
    /// that are used in the same scope. This class should be used instead of
    /// nested using statements.
    /// using(var r1=new Resource())
    /// using(var r2=new Resource())
    /// {
    ///     //do stuff
    /// }
    /// becomes
    /// using(var pool = DisposableResourceTracker<Resource>)
    /// {
    ///     r1=new Resource();
    ///     pool.Manage(r1);
    ///     r2=new Resource();
    ///     pool.Manage(r2);
    ///    //do stuff
    /// }
    /// </summary>
    /// <typeparam name="T">The most primitive type of IDisposable this pool should manage</typeparam>
    public class DisposableResourceTracker<T> : IDisposableResourceTracker<T> where T :IDisposable
    {
        private bool isDisposed;
        private HashSet<T> _pool;
        public DisposableResourceTracker()
        {
            _pool = new HashSet<T>();
        }

        /// <summary>
        /// Adds an externaly created resource to the pool of tracked resources.
        /// </summary>
        /// <param name="newObj"></param>
        public void Track(T newObj)
        {
            _pool.Add(newObj);
        }       

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposed)
            {
                return;
            }

            var exceptonions = new List<Exception>();
            foreach (var disposable in _pool)
            {
                if (disposable == null)
                {
                    continue;
                }

                try
                {
                    disposable.Dispose();
                }
                catch (Exception e)
                {
                    exceptonions.Add(e);
                    continue;
                }
            }

            isDisposed = true;

            if (exceptonions.Count > 0)
            {
                //Log exceptions allthough there should not be any.
                //Normaly a throw new AggregateException(exceptions); statement should be here, but 
                //throwing an exception from dispose method is considered a bad practice
            }
        }

    }
}
