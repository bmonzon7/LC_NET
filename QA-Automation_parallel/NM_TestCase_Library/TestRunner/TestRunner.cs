
using System;
using Browserstack.Interfaces;
using NUnit.Framework;
using System.Reflection;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

/// <summary>
/// Test Runner Engine
/// </summary>
namespace Browserstack.NM_TestCase_Library.Tescase_Library
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]

    public class TestRunner : Attribute, ITestRunnerEngine
    {


        [Test]
        [Category("zPrototypes")]
        [Ignore("not ready")]
        public void Reflect()
        {
            /*
             * https://stackoverflow.com/questions/3467765/find-methods-that-have-custom-attribute-using-reflection
             */
            var methods = Assembly.GetExecutingAssembly().GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.GetCustomAttributes(typeof(CategoryAttribute), false).Length > 0)
                .ToArray();


            for (int i = 0; i < methods.Length; i++)
            {
                Console.WriteLine("Method Name: " + methods[i].Name);

                foreach (var customAttrbs in methods[i].CustomAttributes)
                {
                    if (customAttrbs.AttributeType == typeof(CategoryAttribute))
                    {
                        foreach (var constArgs in customAttrbs.ConstructorArguments)
                        {
                            Console.WriteLine("Attribute Value: " + constArgs.Value.ToString());
                            Console.WriteLine("");
                        }

                    }
                }
            }


            foreach (var method in methods)
            {
                //Debugger.Break();


            }

            /*

            Type myType = Type.GetType("Automation_Framework.NM_TestCase_Library.Tescase_Library.HomePage_Tests");
            // Get the method Mymethod on the type.
            MethodBase Mymethodbase = myType.GetMethod("TestHomePageLoads");
            // Display the method name.
            Console.WriteLine("Mymethodbase = " + Mymethodbase);

            // Get the MethodAttribute enumerated value.
            MethodAttributes Myattributes = Mymethodbase.Attributes;

            // Display the flags that are set.
            PrintAttributes(typeof(System.Reflection.MethodAttributes), (int)Myattributes);
            //return 0;
        }
        public static void PrintAttributes(Type attribType, int iAttribValue)
        {
            if (!attribType.IsEnum)
            {
                Console.WriteLine("This type is not an enum.");
                return;
            }

            FieldInfo[] fields = attribType.GetFields(BindingFlags.Public | BindingFlags.Static);
            for (int i = 0; i < fields.Length; i++)
            {
                int fieldvalue = (Int32)fields[i].GetValue(null);
                if ((fieldvalue & iAttribValue) == fieldvalue)
                {
                    Console.WriteLine(fields[i].Name);
                }
            }
            */
        }
        [Test]
        [Category("zPrototypes")]
        [Ignore("not ready")]
        public void RunTests()
        {

            Type myType = typeof(HomePage_Tests);

            MethodInfo[] myArrayMethodInfo = myType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            Console.WriteLine("\nThe number of public methods is {0}.", myArrayMethodInfo.Length);
            // Display all the methods.
            DisplayMethodInfo(myArrayMethodInfo);
            // Get the nonpublic methods.
            MethodInfo[] myArrayMethodInfo1 = myType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            Console.WriteLine("\nThe number of protected methods is {0}.", myArrayMethodInfo1.Length);
            // Display information for all methods.
            DisplayMethodInfo(myArrayMethodInfo1);
        }

        public static void DisplayMethodInfo(MethodInfo[] myArrayMethodInfo)
        {
            // Display information for all methods.
            for (int i = 0; i < myArrayMethodInfo.Length; i++)
            {
                MethodInfo myMethodInfo = (MethodInfo)myArrayMethodInfo[i];
                Console.WriteLine("\nThe name of the method is {0}.", myMethodInfo.Name);
            }
        }


        /*


         HomePage_Tests homePage_Tests = new HomePage_Tests();
         Doctors2Page_Tests doctors2Page_Tests = new Doctors2Page_Tests();
         homePage_Tests.PageInit();
         homePage_Tests.TestHomePageLoads();
         doctors2Page_Tests.PageInit();
         doctors2Page_Tests.TestDoctors2SearchByDoc_FullName();
        */

    }
}






