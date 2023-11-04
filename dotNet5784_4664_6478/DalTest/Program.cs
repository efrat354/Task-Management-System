﻿using Dal;
using DalApi;
using System.Globalization;

namespace DalTest
{
    internal class Program
    {
         private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
         private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1
         private static ITask? s_dalTask = new TaskImplementation(); //stage 1
        static void Main(string[] args)
        {
         try{
                Initialization.Do(s_dalEngineer, s_dalDependency, s_dalTask);

            }
          catch(Exception e){
                Console.WriteLine(e.Message);
         }
        }   
    }
 

}