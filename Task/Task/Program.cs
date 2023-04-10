using System.Threading.Tasks;

int countDouble = 0;
int countOdd = 0;

object Double = new();
object Odd = new();
async Task generate()
{
    int a = 0;
    Random rnd = new Random();
    Parallel.For(1, 100, index =>
    {
        a = rnd.Next(1, 100);
        if (a % 2 == 0)
            lock (Double) { countDouble++; }
        else lock (Odd) { countOdd++; }
    });

}

Task[] tasks = new Task[]
{
   new Task(async ()=>await generate()),
   new Task(async ()=>await generate()),
   new Task(async ()=>await generate()),
   new Task(async ()=>await generate()),
   new Task(async ()=>await generate()),
   new Task(async ()=>await generate()),
   new Task(async ()=>await generate()),
   new Task(async ()=>await generate()),
   new Task(async ()=>await generate()),
   new Task(async ()=>await generate())
};


foreach (var t in tasks)
    t.Start();
    

await generate();
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
            var createArr = Task.Run(() =>
            {
                for (int i = 0; i<100000; i++)
                {
                    array[i] = i;
                }
            });

            List<int> result = new List<int>();

            async Task CalculateAsync(Params p)
            {
                if (p==null) return;

                await Task.Run(() => intRootMeth(p));
            }

            var par1 = new Params()
            {
                From=0,
                To = 50000
            };
            var par2 = new Params()
            {
                From=50000,
                To = 100000
            };
            void intRootMeth(Params p)
            {
                for (int i = p.From; i<p.To; i++)
                {
                    if (Math.Sqrt(array[i])%1==0)
                    {
                        result.Add(array[i]);
                    }

                }
            }
            await createArr;
            await CalculateAsync(par1);
            await CalculateAsync(par2);

            Console.WriteLine("Числа, що мають цілий корінь - "+string.Join(",", result));

        }
    }
}


