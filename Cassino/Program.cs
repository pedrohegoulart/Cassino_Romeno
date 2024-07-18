using System;
using Cassino.Entities;
using Cassino.Entities.Exceptions;

namespace Init
{
    class Program
    {
        static void Main(string[] args)
        {
            Game romeno = new Game();

            while (true)
            {
                try
                {
                    Tuple<int, int, int>? inputs = InputData();
                    if (inputs == null)
                        break;

                    int N = inputs.Item1;
                    int k = inputs.Item2;
                    int m = inputs.Item3;

                    //romeno.Rounds(k, m); Vericar caso não adicionado os Apostadores antes de iniciar a rodada

                    for (int i = 1; i <= N; i++)
                        romeno.AddGambler(i);

                    romeno.Rounds(k, m);
                }
                catch (DomainException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
            }
        }

        static Tuple<int, int, int>? InputData()
        {
            
            //Console.WriteLine("Digite os valores de N, k e m (separados por espaço) ou '0 0 0' para sair:");
            string[] input = (Console.ReadLine() ?? "").Split(' ');

            if (input.Length != 3)
                throw new DomainException("Entrada inválida. Certifique-se de fornecer exatamente três números.");
            
            if (!int.TryParse(input[0], out int N) || !int.TryParse(input[1], out int k) || !int.TryParse(input[2], out int m))
                throw new DomainException("Entrada inválida. Certifique-se de fornecer números inteiros.");

            if (N > 20)
                throw new DomainException("Entrada inválida. Certifique-se de fornecer o parametro N igual ou menor que 20.");

            if (N == 0 && k == 0 && m == 0)
                return null;

            if (N < 1 || k < 1 || m < 1)
                throw new DomainException("Entrada inválida. Certifique-se de fornecer TODOS números inteiros maiores que 0, ou TODOS iguais a 0 para encerrar .");

            return Tuple.Create(N, k, m); 
        }
    }
}

