using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace exercise_29_6
{
    class Program
    {
        static void Main(string[] args)
        {
            //userInfo();
            Console.WriteLine(getLvl(50, 500));
        }
        //ex1
        public static void userInfo()
        {
            int childrenAmount = 0, age = 0, yearsMarried = 0, MarriedYear = 0;
            int[] childrenAges = { };
            string[] childrenNames = { };
            string name, favColor;
            bool hasChildren = false;
            Console.WriteLine("What's your name?");
            name = Console.ReadLine();
            int yearBorn = RequireYearAnswer("What year were you born?");

            age = DateTime.Now.Year - yearBorn;

            Console.WriteLine("What city were you born in?");
            string city = Console.ReadLine();
            bool isMarried = RequireYesNoAnswer("Are you married?");
            if (isMarried && age < 18)
            {
                Console.WriteLine("You can't be married if you're underage");
                while (RequireYesNoAnswer("Are you married?"))
                {
                    Console.WriteLine("You can't be married if youre underage");

                }
            }
            else if (isMarried && age > 18)
            {


                bool flag = false;
                while (!flag)
                {
                    MarriedYear = RequireYearAnswer("What year did you marry?");
                    yearsMarried = DateTime.Now.Year - MarriedYear;
                    if (age - yearsMarried - 18 < 0)
                    {
                        Console.WriteLine("Error, you couldn't have married underage. How can you have married at {0} if you became an adult at {1}?", MarriedYear, yearBorn + 18);
                    }
                    else
                        flag = true;
                }

                hasChildren = RequireYesNoAnswer("Do you have children?");
                if (hasChildren)
                {
                    flag = false;
                    while (!flag)
                    {
                        Console.WriteLine("How many children do you have?");
                        if (!int.TryParse(Console.ReadLine(), out childrenAmount))
                        {
                            Console.WriteLine("Please enter an integer for the number of children");
                        }
                        else if (childrenAmount < 0)
                        {
                            Console.WriteLine("The amount of children has to be equal or higher than 0");
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                    childrenAges = new int[childrenAmount];
                    childrenNames = new string[childrenAmount];
                    for (int i = 0; i < childrenAmount; i++)
                    {
                        Console.WriteLine("What's the name of child number {0}", i + 1);
                        childrenNames[i] = Console.ReadLine();
                        flag = false;
                        while (!flag)
                        {
                            Console.WriteLine("What is {0}'s age?", childrenNames[i]);

                            if (!int.TryParse(Console.ReadLine(), out childrenAges[i]))
                            {
                                Console.WriteLine("Error, you need to enter an integer");
                            }
                            if (i == 0)
                            {
                                if (childrenAges[i] > yearsMarried)
                                {
                                    Console.WriteLine("Error, You can't be married for {0} years and have a {1} year old child", yearsMarried, childrenAges[i]);
                                }
                            }
                            if (childrenNames.Length > 1 && i > 0)
                            {
                                if (childrenAges[i] > childrenAges[i - 1])
                                {
                                    Console.WriteLine("Error, child number {0} can't be older than child number {1}", i + 1, i);
                                }
                            }


                            {
                                flag = true;
                            }
                        }
                    }

                }
            }
            Console.WriteLine("What's your favorite color?");
            favColor = Console.ReadLine();

            string output = "Dear " + name + ", you were born in " + yearBorn + " and born in the city of " + city + "\n";
            if (isMarried)
            {
                output += "You have been married for " + yearsMarried + " years and you got married in " + MarriedYear + "\n";
            }
            if (hasChildren)
            {
                output += "You have " + childrenAmount + " kid(s).\n";
                for (int i = 0; i < childrenAmount; i++)
                {
                    output += ("The name of your " + i + " child is " + childrenNames[i] + " and was born in the year of " + (DateTime.Now.Year - childrenAges[i]) + "\n");
                }
            }
            output += "Your favorite color is " + favColor;

            Console.WriteLine(output);


        }

        public static int RequireYearAnswer(string question)
        {
            int answer;
            Console.WriteLine(question);
            while (!int.TryParse(Console.ReadLine(), out answer) && answer < DateTime.Now.Year)
            {
                Console.WriteLine("You need to enter a four digit year that is prior to the current year");
                Console.WriteLine(question);

            }
            return answer;
        }

        public static bool RequireYesNoAnswer(string question)
        {
            bool flag = false;
            string answer = "";
            bool isYes = false; ;
            while (!flag)
            {
                Console.WriteLine(question);

                answer = Console.ReadLine().ToLower();
                if (answer == "no")
                {
                    isYes = false;
                    flag = true;
                }
                else if (answer == "yes")
                {
                    isYes = true;
                    flag = true;
                }
                else
                {
                    Console.WriteLine("You need to enter 'yes' or 'no'.");
                }

            }
            return isYes;
        }


        //ex4 dnd

        // format of Character: { "strength", "dexterity", "constitution", "intelligence", "wisdom", "charisma" }.
       
        public static List<float> ModifyCharacter(List<float> attributes, string class_type, int exp, int exp_gained)
        {
            class_type = class_type.ToLower();
            if (class_type == "paladin")
            {
                attributes[0] *= 2.0f;
                attributes[5] *= 2.0f;
                for (int i = 1; i < attributes.Count - 1; i++)
                    //update * 0.8 the rest of the attributes
                    attributes[i] *= 0.8f;

            }
            if (class_type == "barbarian" || class_type == "fighter")
            {
                attributes[0] *= 2.0f;
                for (int i = 1; i < 6; i++)
                    attributes[i] *= 0.8f;


            }
            else if (class_type == "bard" || class_type == "sorcerer" || class_type == "warlock")
            {
                attributes[attributes.Count - 1] *= 2.0f;
                for (int i = 0; i < attributes.Count - 1; i++)
                    attributes[i] *= 0.8f;

            }
            else if (class_type == "cleric" || class_type == "druid" || class_type == "monk")
            {
                attributes[attributes.Count - 2] *= 2.0f;
                attributes[attributes.Count - 1] *= 0.8f;
                for (int i = 0; i < attributes.Count - 2; i++)
                    attributes[i] *= 0.8f;

            }
            else if (class_type == "rogue" || class_type == "ranger")
            {
                attributes[1] *= 2.0f;
                attributes[0] *= 0.8f;
                for (int i = 3; i < attributes.Count; i++)
                    attributes[i] *= 0.8f;

            }
            else if (class_type == "wizard")
            {
                attributes[3] *= 2.0f;

                for (int i = 0; i < 3; i++)
                    attributes[i] *= 0.8f;
                for (int i = 4; i < attributes.Count; i++)
                    attributes[i] *= 0.8f;

            }

            return attributes;
        }
        

       
        //return new lvl method
        public static int getLvl(int exp, int exp_gained)
        
        {
            int pointer = 0;
            int prev_lvl = 0, cur_lvl = 0;
            int lvl = 1;
            int lvls_gained = 0;
            int cur_exp = exp + exp_gained;
            for (int i = 1; ; i++)
            {
                pointer = pointer + 100 * i;
                //lvl++;
                if (exp < pointer)
                {
                    Console.WriteLine("pointer exp=" + pointer);
                    prev_lvl = i ;
                    Console.WriteLine("prev lvl=" + prev_lvl);
                    break;
                }

            }
            cur_lvl = prev_lvl;
            for (int i = cur_lvl + 1; ; i++)
            {
                lvls_gained++;
                pointer = pointer + 100 * i;
                if (cur_exp < pointer)
                {
                    cur_lvl = i;

                    break;
                }
            }

            return cur_lvl;
        }
    }
}
