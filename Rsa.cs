using BigInteger = System.Numerics.BigInteger;

namespace Lab_7_variant_10;

internal class Rsa
{
    private readonly int _e;
    private readonly int _N;
    private readonly int _C;
    private readonly int _p;
    private readonly int _q;
    private int _d;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="e"></param>
    /// <param name="N"></param>
    /// <param name="C"></param>
    public Rsa(int e, int N, int C)
    {
        _e = e;
        _N = N;
        _C = C;

        var multipliers = GetPrimeFactors(N);

        if (multipliers.Count == 2)
        {
            if (multipliers.Any(multiplier => !IsPrime(multiplier)))
            {
                Console.WriteLine("Число {0} не является произведением двух простых чисел", N);
                return;
            }

            _p = multipliers[0];
            _q = multipliers[1];

            Console.WriteLine("Число N = {0} является произведением двух простых чисел: p = {1} и q = {2}\n", _N, _p, _q);
        }
        else
        {
            Console.WriteLine("Число {0} не является произведением двух простых чисел", N);
        }
    }

    /// <summary>
    /// Разложение числа на простые множители
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    private static List<int> GetPrimeFactors(int number)
    {
        var factors = new List<int>();
        var divisor = 2;

        while (number > 1)
        {
            if (number % divisor == 0)
            {
                factors.Add(divisor);
                number /= divisor;
            }
            else
            {
                divisor++;
            }
        }

        return factors;
    }

    /// <summary>
    /// Проверка числа на простоту с помощью решета Эратосфена
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    private static bool IsPrime(int number)
    {
        if (number < 2) return false;

        var primes = new bool[number + 1];

        for (var i = 2; i <= number; i++)
        {
            primes[i] = true;
        }

        for (var p = 2; p * p <= number; p++)
        {
            if (primes[p])
            {
                for (var i = p * 2; i <= number; i += p)
                {
                    primes[i] = false;
                }
            }
        }
        return primes[number];
    }

    /// <summary>
    /// Вычисляем d из закрытого ключа
    /// </summary>
    /// <returns></returns>
    public int CountD()
    {
        var x = 0;
        var y = 0;
        var gcd = ExtendedEuclid(_e, (_p - 1) * (_q - 1), ref x, ref y);

        if (gcd != 1)
        {
            Console.WriteLine("Уравнение не имеет решений.");
            return 0;
        }
        
        while (x < 0)
        {
            x += (_p - 1) * (_q - 1);
        }

        _d = x;
        return x;
    }

    /// <summary>
    /// Вычисление НОД используя расширенный алгоритм Евклида
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private static int ExtendedEuclid(int a, int b, ref int x, ref int y)
    {
        if (b == 0)
        {
            x = 1;
            y = 0;
            return a;
        }

        int x1 = 0, y1 = 0;
        var gcd = ExtendedEuclid(b, a % b, ref x1, ref y1);

        x = y1;
        y = x1 - (a / b) * y1;
        return gcd;
    }

    /// <summary>
    /// Расшифровываем сообщение
    /// </summary>
    /// <returns></returns>
    public int DecryptMessage()
    {
        return (int)(BigInteger.Pow(_C, _d) % _N);
    }
}