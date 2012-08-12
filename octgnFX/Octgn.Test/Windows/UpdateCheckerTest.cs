using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Octgn.Windows;
using Xunit;

namespace Octgn.Test.Windows
{
    public class UpdateCheckerTest
    {
        private UpdateChecker window;

        UpdateCheckerTest()
        {
            window = new UpdateChecker(true);
        }
    }
}
