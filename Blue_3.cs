using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_3
    {
        public class Participant
        {
            //поля
            private string _name;
            private string _surname;
            protected int[] _penaltytimes;

            //свойства
            public string Name => _name;
            public string Surname => _surname;
            public int[] Penalties
            {
                get
                {
                    if (_penaltytimes == null)
                    {
                        return null;
                    }

                    int[] newArr = new int[_penaltytimes.Length];
                    Array.Copy(_penaltytimes, newArr, _penaltytimes.Length);
                    return newArr;
                }
            }
            public int Total
            {
                get
                {
                    if (_penaltytimes == null || _penaltytimes.Length == 0)
                    {
                        return 0;
                    }
                    int sum = 0;
                    for (int i = 0; i < _penaltytimes.Length; i++)
                    {
                        sum += _penaltytimes[i];
                    }
                    return sum;
                }
            }

            virtual public bool IsExpelled
            {
                get
                {
                    if (_penaltytimes == null)
                    {
                        return true;
                    }
                    for (int i = 0; i < _penaltytimes.Length; i++)
                    {
                        if (_penaltytimes[i] == 10)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }

            //конструкторы
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penaltytimes = new int[0];
            }

            //методы
            virtual public void PlayMatch(int time)
            {
                if (_penaltytimes == null)
                {
                    return;
                }
                Array.Resize(ref _penaltytimes, _penaltytimes.Length + 1);
                _penaltytimes[_penaltytimes.Length - 1] = time;
            }

            public static void Sort(Participant[] participants)
            {
                if (participants == null)
                {
                    return;
                }

                for (int i = 0; i < participants.Length; i++)
                {
                    for (int j = 0; j < participants.Length - i - 1; j++)
                    {
                        if (participants[j + 1].Total < participants[j].Total)
                        {
                            (participants[j], participants[j + 1]) = (participants[j + 1], participants[j]);
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Surname} {Total} {IsExpelled}");
            }
        }

        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string name, string surname) : base(name, surname) { }

            public override bool IsExpelled
            {
                get
                {
                    if (_penaltytimes == null || _penaltytimes.Length == 0)
                    {
                        return true;
                    }

                    int fouls_5 = 0;
                    for (int i = 0; i < _penaltytimes.Length; i++)
                    {
                        if (_penaltytimes[i] == 5)
                        {
                            fouls_5++;
                        }
                    }

                    double percentfouls_5 = (double)fouls_5 / _penaltytimes.Length;

                    bool con1 = percentfouls_5 > 0.1; //более 10% матчей с 5 фолами
                    bool con2 = Total > 2 * _penaltytimes.Length; //суммарное количество фолов вдвое больше, чем количество матчей

                    return !(con1 && con2);
                }
            }

            public override void PlayMatch(int fouls)
            {
                if (fouls < 0 || fouls > 5)
                {
                    return;
                }

                Array.Resize(ref _penaltytimes, _penaltytimes.Length + 1);
                _penaltytimes[_penaltytimes.Length - 1] = fouls;
            }
        }

        public class HockeyPlayer : Participant
        {
            private static int _totalPenaltyTime = 0;
            private static int _num = 0;

            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _num++;
            }

            public override bool IsExpelled
            {
                get
                {
                    if (_penaltytimes == null || _penaltytimes.Length == 0)
                    {
                        return true;
                    }

                    bool con1 = false; //хотя бы один матч с 10 минутами штрафа
                    for (int i = 0; i < _penaltytimes.Length; i++)
                    {
                        if (_penaltytimes[i] == 10)
                        {
                            con1 = true;
                            break;
                        }
                    }

                    double avPenaltyTime = (double)_totalPenaltyTime / _num;
                    bool con2 = Total > 0.1 * avPenaltyTime; //суммарное штрафное время больше 10% от среднего

                    return !(con1 && con2);
                }
            }

            public override void PlayMatch(int penaltyTime)
            {
                if (penaltyTime < 0)
                {
                    return;
                }

                Array.Resize(ref _penaltytimes, _penaltytimes.Length + 1);
                _penaltytimes[_penaltytimes.Length - 1] = penaltyTime;
                _totalPenaltyTime += penaltyTime;
            }
        }
    }
}