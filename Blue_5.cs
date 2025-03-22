using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_5
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private int _place;

            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _place = 0;
            }

            public void SetPlace(int place)
            {
                if (_place != 0)
                {
                    return;
                }
                _place = place;
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Surname} {Place} ");
            }
        }

        public abstract class Team
        {
            private string _name;
            private Sportsman[] _sportsmen;
            private int _cnt;

            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmen;
            public int SummaryScore
            {
                get
                {
                    int sum = 0;
                    if (_sportsmen == null)
                    {
                        return 0;
                    }

                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        if (_sportsmen[i].Place == 1)
                        {
                            sum += 5;
                        }
                        if (_sportsmen[i].Place == 2)
                        {
                            sum += 4;
                        }
                        if (_sportsmen[i].Place == 3)
                        {
                            sum += 3;
                        }
                        if (_sportsmen[i].Place == 4)
                        {
                            sum += 2;
                        }
                        if (_sportsmen[i].Place == 5)
                        {
                            sum += 1;
                        }
                    }
                    return sum;
                }
            }

            public int TopPlace
            {
                get
                {
                    int w = 18;
                    if (_sportsmen == null)
                    {
                        return 0;
                    }
                    for (int i = 0; i < _cnt; i++)
                    {
                        if (_sportsmen[i].Place < w)
                        {
                            w = _sportsmen[i].Place;
                        }
                    }
                    return w;
                }
            }

            protected Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[6];
                _cnt = 0;
            }

            public void Add(Sportsman sportsman)
            {
                if (_sportsmen == null || _cnt >= 6)
                {
                    return;
                }

                if (_cnt < 6)
                {
                    _sportsmen[_cnt++] = sportsman;
                }
            }

            public void Add(Sportsman[] sportsmen)
            {
                if (_sportsmen == null || _cnt >= 6)
                {
                    return;
                }
                for (int i = 0; i < sportsmen.Length; i++)
                {
                    if (_cnt >= 6)
                    {
                        return;
                    }
                    if (_cnt < 6)
                    {
                        _sportsmen[_cnt++] = sportsmen[i];
                    }
                }
            }

            public static void Sort(Team[] teams)
            {
                for (int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        if (teams[j].SummaryScore < teams[j + 1].SummaryScore ||
                            (teams[j].SummaryScore == teams[j + 1].SummaryScore && teams[j].TopPlace > teams[j + 1].TopPlace))
                        {
                            (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {SummaryScore} {TopPlace}");
            }

            protected abstract double GetTeamStrength();

            public double CalculateTeamStrength() //публичный метод для обращения к protected
            {
                return GetTeamStrength();
            }
            public static Team GetChampion(Team[] teams)
            {
                Team champion = null;
                double maxStrength = double.MinValue;

                foreach (var team in teams)
                {
                    double strength = team.GetTeamStrength();
                    if (strength > maxStrength)
                    {
                        maxStrength = strength;
                        champion = team;
                    }
                }

                return champion;
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name)
            {
            }

            protected override double GetTeamStrength()
            {
                if (Sportsmen == null || Sportsmen.Length == 0)
                {
                    return 0;
                }

                double sumPlaces = 0;
                int count = 0;

                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman != null)
                    {
                        sumPlaces += sportsman.Place;
                        count++;
                    }
                }

                if (count == 0)
                {
                    return 0;
                }

                return 100 / (sumPlaces / count); //чем меньше среднее значение мест, тем выше сила
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name)
            {
            }

            protected override double GetTeamStrength()
            {
                if (Sportsmen == null || Sportsmen.Length == 0)
                {
                    return 0;
                }

                double sumPlaces = 0;
                double productPlaces = 1;
                int count = 0;

                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman != null)
                    {
                        sumPlaces += sportsman.Place;
                        productPlaces *= sportsman.Place;
                        count++;
                    }
                }

                if (productPlaces == 0)
                {
                    return 0;
                }

                return 100 * count / (sumPlaces * productPlaces); //чем меньше сумма и произведение мест, тем выше сила
            }
        }
    }
}