using System;
using System.Reflection;

namespace SCGS.CORE.Util
{
    internal class StringValue : Attribute
    {
        private string m_name;
        public StringValue(string name)
        {
            this.m_name = name;
        }
        public static string Get(Type tp, string name)
        {
            MemberInfo[] mi = tp.GetMember(name);
            if (mi != null && mi.Length > 0)
            {
                StringValue attr = Attribute.GetCustomAttribute(mi[0],
                    typeof(StringValue)) as StringValue;
                if (attr != null)
                {
                    return attr.m_name;
                }
            }
            return null;
        }
        public static string Get(object enm)
        {
            if (enm != null)
            {
                MemberInfo[] mi = enm.GetType().GetMember(enm.ToString());
                if (mi != null && mi.Length > 0)
                {
                    StringValue attr = Attribute.GetCustomAttribute(mi[0],
                        typeof(StringValue)) as StringValue;
                    if (attr != null)
                    {
                        return attr.m_name;
                    }
                }
            }
            return null;
        }
    }
}