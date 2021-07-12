using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Builders.Models
{
    public class BinarySearchTree
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Node Root { get; private set; }
        public void AddNode(int value)
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
    }
}