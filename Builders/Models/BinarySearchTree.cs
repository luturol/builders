using System.Collections.Generic;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Builders.Models
{
    public class BinarySearchTree
    {
        public Node Root { get; private set; }

        public BinarySearchTree(List<int> nodes)
        {
            AddNodes(nodes);
        }

        private void AddNode(int value)
        {
            Root = AddNodeRecursive(Root, value);
        }

        private Node AddNodeRecursive(Node node, int value)
        {
            if (node == null)
            {
                node = new Node { Value = value };
                return node;
            }
            else if (node.Value > value)
            {
                node.Left = AddNodeRecursive(node.Left, value);
            }
            else if (node.Value < value)
            {
                node.Right = AddNodeRecursive(node.Right, value);
            }

            return node;
        }

        public void AddNodes(List<int> values)
        {
            foreach (int node in values)
            {
                AddNode(node);
            }
        }

        public Node FindWithValue(int value)
        {
            return FindWithValueRecursive(Root, value);
        }

        private Node FindWithValueRecursive(Node node, int value)
        {
            if (node is null)
            {
                return null;
            }
            else if (value == node.Value)
            {
                return node;
            }
            else if (value > node.Value)
            {
                return FindWithValueRecursive(node.Right, value);
            }
            else
            {
                return FindWithValueRecursive(node.Left, value);
            }
        }

        private int GetMaxRightRecursive(Node node)
        {
            if (node.Right is null)
                return node.Value;
            else
                return GetMaxRightRecursive(node.Right);
        }

        private int GetMinLeftRecursive(Node node)
        {
            if (node.Left is null)
                return node.Value;
            else
                return GetMinLeftRecursive(node.Left);
        }

        public List<int> GetSimplifiedBinarySearchTree()
        {
            List<int> nodes = new List<int>();
            GetSimplifiedBinarySearchTreeRecursive(nodes, Root);

            return nodes;
        }

        private void GetSimplifiedBinarySearchTreeRecursive(List<int> nodes, Node node)
        {
            if (node is null) return;

            nodes.Add(node.Value);

            GetSimplifiedBinarySearchTreeRecursive(nodes, node.Left);
            GetSimplifiedBinarySearchTreeRecursive(nodes, node.Right);
        }

        public bool IsBst()
        {
            return IsBstRecursive(Root);
        }

        private bool IsBstRecursive(Node node)
        {
            if (node == null)
            {
                return true;
            }

            var minValue = GetMinLeftRecursive(node);
            var maxValue = GetMaxRightRecursive(node);

            if (node.Value < minValue || node.Value > maxValue)
                return false;

            return IsBstRecursive(node.Left) && IsBstRecursive(node.Right);
        }
    }
}