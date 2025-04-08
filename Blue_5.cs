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

            private bool _place_set;
            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _place = 0;
                _place_set = false;
            }

            public void SetPlace(int place)
            {
                if (_place_set == false)
                {
                    return;
                }
                _place = place;
                _place_set = true;
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
                    if (_sportsmen == null || _sportsmen.Length == 0)
                    {
                        return 0;
                    }

                    foreach (var sportsman in _sportsmen)
                    {
                        switch (sportsman.Place)
                        {
                            case 1:
                                sum += 5;
                                break;
                            case 2:
                                sum += 4;
                                break;
                            case 3:
                                sum += 3;
                                break;
                            case 4:
                                sum += 2;
                                break;
                            case 5:
                                sum += 1;
                                break;
                            default:
                                break;
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
                    foreach (Sportsman sportsman in _sportsmen)
                    {
                        if (sportsman != null && sportsman.Place != 0)
                        {
                            w = Math.Min(w, sportsman.Place);
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
                if (sportsman == null) _sportsmen = new Sportsman[6];
                _sportsmen[_cnt++] = sportsman;
            }

            public void Add(Sportsman[] sportsmen)
            {
                if (_sportsmen == null || _cnt >= 6 || sportsmen == null)
                {
                    return;
                }
                for (int i = 0; i < sportsmen.Length; i++)
                {
                    if (_cnt >= 6)
                    {
                        return;
                    }
                    _sportsmen[_cnt++] = sportsmen[i];
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

            protected abstract double GetTeamStrength();

            private static Team[] Sort_strength(Team[] teams)
            {
                if (teams == null) return null;
                var sortedTeams = teams.OrderByDescending(t => t.GetTeamStrength()).ToArray();
                return sortedTeams;
            }
            public static Team GetChampion(Team[] teams)
            {
                if (teams == null) return null;
                teams = Sort_strength(teams);

                return teams[0];
            }
            public void Print()
            {
                Console.WriteLine($"{Name} {SummaryScore} {TopPlace}");
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
                double count = 0;

                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman != null && sportsman.Place != 0)
                    {
                        sumPlaces += sportsman.Place;
                        count++;
                    }
                }

                if (count == 0)
                {
                    return 0;
                }

                return 100.0 / (sumPlaces / count); //чем меньше среднее значение мест, тем выше сила
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                if (Sportsmen == null || Sportsmen.Length == 0)
                {
                    return 0;
                }

                double sumPlaces = 0;
                double Places = 1;
                double count = 0;

                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman.Place != 0)
                    {
                        sumPlaces += sportsman.Place;
                        Places *= sportsman.Place;
                        count++;
                    }
                }

                if (Places == 0)
                {
                    return 0;
                }

                return 100.0 * ((count * sumPlaces) / Places); //чем меньше сумма и произведение мест, тем выше сила
            }
        }
    }
}