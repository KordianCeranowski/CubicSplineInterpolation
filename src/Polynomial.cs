using System;
using System.Collections.Generic;
using System.Text;

namespace CubicSplineInterpolation
{
    public class Polynomial
    {
        double[] variables;

        public double A
        {
            get => variables[0];
            set => variables[0] = value;
        }

        public double B
        {
            get => variables[1];
            set => variables[1] = value;
        }

        public double C
        {
            get => variables[2];
            set => variables[2] = value;
        }

        public double D
        {
            get => variables[3];
            set => variables[3] = value;
        }


        public Polynomial(double[] variables)
        {
            this.variables = variables;
        }

        public Polynomial(double a, double b, double c, double d)
        {
            this.variables = new double[4] { a, b, c, d };
        }

        public override string ToString()
        {
            string polynomialString = "";

            polynomialString += D.ToString("0." + new string('#', 20)) + " x^3 + ";
            polynomialString += C.ToString("0." + new string('#', 20)) + " x^2 + ";
            polynomialString += B.ToString("0." + new string('#', 20)) + " x + ";
            polynomialString += A.ToString("0." + new string('#', 20));

            return polynomialString;
        }
    }
}
