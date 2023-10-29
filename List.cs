using System.Collections.Generic;
using System.Collections;

namespace Lab_3
{
    public class El_List // Клас який описує елемент підсписку
    {
        public int key; // поле ключа
        public string inf; // інформаційне поле

        // Конструктор елементу підсписка
        public El_List(int key, string inf)
        { 
            this.key = key;
            this.inf = inf;
        }

        public override string ToString()
        {
            string res = "";
            res = key.ToString() + "\t";
            return res;
        }
    }

    public class El_Queue // Клас, який описує елемент списку
    {
        public int L_inf; // Інформаційне поле
        public List<El_List> lst; // Підсписок

        // Конструктор елементу списка
        public El_Queue(int inf)
        {
            L_inf = inf;
            lst = new List<El_List>();
        }

        // Метод додавання елементу у підсписок
        public void Add_El_Sp(El_List el)
        {
            lst.Add(el);
        }

        // Метод видалення елементу з підсписку за індексом
        public void RemoveAt_El_Sp(int i)
        {
            lst.RemoveAt(i);
        }

        // Метод для отримання першого елементу підсписка
        public El_List First()
        {
            return lst.FirstOrDefault();
        }

        // Метод перевірки, порожній підсписок чи ні
        public bool IsEmpty()
        {
            return lst.Count == 0;
        }

        // Метод виведення інформації з підсписка
        public override string ToString()
        {
            string res = "";

            res = L_inf.ToString() + " пiдсписок\t---> ";
            if (lst.Count != 0)
                foreach (El_List el in lst)
                    res += el.ToString();

            return res + '\n';
        }
    }

    public class Sp // Клас, який описує структуру список підсписків
    {
        public Queue MyQueue;

        // Конструктор структури
        public Sp()
        {
            MyQueue = new Queue();
        }

        // Метод додавання підсписку у список
        public void EnQueue(El_Queue el_Sp)
        {
            MyQueue.Enqueue(el_Sp);
        }

        // Метод видалення підсписку зі списку
        public void DeQueue()
        {
            MyQueue.Dequeue();
        }

        // Метод перегляду першого елементу списка
        public El_Queue? Peek()
        {
            return (El_Queue?)MyQueue.Peek();
        }

        // Метод перевірки, порожній список чи ні
        public bool IsEmpty()
        {
            return MyQueue.Count == 0;
        }

        // Метод виведення інформації зі списку
        public override string ToString()
        {
            string res = "";

            if (MyQueue.Count != 0)
                foreach (var el in MyQueue)
                    res += el.ToString();

            return res;
        }
    }
}
