using System;

namespace EECS214Assignment1
{
    /// <summary>
    /// A queue internally implemented as an array
    /// </summary>
    /// 
    public class ArrayQueue : Queue
    {
        /// <summary>
        /// Add object to end of queue
        /// </summary>
        /// 
        private int max_size = 250;

        private object[] queue = new object[250];

        private int _front = 0;
        private int _back = 0;

        /// <param name="o">object to add</param>
        public override void Enqueue(object o)
        {
            if (IsFull)
            {
                throw new QueueFullException();
            }
            else
            {
                queue[_back] = o;
                _back++;
            }
        }

        /// <summary>
        /// Remove object from beginning of queue.
        /// </summary>
        /// <returns>Object at beginning of queue</returns>
        public override object Dequeue()
        {
           
            object toreturn = null;

            if (_front == _back)
            {
                throw new QueueEmptyException();
            }
            else
            {
                toreturn = queue[_front];
                _front++;
               
            }

            return toreturn;
        }

        /// <summary>
        /// The number of elements in the queue.
        /// </summary>
        public override int Count
        {
            get {
                // TODO: check that you implementation runs in O(1) time
                // TODO: Replace this line with your code.
                return _back - _front;
            }
        }

        /// <summary>
        /// True if the queue is full and enqueuing of new elements is forbidden.
        /// </summary>
        public override bool IsFull
        {
            get {
                // TODO: check that your implementation runs in O(1) time
                return _back == max_size;
            }
        }
    }
}
