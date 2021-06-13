using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Collections;
using System.Xml;

namespace ConsoleApp3
{
    public class CarEventHandler
    {
        public CarEventHandler(string message)
        {
            Message = message;
          
        }
        public string Message { get; set; }
       
    }
    public abstract class Car
    {
        public delegate void CarHandler(object sender, CarEventHandler c);
        public static event CarHandler notify;
        public void Notify(object sender,CarEventHandler c) {

            notify?.Invoke(sender, c);
        }
        public Random random;
        protected Car(string carName, int speed)
        {
            CarName = carName;
            Speed = speed;
            Distance = 0;
            random = new Random();
        }
        public Car()
        {
            CarName = "";
            Speed = 0;
            Distance = 0;
            random = null;
        }
        public string CarName { get; set; }
        public int Speed { get; set; }
        public int Distance { get; set; }
        abstract public void Drive();
        
        public override string ToString()
        {
            return $"CarName{CarName}  Speed{Speed} Distance {Distance}";
        }
    }
    public class Sportcar : Car
    {
        public Sportcar(string carname,int speed):base(carname,speed)
        {

        }

        public override void Drive()
        {
            Distance += Speed;
            Speed = random.Next(0,100);
            Thread.Sleep(100);
        }
    }
    public class Bus : Car
    {
        public Bus(string carname, int speed) : base(carname, speed)
        {

        }

        public override void Drive()
        {
            Distance += Speed;
            Speed = random.Next(0, 50);
            Thread.Sleep(50);
        }
    }

    public class Cargo : Car
    {
        public Cargo(string carname, int speed) : base(carname, speed)
        {

        }

        public override void Drive()
        {
            Distance += Speed;
            Speed = random.Next(50, 150);
            Thread.Sleep(120);
        }
    }
    class CarRace 
    {
        public CarRace(Car[] cars, int distance)
        {
            Cars = cars;
            Distance = distance;
        }

        public Car [] Cars { get; set; }

        public int Distance { get; set; }
       
        public void DriveCars() {
            int count = 0;
            foreach (Car item in Cars)
            {
                item.Drive();
             
                if (item.Distance>=Distance) {
                    count++;
                    Console.Write($"{count}.place= ");
                    item.Notify(item, new CarEventHandler($"{item.CarName} won with the distance of {item.Distance}/{Distance}"));
                }



            }
        }
       
    }

    class Program
    {
        public static void SendMessage(object sender, CarEventHandler e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            Car.notify += SendMessage;

            Car[] Cars = new Car[3] { new Sportcar("Sport1", 100), new Bus("Bus1", 50), new Cargo("Cargo1", 120)  };
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Welcome...");
            CarRace game = new CarRace(Cars,100);
          
            game.DriveCars();
            
        }
    }
  
}

