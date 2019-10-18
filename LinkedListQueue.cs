using System;
using System.Collections.Generic;

namespace EECS214Assignment1
{
    /// <summary>
    /// A queue internally implemented as a linked list of objects
    /// </summary>
    ///
    ///

    public class LNode
    {
        public object o;
        public LNode next;

        public LNode(object given)
        {
            o = given;
            next = null;
        }
    }

    public class LinkedListQueue : Queue
    {
        /// <summary>
        /// Add object to end of queue
        /// </summary>
        ///private int max_size = 250;

        private LNode _front = null;
        private LNode _back = null;
        private int _count = 0;

        
        /// <param name="o">object to add</param>
        public override void Enqueue(object o)
        {

            if (IsFull)
            {
                throw new QueueFullException();
            }

            LNode nd  = new LNode(o);
            if (_count == 0)
            {
                _front = nd;
                _back = nd;
            }
            else
            {
                _back.next = nd;
                _back = nd;
            }
            _count++;
        }

        /// <summary>
        /// Remove object from beginning of queue.
        /// </summary>
        /// <returns>Object at beginning of queue</returns>
        public override object Dequeue()
        {
            if (_count == 0)
            {
                throw new QueueEmptyException();
            }

            object toreturn = _front.o;
            _front = _front.next;
            _count--;

            return toreturn;
        }

        /// <summary>
        /// The number of elements in the queue.
        /// 
        /// </summary>
        public override int Count
        {
            get { return _count; }
        }

        /// <summary>
        /// True if the queue is full and enqueuing of new elements is forbidden.
        /// Note: LinkedListQueues can be grown to arbitrary length, and so can
        /// never fill.
        /// </summary>
        public override bool IsFull
        {
            get
            { return false; }
        }
    }
}
