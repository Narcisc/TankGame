using TankGame.Engine;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace TankGame.API.Helpers
{
    /// <summary>
    /// Few extensions methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Serialize a int[][]
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToJson(this int[][] input)
        {
            return JsonSerializer.Serialize(input);
        }

        /// <summary>
        /// Deserialize a int[][]
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int[][] ToMatrix(this string input)
        {
            return JsonSerializer.Deserialize<int[][]>(input);
        }

        /// <summary>
        /// Deserialize a list of game's states
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<GameState> ToList(this string input)
        {
            return JsonSerializer.Deserialize<List<GameState>>(input);
        }

        public static ApiDescription ResolveActionUsingAttribute(this IEnumerable<ApiDescription> apiDescriptions)
        {
            ApiDescription returnDescription = null;
            int currentPriority = 0;

            foreach (var item in apiDescriptions.Where(f => f.ActionDescriptor.ActionConstraints.Any(a => a.GetType() == typeof(HttpRequestPriority))))
            {
                //check the current HttpRequestPriority and return the highest
                var priority = (HttpRequestPriority)item.ActionDescriptor.ActionConstraints.FirstOrDefault(a => a.GetType() == typeof(HttpRequestPriority));

                if (priority != null && (int)priority.Priority > currentPriority)
                {
                    currentPriority = (int)priority.Priority;
                    returnDescription = item;
                }
            }

            if (returnDescription == null)
                returnDescription = apiDescriptions.First();

            return returnDescription;
        }
    }
}
