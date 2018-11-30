using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticPartial {
    class Program {
        static void Main(string[] args) {
            DataBase dataBase = new DataBase();
            Student loveCarmen = new Student("Stefan", "Fechet", "325CC");
            dataBase.addStudent(new Student("Florin", "Mihalache", "336CB"));
            dataBase.addStudent(new Student("Andrada", "Hristu", "335CA"));
            dataBase.addStudent(new Student("Razvan", "Drumesi", "325CC"));
            dataBase.addStudent(new Student("Mihai", "Zanfir", "325CC"));
            dataBase.addStudent(loveCarmen);

            List<Student> list = dataBase.getStudents();
            foreach (Student std in list)
                Console.WriteLine(std);

            List<string> list2 = dataBase.getUsers();
            foreach (string std in list2)
                Console.WriteLine(std);

            //dataBase.removeStudent(dataBase.getUsernameStudent(loveCarmen));
            dataBase.removeStudent(loveCarmen);
            list = dataBase.getStudents();
            foreach (Student std in list)
                Console.WriteLine(std);

            dataBase.addStudent(new Student("Florin", "Mihalache", "323CC"));
            list2 = dataBase.getUsers();
            foreach (string std in list2)
                Console.WriteLine(std);
        }
    }
}
