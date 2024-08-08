using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class ExponentialXP : SerializedMonoBehaviour
    {
        private double a;
        private double b;
        private int startingXP;
        public Dictionary<int, int> xpDictMap = new Dictionary<int, int>();


        private void Start()
        {
            Initialize(10, 214, 19, 	3337);
        }

        public ExponentialXP(double a, double b, int startingXP)
        {
            this.a = a;
            this.b = b;
            this.startingXP = startingXP;
        }

        public int CalculateXP(int level, int previousXP)
        {
            if (level == 1) return startingXP;
            int currentXP = (int)(a * Math.Pow(b, level - 1)); // Shift levels for proper exponential scaling
            return Math.Max(currentXP, previousXP + 1); // Ensure XP is greater than or equal to previous level's XP
        }

        public int CalculateTotalXP(int startingLevel, int upToLevel)
        {
            int totalXP = 0;
            int previousXP = 0;
            for (int level = startingLevel; level < upToLevel; level++) // Up to but not including upToLevel
            {
                int currentXP = CalculateXP(level, previousXP);
                totalXP += currentXP;
                previousXP = currentXP;
            }

            return totalXP;
        }

        public Dictionary<int, int> GetXPDictionary(int startingLevel, int maxLevel)
        {
            var xpDict = new Dictionary<int, int>();
            int previousXP = 0;
            for (int level = startingLevel; level <= maxLevel; level++)
            {
                int currentXP = CalculateXP(level, previousXP);
                xpDict[level] = currentXP;
                previousXP = currentXP;
            }

            return xpDict;
        }

        public void Initialize(int startingLevel, int startingXP, int desiredLevel, int desiredXP)
        {
            // Initial guesses for a and b
            double a = 1.0;
            double b = 1.1;
            double learningRate = 0.00001;
            int maxIterations = 1000000;

            // Optimize a and b
            for (int i = 0; i < maxIterations; i++)
            {
                var expXP = new ExponentialXP(a, b, startingXP);
                int currentTotalXP = expXP.CalculateTotalXP(startingLevel, desiredLevel);

                // Calculate error
                double error = desiredXP - currentTotalXP;

                // Adjust a and b using simple gradient descent
                a += learningRate * error / desiredLevel;
                b += learningRate * error / desiredLevel;
            }

            var finalExpXP = new ExponentialXP(a, b, startingXP);

            // Display the results
            Debug.Log($"Optimized values: a = {a}, b = {b}");
            Debug.Log($"Total XP to reach Level {desiredLevel}: {finalExpXP.CalculateTotalXP(startingLevel, desiredLevel)}");

            // Get XP dictionary for levels from startingLevel to desiredLevel
            var xpDictionary = finalExpXP.GetXPDictionary(startingLevel, desiredLevel);
            xpDictMap = xpDictionary;

            Debug.Log(xpDictMap.Sum(x => x.Value));

            // foreach (var kvp in xpDictionary)
            // {
            //     Debug.Log($"Level {kvp.Key}: {kvp.Value} XP");
            // }
        }
    }
}