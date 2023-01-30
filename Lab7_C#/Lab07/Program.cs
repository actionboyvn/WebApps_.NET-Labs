using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Lab07
{
    public class Department
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public Department(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public override string ToString()
        {
            return $"{Id,2}), {Name,11}";
        }

    }

    public enum Gender
    {
        Female,
        Male
    }

    public class StudentWithTopics
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public bool Active { get; set; }
        public int DepartmentId { get; set; }

        public List<string> Topics { get; set; }
        public StudentWithTopics(int id, int index, string name, Gender gender, bool active,
            int departmentId, List<string> topics)
        {
            this.Id = id;
            this.Index = index;
            this.Name = name;
            this.Gender = gender;
            this.Active = active;
            this.DepartmentId = departmentId;
            this.Topics = topics;
        }

        public override string ToString()
        {
            var result = $"{Id,2}) {Index,5}, {Name,11}, {Gender,6},{(Active ? "active" : "no active"),9},{DepartmentId,2}, topics: ";
            foreach (var str in Topics)
                result += str + ", ";
            return result;
        }
    }
    public class Topic
    {
        public int Id { get; set; }
        public string TopicName { get; set; }
        public Topic(int id, string topicName)
        {
            this.Id = id;
            this.TopicName = topicName;
        }
    }
    public class Student
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public bool Active { get; set; }
        public int DepartmentId { get; set; }

        public List<int> Topics { get; set; }
        public Student(int id, int index, string name, Gender gender, bool active,
            int departmentId, List<int> topics)
        {
            this.Id = id;
            this.Index = index;
            this.Name = name;
            this.Gender = gender;
            this.Active = active;
            this.DepartmentId = departmentId;
            this.Topics = topics;
        }
        public override string ToString()
        {
            var result = $"{Id,2}) {Index,5}, {Name,11}, {Gender,6},{(Active ? "active" : "no active"),9},{DepartmentId,2}, topics: ";
            foreach (var id in Topics)
                result += id + ", ";
            return result;
        }
    }
    public static class Generator
    {       
        public static List<StudentWithTopics> GenerateStudentsWithTopicsEasy()
        {
            return new List<StudentWithTopics>() {
            new StudentWithTopics(1,12345,"Nowak", Gender.Female,true,1,
                    new List<string>{"C#","PHP","algorithms"}),
            new StudentWithTopics(2, 13235, "Kowalski", Gender.Male, true,1,
                    new List<string>{"C#","C++","fuzzy logic"}),
            new StudentWithTopics(3, 13444, "Schmidt", Gender.Male, false,2,
                    new List<string>{"Basic","Java"}),
            new StudentWithTopics(4, 14000, "Newman", Gender.Female, false,3,
                    new List<string>{"JavaScript","neural networks"}),
            new StudentWithTopics(5, 14001, "Bandingo", Gender.Male, true,3,
                    new List<string>{"Java","C#"}),
            new StudentWithTopics(6, 14100, "Miniwiliger", Gender.Male, true,2,
                    new List<string>{"algorithms","web programming"}),
            new StudentWithTopics(11,22345,"Nowak", Gender.Female,true,2,
                    new List<string>{"C#","PHP","algorithms"}),
            new StudentWithTopics(12, 23235, "Nowak", Gender.Male, true,1,
                    new List<string>{"C#","C++","fuzzy logic"}),
            new StudentWithTopics(13, 23444, "Schmidt", Gender.Male, false,1,
                    new List<string>{"Basic","Java"}),
            new StudentWithTopics(14, 24000, "Newman", Gender.Female, false,1,
                    new List<string>{"JavaScript","neural networks"}),
            new StudentWithTopics(15, 24001, "Bandingo", Gender.Male, true,3,
                    new List<string>{"Java","C#"}),
            new StudentWithTopics(16, 24100, "Bandingo", Gender.Male, true,2,
                    new List<string>{"algorithms","web programming"}),
            };
        }

        public static List<Topic> GenerateTopics()
        {
            return new List<Topic>()
            {
                new Topic(1, "PHP"),
                new Topic(2, "C++"),
                new Topic(3, "fuzzy logic"),
                new Topic(4, "Basic"),
                new Topic(5, "JavaScript"),
                new Topic(6, "neural networks"),
                new Topic(7, "web programming"),
                new Topic(8, "algorithms"),
                new Topic(9, "Java"),
                new Topic(10, "C#"),
            };
        }        
    }
    class Program
    {
        public static void Zad1(int n)
        {
            List<StudentWithTopics> S = Generator.GenerateStudentsWithTopicsEasy();
            var resStud = from s in S
                          group s by (S.FindIndex(x => x.Id == s.Id) / n) into sGroup
                          select new
                          {
                              Students = sGroup.OrderBy(s => (s.Name, s.Index))
                          };
                                      
            foreach (var group in resStud)
            {                
                group.Students.ToList().ForEach(s => Console.WriteLine("  " + s));
                Console.WriteLine();
            }
        }
        public static void Zad2()
        {
            List<StudentWithTopics> S = Generator.GenerateStudentsWithTopicsEasy();
            Console.WriteLine("//////////////////MALE + FEMALE/////////////////");           
            var resTopics = from s in S
                            from t in s.Topics
                            group t by t into tGroup
                            orderby tGroup.Count() descending
                            select new
                            {
                                tGroup
                            };
            
            foreach (var group in resTopics)
            {
                Console.WriteLine($"{group.tGroup.ToList()[0]} {group.tGroup.ToList().Count()}");
            }

            //Zad2b
            Console.WriteLine("//////////////////MALE/////////////////");                        
            var resTopicsMale = from s in S.FindAll(x => x.Gender == Gender.Male)
                                from t in s.Topics
                                group t by t into tGroup
                                orderby tGroup.Count() descending
                                select new
                                {
                                    tGroup
                                };
            foreach (var group in resTopicsMale)
            {              
                Console.WriteLine($"{group.tGroup.ToList()[0]} {group.tGroup.ToList().Count()}");
            }
            Console.WriteLine("///////////////////FEMALE////////////////");           
            var resTopicsFemale = from s in S.FindAll(x => x.Gender == Gender.Female)
                                from t in s.Topics
                                group t by t into tGroup
                                orderby tGroup.Count() descending
                                  select new
                                {
                                    tGroup
                                };
            foreach (var group in resTopicsFemale)
            {                
                Console.WriteLine($"{group.tGroup.ToList()[0]} {group.tGroup.ToList().Count()}");
            }
            
        }
        public static void Zad3()
        {
            List<StudentWithTopics> S = Generator.GenerateStudentsWithTopicsEasy();
            List<Topic> T = Generator.GenerateTopics();
            
            var transformedList = from s in S
                                  from t in T
                                  where s.Topics.Exists(x => x == t.TopicName)
                                  group s by new { s, t } into sGroup
                                  select new
                                  {
                                      Stud = sGroup.Key.s,
                                      Tp = sGroup.Key.t
                                  };
            List<Student> newList = new List<Student>();            
            foreach (var s in transformedList)
            {
                if (newList.Count == 0 || newList.Last().Id != s.Stud.Id)
                {
                    newList.Add(new Student(s.Stud.Id, s.Stud.Index, s.Stud.Name, s.Stud.Gender, s.Stud.Active, s.Stud.DepartmentId, new List<int> { s.Tp.Id }));
                }
                else
                    newList.Last().Topics.Add(s.Tp.Id);                                
            }
            /*
            List<Student> newList = S.Select(s => new Student(s.Id,
                s.Index, s.Name, s.Gender, s.Active, s.DepartmentId,
                T.Select(t => t.Id).ToList()));
            */
            foreach (var s in newList)
            {
                Console.WriteLine(s.ToString());
            }

        }
        public static void Zad4()
        {                        
            Object x = "ABABABABABBBBBBBB";
            MethodInfo methodInfo = x.GetType().GetMethod("Replace", new Type[] { typeof(char), typeof(char) });
            string res = (string)methodInfo.Invoke(x, new object[] { 'B', 'A' });
            Console.WriteLine(res);            
        }
        static void Main(string[] args)
        {
            //Zad1(4);
            //Zad2();
            //Zad3();
            Zad4();

        }
    }
}
