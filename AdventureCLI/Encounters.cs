using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureCLI
{
    public class Encounters
    {
        static Random rand = new Random();

        //Encounter Generic


        //Encounters
        public static void FirstEncounter()
        {
            Program.Print("You throw open the door and grab a rusty metal sword while chargin toward your captor");
            Program.Print("He turns...");
            Console.ReadKey();
            Combat(false, "Human Rouge", 1, 5);

        }

        public static void BasicFightEncounter()
        {
            Console.Clear();
            Program.Print("You turn the corner and there you see a hulking beast...");
            Console.ReadKey();
            Combat(true, "", 0, 0);
        }
        public static void WizardEncounter()
        {
            Console.Clear();
            Program.Print("The door slowly creaks open as you peer into the dark room. You see a tall man with a ");
            Program.Print("long beard looking at a large tome.");
            Console.ReadKey();
            Combat(false, "Dark Wizard", 4, 2);
        }


        //Encounter Tools

        public static void RandomEncounter()
        {
            switch (rand.Next(0, 2))
            {
                case 0:
                    BasicFightEncounter();
                    break;
                case 1:
                    WizardEncounter();
                    break;
            }
        }

        public static void Combat(bool random, string name, int power, int health)
        {
            string n = "";
            int p = 0;
            int h = 0;

            if (random)
            {
                n = GetName();
                p = Program.currentPlayer.GetPower();
                h = Program.currentPlayer.GetHealth();
            }
            else
            {
                n = name;
                p = power;
                h = health;

            }
            while (h > 0)
            {
                Console.Clear();
                Console.WriteLine(n);
                Console.WriteLine(p + "/" + h);
                Console.WriteLine("=====================");
                Console.WriteLine("| (A)ttack (D)efend |");
                Console.WriteLine("|   (R)un   (H)eal  |");
                Console.WriteLine("=====================");
                Console.WriteLine(" Potions: " + Program.currentPlayer.potion + "  Health:  " + Program.currentPlayer.health);
                string input = Console.ReadLine();
                if (input.ToLower() == "a" || input.ToLower() == "attack")
                {
                    //Attack
                    Console.WriteLine("With haste you surge forth, your sword flying in your hands! as you pass the " + n + " strikes you.");
                    int damage = p - Program.currentPlayer.armorValue;
                    if (damage < 0)
                        damage = 0;
                    int attack = rand.Next(0, Program.currentPlayer.weaponValue) + rand.Next(1, 4) + ((Program.currentPlayer.currentClass == Player.PlayerClass.Warrior)?2:0);

                    Program.Print("You lose " + damage + " health and deal " + attack + " damage");
                    Program.currentPlayer.health -= damage;
                    h -= attack;
                }
                else if (input.ToLower() == "d" || input.ToLower() == "defend")
                {
                    //Defend
                    Program.Print("As the " + n + " prepares to strike, you ready your sword in a defensive stance");
                    int damage = (p / 4) - Program.currentPlayer.armorValue;
                    if (damage < 0)
                        damage = 0;
                    int attack = rand.Next(0, Program.currentPlayer.weaponValue) / 2;

                    Program.Print("You lose " + damage + " health and deal " + attack + " damage");
                    Program.currentPlayer.health -= damage;
                    h -= attack;
                }
                else if (input.ToLower() == "r" || input.ToLower() == "run")
                {
                    //Run
                    if (Program.currentPlayer.currentClass != Player.PlayerClass.Archer && rand.Next(0, 2) == 0)
                    {
                        Program.Print("As you sprint aways from the " + n + " its strike catches you in the back, sending you sprawling until the ground");
                        int damage = p - Program.currentPlayer.armorValue;
                        if (damage < 0)
                            damage = 0;
                        Program.Print("You lose " + damage + " health and are unable to scape");
                        Program.currentPlayer.health -= damage;
                        Console.ReadKey();
                    }
                    else
                    {
                        Program.Print("You use your crazy ninja moves to evade the " + n + " and you succesfully escape!");
                        Console.ReadKey();
                        Shop.LoadShop(Program.currentPlayer);
                    }
                }
                else if (input.ToLower() == "h" || input.ToLower() == "heal")
                {
                    //Heal
                    if (Program.currentPlayer.potion == 0)
                    {
                        Program.Print("As you desperatly grasp for a potion in your bag, all that you feel are empty glass flask");
                        int damage = p - Program.currentPlayer.armorValue; ;
                        if (damage < 0)
                            damage = 0;
                        Program.Print("The " + n + " strikes you with a mighty blow and you lose " + damage + " health!");
                    }
                    else
                    {
                        Program.currentPlayer.potion -= 1;
                        Program.Print("You reach into your bag and pull out a glowing, purple flask. You take a long drink.");
                        int potionValue = 5 + ((Program.currentPlayer.currentClass == Player.PlayerClass.Mage) ? +4 : 0);
                        Program.Print("You gain " + potionValue + " health");
                        Program.currentPlayer.health += potionValue;
                    }
                    if(Program.currentPlayer.health <= 0)
                    {
                        //Death Code
                        Program.Print("As the " + n + " stands tall and comes down to strike. You have been slayn by the mighty " + n);
                        Console.ReadKey();
                        System.Environment.Exit(0);
                    }
                    Console.ReadKey();
                }
                Console.ReadKey();
            }
            int c = Program.currentPlayer.GetCoins();
            int x = Program.currentPlayer.GetXP();
            Console.WriteLine("As you stand victorious over the " + n + " ,its body dissolves into " + c + " gold coins! You have gained"+x+"XP!");
            Program.currentPlayer.coins += c;
            Program.currentPlayer.xp += x;

            if(Program.currentPlayer.CanLevelUp())
                Program.currentPlayer.LevelUp();

            Console.ReadKey();
        }

        public static string GetName()
        {
            switch (rand.Next(0, 4))
            {
                case 0:
                    return "Skeleton";
                case 1:
                    return "Slime";
                case 2: 
                    return "Zombie";
                case 3:
                    return "Human Cultist";
            }
            return "Human";
        }
    }
}
