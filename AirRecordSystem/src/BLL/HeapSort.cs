using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirRecordSystem.src.BLL
{
    public class HeapSort
    {
        private List<int> element;
        private int maxSize;
        private int curSize;

        public HeapSort()
        {
            this.element = new List<int>();
            this.maxSize = 0;
            this.curSize = -1;
        }

        public HeapSort(List<int> arr)
        {

            this.maxSize = arr.Count;
            this.curSize = arr.Count - 1;

            element = arr;

            int pos = (curSize - 1) / 2;

            while (pos >= 0)
                filterDown(pos--);

        }

        public int Top()
        {
            return element[0];
        }

        public bool IsEmpty()
        {
            return curSize == -1;
        }

        public bool IsFull()
        {
            return curSize + 1 >= maxSize;
        }

        public bool Pop()
        {
            if (IsEmpty())
                return false;

            element[0] = element[curSize--];
            filterDown(0);

            return true;
        }

        public bool Push(int data)
        {
            if (IsFull())
                return false;

            element[++curSize] = data;
            FilterUp(curSize);

            return true;
        }

        public void FilterUp(int pos)
        {

            int dad = 0;

            while (pos >= 0)
            {
                dad = (pos - 1) / 2;
                if (element[pos] > element[dad])
                {
                    Swap(pos, dad);
                    pos = dad;
                }
                else break;
            }
        }

        public void filterDown(int pos)
        {

            int adjust = 0;
            int child = 0;

            while ((child = pos * 2 + 1) <= curSize)
            {
                if (!(pos * 2 + 2 > curSize))
                {
                    int left = child;
                    int right = child + 1;
                    child = element[left] > element[right] ? left : right;
                }

                if (element[pos] < element[child])
                {
                    adjust = child;
                    Swap(pos, adjust);
                    pos = adjust;
                }
                else break;
            }

        }

        public void Swap(int pos1, int pos2)
        {
            int data = element[pos1];
            element[pos1] = element[pos2];
            element[pos2] = data;
        }

        public List<int> Sort()
        {
            List<int> sorted = new List<int>();

            while(!IsEmpty())
            {
                sorted.Add(Top());
                Pop();
            }

            return sorted;
        }

    }
}
