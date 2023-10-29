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
    }
}
