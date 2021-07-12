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

        public void AddNode(int value)
        {
            Root = AddNodeRecursive(Root, value);
        }

        public void AddNode(List<int> values)
        {
            foreach (int node in values)
            {
                AddNode(node);
            }
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

        public SimplifiedBinarySearchTree GetSimplifiedBst()
        {
            List<int> nodes = new List<int>();
            GetSimplifiedBstRecursive(nodes, Root);

            return new SimplifiedBinarySearchTree { Nodes = nodes };
        }

        private void GetSimplifiedBstRecursive(List<int> nodes, Node node)
        {
            if (node is null) return;

            nodes.Add(node.Value);

            GetSimplifiedBstRecursive(nodes, node.Left);
            GetSimplifiedBstRecursive(nodes, node.Right);
        }
    }
}