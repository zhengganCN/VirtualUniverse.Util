using System;

namespace VirtualUniverse.Repository.EFCore.Attributes
{
    /// <summary>
    /// 视图特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class DBViewAttribute : Attribute
    {
    }
}
