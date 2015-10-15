using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseTwo
{
    class SuperList<T> : IList<T>
    {
        private Node<T> head;

      //  public delegate void func(T item);
        public void ActAllItems(Func<T, T> func )
        {

            Node<T> curr = head;
            while (curr != null)
            {
                func(curr.Value);
                curr = curr.Next;
            }
        }

        public void ActOnIndex(Func<T,T> func, int index)
        {
            if (index < 0)
            {
                throw new IndexOutOfRangeException("Index if out of range of the linked list");
            }

            Node<T> curr = head;
            int i = 0;
            while (curr != null)
            {
                if (i == index)
                {
                    func(curr.Value);
                    return;
                }
                curr = curr.Next;
                i++;
            }
            if (index > i)
            {
                throw new IndexOutOfRangeException("Index if out of range of the linked list");
            }
        }

        public void ActIfCriteria(Func<T,T> func,Func<T,bool> criteria)
        {
            Node<T> curr = head;
            while (curr != null)
            {
                if (criteria(curr.Value))
                {
                    func(curr.Value);
                }
                curr = curr.Next;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> curr = head;
            while (curr != null)
            {
                yield return curr.Value;
                curr = curr.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            Node<T> newNode = new Node<T>(item);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                newNode.Next = head;
                head.Prev = newNode;
                head = newNode;

            }
        }

        public void Clear()
        {
            head = null;
        }

        public bool Contains(T item)
        {
            Node<T> curr = head;
            while (curr != null)
            {
                if (curr.Value.Equals(item))
                {
                    return true;
                }
                curr = curr.Next;
            }
            return false;
        }
    

        public void CopyTo(T[] array, int arrayIndex)
        {
            Node<T> curr = head;
            while (curr != null)
            {
                array[arrayIndex] = curr.Value;
                curr = curr.Next;
                arrayIndex++;
            }
        }

        public bool Remove(T item)
        {
            if (item == null)
            {
                return false;
            }
            if (head.Value.Equals(item))
            {
                head = head.Next;
                head.Prev = null;
            }
            Node<T> curr = head;
            while (curr != null)
            {
                if (curr.Value.Equals(item))
                {
                    curr.Prev.Next = curr.Next;
                    curr.Next.Prev = curr.Prev;
                    return true;
                }
                curr = curr.Next;
            }
            return false;
        }

        public int Count { get; }
        public bool IsReadOnly { get; }
        public int IndexOf(T item)
        {
            if (item == null)
            {
                return -1;
            }

            int index = 0;
            Node<T> curr = head;
            while (curr != null)
            {
                if (curr.Value.Equals(item))
                {
                    return index;
                }
                curr = curr.Next;
                index++;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index <0)
            {
                throw new IndexOutOfRangeException("Index if out of range of the linked list");
            }

            Node<T> curr = head;
            int i = 0;
            if (index == i)
            {
                Node<T> newNode = new Node<T>(item);
                newNode.Next = curr;
                head = newNode;
                return;
            }
            curr = curr.Next;
            i++;

            while (curr != null)
            {
                if (i == index)
                {
                    Node<T> newNode = new Node<T>(item);
                    newNode.Next = curr;
                    newNode.Prev = curr.Prev;
                    curr.Prev.Next = newNode;
                    curr.Prev = newNode;
                    return;
                }
                curr = curr.Next;
                i++;
            }
            if (index >i)
            {
                throw new IndexOutOfRangeException("Index if out of range of the linked list");
            }
        }

        public void RemoveAt(int index)
        {
            if (index < 0)
            {
                throw new IndexOutOfRangeException("Index if out of range of the linked list");
            }

            Node<T> curr = head;
            int i = 0;
            if (index == i)
            {
                if (curr.Next == null)
                {
                    head = null;
                    return;
                }
                head = curr.Next;
                head.Prev = null;
                return;
            }
            curr = curr.Next;
            i++;

            while (curr != null)
            {
                if (i == index)
                {
                    curr.Prev.Next = curr.Next;
                    curr.Next.Prev = curr.Prev;
                    return;
                }
                curr = curr.Next;
                i++;
            }
            if (index > i)
            {
                throw new IndexOutOfRangeException("Index if out of range of the linked list");
            }
        }

        public T this[int index]
        {
            get
            {

                if (index < 0)
                {
                    throw new IndexOutOfRangeException("Index if out of range of the linked list");
                }

                Node<T> curr = head;
                int i = 0;
                while (curr != null)
                {
                    if (i == index)
                    {
                        return curr.Value;
                    }
                    curr = curr.Next;
                    i++;
                }
                if (index > i)
                {
                    throw new IndexOutOfRangeException("Index if out of range of the linked list");
                }
                return default(T);
            }
            set
            {
                if (index < 0)
                {
                    throw new IndexOutOfRangeException("Index if out of range of the linked list");
                }

                Node<T> curr = head;
                int i = 0;
                while (curr != null)
                {
                    if (i == index)
                    {
                        curr.Value = value;
                    }
                    curr = curr.Next;
                    i++;
                }
                if (index > i)
                {
                    throw new IndexOutOfRangeException("Index if out of range of the linked list");
                }
            }
        }
    }

    
}
