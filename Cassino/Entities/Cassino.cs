using System;

namespace Cassino.Entities
{
    internal class Gambler
    {
        private int Id { get; set; }
        private string? Nome { get; set; }
        internal Gambler? Next { get; set; }
        internal Gambler? Prev { get; set; }

        public Gambler(int id)
        {
            this.Id = id;
            Next = null;
            Prev = null;
        }
        public Gambler(int id, string nome) : this(id)
        {
            this.Nome = nome;
        }
        public override string ToString()
        {
            return $"* {this.Id}";
        }
    }

    internal class Game
    {
        private Gambler? _head;
        private Gambler? _headAux;

        public Game()
        {
            _head = null;
            _headAux = null;
        }

        public void AddGambler(int id, string nome = "")
        {
            Gambler newGambler = new Gambler(id, nome);
            if (_head == null)
            {
                _head = newGambler;
                _head.Next = _head;
                _head.Prev = _head;
                _headAux = _head;
            }
            else
            {
                Gambler last = _head.Prev!;
                last.Next = newGambler;
                newGambler.Prev = last;
                newGambler.Next = _head;
                _head.Prev = newGambler;
                _headAux = newGambler;
            }
        }

        private void Round(int k, int m)
        {
            if (_head != null && _headAux != null)
            {
                //return _head;

                Gambler clockwise = _head;
                Gambler anticlockwise = _headAux;

                int max = Math.Max(k, m);

                // Faz somente um laço na lista circular
                for (int i = 1; i < max; i++)
                {
                    if (i < k)
                        clockwise = clockwise.Next!;
                    if (i < m)
                        anticlockwise = anticlockwise.Prev!;
                }

                if (clockwise == _head && clockwise.Next == _head && anticlockwise == _head && anticlockwise.Next == _head)
                {
                    _head = null; //Devo ter que mexer
                    _headAux = null;
                    //Console.Write($"{clockwise.Id} - - {anticlockwise.Id},");
                    Display(clockwise, anticlockwise);
                }
                else
                {
                    if (clockwise.Prev == anticlockwise && clockwise.Next == anticlockwise)
                    {
                        _head = null; //Devo ter que mexer
                        _headAux = null;
                    }
                    else if (clockwise.Prev == anticlockwise)
                    {
                        clockwise.Prev.Prev!.Next = clockwise.Next;
                        clockwise.Next!.Prev = clockwise.Prev.Prev;
                        _head = clockwise.Next;
                        _headAux = anticlockwise.Prev;
                    }
                    else if (clockwise.Next == anticlockwise)
                    {
                        clockwise.Prev!.Next = clockwise.Next.Next;
                        clockwise.Next.Next!.Prev = clockwise.Prev;
                        _head = clockwise.Next.Next;
                        _headAux = anticlockwise.Prev!.Prev;
                    }
                    else
                    {
                        clockwise.Prev!.Next = clockwise.Next;
                        clockwise.Next!.Prev = clockwise.Prev;
                        anticlockwise.Prev!.Next = anticlockwise.Next;
                        anticlockwise.Next!.Prev = anticlockwise.Prev;
                        _head = clockwise.Next;
                        _headAux = anticlockwise.Prev;
                    }

                    //Console.Write($"{clockwise.Id} - - {anticlockwise.Id},");
                    Display(clockwise, anticlockwise);
                }

                //return head;
            }
        }

        public void Rounds(int k, int m)
        {
            //Gambler? iRound = null;
            do
            {
                Round(k, m);
            } while (_head != null);
        }

        private void Display(Gambler? clockwise, Gambler? anticlockwise)
        {
            if (clockwise == anticlockwise && _head == null)
            {
                Console.WriteLine($"*{clockwise}"); // Ganhador Unico (Ultimo com **)
            }
            else if(clockwise == anticlockwise && _head != null)
            {
                Console.Write($"{clockwise},"); // Com brinde (Unico retirado com *)
            }
            else if(clockwise != anticlockwise && _head != null)
            {
                Console.Write($"*{clockwise} *{anticlockwise},"); // Sem brindes (Retirados com **)
            }
            else
            {
                Console.WriteLine($"*{clockwise} *{anticlockwise}"); // Ganhador Duplo (com **)
            }
            //if (head == null)
            //    return;
            //
            //Gambler temp = head;
            //do
            //{
            //    Console.WriteLine(temp);
            //    temp = temp.Next;
            //} while (temp != head);
        }
    }
}
