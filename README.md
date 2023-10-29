# Лабораторна робота №7

Лабораторна робота з дисципліни "ООП"

## На тему

Saving and loading of objects.
Serialization/ Deserialization

## Постановка завдання

На основі отриманого на лекції 7 теоретичного матеріалу скорегувати програму для лабораторної роботи № 6 наступним чином:

* 1. В основній програмі додати методи для збереження (серіалізації) колекції List<T> об’єктів предметної області у файли з форматом *.csv (*.txt) та *.json, а також методи для читання (десеріалізації) колекції з відповідних файлів.

    - Якщо у класі предметної області реалізований метод ToString() перетворює об’єкт на рядок, в якому всі значення розділенні комою, то обрати формат *.csv, інакше – *.txt. 

* 2. Модифікувати меню таким чином (з’являються нові пункти!):
    - 1 – додати об’єкт
    - 2 – вивести на екран об’єкти
    - 3 – знайти об’єкт
    - 4 – видалити об’єкт
    - 5 – демонстрація поведінки об’єктів
    - 6 – демонстрація роботи static методів
    - 7 – зберегти колекцію об’єктів у файлі
    - 8 – зчитати колекцію об’єктів з файлу
    - 9 – очистити колекцію об’єктів
    - 0 – вийти з програми

> У пункті меню «7 – зберегти колекцію об’єктів у файлі» необхідно реалізувати підменю:
> 1 – зберегти у файл *.csv (*.txt)
> 2 – зберегти у файл *.json

> У пункті меню «8 – зчитати колекцію об’єктів з файлу» необхідно реалізувати підменю:
> 1 – зчитати з файлу *.csv (*.txt)
> 2 – зчитати з файлу *.json

> Якщо на момент зчитування з файлу у колекції List<T> є наявні об’єкти, то десеріалізовані об’єкти мають додаватися до списку.
> До колекції List<T> об’єктів додаємо тільки коректно десеріалізовані об’єкти, інші – пропускаємо.

* 3. Для нових/перероблених методів додати/скорегувати unit-тести.

* 4. Запустити виконання всіх наявних unit-тестів (як нових, так і з попередньої лабораторної роботи) і досягти повного їх проходження.
* 5. Детально протестувати програму. Мають бути протестовані 7-9 пункти меню. При тестуванні десеріалізації перевіряємо процес перетворення не тільки на коректних файлах *.csv (*.txt) і *.json, а також не забуваємо перевірити і файли з пропущеними даними і невірними типами даних.

## Виконання роботи

#### Class diagram

![Рисунок 1 – Діаграма класу Account у Visual Studio]("C:\Users\User\Desktop\ClassDiagramAccount.png" "Діаграма класу Account у Visual Studio")

![Рисунок 2 – Діаграма класу AccountTest у Visual Studio]("C:\Users\User\Desktop\ClassDiagramAccount.png" "Діаграма класу AccountTest у Visual Studio")

![Рисунок 3 – Діаграма класу ProgramTest у Visual Studio]("C:\Users\User\Desktop\ClassDiagramAccount.png" "Діаграма класу ProgramTest у Visual Studio")

#### Програмна реалізація класу

``using System.Text.Json.Serialization;

namespace Lab_7
{
    public class Account
    {
        // Характеристики класу
        private AccountState _state = new();
        private string? _name;
        private string? _address;
        private int _accountNumber;
        private double _balance;
        private short _overdraft;

        private static int counter;
        private static int withdrawLimit;

        public static int Counter
        {
            get { return counter; }
        }

        public static int WithdrawLimit
        {
            get { return withdrawLimit; }
            set
            {
                if (value >= 0) withdrawLimit = value;
                else withdrawLimit = 0;
            }
        }

        [JsonPropertyName("Account state")]
        public AccountState State
        {
            get { return _state; }
            set { _state = value; }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _name = value;
                else
                    _name = "Unknown";
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _address = value;
                else
                    _address = "Kharkiv";
            }
        }

        [JsonPropertyName("Account Number")]
        public int AccountNumber
        {
            get { return _accountNumber; }
            set
            {
                if (value < 100000 || value > 999999)
                    _accountNumber = 123456;
                else
                    _accountNumber = value;
            }
        }

        public double Balance
        {
            get { return _balance; }
            set
            {
                if (value >= 0)
                    _balance = value;
                else
                    _balance = 0;
            }
        }

        public short Overdraft
        {
            get { return _overdraft; }
            set
            {
                if (value <= 0.25 * _balance)
                    _overdraft = value;
                else
                    _overdraft = 0;
            }
        }

        // Автовластивість, яка показує кількість транзакцій на рахунку
        [JsonIgnore]
        public int TransactionCount { get; set; } = 0;

        // Автовластивість, яка показує процентну ставку рахунку
        [JsonIgnore]
        public double InterestRate { get; private set; } = 10;

        // Обчислювальна властивість, розрахунок доступного залишку на рахунку
        [JsonIgnore]
        public double AvailableBalance
        {
            get { return _balance + _overdraft; }
        }

        // Конструктор без параметрів
        public Account()
        {
            counter++;
        }

        // Конструктор з параметрами
        public Account(AccountState state, string name, string address, int accountNumber, double balance, short overdraft)
        {
            State = state;
            Name = name;
            Address = address;
            AccountNumber = accountNumber;
            Balance = balance;
            Overdraft = overdraft;

            counter++;
        }

        // Конструктор реалізований через виклик іншого власного конструктора
        public Account(AccountState state, string name, string address, double balance)
            : this(state, name, address, 111111, balance, 0)
        {

        }

        // Метод поповнення балансу
        public void Deposit(int amount)
        {
            if (amount > 0)
                _balance += amount;
        }

        // Перевантажена версія методу поповнення балансу
        public void Deposit(double amount)
        {
            if (amount > 0)
                _balance += amount;
        }

        // Перевантажена версія методу поповнення балансу, з урахуванням комісії
        public void Deposit(int amount, int depositFee)
        {
            if (amount > 0 && depositFee > 0)
                _balance += amount - depositFee;
        }

        // Метод зняття грошей з балансу
        public bool WithdrawMoney(int amount)
        {
            if (amount < 0)
                return false;
            if (_balance < amount || amount > WithdrawLimit)
                return false;

            _balance -= amount;
            return true;
        }

        // Статичний метод, який вирішує, чи дозволено комусь оформити кредит
        public static bool CreditAllowed(int amount, int income, int age)
        {
            if (amount <= income * 12 && age >= 18)
                return true;
            return false;
        }

        // Статичний метод який перетворює рядок у об’єкт класу Account
        public static Account Parse(string s)
        {
            if (string.IsNullOrEmpty(s))
                throw new ArgumentNullException("Рядок не може бути порожнiм");

            string[] parts = s.Split(',');

            if (parts.Length != 6)
                throw new ArgumentException("Невiрний формат рядка!");

            return new Account((AccountState)Enum.Parse(typeof(AccountState), parts[0]), parts[1],
                parts[2], int.Parse(parts[3]), double.Parse(parts[4]), short.Parse(parts[5]));
        }

        // Статичний метод, який у разі можливості перетворює рядок у об’єкт класу Account
        public static bool TryParse(string s, out Account? account)
        {
            account = null;
            bool valid = false;

            try
            {
                account = Parse(s);
                valid = true;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return valid;
        }

        // Метод формування рядка з описом об'єкта
        public override string ToString()
        {
            return $"{_state},{_name},{_address},{_accountNumber},{_balance:F0}," +
                   $"{_overdraft}";
        }
    }
}``

#### Програмна реалізація тест-класів

