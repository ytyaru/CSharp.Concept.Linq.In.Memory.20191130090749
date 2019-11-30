using System;
using System.Collections.Generic;
using System.Linq;

namespace Concept.Linq.Lesson0 {
    class Human {
        public string Name { get; }
        public int Age { get; }
        public Human(string name, int age) => (Name, Age) = (name, age);
    }
    class Main {
        public void Run() {
            TestArrayAndList();
            TestDictionary();
            TestLookup();
        }
        private void TestArrayAndList() {
            var list = new List<int>() { 5, 1, 4, 2, 3 };
            var q = from v in list
                    where 1 == (v % 2)
                    select v;
            Console.WriteLine($"ToArray: {string.Join(",", list.ToArray())}");
            Console.WriteLine($"ToList: {string.Join(",", list.ToList())}");
        }
        private void TestDictionary() {
            List<Human> humans = CreateHumans();
            ShowDictionary(Query(in humans).ToDictionary(h => h.Name));
        }
        private List<Human> CreateHumans() {
            return new List<Human> {
                new Human("A", 0),
                new Human("B", 1),
                new Human("C", 2),
                new Human("D", 3),
                new Human("E", 4),
                new Human("F", 5),
            };
        }
        private IEnumerable<Human> Query(in List<Human> humans) {
            return from h in humans
                   where 1 == (h.Age % 2)
                   select h;
        }
        private void ShowDictionary(in IDictionary<string, Human> dict) {
            foreach (KeyValuePair<string, Human> kv in dict) {
                Console.WriteLine($"Key={kv.Key} Value=Name:{kv.Value.Name}, Age:{kv.Value.Age}");
            }
        }
        private void TestLookup() {
            List<Human> humans = CreateHumans();
            ShowLookup(Query(in humans).ToLookup(h => h.Name));
//            ShowLookup(Query(in humans).ToLookup(h => h.Name, h => $"{{ Name:{h.Name}, Age:{h.Age} }}"));
        }
        private void ShowLookup(in ILookup<string, Human> lookup) {
            foreach (var g in lookup) {
                Console.WriteLine($"Key={g.Key}");
                foreach (Human h in g) {
                    Console.WriteLine($"Name={h.Name}, Age={h.Age}");
                }
            }
            // 以下だと実行時エラーになった。謎System.InvalidCastException: Specified cast is not valid.
                /*
            foreach (ILookup<string, Human> g in lookup) {
//                Console.WriteLine($"lookup={g}");
                Console.WriteLine($"Key={g.Key}");
                foreach (Human h in g) {
                    Console.WriteLine($"Name={h.Name}, Age={h.Age}");
                }
            }
                */
        }
    }
}
