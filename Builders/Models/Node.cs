namespace Builders.Models
{
    public class Node
    {
        public int Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node FindWithValue(int value)
        {
            return FindWithValue0(value);
        }

        private Node FindWithValue0(int value)
        {
            if(value == Value)
            {
                return this;
            }
            else if (value > Value)
            {
                return Right?.FindWithValue(value);
            }
            else
            {
                return Left?.FindWithValue(value);
            }
        }
    }
}