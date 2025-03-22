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
                if (team as ManTeam != null)
                {
                    if (_manNum >= 12)
                    {
                        return;
                    }
                    if (_manTeams == null) return;
                    _manTeams[_manNum++] = team;
                }
                else if (team as WomanTeam != null)
                {
                    if (_womanNum >= 12)
                    {
                        return;
                    }
                    if (_womanTeams == null) return;
                    _womanTeams[_womanNum++] = team;
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
            public void Sort()
            {
                SortTeams(_manTeams, _manNum);
                SortTeams(_womanTeams, _womanNum);
            }
            private void SortTeams(Team[] teams, int count)
            {
                if (teams == null || count == 0) return;

                for (int i = 0; i < count; i++)
                {
                    for (int j = 0; j < count - i - 1; j++)
                    {
                        //проверяем, что элементы не null
                        if (teams[j] == null || teams[j + 1] == null) continue;

                        if (teams[j].TotalScore < teams[j + 1].TotalScore)
                        {
                            (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                        }
                    }
                }
            }
            public static Group Merge(Group group1, Group group2, int size)
            {
                Group group = new Group("Финалисты");

                //слияние мужских команд
                MergeTeams(group1.ManTeams, group2.ManTeams, group._manTeams, size / 2);

                //слияние женских команд
                MergeTeams(group1.WomanTeams, group2.WomanTeams, group._womanTeams, size / 2);

                return group;
            }
            public static void MergeTeams(Team[] teams1, Team[] teams2, Team[] mergedTeams, int size)
            {
                int i = 0, j = 0, k = 0;

                //обрабатываем только не-null элементы
                while (i < teams1.Length && j < teams2.Length && k < size)
                {
                    //пропускаем null элементы
                    while (i < teams1.Length && teams1[i] == null) i++;
                    while (j < teams2.Length && teams2[j] == null) j++;

                    //если один из массивов закончился, выходим
                    if (i >= teams1.Length || j >= teams2.Length) break;

                    //сравниваем очки и добавляем в mergedTeams
                    if (teams1[i].TotalScore >= teams2[j].TotalScore)
                    {
                        mergedTeams[k++] = teams1[i++];
                    }
                    else
                    {
                        mergedTeams[k++] = teams2[j++];
                    }
                }

                //добавляем оставшиеся элементы из teams1
                while (i < teams1.Length && k < size)
                {
                    if (teams1[i] != null)
                    {
                        mergedTeams[k++] = teams1[i];
                    }
                    i++;
                }

                //добавляем оставшиеся элементы из teams2
                while (j < teams2.Length && k < size)
                {
                    if (teams2[j] != null)
                    {
                        mergedTeams[k++] = teams2[j];
                    }
                    j++;
                }
            }
            public void Print()
            {
                Console.WriteLine($"{_name} ");
                foreach (Team team in _manTeams)
                {
                    if (team != null) 
                    {
                        team.Print();
                    }
                }
                Console.WriteLine();
                foreach (Team team in _womanTeams)
                {
                    if (team != null) 
                    {
                        team.Print();
                    }
                }
                Console.WriteLine();
            }
        }
    }
}