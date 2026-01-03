using studentConsoleManager.Services;
using studentConsoleManager.View;

namespace studentConsoleManager;

public class Program
{
    static void Main()
    {
        try
        {
            var service = new StudentService();
            var Menu = new Menu(service);
            Menu.Start();
        }
        catch (Exception ex) { Console.WriteLine("Error While Build : "+ex.ToString()); }
    }

}