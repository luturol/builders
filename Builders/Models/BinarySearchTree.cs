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
            Root = AddNode0(Root, value);
        }

        private Node AddNode0(Node node, int value)
        {
            if (node == null)
            {
                node = new Node { Value = value };
                return node;
            }
            else if (node.Value > value)
            {
                node.Left = AddNode0(node.Left, value);
            }
            else if (node.Value < value)
            {
                node.Right = AddNode0(node.Right, value);
            }

            return node;
        }
    }
}