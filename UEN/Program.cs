using System;

namespace UEN
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                bool validity; 
            
                Console.Write("Please enter UEN: ");
                string input = Console.ReadLine();

                validity = UENValidator.Validate(input);
                UENValidator.ShowValidity(validity);
            }
            else
            {
                foreach (string uen in args)
                {
                    UENValidator.ShowValidity(UENValidator.Validate(uen));
                }
            }  
        }
    }
}

// Copy and paste 12345678X 196599999X 202099999X T00LL4444X S99LP4444X R88CC4444X 
// into Debug -> UEN Debug Properties -> Application Argument
// for application to pass arugments to program
//
// if not enter UEN to test at when prompt

