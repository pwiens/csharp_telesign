//-----------------------------------------------------------------------
// <copyright file="Attributes.cs" company="TeleSign Corporation">
//     Copyright (c) TeleSign Corporation 2012.
// </copyright>
// <license>MIT</license>
// <author>Zentaro Kavanagh</author>
//-----------------------------------------------------------------------

namespace TeleSign.TeleSignCmd
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    internal delegate Task CommandDelegate(string[] args);

    internal static class AttributeHelper
    {
        private static Dictionary<string, CommandDelegate> commandLookup;

        static AttributeHelper()
        {
            commandLookup = new Dictionary<string, CommandDelegate>(StringComparer.OrdinalIgnoreCase);

            MethodInfo[] methods = typeof(Commands).GetMethods(BindingFlags.Static | BindingFlags.Public)
                                  .Where(m => m.GetCustomAttributes(typeof(CliCommandAttribute), false).Any())
                                  .ToArray();
            
            foreach (MethodInfo method in methods)
            {
                commandLookup.Add(method.Name, (CommandDelegate)method.CreateDelegate(typeof(CommandDelegate)));
            }
        }

        internal static CommandDelegate LookupCommand(string commandName)
        {
            CommandDelegate d;

            if (commandLookup.TryGetValue(commandName, out d))
            {
                return d;
            }

            return null;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    internal class CliCommandAttribute : Attribute
    {
        public string HelpString { get; set; }
    }
}
