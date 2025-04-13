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
            }

            public void SetPlace(int place)
            {
                if (_place != 0) return;
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
            public int Count => _cnt;
            public Sportsman[] Sportsmen
            {
                get
                {
                    if (_sportsmen == null) return null;
                    return _sportsmen;
                }
            }
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
                    if (_sportsmen == null || _sportsmen.Length == 0)
                    {
                        return 0;
                    }
                    int w = 18;
                    foreach (var sportsman in _sportsmen)
                    {
                        if (sportsman.Place < w && sportsman.Place != 0)
                        {
                            w = sportsman.Place;
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
                if (_sportsmen == null || _sportsmen.Length == 0)
                {
                    return;
                }
                if (_cnt < 6)
                {
                    _sportsmen[_cnt] = sportsman;
                    _cnt++;
                }
            }

            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null || sportsmen.Length == 0) return;
                foreach (var sportsman in sportsmen)
                {
                    Add(sportsman);
                }
            }

            public static void Sort(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return;
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

            public static Team GetChampion(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return null;

                Team champ = teams[0];
                double mx = champ.GetTeamStrength();
                foreach (var team in teams)
                {
                    if (team == null) continue;
                    double strength = team.GetTeamStrength();
                    if (strength > mx)
                    {
                        champ = team;
                        mx = strength;
                    }
                }

                return champ;
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
                if (Sportsmen == null || Count == 0) return 0;

                double sumPlaces = 0;
                int cnt = 0;
                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman != null && sportsman.Place != 0)
                    {
                        sumPlaces += sportsman.Place;
                        cnt++;
                    }
                }
                if (cnt == 0) return 0;
                return 100 / (sumPlaces / cnt);
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                if (Sportsmen == null || Count == 0)
                {
                    return 0;
                }
                double sumPlaces = 0;
                double Places = 1;
                double cnt = 0;

                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman.Place != 0 && sportsman != null)
                    {
                        sumPlaces += sportsman.Place;
                        Places *= sportsman.Place;
                        cnt++;
                    }
                }

                if (Places == 0)
                {
                    return 0;
                }

                return 100.0 * ((cnt * sumPlaces) / Places);
            }
        }
    }
}