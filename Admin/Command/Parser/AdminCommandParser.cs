using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Admin.Model;

namespace EventHorizon.Game.Server.Core.Admin.Command.Parser
{
    public class AdminCommandParser
    {
        public string RawCommand { get; private set; }
        public string Command { get; private set; }
        public List<string> Parts { get; private set; }

        public AdminCommandParser(string command)
        {
            this.RawCommand = command;
            this.Parts = new List<string>();
            this.Parse(command);
        }

        public void Parse(string command)
        {
            var commandQuoteSplit = command.Split("\"", StringSplitOptions.RemoveEmptyEntries);
            if (commandQuoteSplit.Length > 1)
            {
                // Parse the command
                ParseOutCommandAndParts(commandQuoteSplit[0]);
                Parts.Add(commandQuoteSplit.Last());
                return;
            }
            ParseOutCommandAndParts(command);
        }

        private void ParseOutCommandAndParts(string commandAndParts)
        {
            var commandAndPartsSplit = commandAndParts.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            Command = commandAndPartsSplit[0];
            for (int i = 1; i < commandAndPartsSplit.Length; i++)
            {
                Parts.Add(commandAndPartsSplit[i]);
            }
        }
    }
}