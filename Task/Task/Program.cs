int countDouble = 0;
int countOdd = 0;

object Double = new();
object Odd = new();

void generate()
{
    int a = 0;
    Random rnd = new Random();
    for (int i = 0; i<100; i++)
    {
        a = rnd.Next(1, 100);
        if (a % 2 == 0)
            lock (Double) { countDouble++; }
        else lock (Odd) { countOdd++; }
    }


}

const int taskNumber = 10;
var tasks = new List<Task>();
for (int i = 0; i < taskNumber; i++)
    tasks.Add(Task.Run(generate));

Task.WaitAll(tasks.ToArray());


Console.WriteLine("countDouble - " + countDouble);
Console.WriteLine("countOdd - " + countOdd);



namespace Tasks
{
    class Params
    {
        public int From { get; set; }
        public int To { get; set; }
    }
    class Program
    {

        static async Task Main()
        {
            int[] array = new int[100000];

            for (int i = 0; i < 100000; i++)
            {
                array[i] = i;
            }

            List<int> result = new List<int>();

            var par1 = new Params()
            {
                From = 0,
                To = 50000
            };

            var par2 = new Params()
            {
                From = 50000,
                To = 100000
            };

            var task1 = Task.Run(() => intRootMeth(par1));
            var task2 = Task.Run(() => intRootMeth(par2));

            void intRootMeth(Params p)
            {
                for (int i = p.From; i < p.To; i++)
                {
                    if (Math.Sqrt(array[i]) % 1 == 0)
                    {
                        result.Add(array[i]);
                    }
                }
            }
            await task1;
            await task2;

            Console.WriteLine("Числа, що мають цілий корінь - " + string.Join(",", result));

        }
    }
}


