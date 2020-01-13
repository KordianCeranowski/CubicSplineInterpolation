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
        }

        public double B
        {
            get => variables[1];
        }

        public double C
        {
            get => variables[2];
        }

        public double D
        {
            get => variables[3];
        }


        public Polynomial(double[] variables)
        {
            this.variables = variables;
        }

        public Polynomial(double a, double b, double c, double d)
        {
            this.variables = new double[4] { a, b, c, d };
        }
    }
}
