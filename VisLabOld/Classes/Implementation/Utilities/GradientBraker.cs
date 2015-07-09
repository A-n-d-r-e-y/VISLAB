using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace VisLab.Classes
{
    class GradientBraker
    {
        public static IEnumerable<SolidColorBrush> Brake(Color start, Color end, int steps)
        {
            if (steps <= 1) steps = 2;
            Color stepper = Color.FromArgb((byte)((end.A - start.A) / (steps - 1)),
                                           (byte)((end.R - start.R) / (steps - 1)),
                                           (byte)((end.G - start.G) / (steps - 1)),
                                           (byte)((end.B - start.B) / (steps - 1)));

            for (int i = 0; i < steps; i++)
            {
                yield return new SolidColorBrush(Color.FromArgb((byte)(start.A + (stepper.A * i)),
                                            (byte)(start.R + (stepper.R * i)),
                                            (byte)(start.G + (stepper.G * i)),
                                            (byte)(start.B + (stepper.B * i))));
            }
        }
    }
}
