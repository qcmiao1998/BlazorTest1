using MathNet.Numerics.Integration;
using org.mariuszgromada.math.mxparser;
using System;
using MathNet.Numerics.Differentiation;

namespace BlazorTest1.Models
{
    public class Cal
    {
        public enum Method
        {
            NumericalDifferential,
            NumericalIntegration
        };

        public Method MethodSelected { get; set; } = Method.NumericalDifferential;
        public string Expression { get; set; }
        public string Variable { get; set; }
        public double FirstPoint { get; set; }
        public double SecondPoint { get; set; }
        public int Order { get; set; } = 1;
        public string Result { get; private set; }
        public double ErrorLimitation { get; set; } = 1e-6;
        public string InvalidMessageText { get; private set;}

        public void RunCal()
        {
            Result = "";
            InvalidMessageText = "";
            switch (MethodSelected)
            {
                case Method.NumericalDifferential:
                    DifferentialCalculate();
                    break;
                case Method.NumericalIntegration:
                    IntegrationCalculate();
                    break;
            }
        }

        private void IntegrationCalculate()
        {
            try
            {
                Function function = new Function("f", Expression, Variable);
                double Func(double x) => function.calculate(x);
                Result = NewtonCotesTrapeziumRule.IntegrateAdaptive(Func, FirstPoint, SecondPoint, ErrorLimitation).ToString("R");
            }
            catch (Exception e)
            {
                InvalidMessageText = e.Message;
            }
        }

        private void DifferentialCalculate()
        {
            try
            {
                Function function = new Function("f", Expression, Variable);
                double Func(double x) => function.calculate(x);
                NumericalDerivative numericalDerivative = new NumericalDerivative(2 * Order + 1,Order);
                Result = numericalDerivative.EvaluateDerivative(Func, FirstPoint, Order).ToString("R");
            }
            catch (Exception e)
            {
                InvalidMessageText = e.Message;
            }
        }
    }
}
