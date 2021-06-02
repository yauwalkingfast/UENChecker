using Microsoft.VisualStudio.TestTools.UnitTesting;
using UEN;
using System;
using System.Collections.Generic;
using System.Text;

namespace UEN.Tests
{
    //*
    //*             Not able to do unit test on private methods
    //*             Hence test the entire static class
    //*

    [TestClass()]
    public class UENValidatorTests
    {
        [TestMethod()]
        public void Last_Char_IsLetter_Test()
        {
            string test = "123456789";
            bool result = UENValidator.Validate(test);

            // will fail cos last char should be a letter
            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void Length_Less_Than_9_Test()
        {
            string test = "1q2w3e4r";
            bool result = UENValidator.Validate(test);

            // will fail cos length of string is less than 9
            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void Length_Less_Than_10_Test()
        {
            string test = "1q2w3e4r5tX";
            bool result = UENValidator.Validate(test);

            // will fail cos length of string is more than 10
            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void BusinessValidator_All_Numbers_Test()
        {
            char[] test = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'X' };

            bool LetterNotDetected = true;

            for (int i = 0; i < test.Length - 1; i++)
            {
                test[i] = 'X';
                LetterNotDetected = UENValidator.Validate(test.ToString()); // shld come back false if any letter is detected
                test = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'X' };
                if (LetterNotDetected == true)
                {
                    break;
                }
            }
            // Businesses registered with ACRA - nnnnnnnnX
            // If any letter is detected in the loop, LetterNotDetected
            Assert.AreEqual(false, LetterNotDetected);
        }

        [TestMethod()]
        public void Local_Companies_Year_Less_Than_1965_Test()
        {
            string test = "165455555X";

            bool result = UENValidator.Validate(test);

            // will fail cos year is less than 1965
            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void Local_Companies_Year_More_Than_Current_Year_Test()
        {
            string year = DateTime.Now.AddYears(1).ToString("YYYY");
            string sub = "55555X";

            bool result = UENValidator.Validate(year + sub);

            // will fail cos year is less than 1965
            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void entities_First_Letter_Test()
        {
            char[] lettersExceptTSR = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'u', 'v', 'w', 'x', 'y', 'x' };

            char[] test = new char[] { 'X', '0', '0', 'L', 'L', '6', '7', '8', '9', 'X' };

            bool OtherLetterDetected = true;

            for (int i = 0; i < lettersExceptTSR.Length - 1; i++)
            {
                test[0] = lettersExceptTSR[i];
                OtherLetterDetected = UENValidator.Validate(test.ToString()); // shld come back false if any letter is detected
                if (OtherLetterDetected == true)
                {
                    break;
                }
            }

            // OtherLetterDetected shld be false unless letters other than T, S, R are detected
            Assert.AreEqual(false, OtherLetterDetected);
        }
    }
}