using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthTable_Generator_Final_Version
{
    class TruthTable
    {
        private Dictionary<char, List<bool>> _Variables;
        private Dictionary<string, List<bool>> _Rules;
        private List<char> _ListOfVariables;
        private List<string> _ListOfRules;
        public String PostfixExpression;
        private int _TableCount;
        private List<bool> _FinalTable;

        public TruthTable()
        {
            _Variables = new Dictionary<char, List<bool>>();
            _Rules = new Dictionary<string, List<bool>>();
            _ListOfVariables = new List<char>();
            _ListOfRules = new List<string>();
        }

        public TruthTable(string exp)
        {
            _Variables = new Dictionary<char, List<bool>>();
            _Rules = new Dictionary<string, List<bool>>();
            _ListOfVariables = new List<char>();
            _ListOfRules = new List<string>();
            PostfixExpression = InfixToPostfix(exp);
        }

        private void AddVariable(char c)
        {
            _Variables.Add(c, new List<bool>());
        }

          private void AddRule(string c)
          {
            _Rules.Add(c, new List<bool>());
          }

        public void InitializeVariables()
        {
            double startValue = Math.Pow(2, _Variables.Count - 1);
            
            double endValue = startValue * 2;
            _TableCount = Convert.ToInt32(endValue);
            foreach (KeyValuePair<char, List<bool>> c in _Variables)
            {
                while (c.Value.Count != endValue)
                {
                    // True
                    for (int j = 0; j < startValue; j++)
                    {
                        c.Value.Add(true);
                    }

                    // False
                    for (int j = 0; j < startValue; j++)
                    {
                        c.Value.Add(false);
                    }
                }
                startValue /= 2;
            }
            startValue = Math.Pow(2, _Variables.Count - 1);
            endValue = startValue * 2;
            foreach (KeyValuePair<string, List<bool>> c in _Rules)
            {
                while (c.Value.Count != endValue)
                {
                    // True
                    for (int j = 0; j < startValue; j++)
                    {
                        c.Value.Add(true);
                    }

                    // False
                    for (int j = 0; j < startValue; j++)
                    {
                        c.Value.Add(false);
                    }
                }
                startValue /= 2;
            }
        }

        public void DisplayVariablesTable()
        {
            foreach (KeyValuePair<char, List<bool>> c in _Variables)
            {
                Console.Write(String.Format("|{0,5}|", c.Key));
            }
            Console.WriteLine();
            for (int i = 0; i < Math.Pow(2, _Variables.Count - 1) * 2; i++)
            {
                foreach (KeyValuePair<char, List<bool>> c2 in _Variables)
                    Console.Write(String.Format("|{0,5}|", c2.Value[i].ToString()));
                Console.WriteLine();
            }

        }

        public void Display()
        {
            foreach (KeyValuePair<string, List<bool>> c in _Rules)
            {
                Console.Write(String.Format("|{0,5}|\t  ", c.Key));
            }

            Console.WriteLine();
            for (int i = 0; i < _TableCount; i++)
            {
                foreach (KeyValuePair<string, List<bool>> c2 in _Rules)
                    Console.Write(String.Format("|{0,5}|\t  ", c2.Value[i].ToString()));
                Console.WriteLine();
            }


        }
        private bool IsOperator(char C)
        {
	        if (C == '^' || C == '|' || C == '!' || C == '~' || C == '=' || C=='#')
		        return true;

	        return false;
        }
        private int GetOperatorWeight(char op)
        {
	        int weight = -1;
	        switch (op)
	        {
	        case '~':
	        case '=':
		        weight = 1;
                break;
	        case '^':
	        case '|':
            case '#':
		        weight = 2;
                break;
	        case '!':
		        weight = 3;
                break;
	        }
	        return weight;
        }
        private bool IsOperand(char C)
        {
	        if (C >= '0' && C <= '9') return true;
	        if (C >= 'a' && C <= 'z') return true;
	        if (C >= 'A' && C <= 'Z') return true;
	        return false;
        }
       private bool HasHigherPrecedence(char op1, char op2)
         {
	        int op1Weight = GetOperatorWeight(op1);
	        int op2Weight = GetOperatorWeight(op2);

	        // If operators have equal precedence, return true if they are left associative. 
	        // return false, if right associative. 
	        // if operator is left-associative, left one should be given priority. 
	        if (op1Weight == op2Weight)
	        {
		        return true;
	        }

	        return op1Weight > op2Weight ? true : false;
        }
       public string InfixToPostfix(string expression)
        {
	
	        Stack<char> S = new Stack<char>();
	        string postfix = ""; // Initialize postfix as empty string.
	        for (int i = 0; i< expression.Length; i++) 
            {
		        if (expression[i] == ' ' || expression[i] == ',') continue;

		
		        else if (IsOperator(expression[i]))
		        {
			        while (S.Count!=0 && S.Peek() != '(' && HasHigherPrecedence(S.Peek(), expression[i]))
			        {
				        postfix += S.Peek();
				        S.Pop();
			        }
			        S.Push(expression[i]);
		        }
		
		        else if (IsOperand(expression[i]))
		        {
			        postfix += expression[i];
		        }

		        else if (expression[i] == '(')
		        {
			        S.Push(expression[i]);
		        }

		        else if (expression[i] == ')')
		        {
			        while (S.Count!=0 && S.Peek() != '(') 
                    {
				        postfix += S.Peek();
				        S.Pop();
			        }
			        S.Pop();
		        }
	        }

	        while (S.Count!=0) {
		        postfix += S.Peek();
		        S.Pop();
	        }

	        return postfix;
        }

       public void EvaluateExpression(string exp)
       {
           Stack<string> S = new Stack<string>();
           for(int i = 0 ;i <exp.Length;i++)
           {
               if(IsOperand(exp[i]))
               {
                   if (!_ListOfVariables.Contains(exp[i]))
                   {
                       _ListOfVariables.Add(exp[i]);
                       AddVariable(exp[i]);
                       AddRule(exp[i].ToString());
                   }

                   S.Push(exp[i].ToString());
               }

               else if(IsOperator(exp[i]))
               {
                   if(exp[i]=='!')
                   {
                       String newStr = S.Pop();
                       S.Push('!' + newStr);
                       _ListOfRules.Add('!'+newStr);
                   }
                  
                   else if (S.Count != 0)
                   {
                       //newString += '('+S.Pop();
                       String secondPart = S.Pop() + ')';
                       String firstPart = '(' + S.Pop();
                       String newString = firstPart + exp[i] + secondPart;
                       //newString += exp[i];
                       //newString += S.Pop() + ')';
                       //Console.WriteLine(newString);
                       S.Push(newString);
                       _ListOfRules.Add(newString);
                   }
                   
               }
           }
           InitializeVariables();
       }


       public string GetAllRules()
       {
           string newString = "";
           for(int i = 0 ; i<_ListOfRules.Count;i++)
           {
               newString += _ListOfRules[i] + "\n";
           }
           return newString;
       }

       public string GetAllVariables()
       {
           string newString = "";
           for (int i = 0; i < _ListOfVariables.Count; i++)
           {
               newString += _ListOfVariables[i] + "\n";
           }
           return newString;
       }

       private bool EvaluateOperator(bool a,char op,bool b)
       {
           bool returnValue = false;
           switch(op)
           {
               case '^':
                   returnValue = a & b;
                   break;
               case '|':
                   returnValue = a | b;
                   break;
               case '~':
                   if (a && !b)
                       returnValue = false;
                   else
                       returnValue = true;
                   break;
               case '=':
                   if ((a && b) || (!a && !b))
                       returnValue = true;
                   else
                       returnValue = false;
                   break;
               case '#':
                   returnValue = a ^ b;
                   break;
           }
           return returnValue;
       }

       public void InitializeRules()
       {
           Stack<string> Timeline = new Stack<string>();
           for(int i = 0;i < PostfixExpression.Length;i++)
           {
               if(IsOperator(PostfixExpression[i]) && Timeline.Count!=0 && PostfixExpression[i]!='!')
               {
                   String sExp = Timeline.Pop();
                   String fExp = Timeline.Pop();
                   String rule = '(' + fExp + PostfixExpression[i] + sExp + ')';

                   if(!_Rules.ContainsKey(rule))
                   AddRule(rule);

                   for(int j = 0 ;j< _TableCount;j++)
                   {
                      _Rules[rule].Add(EvaluateOperator(_Rules[fExp][j], PostfixExpression[i], _Rules[sExp][j]));
                   }
                   Timeline.Push(rule);
               }

                else if(PostfixExpression[i]=='!')
                {
                    String fExp = Timeline.Pop();
                    string rule = "(!" + fExp + ")";
                    if (!_Rules.ContainsKey(rule))
                        AddRule(rule);
               
                    for (int j = 0; j < _TableCount; j++)
                    {
                        _Rules[rule].Add(!_Rules[fExp][j]);
                    }
                    Timeline.Push(rule);
                }

               else
               {
                   Timeline.Push(PostfixExpression[i].ToString());
               }
           }
       }

        public List<bool> GetFinalTable()
       {
           return _Rules.Values.Last() ;
       }

    }
}
