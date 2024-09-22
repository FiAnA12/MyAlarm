using System;
using System.Timers; 

class Program
{
    static DateTime alarmTime;
    static System.Timers.Timer tickTimer;
    static System.Timers.Timer alarmTimer;

    public delegate void TickHandler();
    public delegate void AlarmHandler();

    public static event TickHandler OnTick;
    public static event AlarmHandler OnAlarm;

    static void Main(string[] args)
    {
        Console.Write("Enter alarm time in format HH:MM AM/PM: ");
        string userInput = Console.ReadLine();

        // datetime input from user
        while (!DateTime.TryParse(userInput, out alarmTime))
        {
            Console.WriteLine("Invalid input. Please enter a valid time (HH:MM AM/PM).");
            userInput = Console.ReadLine();
        }

        // Events subscription
        OnTick += HandleTick;
        OnAlarm += HandleAlarm;

        // tick timer/5 seconds
        tickTimer = new System.Timers.Timer(5000); 
        tickTimer.Elapsed += (sender, e) => OnTick?.Invoke();
        tickTimer.AutoReset = true; 
        tickTimer.Enabled = true;  

        // alarm timer/1 minute
        alarmTimer = new System.Timers.Timer(60000); 
        alarmTimer.Elapsed += (sender, e) => OnAlarm?.Invoke();
        alarmTimer.AutoReset = true;  
        alarmTimer.Enabled = true;   

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    static void HandleTick()
    {
        Console.WriteLine($"Clock Ticked at {DateTime.Now.ToString("hh:mm:ss tt")}");
    }

    static void HandleAlarm()
    {
        if (DateTime.Now > alarmTime)
        {
            Console.WriteLine("Alarm! Time is up Fadji!");
        }
       
        else
        {
            Console.WriteLine("No Alarm. Waiting...");
        }
    }
}
