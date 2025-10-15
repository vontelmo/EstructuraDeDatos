namespace MyBST
{
    public class Node<T>
    {
        public T Value;
        public Node<T> left;
        public Node<T> right;

        public Node(T value)
        {
            this.Value = value;
            left = null;
            right = null;
        }
    }
}