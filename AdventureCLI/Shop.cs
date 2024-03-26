using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureCLI
{
    public class Shop
    {
        public static void LoadShop(Player p)
        {
            RunShop(p);
        }

        public static void RunShop(Player p)
        {
            int potionPrice;
            int armorPrice;
            int weaponPrice;
            int difPrice;

            while(true)
            {
                potionPrice = 20 + 5 * p.mods;
                armorPrice = 50 * (p.armorValue + 1);
                weaponPrice = 50 * p.weaponValue;
                difPrice = 150 * 50 * p.mods;

                Console.Clear();
                Console.WriteLine("          Shop          ");
                Console.WriteLine("========================");
                Console.WriteLine("(W)eapon:         $" + weaponPrice);
                Console.WriteLine("(A)rmor:          $" + armorPrice);
                Console.WriteLine("(P)otions:        $" + potionPrice);
                Console.WriteLine("(D)ifficulty Mod: $" + difPrice);
                Console.WriteLine("(B)ack");
                Console.WriteLine("(Q)uit");
                Console.WriteLine("========================");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(p.name + "'s Stats");
                Console.WriteLine("========================");
                Console.WriteLine("Current Health: " + p.health);
                Console.WriteLine("Coins: " + p.coins);
                Console.WriteLine("Weapon Strength: " + p.weaponValue);
                Console.WriteLine("Armor Defense: " + p.armorValue);
                Console.WriteLine("Potions: " + p.potion);
                Console.WriteLine("Difficulty Mods: " + p.mods);
                Console.WriteLine("========================");


                //Wait for input
                string input = Console.ReadLine().ToLower();
                if (input == "p" || input == "potion")
                {
                    TryBuy("potion", potionPrice, p);
                }
                else if (input == "w" || input == "weapon")
                {
                    TryBuy("weapon", weaponPrice, p);
                }
                else if (input == "a" || input == "armor")
                {
                    TryBuy("armor", armorPrice, p);
                }
                else if (input == "d" || input == "difficulty mod")
                {
                    TryBuy("difficulty", difPrice, p);
                }
                else if(input == "q" || input == "quit")
                {
                    Program.Quit();
                }
                else if (input == "b" || input == "back")
                    break;
            }
        }
        static void TryBuy(string item, int cost, Player p)
        {
            if(p.coins >= cost)
            {
                if (item == "potion")
                    p.potion++;
                else if (item == "weapon")
                    p.weaponValue++;
                else if (item == "armor")
                    p.armorValue++;
                else if (item == "difficulty")
                    p.mods++;

                p.coins -= cost;
            }
            else
            {
                Program.Print("Ya don't have enough gold!");
                Console.ReadKey();
            }
        }
        
        
    }
}
