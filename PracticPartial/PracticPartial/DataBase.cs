using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticPartial {
    class Student : IComparable<Student> {
        private string name, surname;
        private string grupa;

        public Student (string name, string surname, string grupa) {
            this.name = name;
            this.surname = surname;
            this.grupa = grupa;
        }

        public int CompareTo(Student other) {
            if (grupa.Equals(other.grupa)) {
                if (name.Equals(other.name))
                    return surname.CompareTo(other.surname);
                return name.CompareTo(other.name);
            }
            return grupa.CompareTo(other.grupa);
        }

        public override string ToString() {
            return grupa + " " + name + " " + surname;
        }

        public string getNume() {
            return surname;
        }

        public string getPrenume() {
            return name;
        }

        public string getGrupa() {
            return grupa;
        }

        public bool Equals (Student std) {
            return std.getNume().Equals(getNume()) && std.getPrenume().Equals(getPrenume()) && std.getGrupa().Equals(getGrupa());
        }
    }

    class DataBase {
        Dictionary<string, Student> dataBase;

        public DataBase() {
            dataBase = new Dictionary<string, Student>();
        }

        // bonus - custom username, daca exista un student cu acelasi nume, dar de la grupa diferita
        public void addStudent(Student std) {
            string username = std.getPrenume().ToLower() + "." + std.getNume().ToLower();
            if (dataBase.ContainsKey(username)) {
                //return;
                // bonus
                foreach (KeyValuePair<string, Student> pair in dataBase) {
                    if (pair.Key.Equals(username)) {
                        Student value = pair.Value;
                        if (!value.getGrupa().Equals(std.getGrupa())
                            && value.getNume().Equals(std.getNume())
                            && value.getPrenume().Equals(std.getPrenume())) {
                            username += "_" + std.getGrupa();
                            dataBase[username] = std;
                            break;
                        }
                    }
                }
            } else {
                dataBase[username] = std;
            }
        }

        public void removeStudent(string user) {
            dataBase.Remove(user);
        }

        // bonus - remove dupa valoare
        public void removeStudent(Student std) {
            //dataBase.Remove()
            string key = null;
            foreach (KeyValuePair<string, Student> pair in dataBase) {
                if (pair.Value.Equals(std)) {
                    key = pair.Key;
                }
            }
            if (key != null)
                dataBase.Remove(key);
        }

        public List<Student> getStudents() {
            List<Student> students = new List<Student>();
            foreach(KeyValuePair<string, Student> pair in dataBase) {
                students.Add(pair.Value);
            }
            students.Sort();
            return students;
        }

        public List<string> getUsers() {
            List<string> students = new List<string>();
            foreach (KeyValuePair<string, Student> pair in dataBase) {
                students.Add(pair.Key);
            }
            students.Sort();
            return students;
        }

        public string getUsernameStudent(Student std) {
            foreach (KeyValuePair<string, Student> pair in dataBase) {
                if (dataBase[pair.Key].Equals(std))
                    return pair.Key;
            }
            return null;
        }
    }
}
