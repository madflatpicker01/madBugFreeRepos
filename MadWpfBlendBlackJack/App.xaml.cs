using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MadWpfBlendBlackJack
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        // Get Started - add appropriate startup routines here
        public App()
        {
            int x = 0;  // used for breakpoint only
            x++;
            // constructor
        }


        // used to confirm the flow through startup only
        public int myTest()
        {
            int i = 0;
            i++;
            return i;
        }
    }
}
