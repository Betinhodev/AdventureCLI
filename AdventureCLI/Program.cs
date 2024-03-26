using System.Media;
using System.Text.Json;


namespace AdventureCLI
{
    public class Program
    {
        public static Player currentPlayer = new Player();

        public static bool mainLoop = true;

        static void Main(string[] args)
        {
            if (!Directory.Exists("saves"))
            {
                Directory.CreateDirectory("saves");
            }
            currentPlayer = Load(out bool newP);
            if (newP)
                Encounters.FirstEncounter();
            while(mainLoop)
            {
                Encounters.RandomEncounter();
            }
        }

        static Player NewStart(int i)
        {
            Console.Clear();
            Player p = new Player();
            Print("Adventure CLI!");
            Print("Name:");
            p.name = Console.ReadLine();
            Console.WriteLine("Class: (M)age  (A)rcher  (W)arrior");
            bool flag = false;
            while (flag == false)
            {
                flag = true;
                string input = Console.ReadLine().ToLower();
                if(input == "m" || input == "mage")
                    p.currentClass = Player.PlayerClass.Mage;
                else if(input == "a" || input == "archer")
                    p.currentClass = Player.PlayerClass.Archer;
                else if(input == "w" || input == "warrior")
                    p.currentClass = Player.PlayerClass.Warrior;
                else
                {
                    Console.WriteLine("Please choose a existing class!");
                    flag = false;
                }
                Console.Clear();
            }
            p.id = i;
            Console.WriteLine("You awake in a cold, stone, dark room. You feel dazed and are having trouble remembering");
            Console.WriteLine("anything about your past.");
            if(p.name == "")
                Console.WriteLine("You can't even remember your own name...");
            else
                Console.WriteLine("You know your name is " + p.name);
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("You grope around in the darkness until you find a door handle. You feel some resistance as");
            Console.WriteLine("you turn the handle, but the rusty lock breaks with little effort. You see your captor");
            Console.WriteLine("standing with his back to you outside the door.");
            return p;
        }

        public static void Quit()
        {
            Save();
            Environment.Exit(0);
        }

        public static void Save()
        {
            string path = $"saves/" + currentPlayer.id.ToString() + ".json";
            string jsonString = JsonSerializer.Serialize(currentPlayer);
            File.WriteAllText(path, jsonString);
        }

        public static Player Load(out bool newP)
        {
            newP = false;
            Console.Clear();
            string[] filePath = Directory.GetFiles("saves", "*.json");
            List<Player> players = new List<Player>();

            int idCount = 0;

            foreach (string p in filePath)
            {
                string jsonString = File.ReadAllText(p);
                Player player = JsonSerializer.Deserialize<Player>(jsonString);
                players.Add(player);
            }

            idCount = players.Count;

            while (true)
            {
                Console.Clear();
                Print("Choose your player:");

                foreach (Player p in players)
                {
                    Print(p.id + ": " + p.name);
                }

                Print("Please input player name or id (id:# or playername). Additionally,'create' will start new save!");
                string[] data = Console.ReadLine().Split(":");

                try
                {
                    if (data[0] == "id")
                    {
                        if (int.TryParse(data[1], out int id))
                        {
                            foreach (Player p in players)
                            {
                                if (p.id == id)
                                {
                                    return p;
                                }
                            }
                            Console.WriteLine("There is no player with that id");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Your id needs to be a number! Press any key to continue!");
                            Console.ReadKey();
                        }
                    }
                    else if (data[0] == "create")
                    {
                        Player newPlayer = NewStart(idCount);
                        newP = true;
                        return newPlayer;
                    }
                    else
                    {
                        foreach (Player player in players)
                        {
                            if (player.name == data[0])
                            {
                                return player;
                            }
                        }
                        Console.WriteLine("Your id needs to be a number! Press any key to continue!");
                        Console.ReadKey();
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Your id needs to be a number! Press any key to continue!");
                    Console.ReadKey();
                }
            }
        }
            public static void Print(string text, int speed = 40)
            {
                //SoundPlayer soundPlayer = new SoundPlayer("sounds/type.mp3");
                //soundPlayer.PlayLooping();
                foreach (char c in text)
                {
                    Console.Write(c);
                    System.Threading.Thread.Sleep(speed);
                }
                //soundPlayer.Stop();
                Console.WriteLine();
            }

            public static void ProgressBar()
            {
                
            }
    }
}