// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Win32.cs" company="OCTGN">
//   GNU Stuff
// </copyright>
// <summary>
//   Defines the Win32 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Octgn.Utils
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The win 32.
    /// </summary>
    public static class Win32
    {
        /// <summary>
        /// The gw l_ exstyle.
        /// </summary>
        public static Int32 GWL_EXSTYLE = -20;

        /// <summary>
        /// The w s_ e x_ layered.
        /// </summary>
        public static Int32 WS_EX_LAYERED = 0x00080000;

        /// <summary>
        /// The w s_ e x_ transparent.
        /// </summary>
        public static Int32 WS_EX_TRANSPARENT = 0x00000020;

        /// <summary>
        /// The get cursor pos.
        /// </summary>
        /// <param name="pt">
        /// The pt.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetCursorPos(out Point pt);

        /// <summary>
        /// The get window long.
        /// </summary>
        /// <param name="hWnd">
        /// The h wnd.
        /// </param>
        /// <param name="nIndex">
        /// The n index.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern Int32 GetWindowLong(IntPtr hWnd, Int32 nIndex);

        /// <summary>
        /// The set window long.
        /// </summary>
        /// <param name="hWnd">
        /// The h wnd.
        /// </param>
        /// <param name="nIndex">
        /// The n index.
        /// </param>
        /// <param name="newVal">
        /// The new val.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern Int32 SetWindowLong(IntPtr hWnd, Int32 nIndex, Int32 newVal);

        #region Nested type: POINT

        /// <summary>
        /// The point.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            /// <summary>
            /// The x.
            /// </summary>
            public int X;

            /// <summary>
            /// The y.
            /// </summary>
            public int Y;

            /// <summary>
            /// Initializes a new instance of the <see cref="Point"/> structure.
            /// </summary>
            /// <param name="x">
            /// The x.
            /// </param>
            /// <param name="y">
            /// The y.
            /// </param>
            public Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }

        #endregion
    }
}