using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public static class GlobalExtensions
    {
        public static String convertToString(this Enum eff)
        {
            return Enum.GetName(eff.GetType(), eff);
        }
    }
}
