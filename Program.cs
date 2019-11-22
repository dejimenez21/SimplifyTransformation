using System;
using System.Linq;
using System.Collections.Generic;

namespace SimplifyTransformation
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length<1)
            {
                System.Console.WriteLine(string.Empty);
                return;
            }
            string[] transformations = FormatInput(args[0]);

            List<string> result = WordCapitalizationSimplification(transformations);
            result=SpacingBetweenWordsSimplification(result);
            result=TrimsSimplification(result);
            
            FormatOutputAndPrint(result, args);
            
            Console.ReadKey();
        }

        static void FormatOutputAndPrint(List<string> transformations, string[] args)
        {
            Console.Write($"\"{args[0]}\" === \"");
            for(int i=0; i<transformations.Count-1; i++)
            {
                Console.Write(transformations[i] + "->");
            }

            Console.Write(transformations.Last() + "\"");
        }

        static List<string> TrimsSimplification(List<string> transformations)
        {
            bool Trim = false;
            bool LTrim = false;
            bool RTrim = false;

            List<string> result = new List<string>();

            foreach(string trans in transformations)
            {
                switch(trans)
                {
                    case "Trim":
                        Trim=true;
                        break;

                    case "RTrim":
                        RTrim=true;
                        break;
                    
                    case "LTrim":
                        LTrim=true;
                        break;
                    
                    default:
                        result.Add(trans);
                        break;

                }
            }

            if(Trim || (LTrim && RTrim))
                result.Add("Trim");
            else if(LTrim)
                result.Add("LTrim");
            else if(RTrim)
                result.Add("RTrim");
            
            return result;

        }

        static List<string> SpacingBetweenWordsSimplification(List<string> transformations)
        {
            bool SpaceBetweenTransformation = false;
            List<string> result = new List<string>();

            foreach(var trans in transformations)
            {
                if(SpaceBetweenTransformation)
                {
                    if(trans!="Pack" && trans!="Snake")
                    {
                        result.Add(trans);
                    }
                }
                else
                {
                    result.Add(trans);
                    if(trans=="Pack" || trans=="Snake")
                        SpaceBetweenTransformation=true;
                }
            }
            return result;
        }

        static List<string> WordCapitalizationSimplification(string[] transformations)
        {
            int IndexlastCapTrans = -1;

            for(int i=transformations.Length-1; i>-1; i--)
            {
                if(transformations[i]=="Pascal" || transformations[i]=="Cammel" || transformations[i]=="Lower" || transformations[i]=="Upper")
                {
                    IndexlastCapTrans = i;
                    break;
                }
                
            }

            if(IndexlastCapTrans==-1)
            {
                return new List<string>(transformations);
            }

            List<string> result = new List<string>();

            for(int i=0; i<transformations.Length; i++)
            {
                if((transformations[i]=="Pascal" || transformations[i]=="Cammel" || transformations[i]=="Lower" || transformations[i]=="Upper") && i<IndexlastCapTrans)
                {
                    continue;
                }
                else
                {
                    result.Add(transformations[i]);
                }
            }

            return result;
        }

        static string[] FormatInput(string input)
        {
            char[] separator = {'-', '>'};
            string[] transformations = input.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            return transformations;
        }
    }
}
