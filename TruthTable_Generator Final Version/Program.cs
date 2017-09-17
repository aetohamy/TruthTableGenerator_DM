using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region TestCase
/*Console.WriteLine("Write an expression : ");
            String exp = Console.ReadLine();
            TruthTable table = new TruthTable(exp);
            Console.WriteLine("Postfix Expression: "+table.PostfixExpression);
            table.EvaluateExpression(table.PostfixExpression);
            Console.WriteLine("Variables: \n" + table.GetAllVariables());
            Console.WriteLine("------------------------------");
            Console.WriteLine("Rules: \n" + table.GetAllRules());
            Console.WriteLine("Variables Truth Table:\n");
            table.DisplayVariablesTable();
            Console.WriteLine();
            Console.WriteLine("Truth Table:");
           
            Console.WriteLine("-----------------------------------");
            table.InitializeRules();
            table.Display();
            Console.WriteLine("==================================================");
          */
#endregion

namespace TruthTable_Generator_Final_Version
{

 
    class Program
    {
        public static void DisplayMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine(" TRUTH TABLE GENERATOR - By Ahmed Tohamy ^ Mustafa Ahmed");
            Console.WriteLine(" Bioinformatics 2nd Year");
            Console.WriteLine(" Dr. Dina Khatab - Discrete Mathematics");
            Console.WriteLine(" ============================================================");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("   1)  Check for Tautology");
            Console.WriteLine("   2)  Check if two expressions are logically equivalent");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" ============================================================");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Choice<1-2>: ");
        }

        public static void Title()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine(" TRUTH TABLE GENERATOR - By Ahmed Tohamy ^ Mustafa Ahmed");
            Console.WriteLine(" Bioinformatics 2nd Year");
            Console.WriteLine(" Dr. Dina Khatab - Discrete Mathematics");
            Console.WriteLine(" ============================================================");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void Main(string[] args)
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                DisplayMenu();
                int choice = Convert.ToInt32(Console.ReadLine());
                if (choice == 1)
                {
                    Console.Clear();
                    Title();
                    Console.WriteLine();
                    Console.WriteLine("AND: ^ \t OR: | \t XOR: #\n\nNOT: ! \t Implies: ~\t Biconditional: =\n\n");
                    Console.WriteLine("Logical Expression : ");
                    String exp = Console.ReadLine();
                    TruthTable table = new TruthTable(exp);
                    Console.WriteLine("------------------------------------------");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Expression after converting to Postfix : ");
                    Console.WriteLine(table.PostfixExpression);
                    Console.WriteLine();
                    Console.WriteLine(" Truth Table :");
                    Console.WriteLine("----------------");
                    table.EvaluateExpression(table.PostfixExpression);
                    table.InitializeRules();
                    table.Display();
                    List<bool> finalTab = table.GetFinalTable();
                    bool tautology = true;
                    for(int i = 0 ; i < finalTab.Count;i++)
                    {
                        if (!finalTab[i])
                        {
                            tautology = false;
                            break;
                        }
                    }

                    string result = (tautology) ? "The Expression is a Tautology." : "The Expression is NOT a Tautology.";
                    Console.WriteLine();
                    Console.WriteLine(" Result:");
                    Console.WriteLine("----------");
                    Console.WriteLine(result);
                    Console.WriteLine("\nPress any key to continue..");
                    Console.ReadLine();
                }

                else
                {
                    Console.Clear();
                    Title();
                    Console.WriteLine();
                    Console.WriteLine("AND: ^ \t OR: | \t XOR: #\n\nNOT: ! \t Implies: ~\t Biconditional: =\n\n");
                    Console.WriteLine("1st Logical Expression:");
                    string exp1 = Console.ReadLine();
                    TruthTable table1 = new TruthTable(exp1);
                    Console.WriteLine("2nd Logical Expression:");
                    string exp2 = Console.ReadLine();
                    TruthTable table2 = new TruthTable(exp2);
                    Console.WriteLine("------------------------------------------");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" Expression 1 Table:");
                    Console.WriteLine("---------------------");
                    Console.WriteLine("Expression after converting to Postfix : ");
                    Console.WriteLine(table1.PostfixExpression);
                    Console.WriteLine();
                    Console.WriteLine(" Truth Table :");
                    Console.WriteLine("----------------");
                    table1.EvaluateExpression(table1.PostfixExpression);
                    table1.InitializeRules();
                    table1.Display();
                    Console.WriteLine("------------------------------------------");
                   Console.WriteLine("------------------------------------------");
                   Console.WriteLine(" Expression 2 Table:");
                    Console.WriteLine("---------------------");
                    Console.WriteLine("Expression after converting to Postfix : ");
                    Console.WriteLine(table2.PostfixExpression);
                    Console.WriteLine();
                    Console.WriteLine(" Truth Table :");
                    Console.WriteLine("----------------");
                    table2.EvaluateExpression(table2.PostfixExpression);
                    table2.InitializeRules();
                    table2.Display();
                    Console.WriteLine();

                  
                    List<bool> finalTab1 = table1.GetFinalTable();
                    List<bool> finalTab2 = table2.GetFinalTable();
                    bool lequiv = true;
                    if (finalTab1.Count != finalTab2.Count)
                        lequiv = false;
                    else
                    {
                        for(int i = 0 ; i < finalTab1.Count;i++)
                        {
                            if(finalTab1[i]!=finalTab2[i])
                            {
                                lequiv = false;
                                break;
                            }
                        }
                    }
                    string result = (lequiv) ? "Both Logical Expressions ARE Logically Equivalent." : "Both Logical Expressions ARE NOT Logically Equivalent.";
                    Console.WriteLine();
                    Console.WriteLine(" Result:");
                    Console.WriteLine("----------");
                    Console.WriteLine(result);
                    Console.WriteLine("\nPress any key to continue..");
                    Console.ReadLine();

                }
            }
        }
    }
}
