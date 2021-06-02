using System;
using System.Collections.Generic;
using System.Text;

namespace UEN
{
    public static class UENValidator
    {
        public static bool Validate(string input)
        {
            bool validity = false;

            if (Char.IsLetter(input[input.Length - 1]))
            {
                if (input.Length == 9)
                {
                    validity = BusinessValidator(input);
                }
                if (input.Length == 10)
                {
                    if (int.TryParse(input.Substring(0, 4), out int year))
                    {
                        validity = CompanyValidator(input, year);
                    }
                    else
                    {
                        validity = OtherEntities(input);
                    }
                }
            }
            return validity;
        }

        private static bool BusinessValidator(string input)
        {
            // check if all other inputs are numbers 
            for (int i = 0; i < input.Length - 1; i++)
            {
                if (!Char.IsDigit(input[i]))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool CompanyValidator(string input, int year)
        {
            if (year >= 1965 && year <= DateTime.Now.Year) // Assuming companies can only be registered after 1965
            {
                for (int i = 4; i < input.Length - 1; i++)
                {
                    if (!Char.IsDigit(input[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        private static bool OtherEntities(string input)
        {
            char firstLetter = input[0];
            if (firstLetter != 'T' && firstLetter == 'R' && firstLetter == 'S')
            {
                return false;
            }

            string entityTypeIndicator = input.Substring(3, 2).ToUpper();
            if (EntityTypeValid(entityTypeIndicator))
            {
                Dictionary<string, string> dict = LoadDictionary();
                if (!dict.ContainsKey(entityTypeIndicator))
                {
                    return false;
                }
            }

            for (int i = 1; i < input.Length - 1; i++)
            {
                if (i == 3 || i == 4)
                {
                    continue;
                }
                else
                {
                    if (!Char.IsDigit(input[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool EntityTypeValid(string entityTypeIndicator)
        {
            if (Char.IsLetter(entityTypeIndicator[0]) && Char.IsLetterOrDigit(entityTypeIndicator[1]))
            {
                return true;
            }
            return false;
        }

        public static void ShowValidity(bool validity)
        {
            if (validity)
            {
                Console.WriteLine("UEN is valid");
            }
            else
            {
                Console.WriteLine("UEN is not valid");
            }
        }

        private static Dictionary<string, string> LoadDictionary()
        {
            return new Dictionary<string, string>()
            {
                { "LP", "Limited Partnership" },
                { "LL", "Limited Liability Partnerships" },
                { "FC", "Foreign Companies" },
                { "PF", "Public Accounting Firms" },
                { "RF", "Representative Offices of Foreign Companies, Foreign Government Agencies or Foreign Trade Associations/Chambers/Non-Profit Organisations" },

                { "MQ", "Mosques" },
                { "MM", "Madrasahs" },
                { "NB", "News Bureaus" },
                { "CC", "Charities and Institutions of a Public Character" },
                { "CS", "Cooperative Societies" },
                { "MB", "Mutual Benefit Organisations" },
                { "FM", "Foreign Military Units" },
                { "GS", "Government and Government-Aided Schools" },

                { "DP", "High Commissions, Embassies" },
                { "CP", "Consulate" },
                { "NR", "International Organisations (registered with MFA)" },

                { "CM", "Healthcare Institutions and Clinics - Only Medical Clinic" },
                { "CD", "Healthcare Institutions and Clinics - Only Dental Clinic" },
                { "MD", "Healthcare Institutions and Clinics - Both Medical and Dental Clinic" },
                { "HS", "Healthcare Institutions and Clinics - Hospitals" },
                { "VH", "Healthcare Institutions and Clinics - Voluntary Welfare Home" },
                { "CH", "Healthcare Institutions and Clinics - Commercial Home" },
                { "MH", "Healthcare Institutions and Clinics - Maternity Home" },
                { "CL", "Healthcare Institutions and Clinics - Clinical Laboratory" },
                { "XL", "Healthcare Institutions and Clinics - Xray Laboratory" },
                { "CX", "Healthcare Institutions and Clinics - Both Clinical and Xray Laboratory" },

                { "RP", "Foreign Law Practice Representative Offices" },
                { "TU", "Trade Unions" },
                { "TC", "Town Councils" },

                { "FB", "Financial Representative Offices - Bank Representative Offices" },
                { "FN", "Financial Representative Offices - Insurance Representative Offices" },

                { "PA", "PA Services" },
                { "PB", "Grassroot Units" },

                { "SS", "Societies" },

                { "MC", "Management Corporations" },
                { "SM", "Subsidiary Management Corporations" },

                { "GA", "Government Agencies and bodies performing public duties - Organs of State, Ministries and Departments" },
                { "GB", "Government Agencies and bodies performing public duties - Statutory Boards and bodies performing public duties" },
            };
        }
    }
}
