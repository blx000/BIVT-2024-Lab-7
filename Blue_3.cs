using System;
using System.Collections.Generic;
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
                    for (int i = 0; i < newArr.Length; i++)
                    {
                        newArr[i] = _penaltytimes[i];
                    }
                    return newArr;
                }
            }

            public int Total
            {
                get
                {
                    if (_penaltytimes == null)
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


            public virtual bool IsExpelled
            {
                get
                {
                    if (_penaltytimes == null)
                    {
                        return false;
                    }
                    for (int i = 0; i < _penaltytimes.Length; i++)
                    {
                        if (_penaltytimes[i] == 10)
                        {
                            return true;
                        }
                    }
                    return false;
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
            public virtual void PlayMatch(int time)
            {
                if (_penaltytimes == null)
                {
                    return;
                }
                int[] newArray = new int[_penaltytimes.Length + 1];
                Array.Copy(_penaltytimes, newArray, _penaltytimes.Length);
                _penaltytimes = newArray;
                _penaltytimes[_penaltytimes.Length - 1] = time;
            }



            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0)
                {
                    return;
                }
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].Total > array[j + 1].Total)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
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
            public BasketballPlayer(string name, string surname) : base(name, surname)
            {
                _penaltytimes = new int[0];
            }

            public override bool IsExpelled
            {
                get
                {
                    if (_penaltytimes == null && _penaltytimes.Length == 0)
                    {
                        return false;
                    }
                    int fouls_5 = 0;
                    for (int i = 0; i < _penaltytimes.Length; i++)
                    {
                        if (_penaltytimes[i] >= 5)
                        {
                            fouls_5++;
                        }
                    }
                    if (fouls_5 * 100 / _penaltytimes.Length > 10 || Total > 2 * _penaltytimes.Length)
                    {
                        return true;
                    }
                    return false;
                }
            }
            public override void PlayMatch(int fouls)
            {
                if (fouls < 0 || fouls > 5)
                {
                    return;
                }
                base.PlayMatch(fouls);
            }
        }
        public class HockeyPlayer : Participant
        {
            private static int _totalPenaltyTime;
            private static int _num;
            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _penaltytimes = new int[0];
                _num++;
            }
            public override bool IsExpelled
            {
                get
                {
                    if (_penaltytimes == null || _num == 0)
                    {
                        return false;
                    }
                    for (int i = 0; i < Penalties.Length; i++)
                    {
                        if (Penalties[i] >= 10)
                        {
                            return true;
                        }
                    }
                    if (Total > _totalPenaltyTime / _num / 10.0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            public override void PlayMatch(int penaltytime)
            {
                if (penaltytime < 0 || penaltytime > 10 || _penaltytimes == null) return;
                base.PlayMatch(penaltytime);
                _totalPenaltyTime += penaltytime;
            }
        }
    }
}