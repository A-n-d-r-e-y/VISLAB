using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VISSIM_COMSERVERLib;

namespace VisLab.Classes.Integration.Wrappers
{
    public class GraphicsWrapper
    {
        private readonly Graphics gr;

        public GraphicsWrapper(Graphics gr)
        {
            this.gr = gr;
        }

        /// <summary>
        /// Vehicles/aggregated values visualization on/off (true/false)
        /// </summary>
        public bool IsVisualizationEnabled
        {
            get { return (short)gr.get_AttValue("VISUALIZATION") != 0; }
            set { gr.set_AttValue("VISUALIZATION", value ? -1 : 0); }
        }
    }
}
