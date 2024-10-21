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

        internal bool HasFlowInput { get; }
        internal bool HasFlowOutput { get; }
        internal bool HasMultiOutput { get; }

        public NodeInfoAttribute(string name = "", string description = "", string category = "", bool hasFlowInput = true, bool hasFlowOutput = true, bool hasMultiOutput = false)
        {
            Name = name;
            Description = description;
            Category = category;
            HasFlowInput = hasFlowInput;
            HasFlowOutput = hasFlowOutput;
            HasMultiOutput = hasMultiOutput;
        }
        
        

    }
}