using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_2
    {
        public struct Participant
        {
            //поля
            private string _name;
            private string _surname;
            private int[,] _marks;
            private int _count;

            //свойства
            public string Name => _name;
            public string Surname => _surname;

            public int[,] Marks
            {
                get
                {
                    if (_marks == null)
                    {
                        return null;
                    }
                    int[,] nMarks = new int[2, 5];
                    for (int i = 0; i <= 1; i++)
                    {
                        for (int j = 0; j <= 4; j++)
                        {
                            nMarks[i, j] = _marks[i, j];
                        }
                    }
                    return nMarks;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_marks == null || _marks.GetLength(0) == 0 || _marks.GetLength(1) == 0)
                    {
                        return 0;
                    }
                    int cnt = 0;
                    for (int i = 0; i <= 1; i++)
                    {
                        for (int j = 0; j <= 4; j++)
                        {
                            cnt += _marks[i, j];
                        }
                    }
                    return cnt;
                }
            }

            //конструкторы
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _count = 0;
                _marks = new int[2, 5];
            }

            //методы
            public void Jump(int[] result)
            {
                if (result == null || result.Length == 0 || _marks == null)
                {
                    return;
                }
                if (_count < 2)
                {
                    int r = 0;
                    for (int j = 0; j < _marks.GetLength(1); j++)
                    {
                        _marks[_count, j] = result[r++];
                    }
                    _count++;
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 1; j < array.Length - i; j++)
                    {
                        if (array[j].TotalScore > array[j - 1].TotalScore)
                        {
                            (array[j], array[j - 1]) = (array[j - 1], array[j]);
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Surname} {TotalScore}");
            }
        }

        public abstract class WaterJump
        {
            //приватные поля
            private string _name;
            private int _bank;
            private Participant[] _participants;

            //свойства
            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants
            {
                get
                {
                    return _participants;
                }
            }

            public abstract double[] Prize { get; }

            //конструктор
            public WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];
            }

            public void Add(Participant participant)
            {
                if (_participants == null) { return; }
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;
            }

            public void Add(params Participant[] participants)
            {
                if (participants == null || _participants == null) { return; }
                foreach (Participant participant in participants)
                {
                    Add(participant);
                }
            }
        }

        public class WaterJump3m : WaterJump
        {
            //конструктор
            public WaterJump3m(string name, int bank) : base(name, bank) { }
            public override double[] Prize
            {
                get
                {
                    if (Participants == null) { return null; }
                    if (Participants.Length < 3)
                        return null;

                    double[] prizes = new double[3];
                    prizes[0] = Bank * 0.5; // 50% за первое место
                    prizes[1] = Bank * 0.3; // 30% за второе место
                    prizes[2] = Bank * 0.2; // 20% за третье место
                    return prizes;
                }
            }
        }

        public class WaterJump5m : WaterJump
        {
            //конструктор
            public WaterJump5m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    if (Participants == null) { return null; }
                    if (Participants.Length < 3)
                        return null;

                    double[] prizes = new double[Math.Min(Participants.Length, 10)];                   
                    prizes[0] = Bank * 0.4;
                    prizes[1] = Bank * 0.25;
                    prizes[2] = Bank * 0.15;

                    int top = Math.Min(Math.Max(Participants.Length / 2, 3), 10);
                    double remain = 20.0 / top;
                    for (int i = 0; i < top; i++)
                    {
                        prizes[i] = (remain / 100) * Bank;
                    }               
                    return prizes;
                }
            }
        }
    }
}