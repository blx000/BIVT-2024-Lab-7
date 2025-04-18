﻿using System;
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
            public Sportsman[] Sportsmen
            {
                get
                {
                    if (_sportsmen == null)
                    {
                        return null;
                    }
                    return _sportsmen;
                }
            }
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
                        if (_sportsmen[i] == null)
                        {
                            continue;
                        }
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
                    if (_sportsmen == null)
                    {
                        return 18;
                    }
                    int w = 18;
                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        if (_sportsmen[i] == null)
                        {
                            continue;
                        }
                        if (_sportsmen[i].Place < w && _sportsmen[i].Place > 0)
                        {
                            w = _sportsmen[i].Place;
                        }
                    }
                    return w;
                }
            }

            public Team(string name)
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
                _sportsmen[_cnt++] = sportsman;
            }

            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null || _cnt >= 6)
                {
                    return;
                }
                foreach (var sportsman in sportsmen)
                {
                    Add(sportsman);
                }
            }

            public static void Sort(Team[] teams)
            {
                for (int i = 0; i < teams.Length; i++)
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
                double mx = double.MinValue;
                Team champ = null;
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
                if (Sportsmen == null || Sportsmen.Length == 0) return 0;
                double sumPlaces = 0;
                int cnt = 0;
                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman != null && sportsman.Place > 0)
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
                if (Sportsmen == null || Sportsmen.Length == 0)
                {
                    return 0;
                }

                double sumPlaces = 0;
                double Places = 1;
                double cnt = 0;

                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman.Place > 0 && sportsman != null)
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