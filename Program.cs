using Lab_3;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Дикун Дiана, група 623п, Варiант 6:\n" +
            "Список - Queue, Пiдсписок - List<T>\n");

        Sp MySp = new Sp();
        int count_el_sp = 0;
        Random rnd = new Random();

        bool restart = true;
        while (restart)
        {
            Console.WriteLine(" 1 - Додати елемент у список\n 2 - Видалити елемент зi списку або з пiдсписку\n" +
                " 3 - Переглянути перший елемент списку та пiдсписку\n 4 - Перевiрка списку або підсписку, порожнiй вiн або нi" +
                "\n 5 - Вивести структуру на екран\n 0 - Вийти з програми");
            Console.Write("\nВиберiть пункт меню (0 - 5) ---> ");
            int menu = int.Parse(Console.ReadLine());

            switch (menu)
            {
                // Вихід з програми
                case 0:
                    restart = false;
                    break;

                // Додавання елементів у список
                case 1:
                    Console.Write("Введiть кiлькiсть елементiв для додавання у пiдсписок ---> ");
                    int push_elem = int.Parse(Console.ReadLine());
                    El_Queue_Git myElQueue = new El_Queue_Git(++count_el_sp);

                    for (int i = 0; i < push_elem; i++)
                    {
                        int random = rnd.Next(20);
                        Console.WriteLine("Додано елемент у пiдсписок - " + random.ToString());
                        El_List myElLst = new El_List(random, count_el_sp.ToString() + random);
                        myElQueue.Add_El_Sp(myElLst);
                    }
                    
                    MySp.EnQueue(myElQueue);

                    Console.WriteLine("Список пiсля додавання:\n" + MySp.ToString());
                    break;

                // Видалення елементів зі списку та з підсписку
                case 2:
                    Console.Write("Введiть кiлькiсть елементiв для видалення зi списку ---> ");
                    int pop_elem = int.Parse(Console.ReadLine());

                    for (int i = 0; i < pop_elem; i++)
                    {
                        MySp.DeQueue();
                    }
                    Console.WriteLine("Список пiсля видалення:\n" + MySp.ToString());
                    
                    Console.WriteLine("Введiть iндекс елементу який ви хочете видалити з пiдсписку -->");
                    try
                    {
                        int del_el_down = int.Parse(Console.ReadLine());
                        MySp.Peek().RemoveAt_El_Sp(del_el_down);
                        Console.WriteLine("Список пiсля видалення:\n" + MySp.ToString());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Помилка!");
                    }

                    break;

                   // Вивід першого елементу списку та підсписку на екран
                case 3:
                    if (MySp.IsEmpty())
                    {
                        Console.WriteLine("Список порожнiй!");
                        break;
                    }
                    Console.Write("\tПерший елемент списку: ");
                    Console.WriteLine($"{MySp.Peek()}");

                    if (MySp.Peek().IsEmpty())
                    {
                        Console.WriteLine("Пiдсписок порожнpiй!");
                        break;
                    }
                    Console.Write("\tПерший елемент пiдсписку: ");
                    Console.WriteLine($"{MySp.Peek().First()}");
                    break;

                // Перевірка списку та підсписку на порожність
                case 4:
                    if (MySp.IsEmpty())
                    {
                        Console.WriteLine("Список порожнiй!");
                        break;
                    }
                    Console.WriteLine("В списку є елементи");

                    if (MySp.Peek().IsEmpty())
                    {
                        Console.WriteLine("Пiдсписок порожнiй!");
                        break;
                    }
                    Console.WriteLine("В пiдсписку є елементи");
                    break;

                // Вивід структури на екран
                case 5:
                    Console.WriteLine("\n\t ---Список---");
                    Console.WriteLine(MySp.ToString());
                    break;

                default:
                    Console.WriteLine("Пункт меню вiдсутнiй!");
                    break;
            }
        }
    }
}
