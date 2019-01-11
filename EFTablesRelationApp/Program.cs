using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EFTablesRelationApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (FacultyContext db = new FacultyContext())
            {      
                Faculty energyFac = new Faculty { Name = "Энергетический" };
                Faculty economFac = new Faculty { Name = "Экономический" };
                Faculty historyFac = new Faculty { Name = "Истории" };
                db.Faculties.AddRange(new List<Faculty> { energyFac, economFac, historyFac });
                db.SaveChanges();

                Group sep172 = new Group { Name = "SEP172", Faculty = energyFac };
                Group sep272 = new Group { Name = "SEP272", Faculty = energyFac };
                Group sep372 = new Group { Name = "SEP372", Faculty = energyFac };
                Group ep101 = new Group { Name = "EP101", Faculty = economFac };
                Group ep201 = new Group { Name = "EP201", Faculty = economFac };
                Group his103 = new Group { Name = "HIS103", Faculty = historyFac };
                Group his303 = new Group { Name = "HIS303", Faculty = historyFac };
                db.Groups.AddRange(new List<Group> { sep172,sep272,sep372,ep101,ep201,his103,his303});
                db.SaveChanges();

                db.Students.Add(new Student { Fio = "Сабитов Ильяс Маратович", GroupId = sep172.Id });
                db.Students.Add(new Student { Fio = "Кентаев Талгат Жанатович", GroupId = sep172.Id });
                db.Students.Add(new Student { Fio = "Касенов Касым Аманович", GroupId = sep172.Id });
                db.Students.Add(new Student { Fio = "Таукел Кайрат Есболович", GroupId = sep272.Id });
                db.Students.Add(new Student { Fio = "Головкин Генадий Генадьевич", GroupId = sep272.Id });
                db.Students.Add(new Student { Fio = "Пакьяо Мэнни Дапидран", GroupId = ep101.Id });
                db.Students.Add(new Student { Fio = "Ломаченко Василий Олегович", GroupId = ep101.Id });
                db.Students.Add(new Student { Fio = "Порошенко Петр Порошенович", GroupId = ep101.Id });
                db.Students.Add(new Student { Fio = "Майвезер Флойд Майвезерович", GroupId = ep201.Id });
                db.SaveChanges();
               
                while (true)
                {
                    Console.WriteLine("1-Показать студентов");
                    Console.WriteLine("2-Показать группы");
                    Console.WriteLine("3-Показать факультеты");
                    Console.WriteLine("4-Удалить студента");
                    Console.WriteLine("5-Удалить группу");
                    Console.WriteLine("6-Удалить факультет");
                    Console.WriteLine("7-выход");

                    int key;

                    if (int.TryParse(Console.ReadLine(), out key))
                    {
                        switch (key)
                        {
                            case 1: ShowStudents(db.Students); break;
                            case 2: ShowGroups(db.Groups); break;
                            case 3: ShowFaculties(db.Faculties); break;
                            case 4:
                                DeleteStudent(db.Students);
                                db.SaveChanges();
                                break;
                            case 5:
                                DeleteGroup(db.Groups);
                                db.SaveChanges();
                                break;
                            case 6:
                                DeleteFaculty(db.Faculties);
                                db.SaveChanges();
                                break;
                            case 7: return;
                            default:break;
                        }

                    }

                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        static void ShowStudents(DbSet<Student> students)
        {
            foreach (Student s in students.ToList())
            {
                Console.WriteLine("Студент: {0} | Группа: {1} | Факультет: {2}", s.Fio, s.Group == null ?"нет группы": s.Group.Name,
                    s.Group == null ? "нет факультета" : s.Group.Faculty==null?"нет факультета": s.Group.Faculty.Name);
            }
        }

        static void ShowGroups(DbSet<Group> groups)
        {
            foreach (Group g in groups.ToList())
            {
                Console.WriteLine("Группа: {0} | Факультет: {1} | Количество студентов: {2}", g.Name, g.Faculty==null?"нет факультета":g.Faculty.Name, 
                    g.Students.Count);
            }
        }

        static void ShowFaculties(DbSet<Faculty> faculties)
        {
            foreach (Faculty f in faculties.ToList())
            {
                Console.Write("Факультет: {0} | Группы: ",f.Name);
                foreach(Group g in f.Groups)
                {
                    Console.Write("{0}, ", g.Name);
                }
                Console.Write("\b\b ");
                Console.WriteLine();
            }
        }

        static void DeleteStudent(DbSet<Student> students)
        {
            foreach(Student s in students.ToList())
            {
                Console.WriteLine("ID: {0}, ФИО: {1}", s.Id, s.Fio);
            }
            Console.Write("\nВведите ID студента: ");
            int id;

            if (int.TryParse(Console.ReadLine(), out id))
            {
                Student student = students.Find(id);
                if (student != null)
                {
                    students.Remove(student);
                    Console.WriteLine("Студент удален");
                    return;
                }                 
            }

            Console.WriteLine("Студент не найден");
        }

        static void DeleteGroup(DbSet<Group> groups)
        {
            foreach (Group g in groups.ToList())
            {
                Console.WriteLine("ID: {0}, Группа: {1}", g.Id, g.Name);
            }
            Console.Write("\nВведите ID группы: ");
            int id;

            if (int.TryParse(Console.ReadLine(), out id))
            {
                Group group = groups.Find(id);
                if (group != null)
                {
                    groups.Remove(group);
                    Console.WriteLine("Группа удалена");
                    return;
                }
            }

            Console.WriteLine("Группа не найдена");
        }

        static void DeleteFaculty(DbSet<Faculty> faculties)
        {
            foreach (Faculty f in faculties.ToList())
            {
                Console.WriteLine("ID: {0}, Факультет: {1}", f.Id, f.Name);
            }
            Console.Write("\nВведите ID факультета: ");
            int id;

            if (int.TryParse(Console.ReadLine(), out id))
            {
                Faculty faculty = faculties.Find(id);
                if (faculty != null)
                {
                    faculties.Remove(faculty);
                    Console.WriteLine("Факультет удален");
                    return;
                }
            }

            Console.WriteLine("Факультет не найден");
        }
    }
}
