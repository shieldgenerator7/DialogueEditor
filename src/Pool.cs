using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor.src
{
    public delegate object CreateFunction();

    class Pool<T>
    {
        List<T> pool = new List<T>();

        public T checkoutItem(CreateFunction createFunction)
        {
            if (pool.Count == 0)
            {
                return (T)createFunction();
            }
            else
            {
                T item = pool[0];
                pool.Remove(item);
                return item;
            }
        }

        public void returnItem(T item)
        {
            if (!pool.Contains(item))
            {
                pool.Add(item);
            }
        }
    }
}
