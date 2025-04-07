using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_1
    {
        public class Response
        {
            //поля
            private string _name;
            protected int _votes;

            //свойства
            public string Name => _name;
            public int Votes => _votes;

            //конструктор
            public Response(string name)
            {
                _name = name;
                _votes = 0;
            }

            public virtual int CountVotes(Response[] responses)
            {
                if (responses == null || responses.Length == 0)
                {
                    return 0;
                }


                foreach (var response in responses)
                {
                    if (response.Name == this.Name)
                    {
                        _votes++;
                    }
                }
                return _votes;
            }

            public virtual void Print()
            {
                Console.WriteLine($"{_name} {_votes}");
            }
        }

        public class HumanResponse : Response
        {
            //поле
            private string _surname;

            //свойство
            public string Surname => _surname;

            //конструктор
            public HumanResponse(string name, string surname) : base(name)
            {
                _surname = surname;
            }

            public override int CountVotes(Response[] responses)
            {
                if (responses == null || responses.Length == 0)
                {
                    return 0;
                }

                foreach (var response in responses)
                {
                    if (response is HumanResponse humanResponse &&
                        humanResponse.Name == this.Name &&
                        humanResponse.Surname == this.Surname)
                    {
                        _votes++;
                    }
                }
                return _votes;
            }

            public override void Print()
            {
                Console.WriteLine($"{Name} {Surname} {_votes}");
            }
        }
    }
}