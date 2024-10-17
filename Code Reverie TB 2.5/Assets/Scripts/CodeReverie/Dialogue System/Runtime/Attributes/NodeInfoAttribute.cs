using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("Assembly-CSharp-Editor")]

namespace CodeReverie
{
    public class NodeInfoAttribute : Attribute
    {
        /// <summary>
        /// The name of the node.
        /// </summary>
        internal string Name { get; }
        
        /// <summary>
        /// The description of the node's function.
        /// </summary>
        internal string Description { get; }
        
        internal string Category { get; }

        public NodeInfoAttribute(string name = "", string description = "", string menuItem = "")
        {
            Name = name;
            Description = description;
            Category = menuItem;
        }
        
        

    }
}