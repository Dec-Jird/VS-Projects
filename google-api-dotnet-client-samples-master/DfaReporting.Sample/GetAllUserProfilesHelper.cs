/*
Copyright 2014 Google Inc

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

using Google.Apis.Dfareporting.v1_3;
using Google.Apis.Dfareporting.v1_3.Data;

namespace DfaReporting.Sample
{
    /// <summary>
    /// Lists all DFA user profiles associated with your Google Account.
    /// </summary>
    internal class GetAllUserProfilesHelper
    {
        private readonly DfareportingService service;

        /// <summary>
        /// Instantiate a helper for listing the DFA user profiles associated with your Google Account.
        /// </summary>
        /// <param name="service">DfaReporting service object used to run the requests.</param>
        public GetAllUserProfilesHelper(DfareportingService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Runs this sample.
        /// </summary>
        /// <returns>The list of user profiles received.</returns>
        public UserProfileList Run()
        {
            Console.WriteLine("=================================================================");
            Console.WriteLine("Listing all DFA user profiles");
            Console.WriteLine("=================================================================");

            // Retrieve DFA user profiles and display them. User profiles do not support
            // paging.
            var profiles = service.UserProfiles.List().Execute();
            if (profiles.Items.Count > 0)
            {
                foreach (var profile in profiles.Items)
                {
                    Console.WriteLine("User profile with ID \"{0}\" and name \"{1}\" was found.",
                        profile.ProfileId, profile.UserName);
                }
            }
            else
            {
                Console.WriteLine("No profiles found.");
            }

            Console.WriteLine();
            return profiles;
        }
    }
}
