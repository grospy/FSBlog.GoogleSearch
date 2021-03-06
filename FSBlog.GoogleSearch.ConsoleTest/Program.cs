﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FSBlog.GoogleSearch.GoogleClient;

namespace FSBlog.GoogleSearch.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // welcome and prompt user for search query
            ConsoleHelper.PrintWelcome();
            var query = ConsoleHelper.PromptForSearchQuery();
            
            // validate input
            if (!String.IsNullOrWhiteSpace(query))
            {
                IssueSearchRequestAndShowResults(query);
            }

            // show exit prompt
            ConsoleHelper.PromptForExit();
        }

        // PRIVATE METHODS
        private static void IssueSearchRequestAndShowResults(string query)
        {
            // init client
            ConsoleHelper.PrintInitiatingSearchQuery(query);
            var client = new SearchClient(query);

            // print hits one after another
            var hitCounter = 0;
            foreach (var hit in client.Query())
            {
                ConsoleHelper.PrintSearchResultHit(hit, ++hitCounter);

                // after each pack of ten hits, prompt user for more hits, else quit
                if (PackOfTenReached(hitCounter) && !ConsoleHelper.PromptForMore())
                {
                    break;
                }
            }
        }
        private static void IssueSearchRequestAndShowTopXResults(string query, int limit) {
            var client = new SearchClient(query);

            foreach (var hit in client.Query().Take(limit))
            {
                Console.WriteLine(hit);
            }
        }
        private static bool PackOfTenReached(int hitCounter)
        {
            return hitCounter % 10 == 0;
        }
    }
}