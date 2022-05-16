using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.GettingBoardId
{
    [AttributeUsage(AttributeTargets.Class)]
    public class UserIsMemberBoardAttribute: Attribute
    {
        public UserIsMemberBoardAttribute(Type type) 
        {
            Type = type;    
        }
        public bool NoAllowedGuest { get; set; }=false;
        public bool NoAllowedAdmin { get; set; }=false;
        public bool NoAllowedOwner { get; set; }=false;
        public Type Type{ get; set; }
    }
}