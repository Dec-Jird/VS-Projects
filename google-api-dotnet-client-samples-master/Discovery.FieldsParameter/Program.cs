﻿/*
Copyright 2011 Google Inc

Licensed under the Apache License, Version 2.0(the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;

using Google.Apis.Discovery.v1;
using Google.Apis.Discovery.v1.Data;
using System.Threading.Tasks;

namespace Discovery.FieldsParameter
{
    /// <summary>
    /// This example demonstrates how to do a Partial GET using field parameters.
    /// http://code.google.com/apis/discovery/v1/using.html
    /// </summary>
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Discovery API Field Parameter Sample");
            Console.WriteLine("====================================");

            try
            {
                new Program().Run().Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("ERROR: " + e.Message);
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private async Task Run()
        {
            var service = new DiscoveryService();

            // Run the request.
            Console.WriteLine("Executing Partial GET ...");
            var request = service.Apis.GetRest("discovery", "v1");
            request.Fields = "description,title";
            var result = await request.ExecuteAsync();

            // Display the results.
            Console.WriteLine("\tDescription: " + result.Description);
            Console.WriteLine();
            Console.WriteLine("\tTitle:" + result.Title);
            Console.WriteLine();
            Console.WriteLine("\tName (not requested): " + result.Name);
        }
    }
}
