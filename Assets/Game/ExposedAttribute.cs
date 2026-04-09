using System;
using UnityEngine;

namespace CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ExposedPropertyAttribute : PropertyAttribute { }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ExposedFieldAttribute : PropertyAttribute { }
}