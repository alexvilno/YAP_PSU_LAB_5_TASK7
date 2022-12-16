using System;
namespace PSU_PL_LAB5_TASK_7
{
    public class Solutions
    {
        public static void TASK_1()
        {
            Console.WriteLine("ЗАДАЧА 1");

            List<int> list = new List<int>();
            Random rnd = new Random();
            Console.WriteLine("Дан список: ");
            for (int i = 0; i < 10; ++i)
            {
                list.Add(rnd.Next(1, 100));
            }
            foreach (int i in list)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();
            Console.WriteLine("Ответ: ");
            (list[0], list[^1]) = (list[^1], list[0]);
            foreach (int i in list)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();
            Console.WriteLine(new String('-', 40));
        }

        public static void TASK_2()
        {
            Console.WriteLine("ЗАДАЧА 2");

            LinkedList<int> list = new LinkedList<int>();
            Random rnd = new Random();
            Console.WriteLine("Дан список: ");
            for (int i = 0; i < 10; ++i)
            {
                list.AddFirst(rnd.Next(1, 10));
            }
            foreach (int i in list)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();

            var current = list.First;
            current = current.Next;

            while (current != null && current != list.Last && list.Count >= 3)
            {
                var old_cur = current;
                if (current.Previous.Value == current.Next.Value)
                {
                    old_cur = current;
                    list.Remove(current);

                }
                current = old_cur.Next;
            }

            if (list.Last.Previous.Value == list.First.Value)
            {
                list.Remove(list.Last);
            }

            if (list.Last.Value == list.First.Next.Value)
            {
                list.Remove(list.First);
            }

            Console.WriteLine("Ответ: ");

            foreach (int i in list)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();
            Console.WriteLine(new String('-', 40));
        }

        public static void TASK_3(string read_path)
        {
            Console.WriteLine("ЗАДАЧА 3");

            StreamReader fin;
            try
            {
                fin = new StreamReader(read_path);
            }
            catch (Exception e)
            {
                Console.WriteLine("Smth went wrong");
                return;
            }

            HashSet<string> fabriques = new HashSet<string>();
            int n = Convert.ToInt32(fin.ReadLine());
            int k = Convert.ToInt32(fin.ReadLine());

            HashSet<string>[] buyers = new HashSet<string>[k];

            for (int i = 0; i < k; ++i)
            {
                buyers[i] = new HashSet<string>();
            }

            for (int i = 0; i < n; ++i)
            {
                fabriques.Add(fin.ReadLine());
            }

            Console.WriteLine();

            string[] scan = new string[n];
            for (int i = 0; i < k; ++i)
            {
                scan = fin.ReadLine().Split(' ');
                foreach (var fab in scan)
                {
                    buyers[i].Add(fab);
                }
            }

            var buyers_copy = buyers;

            for (int i = 0; i < k; ++i)
            {
                buyers[i].IntersectWith(fabriques);
            }

            for (int i = 1; i < k; ++i)
            {
                buyers[0].IntersectWith(buyers[i]);
            }
            

            
            if (buyers[0].Count != 0)
            {
                Console.Write("Мебель этих фабрик есть у всех покупателей: ");
                foreach (var i in buyers[0])
                {
                    Console.WriteLine(i + " ");
                }
            }
            else Console.WriteLine("Мебель ни одной фабрики не присутствует у всех покупателей.");

            for (int i = 0; i < k; ++i)
            {
                buyers_copy[i].IntersectWith(fabriques);
            }

            for (int i = 0; i < k; ++i)
            {
                buyers_copy[0].UnionWith(buyers_copy[i]);
            }

            if (buyers_copy[0].Count != 0)
            {
                Console.Write("Мебель этих фабрик присутствует хотя бы у одного из покупателей: ");
                foreach (var i in buyers_copy[0])
                {
                    Console.Write(i + " ");
                }

                fabriques.ExceptWith(buyers_copy[0]);

                Console.WriteLine();

                if (fabriques.Count != 0)
                {
                    Console.Write("У этих фабрик нет ни одного покупателя: ");
                    foreach(var i in fabriques)
                    {
                        Console.Write(i + " ");
                    }
                }
                else Console.WriteLine("У всех фабрик есть хотя бы один покупатель.");
            }
            else Console.WriteLine("Ни у одной из фабрик нет ни одного покупателя.");

            Console.WriteLine();
            Console.WriteLine(new String('-', 40));
            fin.Close();
        }
    


    public static void TASK_4(string read_path)
    {
            Console.WriteLine("ЗАДАЧА 4");

            StreamReader fin;
            try
            {
                fin = new StreamReader(read_path);
            }
            catch (Exception e)
            {
                Console.WriteLine("Smth wrong");
                return;
            }

            HashSet<char> letters = new HashSet<char>();
            HashSet<char> exists = new HashSet<char>();
            string cons = "БВГДЖЗЛМНРЦЧбвгджзлмнрцч";

            foreach (char i in cons)
            {
                letters.Add(i);
            }

            Random rnd = new Random();
            Console.WriteLine("Дана строка: ");

            string s = fin.ReadLine();
            Console.WriteLine(s);

            List<char> res = new List<char>();

            string[] split = s.Split(' ');
            for (int i = 0; i < split.Length; ++i)
            {
                if (i % 2 == 0)
                {
                    continue;
                }

                HashSet<char> temp = new HashSet<char>();

                foreach (char c in split[i])
                {
                    temp.Add(c);
                }

                temp.IntersectWith(letters);

                foreach (char c in temp)
                {
                    res.Add(c);
                }
            }

            res.Sort();

            foreach (char c in res)
            {
                exists.Add(c);
            }

            Console.Write("Ответ: ");

            foreach (char c in exists)
            {
                Console.Write(c + " ");
            }

            Console.WriteLine();

            fin.Close();
            Console.WriteLine(new String('-', 40));
        }

        public static void TASK_5(string read_path)
        {
            Console.WriteLine("ЗАДАЧА 5");

            StreamReader fin;
            try
            {
                fin = new StreamReader(read_path);
            }
            catch (Exception e)
            {
                Console.WriteLine("Smth went wrong");
                return;
            }
            string[] values = new string[4];
            string scan;
            Console.WriteLine("Данные из файла:");

            var sorted_list = new SortedList<string, string>();

            while ((scan = fin.ReadLine()) != null)
            {
                values = scan.Split(" ");
                sorted_list.Add(values[1], values[2] + " " + values[3]);
            }
            Console.WriteLine();

            foreach (var info in sorted_list)
            {
                Console.WriteLine(info.Key + " " + info.Value + "\n");
            }

            int min_15_price = 999999999;
            int min_20_price = 999999999;
            int min_25_price = 999999999;

            int count_15 = 0;
            int count_20 = 0;
            int count_25 = 0;

            foreach (var info in sorted_list)
            {
                switch(info.Value.Split(" ")[0])
                {
                    case "15":
                        if (Convert.ToInt32(info.Value.Split(" ")[1]) < min_15_price)
                        {
                            min_15_price = Convert.ToInt32(info.Value.Split(" ")[1]);
                            count_15 = 1;
                            break;
                        }
                        if (Convert.ToInt32(info.Value.Split(" ")[1]) == min_15_price)
                        {
                            count_15++;
                            break;
                        }
                        break;
                    case "20":
                        if (Convert.ToInt32(info.Value.Split(" ")[1]) < min_20_price)
                        {   
                            min_20_price = Convert.ToInt32(info.Value.Split(" ")[1]);
                            count_20 = 1;
                            break;
                        }
                        if (Convert.ToInt32(info.Value.Split(" ")[1]) == min_20_price)
                        {
                            count_20++;
                            break;
                        }
                        break;
                    case "25":
                        if (Convert.ToInt32(info.Value.Split(" ")[1]) < min_25_price)
                        {
                            min_25_price = Convert.ToInt32(info.Value.Split(" ")[1]);
                            count_25 = 1;
                            break;
                        }
                        if (Convert.ToInt32(info.Value.Split(" ")[1]) == min_25_price)
                        {
                            count_25++;
                            break;
                        }
                        break;
                }
            }

            Console.Write("Ответ: ");
            Console.WriteLine(count_15 + " " + count_20 + " " + count_25);
            Console.WriteLine(new String('-', 40));
            fin.Close();
        }
    }
}