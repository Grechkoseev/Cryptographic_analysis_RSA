namespace Lab_7_variant_10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int e = 251;
            var N = 42869;
            var C = 9491;

            Console.Title = "Криптоанализ шифра RSA";
            Console.WriteLine(
                "Открытым ключом шифрования RSA является пара чисел (e, N), равная ({0}, {1}), зашифрованное сообщение {2}\n",
                e, N, C);

            var rsa = new Rsa(e, N, C);

            Console.WriteLine(
                "Закрытым ключом шифрования RSA является пара чисел (d, N), равная ({0}, {1})\n", rsa.CountD(), N);

            var M = rsa.DecryptMessage();

            Console.WriteLine("Сообщение С = {0} расшифровано в M = {1}", C, M);
            Console.ReadKey();
        }
    }
}