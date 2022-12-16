using System;
using PSU_PL_LAB5_TASK_7;


class program
{
    public static void Main(string[] args)
    {
        var path3 = "/Users/alexvilno/Projects/PSU_PL_LAB5_TASK_7/PSU_PL_LAB5_TASK_7/task_3.txt";
        var path4 = "/Users/alexvilno/Projects/PSU_PL_LAB5_TASK_7/PSU_PL_LAB5_TASK_7/task_4.txt";
        var path5 = "/Users/alexvilno/Projects/PSU_PL_LAB5_TASK_7/PSU_PL_LAB5_TASK_7/task_5.txt";

        List<char> chars = new List<char>();
        chars.Add('a');
        chars.Add('b');
        chars.Add('c');
        LinkedList<char> chars1 = new LinkedList<char>();
        chars1.AddFirst('a');
        chars1.AddFirst('b');
        chars1.AddFirst('a');
        chars1.AddFirst('c');
        chars1.AddFirst('d');

        Solutions.TASK_1(ref chars);
        Solutions.TASK_2(ref chars1);
        Solutions.TASK_3(path3);
        Solutions.TASK_4(path4);
        Solutions.TASK_5(path5);
    }
}

