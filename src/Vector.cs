﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CubicSplineInterpolation
{
    public class Vector
    {
        private double[] fields;

        public int Length;

        public Vector(int size)
        {
            this.fields = new double[size];
            this.Length = size;
        }
        public double this[int row]
        {
            get => GetValue(row);
            set => SetValue(row, value);
        }

        private double GetValue(int row)
        {
            return fields[row];
        }

        private void SetValue(int row, double value)
        {
            fields[row] = value;
        }

        public void SwapRows(int rowOne, int rowTwo)
        {
            double temp = this[rowOne];
            this[rowOne] = this[rowTwo];
            this[rowTwo] = temp;
        }

        public override string ToString()
        {
            string vectorString = "[";
            foreach (var value in this.fields)
            {
                vectorString += value + ", ";
            }
            vectorString += "\b\b]\n";

            return vectorString;
        }

        // Paste into https://www.desmos.com/calculator
        public string AsPolynomials()
        {
            string polynomials = "";

            for (int i = 0; i < fields.Length; i += CSI.VARIABLES_IN_POLYNOMIAL)
            {

                for (int j = 0; j < CSI.VARIABLES_IN_POLYNOMIAL; j++)
                {
                    if (fields[i + j] >= 0)
                        polynomials += " + ";
                    else
                        polynomials += " - ";
                    polynomials += Math.Round(Math.Abs(fields[i + j]), 3) + " x^" + (CSI.VARIABLES_IN_POLYNOMIAL - j - 1);
                }

                polynomials += "\n";

            }

            return polynomials;
        }
        


    }
}
