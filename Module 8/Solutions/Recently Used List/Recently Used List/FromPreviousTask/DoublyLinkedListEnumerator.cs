using System.Collections;

namespace Recently_Used_List.FromPreviousTask;

public partial class DoublyLinkedList<T>
{
    private class DoublyLinkedListEnumerator : IEnumerator<T>
    {
        private readonly DoublyLinkedList<T> _list;
        private Node _currentNode;

        public T Current => _currentNode.Data;

        object IEnumerator.Current => Current;

        public DoublyLinkedListEnumerator(DoublyLinkedList<T> list)
        {
            _list = list;
            _currentNode = null;
        }

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            _currentNode = _currentNode is null ? _list.HeadNode : _currentNode.Next;
            return _currentNode is not null;
        }

        public void Reset()
        {
            _currentNode = null;
        }
    }
}