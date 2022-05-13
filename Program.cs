
var pathacc = System.IO.Path.GetFullPath(@"account.txt"); // путь для macOs
var pathtime = System.IO.Path.GetFullPath(@"time.txt");
var pathinfo = System.IO.Path.GetFullPath(@"test.txt");

List<Person> children = new List<Person> { };
string[] infoacc = File.ReadAllLines(pathacc); // считование файла с логином и паролем
for (int i = 0; i < infoacc.Length; i += 2)
{
    children.Add(new Person(infoacc[i], infoacc[i + 1]));
}


Console.Write("Sing in or Sing up: "); // регестрироваться или войти
string choice = Console.ReadLine();

if (choice == "Sing in")
{
    SingIn child = new SingIn();

    int index = -1;

    index = child.Entry(pathtime, index, children);

    child.GetInfo(pathtime, pathinfo, index, children);
}
else if (choice == "Sing up")
{
    Console.Write("Login: "); // регестрация нового пользователя 
    string? hlog = Console.ReadLine();
    while (hlog == null)
    {
        Console.WriteLine("Login must be not null ");
        hlog = Console.ReadLine();
    }

    Console.Write("Password: ");
    string? hpas = Console.ReadLine();
    while (hpas == null)
    {
        Console.WriteLine("Password must be not null ");
        hpas = Console.ReadLine();
    }

    Console.Write("Write information about you: ");
    string? information = Console.ReadLine();

    SingUn child = new SingUn(hlog, hpas);

    child.WriteInfo(pathinfo, pathacc, pathtime, information);

    child.GetInfo(pathinfo, pathtime);
}
else Console.WriteLine("Unknown  command");

class Person
{
    public string Login { get; set; }
    public string Password { get; set; }
    public Person(string login, string password)
    {
        Login = login;
        Password = password;
    }
}

class SingIn
{
    public int Entry(string pathtime, int index, List<Person> children)
    {
        bool checker = false;
        while (checker == false) // проверка ли существует такой аккаунт с паролем
        {
            Console.Write("Login: ");
            string login = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            foreach (Person p in children)
            {
                index++;
                if (p.Login == login && p.Password == password)
                {
                    checker = true;
                    string[] time = File.ReadAllLines(pathtime);
                    time[index * 2] = $"{login} input:{DateTime.Now}";
                    File.WriteAllLines(pathtime, time);
                    break;
                }
            }
            if (checker == false) Console.WriteLine("Wrong login or password");
        }
        return index;
    }

    public void GetInfo(string pathtime, string pathinfo, int index, List<Person> children)
    {
        string action = " "; // выбор дейстий на аккаунте просмотреть личную информацию или выйти с системы
        while (action != "exit")
        {
            Console.Write("What do you want do?(view my information or exit): ");
            action = Console.ReadLine();
            if (action == "view my information")
            {
                string[] info = File.ReadAllLines(pathinfo);
                Console.WriteLine(info[index]);
            }
            else if (action != "exit") Console.WriteLine("Unknown command");
        }
        if (action == "exit")
        {
            string[] time = File.ReadAllLines(pathtime);
            time[index * 2 + 1] = $"{children[index].Login} output:{DateTime.Now}";
            File.WriteAllLines(pathtime, time);
            Console.WriteLine("Finish program");
        }
    }
}

class SingUn
{
    public string login { get; set; }
    public string password { get; set; }

    public SingUn(string login, string password)
    {
        this.login = login;
        this.password = password;
    }

    public void GetInfo(string pathinfo, string pathtime)
    {
        string action = " ";
        while (action != "exit")
        {
            Console.WriteLine("What do you want do?(view my information or exit)");
            action = Console.ReadLine();
            if (action == "view my information")
            {
                string[] info = File.ReadAllLines(pathinfo);
                Console.WriteLine(info[info.Length - 1]);
            }
            else if (action != "exit") Console.WriteLine("Unknown command");
        }
        if (action == "exit")
        {
            File.AppendAllText(pathtime, $"{login} output: {DateTime.Now}");
            File.AppendAllText(pathtime, "\n");
            Console.WriteLine("Finish program");
        }
    }

    public void WriteInfo(string pathinfo, string pathacc, string pathtime, string? information)
    {
        File.AppendAllText(pathacc, login);
        File.AppendAllText(pathacc, "\n");

        File.AppendAllText(pathacc, password);
        File.AppendAllText(pathacc, "\n");

        File.AppendAllText(pathinfo, information);
        File.AppendAllText(pathinfo, "\n");

        File.AppendAllText(pathtime, $"{login} input: {DateTime.Now}");
        File.AppendAllText(pathtime, "\n");
    }

}