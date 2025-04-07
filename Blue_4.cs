using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_4
    {
        public abstract class Team
        {
            private string _name;
            private int[] _scores;
            public string Name => _name;
            public int[] Scores
            {
                get
                {
                    if (_scores == null)
                    {
                        return null;
                    }
                    int[] cp_scores = new int[_scores.Length];
                    Array.Copy(_scores, cp_scores, cp_scores.Length);
                    return cp_scores;
                }
            }
            public int TotalScore
            {
                get
                {
                    if (_scores == null)
                    {
                        return 0;
                    }
                    int cnt = 0;
                    for (int i = 0; i < _scores.Length; i++)
                    {
                        cnt += _scores[i];
                    }
                    return cnt;
                }
            }
            public Team(string name)
            {
                _name = name;
                _scores = new int[0];
            }

            public void PlayMatch(int result)
            {
                if (_scores == null)
                {
                    return;
                }
                Array.Resize(ref _scores, _scores.Length + 1);
                _scores[_scores.Length - 1] = result;
            }
            public void Print()
            {
                Console.WriteLine($"{Name} {TotalScore}");
            }
        }
        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }
        }
        public class Group
        {
            private string _name;
            private int _manNum;
            private int _womanNum;
            private Team[] _manTeams;
            private Team[] _womanTeams;


            public string Name => _name;
            public Team[] ManTeams => _manTeams;
            public Team[] WomanTeams => _womanTeams;

            public Group(string name)
            {
                _name = name;
                _manTeams = new ManTeam[12];
                _womanTeams = new WomanTeam[12];
                _womanNum = 0;
                _manNum = 0;
            }
            public void Add(Team team)
            {
                if (team is ManTeam mt)
                {
                    if (_manNum >= 12 || _manTeams == null)
                    {
                        return;
                    }
                    _manTeams[_manNum++] = mt;
                }
                else if (team is WomanTeam wt)
                {
                    if (_womanNum >= 12 || _womanTeams == null)
                    {
                        return;
                    }
                    _womanTeams[_womanNum++] = wt;
                }
            }

            public void Add(Team[] teams)
            {
                if (teams == null) return;

                foreach (var team in teams)
                {
                    Add(team);
                }
            }
            private void SortTeams(Team[] teams)
            {
                if (teams == null) return;

                for (int i = 0; i < teams.Length; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {                      
                        if (teams[j].TotalScore < teams[j + 1].TotalScore)
                        {
                            (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                        }
                    }
                }
            }
            public void Sort()
            {
                SortTeams(_manTeams);
                SortTeams(_womanTeams);
            }
            public static void MergeTeams(Team[] teams1, Team[] teams2, Team[] teams, int size)
            {
                int i = 0, j = 0, k = 0;
                while (i < teams1.Length && j < teams2.Length && i < size && j < size)
                {
                    if (teams1[i].TotalScore >= teams2[j].TotalScore)
                    {
                        teams[k++] = teams1[i++];
                    }
                    else
                    {
                        teams[k++] = teams2[j++];
                    }
                }

                while (i < teams1.Length && i < size)
                {
                    teams[k++] = teams1[i++];
                }
                while (j < teams2.Length && j < size)
                {
                    teams[k++] = teams1[j++];
                }
            }
            public static Group Merge(Group group1, Group group2, int size)
            {
                Group group = new Group("Финалисты");

                MergeTeams(group1.ManTeams, group2.ManTeams, group._manTeams, size / 2);
                MergeTeams(group1.WomanTeams, group2.WomanTeams, group._womanTeams, size / 2);

                return group;
            }
            public void Print()
            {
                Console.WriteLine($"{_name} ");
                foreach (Team team in _manTeams)
                {
                    team.Print();
                }
                Console.WriteLine();
                foreach (Team team in _womanTeams)
                {
                    team.Print();
                }
                Console.WriteLine();
            }
        }
    }
}