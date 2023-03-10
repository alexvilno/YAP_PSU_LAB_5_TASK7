# Лабораторная работа 5, вариант 7, ПГНИУ ЯП МФТИ-1 2022

Решение всех задач оформить в виде одного класса со статическими методами, решающими поставленные задачи. В классе могут присутствовать методы со спецификатором доступа private вспомогательного характера.

## Задание 1. Решить задачу, используя класс List
Составить программу, которая переносит в конец непустого списка L его
первый элемент.

```c#
public static void TASK_1<T>(ref List<T> list) //T - параметр типа
{
    Console.WriteLine("ЗАДАЧА 1");
    Random rnd = new Random();
    Console.WriteLine("Дан список: ");
    foreach (var i in list)
    {
        Console.Write(i + " ");
    }

    Console.WriteLine();
    Console.WriteLine("Ответ: ");
    (list[0], list[^1]) = (list[^1], list[0]);
    foreach (var i in list)
    {
        Console.Write(i + " ");
    }

    Console.WriteLine();
    Console.WriteLine(new String('-', 40));
}
```

И меняем первый и последний элементы местами.


Например для списка 
```
a b c
```

Будет выведено:

```
c b a
```
Для списка 

```
60 5 27 33 17 50 23 76 74 3 
```

Будет выведено:

```
ЗАДАЧА 1
Дан список: 
60 5 27 33 17 50 23 76 74 3 
Ответ: 
3 5 27 33 17 50 23 76 74 60 
----------------------------------------
```

## Задание 2. Решить задачу, используя класс LinkedList

из списка L, содержащего не менее двух элементов, удаляет все элементы, у
которых одинаковые «соседи» (первый и последний элементы считать
соседями);

LinkedList передается по ссылке в качестве параметра функции ```TASK_2();```

```c#
public static void TASK_2<T>(ref LinkedList<T> list);
```

Создаем указатель на первый узел в списке и двигаем его на следующий элемент (это нужно для того, чтобы проверить prev)

```c#
var current = list.First;
current = current.Next;
```

Далее идем по списку пока не достигнем конца и проверяем условие задачи

```c#
while (current != null && current != list.Last && list.Count >= 3)
{
    var old_cur = current;
    if (current.Previous.Value.ToString() == current.Next.Value.ToString())
    {
        old_cur = current;
        list.Remove(current); //удаляем элемент с одинаковыми соседями

    }
    current = old_cur.Next;
}
```

Следующие две проверки позволяют обработать ситуацию, если соседи элемента находятся на разных концах списка:

```c#
if (list.Last.Previous.Value.ToString() == list.First.Value.ToString())
{
    list.Remove(list.Last);
}

if (list.Last.Value.ToString() == list.First.Next.Value.ToString())
{
    list.Remove(list.First);
}
```
Например для списка 
```
6 2 6 1 4 1 1 9 9 7 
```

Вывод:
```
ЗАДАЧА 2
Дан список: 
6 2 6 1 4 1 1 9 9 7 
Ответ: 
6 6 1 4 1 1 9 9 7 
----------------------------------------
```

А для списка
```
d c a b a
```
Вывод:
```
ЗАДАЧА 2
d c a b a 
Ответ: 
d c a a 
----------------------------------------
```

## Задание3. Решить задачу, используя класс HashSet

Есть перечень мебельных фабрик, продукция которых представлена в мебельном магазине. Известно, мебель каких фабрик приобреталась n покупателями. Определить для каждой фабрики, мебель каких из них приобреталась всеми покупателями, каких — некоторыми из покупателей, и каких — никем из покупателей.

Идея состоит в том, чтобы использовать ```HashSet``` для хранения названий фабрик, и создать для каждого покупателя свой ```HashSet``` где будут храниться названия фабрик, мебель которых есть у покупателя. Для получения ответа нужно будет использовать пересечения, разность HashSet ов. 

```c#
HashSet<string>[] buyers = new HashSet<string>[k]; //k - покупателей

for (int i = 0; i < k; ++i)
{
    buyers[i] = new HashSet<string>(); //создаем хешсеты для каждого...
}
```

Ответ на первый вопрос получается пересечением пересечений хешсетов покупателей и хешсета фабрик

```c#
for (int i = 0; i < k; ++i) //в хешсет кадого покупателя записываем пересечение его хешсета и хешсета фабрик
{
    buyers[i].IntersectWith(fabriques);
}

for (int i = 1; i < k; ++i) //в хешсет 
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
```

Ответ на второй вопрос получается объединением пересечений хешсетов покупателей и хешсета фабрик
```c#
for (int i = 0; i < k; ++i)
{
    buyers_copy[i].IntersectWith(fabriques);
}

for (int i = 0; i < k; ++i)
{
    buyers_copy[0].UnionWith(buyers_copy[i]);
}
```

Если ```buyers_copy[0]``` не пуст, ответом на третий вопрос будет расность хешсета фабрик и хешсета ```buyers_copy[0]```. 

```c#
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
```

Например для входных данных
```
4
4
LAZURIT
IKEA
DOMADOM
UZBEK
IKEA
DOMADOM LAZURIT
ABOBAFURNITURE LAZURIT
LAZURIT IKEA DOMADOM UZBEK
```
Ответ будет таким:
```
Мебель ни одной фабрики не присутствует у всех покупателей.
Мебель этих фабрик присутствует хотя бы у одного из покупателей: DOMADOM LAZURIT IKEA UZBEK 
У всех фабрик есть хотя бы один покупатель.
----------------------------------------
```

А для таких: 

```
4
4
LAZURIT
IKEA
DOMADOM
UZBEK
IKEA LAZURIT
DOMADOM LAZURIT
ABOBAFURNITURE LAZURIT
LAZURIT IKEA DOMADOM
```

Ответ будет таким:
```
Мебель этих фабрик есть у всех покупателей: LAZURIT 
Мебель этих фабрик присутствует хотя бы у одного из покупателей: DOMADOM LAZURIT IKEA 
У этих фабрик нет ни одного покупателя: UZBEK 
----------------------------------------
```

## Задание 4. Решить задачу, используя класс HashSet
Файл содержит текст на русском языке. Напечатать в алфавитном порядке все глухие согласные буквы, которые входят в каждое нечетное слово и не входят хотя бы одно четное слово.

Создадим хешсет всех глухих согласных букв(чтобы с ним потом пересечься)

```c#
HashSet<char> letters = new HashSet<char>();
HashSet<char> exists = new HashSet<char>();
string cons = "ПФКТШСХЦЧЩпфктшсхцчщ";

foreach (char i in cons)
{
    letters.Add(i);
}
```

Считывая строки из файла будем слитить их по пробелу. Создадим также ```List``` результатов пересечения хешсета каждой конкретной строки с нашим хешсетом букв ```letters```

```c#
string s = fin.ReadLine();
string[] split = s.Split(' ');
List<char> res = new List<char>();
```

Потом будем обрабатывать слова только нечетные по счету

```C#
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

    temp.IntersectWith(letters);//пересечение

    foreach (char c in temp)
    {
        res.Add(c);
    }
}
```
Создадим еще 2 хешсета. Один будет использоваться для конкретного четного слова, второй - будет объединением пересечения первого и ```letters```

```c#
HashSet<char> i_2chars = new HashSet<char>();
HashSet<char> i_2chars_union = new HashSet<char>();

for (int i = 0; i < split.Length; ++i)
{
    if (i % 2 == 0)
    {
        continue;
    }
    foreach (char c in split[i])
    {
        i_2chars.Add(c);
    }
    i_2chars.IntersectWith(letters);
    i_2chars_union.UnionWith(i_2chars);
}

res.Sort();

foreach (char c in res)
{
    exists.Add(c);
}

exists.ExceptWith(i_2chars_union);
```

Потом нужно будет отсортировать res и добавить в ```exists``` его элементы и вычесть объединение согласных глухих букв ```i_2chars_union```

Для такой строки 
```
Кризис грянул и нет спасенья, нет спасенья, хоть прыгай с кручи… Все твои щас и к месту рвенья, глянь какие подходят тучи. Глянь, какая сверкнула сабля, сколько сразу голов срубила, А за саблей, упала капля, кровь барыги с асфальта смыла.
```

Вывод будет таким:
```
ЗАДАЧА 4
Дана строка: 
Кризис грянул и нет спасенья, нет спасенья, хоть прыгай с кручи… Все твои щас и к месту рвенья, глянь какие подходят тучи. Глянь, какая сверкнула сабля, сколько сразу голов срубила, А за саблей, упала капля, кровь барыги с асфальта смыла.
Ответ: К ф
```

А для такой строки
``` 
цч цс цчмьс лллв
```
Ответ такой
```
ЗАДАЧА 4
Дана строка: 
цч цс цчмьс лллв
Ответ: ч 
```

## Задание 5. Решить задачу, используя класс Dictionary
В молочных магазинах города Х продается сметана с жирностью 15, 20 и 25 процентов. В городе X был проведен мониторинг цен на сметану. Напишите эффективную по времени работы и по используемой памяти программу, которая будет определять для каждого вида сметаны, сколько магазинов продают ее дешевле всего. На вход программе сначала подается число магазинов N. В каждой из следующих N строк находится информация в следующем формате:
**<Фирма> <Улица> <Жирность> <Цена>**
где <Фирма> – строка, состоящая не более, чем из 20 символов без пробелов, <Улица> – строка, состоящая не более, чем из 20 символов без пробелов, <Жирность> – одно из чисел – 15, 20 или 25, <Цена> – целое число в диапазоне от 2000 до 5000, обозначающее стоимость одного литра сметаны в копейках. <Фирма> и <Улица>, <Улица> и <Жирность>, а также <Жирность> и <Цена> разделены ровно одним пробелом. Пример входной строки:
**Перекресток Короленко 25 3200**
Программа должна выводить через пробел 3 числа – количество магазинов, продающих дешевле всего сметану с жирностью 15, 20 и 25 процентов. Если какой-то вид сметаны нигде не продавался, то следует вывести 0.
Пример выходных данных:
**12 10 0**

Хранить в сортированном списке будем ключ(адрес магазина) и значение (жирность и цена).

```c#
string[] values = new string[4];
string scan;
Console.WriteLine("Данные из файла:");

var sorted_list = new SortedList<string, string>();

while ((scan = fin.ReadLine()) != null)
{
    values = scan.Split(" ");
    sorted_list.Add(values[1], values[2] + " " + values[3]); //values[1] это адрес маганина
}
```
Потом проходимся по листу и считаем, сколько у элементов имеет минимальную цену по каждой конкретной жирности
```c#
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
```
Например для таких входных данных
```
PYATEROCHKA BELYAEVA,7 15 5000
MAGNIT KOMINTERNA,30 15 5000
MAGNIT OGUZKOVA,30 15 5000
MAGNIT KOMINTERNA,26 15 5500
PYATEROCHKA LENINA,83 20 6600
PYATEROCHKA LENINA,3 20 6600
K&B PETROPAVLOVSKAYA,123 20 6900
K&B PETROPAVLOVSKAYA,3 20 6900
VISHENKA DEKABRISTOV,23 25 7000
VISHENKA DEKABRISTOV,2 25 7000
GEOFAK GENKELYA,7 25 7200
```

Ответ будет таким:
```
Ответ: 3 2 2 
----------------------------------------
```
