namespace Tasks;

public partial class DoublyLinkedList<T>
{
    public class Node
    {
        public T Data;
        public Node Next;
        public Node Previous;

        public Node(T data)
        {
            Data = data;
            Previous = null;
        }

        public Node(T data, Node previousNode)
        {
            Data = data; 
            Previous = previousNode;
        }

        public void AddToEnd(Node previousNode, T data)
        {
            if (Next is null)
            {
                Next = new Node(data, previousNode);
            }
            else
            {
                Next.AddToEnd(this, data);
            }
        }
    }

}
