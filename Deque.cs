using System;

namespace EECS214Assignment1
{
    /// <summary>
    /// A double-ended queue
    /// Implement this as a doubly-linked list
    /// </summary>
    ///

    ///<summary>
    /// Class for a node in the doubly linked-list
    /// </summary>
    public class DLnode
    {
        public object o = null;
        public DLnode _prev = null;
        public DLnode _next = null;

        public DLnode(object o, DLnode prev, DLnode next)
        {
            this.o = o;
            _prev = prev;
            _next = next;
        }
    }

    public class Deque
    {
        /// <summary>
        /// Add object to front of queue
        /// </summary>
        ///

        private DLnode _front = null;
        private DLnode _back = null;
        private int _count = 0;

        /// 
        /// <param name="o">object to add</param>
        public void AddFront(object o)
        {
            if (IsEmpty)
            {
                DLnode dln = new DLnode(o, null, null);
                _front = dln;
                _back = dln;
            }
            else
            {
                DLnode dln = new DLnode(o, null, _front);
                _front._prev = dln;
                _front = dln;
            }

            _count++;
        }

        /// <summary>
        /// Remove object from front of queue.
        /// </summary>
        /// <returns>Object at beginning of queue</returns>
        public object RemoveFront()
        {
            if (IsEmpty)
            {
                throw new QueueEmptyException();
            }

            object toreturn = _front.o;
            _front = _front._next;
            _count--;

            return toreturn;
        }

        /// <summary>
        /// Add object to end of queue
        /// </summary>
        /// <param name="o">object to add</param>
        public void AddEnd(object o)
        {
            if (IsEmpty)
            {
                DLnode dln = new DLnode(o, null, null);
                _front = dln;
                _back = dln;
            }
            else
            {
                DLnode dln = new DLnode(o, _back, null);
                _back._next = dln;
                _back = dln;
            }

            _count++;
        }

        /// <summary>
        /// Remove object from end of queue.
        /// </summary>
        /// <returns>Object at end of queue</returns>
        public object RemoveEnd()
        {
            if (IsEmpty)
            {
                throw new QueueEmptyException();
            }

            object toreturn = _back.o;
            _back = _back._prev;
            _count--;

            return toreturn;
        }

        /// <summary>
        /// The number of elements in the queue.
        /// </summary>
        public int Count
        {
            get { return _count; }
        }

        /// <summary>
        /// True if the queue is empty and dequeuing is forbidden.
        /// </summary>
        public bool IsEmpty => Count == 0;
    }
}
