using System;
using System.Collections;
using DepthCrawlerGame.Items;

namespace DepthCrawlerGame.Ressources
{
    public class ConsumableList : IEnumerable
    {
        private ItemNode head;
        private int count;

        public ConsumableList()
        {
            this.head = null;
            this.count = 0;
        }

        public bool Empty
        {
            get { return this.count == 0; }
        }

        public int Count
        {
            get { return this.count; }
        }

        public void Add(Consumable consumable, int quantity)
        {
            if (head == null)
            {
                head = new ItemNode(consumable, quantity);
            }
            else
            {
                ItemNode current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = new ItemNode(consumable, quantity);
            }

            count++;
        }

        public void Remove(string item)
        {
            if (head == null)
            {
                return;
            }

            if (head.consumable.Name.ToLower() == item.ToLower())
            {
                head = head.Next;
                count--;
                return;
            }

            ItemNode current = head;
            while (current.Next != null)
            {
                if (current.Next.consumable.Name.ToLower() == item.ToLower())
                {
                    current.Next = current.Next.Next;
                    count--;
                    return;
                }
                current = current.Next;
            }
        }

        public void Add(Consumable consumable)
        {
            Add(consumable, 1);
        }

        public IEnumerator GetEnumerator()
        {
            ItemNode current = head;
            while (current != null)
            {
                yield return (current.consumable, current.quantity);
                current = current.Next;
            }
        }
    }
}
