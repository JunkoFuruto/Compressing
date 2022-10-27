using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compressing.Huffman
{
	public class PriorityQueue<T> where T : IComparable
	{
		protected List<T> LastHeap = new List<T>();

		public virtual int Count
		{
			get { return LastHeap.Count; }
		}

		public virtual void Add(T val)
		{
			LastHeap.Add(val);
			SetAt(LastHeap.Count - 1, val);
			UpHeap(LastHeap.Count - 1);
		}
		public virtual T Peek()
		{
			if (LastHeap.Count == 0)
			{
				throw new IndexOutOfRangeException("Peeking at an empty priority queue");
			}

			return LastHeap[0];
		}
		public virtual T Pop()
		{
			if (LastHeap.Count == 0)
			{
				throw new IndexOutOfRangeException("Popping an empty priority queue");
			}

			T valRet = LastHeap[0];

			SetAt(0, LastHeap[LastHeap.Count - 1]);
			LastHeap.RemoveAt(LastHeap.Count - 1);
			DownHeap(0);
			return valRet;
		}
		protected virtual void SetAt(int i, T val)
		{
			LastHeap[i] = val;
		}
		protected bool RightSonExists(int i)
		{
			return RightChildIndex(i) < LastHeap.Count;
		}
		protected bool LeftSonExists(int i)
		{
			return LeftChildIndex(i) < LastHeap.Count;
		}
		protected int ParentIndex(int i)
		{
			return (i - 1) / 2;
		}
		protected int LeftChildIndex(int i)
		{
			return 2 * i + 1;
		}
		protected int RightChildIndex(int i)
		{
			return 2 * (i + 1);
		}
		protected T ArrayVal(int i)
		{
			return LastHeap[i];
		}
		protected T Parent(int i)
		{
			return LastHeap[ParentIndex(i)];
		}
		protected T Left(int i)
		{
			return LastHeap[LeftChildIndex(i)];
		}
		protected T Right(int i)
		{
			return LastHeap[RightChildIndex(i)];
		}
		protected void Swap(int i, int j)
		{
			T valHold = ArrayVal(i);
			SetAt(i, LastHeap[j]);
			SetAt(j, valHold);
		}
		protected void UpHeap(int i)
		{
			while (i > 0 && ArrayVal(i).CompareTo(Parent(i)) > 0)
			{
				Swap(i, ParentIndex(i));
				i = ParentIndex(i);
			}
		}
		protected void DownHeap(int i)
		{
			while (i >= 0)
			{
				int iContinue = -1;

				if (RightSonExists(i) && Right(i).CompareTo(ArrayVal(i)) > 0)
				{
					iContinue = Left(i).CompareTo(Right(i)) < 0 ? RightChildIndex(i) : LeftChildIndex(i);
				}
				else if (LeftSonExists(i) && Left(i).CompareTo(ArrayVal(i)) > 0)
				{
					iContinue = LeftChildIndex(i);
				}

				if (iContinue >= 0 && iContinue < LastHeap.Count)
				{
					Swap(i, iContinue);
				}

				i = iContinue;
			}
		}
	}
}
