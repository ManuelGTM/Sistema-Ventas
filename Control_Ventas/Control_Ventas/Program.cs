using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Control_Ventas
{
    internal static class Program
    {
        public static int vidCliente = 0;
        public static int vidPedido = 0;
        public static int vidProducto = 0;
        public static bool nuevo = false;
        public static bool modificar = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Menu());
        }
    }
}
