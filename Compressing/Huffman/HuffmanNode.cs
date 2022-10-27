using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compressing.Huffman
{
	internal class HuffmanNode<T> : IComparable
	{
		internal HuffmanNode<T> LeftChild { get; set; }
		internal HuffmanNode<T> RightChild { get; set; }
		internal HuffmanNode<T> Parent { get; set; }
		internal T Value { get; set; }
		internal bool IsLeaf { get; set; }
		internal bool IsZero { get; set; }
		internal double Probability { get; set; }

		internal int Bit
		{
			get { return IsZero ? 0 : 1; }
		}
		internal bool IsRoot
		{
			get { return Parent == null; }
		}

		internal HuffmanNode(double probability, T value)
		{
			Probability = probability;
			LeftChild = RightChild = Parent = null;
			Value = value;
			IsLeaf = true;
		}
		internal HuffmanNode(HuffmanNode<T> leftSon, HuffmanNode<T> rightSon)
		{
			LeftChild = leftSon;
			RightChild = rightSon;
			Probability = leftSon.Probability + rightSon.Probability;
			leftSon.IsZero = true;
			rightSon.IsZero = false;
			leftSon.Parent = rightSon.Parent = this;
			IsLeaf = false;
		}
		public int CompareTo(object obj)
		{
			return -Probability.CompareTo(((HuffmanNode<T>)obj).Probability);
		}
	}
}
