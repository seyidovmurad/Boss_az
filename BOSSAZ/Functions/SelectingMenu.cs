using System;
using System.Linq;
namespace Menu
{
    public static class SelectingMenu
    {
        private static void MakeButton(string item,int maxLength){
            if (maxLength % 2 != 0) maxLength += 7;
            else maxLength += 6;
            Console.Write(" ");
            Console.Write(new string('-', maxLength));
            Console.Write("\n");
            Console.Write("|");
            Console.Write(new string(' ', (maxLength - item.Length) / 2));
            Console.Write(item);
            Console.Write(new string(' ', maxLength - (item.Length + (maxLength - item.Length) / 2)));
            Console.Write("|\n");
            Console.Write(" ");
            Console.Write(new string('-', maxLength));
            Console.Write("\n");
        }

        public static int Choose(string[] items)
        {
            int size = items.Length;
            bool[] isChoosenLine = new bool[size];
            int index = 0;
            while (true)
            {
                Console.Clear();
                int j = 0;
                int maxLength = items.Max(i => i.Length);
                if (maxLength > 40) maxLength = 40;
                foreach (var item in items)
                {
                    string temp = item;
                    if (temp.Length > 40)
                    {
                        temp = item.Substring(0, 37);
                        temp += "...";
                    }
                    if (isChoosenLine[j])
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                    MakeButton(temp, maxLength);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    j++;
                }
                ConsoleKeyInfo rKey = Console.ReadKey();
                if (rKey.Key == ConsoleKey.UpArrow)
                {
                    index--;
                    if (index == -1)
                        index = size - 1;
                }
                else if (rKey.Key == ConsoleKey.DownArrow)
                {
                    index++;
                    if (index == size)
                        index = 0;
                }
                else if (rKey.Key == ConsoleKey.Enter)
                {
                    return index;
                }
                for (int i = 0; i < size; i++)
                {
                    isChoosenLine[i] = false;
                }
                isChoosenLine[index] = true;

            }
        }

        public static int Choose(string dontSelect, string[] items)
        {
            int size = items.Length;
            bool[] isChoosenLine = new bool[size];
            int index = 0;
            int maxLength = items.Max(i => i.Length); 
            while (true)
            {
                Console.Clear();
                int j = 0;
                Console.WriteLine(dontSelect);
                if (maxLength > 40) maxLength = 40;
                foreach (var item in items)
                {
                    string temp = item;
                    if (temp.Length > 40)
                    {
                        temp = item.Substring(0, 37);
                        temp += "...";
                    }
                    if (isChoosenLine[j])
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                    MakeButton(temp, maxLength);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    j++;
                }
                ConsoleKeyInfo rKey = Console.ReadKey();
                if (rKey.Key == ConsoleKey.UpArrow)
                {
                    index--;
                    if (index == -1)
                        index = size - 1;
                }
                else if (rKey.Key == ConsoleKey.DownArrow)
                {
                    index++;
                    if (index == size)
                        index = 0;
                }
                else if (rKey.Key == ConsoleKey.Enter)
                {
                    return index;
                }
                for (int i = 0; i < size; i++)
                {
                    isChoosenLine[i] = false;
                }
                isChoosenLine[index] = true;

            }
        }

        public static string ChooseByString(string[] items)
        {
            int size = items.Length;
            bool[] isChoosenLine = new bool[size];
            int index = 0;
            int maxLength = items.Max(i => i.Length);
            while (true)
            {
                Console.Clear();
                int j = 0;
                if (maxLength > 40) maxLength = 40;
                foreach (var item in items)
                {
                    string temp = item;
                    if (temp.Length > 40)
                    {
                        temp = item.Substring(0, 37);
                        temp += "...";
                    }
                    if (isChoosenLine[j])
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                    MakeButton(temp, maxLength);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    j++;
                }
                ConsoleKeyInfo rKey = Console.ReadKey();
                if (rKey.Key == ConsoleKey.UpArrow)
                {
                    index--;
                    if (index == -1)
                        index = size - 1;
                }
                else if (rKey.Key == ConsoleKey.DownArrow)
                {
                    index++;
                    if (index == size)
                        index = 0;
                }
                else if (rKey.Key == ConsoleKey.Enter)
                {
                    return items[index];
                }
                for (int i = 0; i < size; i++)
                {
                    isChoosenLine[i] = false;
                }
                isChoosenLine[index] = true;

            }
        }

        public static int ChooseVertical(string[] items)
        {
            int size = items.Length;
            bool[] isChoosenLine = new bool[size];
            int index = 0;
            while (true)
            {
                Console.Clear();
                int j = 0;
                foreach (var item in items)
                {
                    Console.Write(" | ");
                    if (isChoosenLine[j])
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(item);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(" | ");
                    j++;
                }
                ConsoleKeyInfo rKey = Console.ReadKey();
                if (rKey.Key == ConsoleKey.LeftArrow)
                {
                    index--;
                    if (index == -1)
                        index = size - 1;
                }
                else if (rKey.Key == ConsoleKey.RightArrow)
                {
                    index++;
                    if (index == size)
                        index = 0;
                }
                else if (rKey.Key == ConsoleKey.Enter)
                {
                    return index;
                }
                for (int i = 0; i < size; i++)
                {
                    isChoosenLine[i] = false;
                }
                isChoosenLine[index] = true;

            }
        }
    }
}