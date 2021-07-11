using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Builders.Models
{
    public class BinarySearchTree
    {        
        [JsonIgnore]
        [BsonId]
        public ObjectId Id {get;set;}
        public Node root { get; private set; }

        public void AddNode(int value)
        {
            root = AddNode0(root, value);
        }

        private Node AddNode0(Node node, int value)
        {
            if (node == null)
            {
                node = new Node { Value = value };
                return node;
            }
            else if (root.Value > value)
            {
                node.Left = AddNode0(node.Left, value);
            }
            else if (root.Value < value)
            {
                node.Right = AddNode0(node.Right, value);
            }

            return node;
        }
    }
}