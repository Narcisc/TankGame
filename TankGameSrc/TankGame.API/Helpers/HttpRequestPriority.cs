using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TankGame.API.Helpers
{
    public class HttpRequestPriority : Attribute, IActionConstraint
    {
        public readonly Priority Priority;

        public HttpRequestPriority(Priority priority = Priority.First)
        {
            Priority = priority;
        }

        public int Order
        {
            get
            {
                return 0;
            }
        }

        public bool Accept(ActionConstraintContext context)
        {
            if (Priority == Priority.First || context.Candidates.Count == 1)
                return true;

            //check the other candidates
            foreach (var item in context.Candidates.Where(f => !f.Equals(context.CurrentCandidate)))
            {
                var attr = item.Action.ActionConstraints.FirstOrDefault(f => f.GetType() == typeof(HttpRequestPriority));

                if (attr == null)
                {
                    return true;
                }
                else
                {
                    HttpRequestPriority httpPriority = attr as HttpRequestPriority;
                    if (httpPriority.Priority > Priority)
                        return false;
                }
            }

            return true;


        }
    }
}
