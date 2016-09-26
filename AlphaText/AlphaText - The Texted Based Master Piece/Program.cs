using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;

namespace AlphaText___The_Texted_Based_Master_Piece
{
    class Program
    {
        public enum Zone { Start, Forest, Desert, UnderWaterCity, Volcano, END }
        private static Zone area;
        event EventHandler AreaChanged;

        public Zone Area
        {
            get { return area; }
            set
            {
                area = value;
                OnAreaChanged();
            }
        }


        static void Main(string[] args)
        {

            #region Enemies
            string[] Zarea = { "Forest", "Desert", "UnderWaterCity", "Volcano", "END" };

            string[] ForestEnemies = { "Rabid Squirrel", "Young Wolf", "Pretty Butterfly", "Adult Wolf", "Ravenous Bear" };
            int[] ForestEnemiesAttack = { 3, 8, 1, 10, 8 };
            int[] ForestEnemiesDefense = { 1, 2, 0, 3, 4 };
            int[] ForestEnemiesHealth = { 10, 15, 5, 20, 25 };
            string ForestBoss = "Armored Turtle";
            int ForestBossAttack = 4;
            int ForestBossDefense = 7;
            int ForestBossHealth = 50;

            string[] DesertEnemies = { "Live Cactus", "Jumping Spider", "Angry Vulture", "Armadillo", "Sand Wolf" };
            int[] DesertEnemiesAttack = { 6, 10, 8, 7, 14 };
            int[] DesertEnemiesDefense = { 6, 2, 4, 10, 7 };
            int[] DesertEnemiesHealth = { 20, 15, 25, 30, 30 };
            string DesertBoss = "SandStorm";
            int DesertBossAttack = 10;

            string[] UnderWaterCityEnemies = { "Spiked BlowFish", "BloodThirsty Whale", "Scooba Wolf", "Young Shark", "Electric eel" };
            string UnderWaterCityBoss = "Angler Fish";

            string[] VolcanoEnemies = { "Fire Wolf", "Ash Covered Ram", "Fire Hawk", "Devil Squirrel", "Hardened Beetle" };
            string VolcanoBoss = "Burning Giant Elephant";

            string[] EndEnemies = { "Void Wolf", "Void Tiger", "Void Raptor", "Void Armored Turtle", "Void Humaniod" };
            string EndBoss = "Void Demon";
            #endregion

            string name;
            string gender;
            string profession;
            int obey = 0;
            int distance = 0;
            string CurrentFoe = "";
            int FoeAttack = 0;
            int FoeDefense = 0;
            int FoeHP = 0;

            #region PlayerStats
            int Money = 10;
            int PlayerHealth = 100;
            int PlayerDefense = 1;
            int PlayerAttack = 1;
            int PlayerMagic = 1;
            int ArmorMP = 0;
            int XP = 1;
            int baseMP = 5;
            int HealthPotionCount = 1;
            int MegaHealthPotionCount = 0;
            int MPPotionCount = 1;
            int MegaMPPotionCount = 0;
            int TotalMP = baseMP + ArmorMP;
            int CurrentMP = TotalMP;
            List<string> Spells = new List<string>();
            #endregion

            area = Zone.Start;



            while (true)
            {
                Console.WriteLine("What is your proffesion?");
                profession = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Good. Now, your gender?");
                gender = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("We are ready to begin");
                KeyCon();
                Console.WriteLine("HEY YOU {0}! COME HERE", profession);
                Thread.Sleep(500);
                Console.WriteLine("Yes you {0}", gender);
                Thread.Sleep(500);
                Console.WriteLine("What is your name?");
                name = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("I'm not one to judge.\n{0}, I have seen a prophecy with your face in it. \nThe blight we have been experiencing has its source there, in the volcano", name);
                KeyCon();
                Console.WriteLine("You will journey out through the Forest into the Desert. \nThe Desert conects to a mountain valley which leads to the ocean. \nIn the ocean you will journey through the Underwater City. \nThe island the Volcano is on will be close. \nYou will travel into the Volcano and stop the source.");
                KeyCon();
                Console.WriteLine("AND NO OBJECTING OR IT WILL BE YOUR END");
                KeyCon();
                Console.WriteLine("Do you object?");
                Console.WriteLine("1. Yes\n2. No");
                while (true)
                {
                    try
                    {
                        obey = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.Clear();
                        Console.WriteLine("Do you object?");
                        Console.WriteLine("1. Yes\n2. No");
                        continue;
                    }
                    if (obey == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Well you called my bluff. Have fun helping no one you worthless sack of trash");
                        KeyCon();
                        Environment.Exit(0);
                    }
                    else if (obey == 2)
                    {
                        Console.Clear();
                        Console.WriteLine("Good, Good.");
                        Thread.Sleep(500);
                        Console.WriteLine("You should be on your way then");
                        Thread.Sleep(1000);
                        Console.WriteLine("Oh and good luck");
                        KeyCon();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("CHOOSE THE RIGHT STUFF YOU INCOMPETANT PLAYER!");
                    }
                }
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("And so the journey began. {0} was on there way towards the volcano to save the town! \nBut first {1} had to make there way through the forest.", name, name);
                    KeyCon();
                    Decision(ref name, ref PlayerHealth, ref TotalMP, ref CurrentMP, ref area, ref PlayerDefense, ref PlayerMagic, ref ArmorMP, ref PlayerAttack, ref Money, ref XP, ref HealthPotionCount, ref MegaHealthPotionCount, ref MPPotionCount, ref MegaHealthPotionCount, ref Spells, ForestEnemies, ForestEnemiesAttack, ForestEnemiesDefense, ForestEnemiesHealth, CurrentFoe, FoeHP, FoeAttack, FoeDefense, ref distance);

                }
            }
        }



        public static void KeyCon()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.Clear();
        }

        public static void Shop(ref int Money, ref int PlayerAttack, ref int PlayerDefense, ref int PlayerMagic, ref int ArmorMP, ref int HealthPotionCount, ref int MegaHealthPotionCount, ref int MPPotionCount, ref int MegaMPPotionCount, ref List<string> Spell)
        {
            int selection = 0;
            bool purchase = false;
            switch (area)
            {
                case Zone.Start:

                    while (true)
                    {
                        bool goodValue = false;
                        while (!goodValue)
                        {
                            Console.Clear();
                            Console.WriteLine("Welcome to the shop. What would you like to buy? \nMoney: {0}", Money);
                            Console.WriteLine("1. Weapons \n2. Armor \n3. Potions \n4. Spells \n5. Exit");
                            Console.WriteLine();
                            selection = Selection(ref goodValue);
                            Console.Clear();
                        }
                        #region Weapon Purchase Start
                        if (selection == 1)
                        {
                            while (true)
                            {
                                goodValue = false;
                                while (!goodValue)
                                {
                                    Console.Clear();
                                    Console.WriteLine("These are the weapons I have to offer \nMoney: {0} \n *Note you can only have one weapon*", Money);
                                    Console.WriteLine("1. Wooden Sword - 5g (+2 damage) \n2. Spear - 10g (+4 Damage) \n3. Wooden Staff - 5g (+ 2 Magic) \n4. Back");
                                    Console.WriteLine();
                                    selection = Selection(ref goodValue);
                                }
                                if (selection == 1)
                                {
                                    purchase = CheckPurchase(Money, 5);
                                    if (purchase == true)
                                    {
                                        Money = Money - 5;
                                        PlayerAttack = 2;
                                        Console.Clear();
                                        Console.WriteLine("Purhase made");
                                        KeyCon();

                                    }
                                    else
                                    {
                                        Console.WriteLine("You don't have the money to buy that");
                                        KeyCon();
                                    }
                                }
                                else if (selection == 2)
                                {
                                    purchase = CheckPurchase(Money, 10);
                                    if (purchase == true)
                                    {
                                        Money -= 10;
                                        PlayerAttack = 4;
                                        Console.Clear();
                                        Console.WriteLine("Purhase made");
                                        KeyCon();
                                    }
                                    else
                                    {
                                        Console.WriteLine("You don't have the money to buy that");
                                        KeyCon();
                                    }
                                }
                                else if (selection == 3)
                                {
                                    purchase = CheckPurchase(Money, 5);
                                    if (purchase == true)
                                    {
                                        Money -= 5;
                                        PlayerMagic = 2;
                                        Console.Clear();
                                        Console.WriteLine("Purhase made");
                                        KeyCon();
                                    }
                                    else
                                    {
                                        Console.WriteLine("You don't have the money to buy that");
                                        KeyCon();
                                    }
                                }
                                else if (selection == 4)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Dont enter falsities worm!");
                                    KeyCon();
                                }
                            }
                        }
                        #endregion
                        #region Armor Purchase Start
                        else if (selection == 2)
                        {
                            while (true)
                            {
                                goodValue = false;
                                while (!goodValue)
                                {
                                    Console.Clear();
                                    Console.WriteLine("These are the Armors I have to offer \nMoney: {0} \n *Note you can only have one Armor*", Money);
                                    Console.WriteLine("1. Cloth Armor - 5g (+2 Defense) \n2. Raggedy Robes - 5g (+10 MP) \n3. back");
                                    Console.WriteLine();
                                    selection = Selection(ref goodValue);
                                }
                                
                                if (selection == 1)
                                {
                                    purchase = CheckPurchase(Money, 5);
                                    if (purchase == true)
                                    {
                                        PlayerDefense = 2;
                                        Money -= 5;
                                        Console.Clear();
                                        Console.WriteLine("Purhase made");
                                        KeyCon();
                                    }
                                    else
                                    {
                                        Console.WriteLine("You don't have the money to buy that");
                                        KeyCon();

                                    }
                                }
                                else if (selection == 2)
                                {
                                    purchase = CheckPurchase(Money, 5);
                                    if (purchase == true)
                                    {
                                        Money -= 5;
                                        ArmorMP = 10;
                                        Console.Clear();
                                        Console.WriteLine("Purhase made");
                                        KeyCon();
                                    }
                                    else
                                    {
                                        Console.WriteLine("You don't have the money to buy that");
                                        KeyCon();
                                    }
                                }
                                else if (selection == 3)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Dont enter falsities worm!");
                                    KeyCon();
                                }
                            }
                        }
                        #endregion
                        #region Potion Purchase Start
                        else if (selection == 3)
                        {
                            while (true)
                            {
                                goodValue = false;
                                while (!goodValue)
                                {
                                    Console.WriteLine("These are the Potions I have to offer \nMoney: {0} \nYou have {1} Health Potions and {2} Mana Potions", Money, HealthPotionCount, MPPotionCount);
                                    Console.WriteLine("1. Health Potion - 50HP (5g) \n2. Mana Potion - 10MP (5g) \n3. back");
                                    Console.WriteLine();
                                    selection = Selection(ref goodValue);
                                }
                                if (selection == 1)
                                {
                                    purchase = CheckPurchase(Money, 5);
                                    if (purchase == true)
                                    {
                                        Money -= 5;
                                        HealthPotionCount++;
                                        Console.Clear();
                                        Console.WriteLine("Purhase made");
                                        KeyCon();
                                    }
                                    else
                                    {
                                        Console.WriteLine("You don't have the money to buy that");
                                        KeyCon();
                                    }
                                }
                                else if (selection == 2)
                                {
                                    purchase = CheckPurchase(Money, 5);
                                    if (purchase == true)
                                    {
                                        Money -= 5;
                                        MPPotionCount++;
                                        Console.Clear();
                                        Console.WriteLine("Purhase made");
                                        KeyCon();
                                    }
                                    else
                                    {
                                        Console.WriteLine("You don't have the money to buy that");
                                        KeyCon();
                                    }
                                }
                                else if (selection == 3)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Dont enter falsities worm!");
                                    KeyCon();
                                }
                            }
                        }
                        #endregion
                        #region Spell Purchase Start
                        else if (selection == 4)
                        {
                            while (true)
                            {
                                goodValue = false;
                                while (!goodValue)
                                {
                                    Console.Clear();
                                    Console.WriteLine("These are the Spells I have to offer \nMoney: {0}", Money);
                                    Console.WriteLine("1. Basic Heal - 5MP (5g) \n2. FireBall - 5MP (5g) \n3. back");
                                    Console.WriteLine();
                                    selection = Selection(ref goodValue);
                                }
                                if (selection == 1)
                                {
                                    purchase = CheckPurchase(Money, 5);
                                    if (purchase == true)
                                    {
                                        SpellAdd(ref Spell, "Heal");
                                        Money -= 5;
                                        Console.Clear();
                                        Console.WriteLine("Purhase made");
                                        KeyCon();
                                    }
                                    else
                                    {
                                        Console.WriteLine("You don't have the money to buy that");
                                        KeyCon();
                                    }
                                }
                                else if (selection == 2)
                                {
                                    purchase = CheckPurchase(Money, 5);
                                    if (purchase == true)
                                    {
                                        SpellAdd(ref Spell, "FireBall");
                                        Money -= 5;
                                        Console.Clear();
                                        Console.WriteLine("Purhase made");
                                        KeyCon();
                                    }
                                    else
                                    {
                                        Console.WriteLine("You don't have the money to buy that");
                                        KeyCon();
                                    }
                                }
                                else if (selection == 3)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Dont enter falsities worm!");
                                    KeyCon();
                                }
                            }
                        }
                        #endregion
                        else if (selection == 5)
                        {
                            Console.Clear();
                            break;

                        }
                    }
                    break;
                case Zone.Forest:
                    Console.WriteLine("");
                    break;
                case Zone.Desert:

                    break;
                case Zone.UnderWaterCity:

                    break;
                case Zone.Volcano:

                    break;
                case Zone.END:

                    break;
            }
        }

        private void OnAreaChanged()
        {
            AreaChanged?.Invoke(this, EventArgs.Empty);
        }

        public static int Selection(ref bool goodValue)
        {
            int selection = 0;
            try
            {
                selection = Convert.ToInt32(Console.ReadLine());
                goodValue = true;
            }
            catch
            {
                Console.Clear();

            }
            return selection;
        }

        public static bool CheckPurchase(int MoneyOwnded, int MoneyNeeded)
        {
            if (MoneyOwnded >= MoneyNeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void SpellAdd(ref List<string> spellName, string SpellName)
        {
            spellName.Add(SpellName);
        }

        public static void Decision(ref string name, ref int Health, ref int TotalMP, ref int CurrentMP, ref Zone area, ref int armor, ref int magic, ref int ArmorMP, ref int attack, ref int money, ref int XP, ref int HealthPotionCount, ref int MegaHealthPotionCount, ref int MPpotionCount, ref int MegaMPpotionCount, ref List<string> spellName, string[] Enemies, int[] EnemiesAttack, int[] EnemiesDefense, int[] EnemiesHealth, string CurrentFoe, int FoeHP, int FoeAttack, int FoeDefense, ref int Distance)
        {
            int selection = 0;

            while (true)
            {
                bool goodValue = false;
                while (!goodValue)
                {
                    Console.WriteLine("                          {0}", area);
                    Console.WriteLine();
                    Console.WriteLine("What would you like to do?");
                    Console.WriteLine();
                    Console.WriteLine("1. Travel \n2. Check Stats \n3. Spells \n5. Retire");
                    Console.WriteLine();
                    Console.WriteLine("Health: {0}   MagicPoints: {1}/{2}", Health, CurrentMP, TotalMP);
                    selection = Selection(ref goodValue);
                    Console.Clear();
                }
                if (selection == 1)
                {
                    goodValue = false;
                    while (!goodValue)
                    {
                        Console.WriteLine("Before you start you journey would you like to go to the shop?");
                        Console.WriteLine();
                        Console.WriteLine("1. Yes \n2. No shops are for kittens");
                        selection = Selection(ref goodValue);
                        Console.Clear();
                    }
                    if (selection == 1)
                    {
                        Shop(ref money, ref attack, ref armor, ref magic, ref ArmorMP, ref HealthPotionCount, ref MegaHealthPotionCount, ref MPpotionCount, ref MegaHealthPotionCount, ref spellName);
                    }
                    else if (selection == 2)
                    {
                        Travel(Enemies, EnemiesAttack, EnemiesDefense, EnemiesHealth, ref Distance, ref CurrentFoe, ref FoeAttack, ref FoeDefense, ref FoeHP, ref Health, TotalMP, ref CurrentMP, attack, armor, ref HealthPotionCount, ref MegaHealthPotionCount, ref MPpotionCount, ref MegaMPpotionCount, spellName);
                       
                    }
                }
                else if (selection == 2)
                {
                    Console.WriteLine("{0} \nXP: {1} \nHealth: {2} \nMagic Points: {3} \nDefense: {4} \nAttack: {5} \nMagic: {6} \nHealth Potions: {7} \n MP Potions: {8} \nMega Health Potions: {9} \nMega MP Potions: {10} \nMoney: {11}", name, XP, Health, CurrentMP, armor, attack, magic, HealthPotionCount, MPpotionCount, MegaHealthPotionCount, MegaMPpotionCount, money);
                    KeyCon();
                }
                else if (selection == 5)
                {
                    Console.WriteLine("Are you sure you want to quit? \n1. Yes \n2. No");
                    selection = Selection(ref goodValue);
                    if (selection == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("You have failed everyone filth!");
                        KeyCon();
                        Environment.Exit(0);
                    }
                    else if (selection == 2)
                    {
                        Console.Clear();
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Dont enter falsities worm!");
                    KeyCon();
                }

            }
        }

        public static void Travel(string[] Enemies, int[] EnemiesAttack, int[] EnemiesDefense, int[] EnemiesHealth, ref int Distance, ref string CurrentFoe, ref int FoeAttack, ref int FoeDefense, ref int FoeHP, ref int Health, int TotalMP, ref int CurrentMP, int attack, int defense, ref int HealthPotionCount, ref int MegaHealthPotionCount, ref int MPpotionCount, ref int MegaMPpotionCount, List<string> spellName)
        {
            Random rand = new Random();
            int EnemySelection = rand.Next(0, 6);
            FoeAttack = EnemiesAttack[EnemySelection];
            FoeDefense = EnemiesDefense[EnemySelection];
            FoeHP = EnemiesHealth[EnemySelection];
            CurrentFoe = Enemies[EnemySelection];
            int distanceTraveled = rand.Next(8, 12);
            Distance += distanceTraveled;
            int selection = 0;
            int FleeCount;
            int distanceLost;

            Console.WriteLine("You travel {0} distance", distanceTraveled);
            Console.WriteLine();
            Console.WriteLine("When you come across a {0}", CurrentFoe);
            KeyCon();
            while (true)
            {
                bool goodValue = false;
                while (!goodValue)
                {
                    Console.WriteLine("Do you want to flee? \n");
                    Console.WriteLine("1. Fight \n2. Run Away");
                    selection = Selection(ref goodValue);
                }
                if (selection == 1)
                {
                    Fight(ref Health, TotalMP, ref CurrentMP, attack, defense, ref HealthPotionCount, ref MegaHealthPotionCount, ref MPpotionCount, ref MegaMPpotionCount, spellName, CurrentFoe, FoeAttack, FoeDefense, FoeHP);
                }
                else if (selection == 2)
                {
                    FleeCount = rand.Next(0,2);
                    if (FleeCount == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("You failed to run away! Prepare to fight!");
                        KeyCon();
                    }
                    else
                    {
                        distanceLost = rand.Next(3, 8);
                        Distance -= distanceLost;
                        Console.Clear();
                        Console.WriteLine("You ran away succesfully but lost some distance \n\nDistance Lost: {0}", distanceLost);
                        KeyCon();
                        break;
                    }
                }
            }
        }

        public static void Fight(ref int health, int TotalMP, ref int CurrentMP, int attack, int defense, ref int HealthPotionCount, ref int MegaHealthPotionCOunt, ref int MPpotionCount, ref int MegaMPpotionCount, List<string> Spells, string CurrentFoe, int FoeAttack, int FoeDefense, int FoeHP )
        {
            Random rand = new Random();
            int selection = 0;
            int damage;
            

            while (true)
            {
                bool goodValue= true;
                while (!goodValue)
                {
                    Console.WriteLine("                         {0}", CurrentFoe);
                    Console.WriteLine("Make Your Move \n");
                    Console.WriteLine("Enemy Health: {0}\n", FoeHP);
                    Console.WriteLine("1. Attack \n2. Spells \n3. Potions \n4. Guard");
                    selection = Selection(ref goodValue);
                }
                if (selection == 1)
                {
                    damage = rand.Next(attack - 1, attack + 1);
                    damage = Convert.ToInt32((damage + (damage* 0.5f)) - (FoeDefense * 0.75f));
                    Console.WriteLine("You swing for {0} damage", damage);
                    FoeHP -= damage;
                }
            }

        }
    }
}

