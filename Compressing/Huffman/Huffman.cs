using System;
using System.Collections.Generic;
using System.Text;

namespace Compressing.Huffman
{
	public class Huffman
	{
		private readonly Dictionary<char, HuffmanNode<char>> _leafDictionary = new Dictionary<char, HuffmanNode<char>>();
		private readonly HuffmanNode<char> _root;

		public static string EncodeToString(string text)
		{
			Huffman huffman = new Huffman(text);
			List<int> encoding = huffman.Encode(text);
			HashSet<char> chars = new HashSet<char>(text);
			Dictionary<char, string> alphabet = new Dictionary<char, string>();

			foreach (char c in chars)
			{
				StringBuilder tempBitBuffer = new StringBuilder();
				encoding = huffman.Encode(c);
				foreach (int bit in encoding)
				{
					tempBitBuffer.Append(bit);
				}
				alphabet.Add(c, tempBitBuffer.ToString());
			}

			StringBuilder output = new StringBuilder();
			foreach (char c in text)
			{
				string tempValue;
				alphabet.TryGetValue(c, out tempValue);
				output.Append(tempValue);
			}

			return output.ToString();
		}
		public static Dictionary<char, string> GetAlphabet(string text)
		{
			Huffman huffman = new Huffman(text);
			List<int> encoding = huffman.Encode(text);
			HashSet<char> chars = new HashSet<char>(text);
			Dictionary<char, string> alphabet = new Dictionary<char, string>();

			foreach (char c in chars)
			{
				StringBuilder tempBitBuffer = new StringBuilder();
				encoding = huffman.Encode(c);
				foreach (int bit in encoding)
				{
					tempBitBuffer.Append(bit);
				}
				alphabet.Add(c, tempBitBuffer.ToString());
			}

			return alphabet;
		}

		public Huffman(IEnumerable<char> values)
		{
			var counts = new Dictionary<char, int>();
			var priorityQueue = new PriorityQueue<HuffmanNode<char>>();
			int valueCount = 0;

			foreach (char value in values)
			{
				if (!counts.ContainsKey(value))
				{
					counts[value] = 0;
				}
				counts[value]++;
				valueCount++;
			}

			foreach (char value in counts.Keys)
			{
				var node = new HuffmanNode<char>((double)counts[value] / valueCount, value);
				priorityQueue.Add(node);
				_leafDictionary[value] = node;
			}

			while (priorityQueue.Count > 1)
			{
				HuffmanNode<char> leftSon = priorityQueue.Pop();
				HuffmanNode<char> rightSon = priorityQueue.Pop();
				var parent = new HuffmanNode<char>(leftSon, rightSon);
				priorityQueue.Add(parent);
			}

			_root = priorityQueue.Pop();
			_root.IsZero = false;
		}
		public List<int> Encode(char value)
		{
			var returnValue = new List<int>();
			Encode(value, returnValue);
			return returnValue;
		}
		public void Encode(char value, List<int> encoding)
		{
			if (!_leafDictionary.ContainsKey(value))
			{
				throw new ArgumentException("Invalid value in Encode");
			}
			HuffmanNode<char> nodeCur = _leafDictionary[value];
			var reverseEncoding = new List<int>();
			while (!nodeCur.IsRoot)
			{
				reverseEncoding.Add(nodeCur.Bit);
				nodeCur = nodeCur.Parent;
			}

			reverseEncoding.Reverse();
			encoding.AddRange(reverseEncoding);
		}
		public List<int> Encode(IEnumerable<char> values)
		{
			var returnValue = new List<int>();

			foreach (char value in values)
			{
				Encode(value, returnValue);
			}
			return returnValue;
		}
		public char Decode(List<int> bitString, ref int position)
		{
			HuffmanNode<char> nodeCur = _root;
			while (!nodeCur.IsLeaf)
			{
				if (position > bitString.Count)
				{
					throw new ArgumentException("Invalid bitstring in Decode");
				}
				nodeCur = bitString[position++] == 0 ? nodeCur.LeftChild : nodeCur.RightChild;
			}
			return nodeCur.Value;
		}
		public List<char> Decode(List<int> bitString)
		{
			int position = 0;
			var returnValue = new List<char>();

			while (position != bitString.Count)
			{
				returnValue.Add(Decode(bitString, ref position));
			}
			return returnValue;
		}
	}
}
