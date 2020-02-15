using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using SharpDX.XInput;

namespace WpfXInput
{
    class XInputController
    {
        private Controller controller;
        private Gamepad gamepad;
        public bool connected = false;
        public float deadband = 5;
        public Point leftThumb, rightThumb = new Point(0, 0);
        public float leftTrigger, rightTrigger;
        public bool ButtonStart, ButtonBack, ButtonDPadUp, ButtonDPadDown, ButtonDPadLeft, ButtonDPadRight,
            ButtonLeftThumb, ButtonRightThumb, ButtonLeftShoulder, ButtonRightShoulder, ButtonA, ButtonB, ButtonX, ButtonY;

        public bool Start
        {
            get { return ButtonStart; }
        }
        public bool Back
        {
            get { return ButtonBack; }
        }
        public bool DPadUp
        {
            get { return ButtonDPadUp; }
        }
        public bool DPadDown
        {
            get { return ButtonDPadDown; }
        }
        public bool DPadLeft
        {
            get { return ButtonDPadLeft; }
        }
        public bool DPadRight
        {
            get { return ButtonDPadRight; }
        }
        public bool LeftThumb
        {
            get { return ButtonLeftThumb; }
        }
        public bool RightThumb
        {
            get { return ButtonRightThumb; }
        }
        public bool LeftShoulder
        {
            get { return ButtonLeftShoulder; }
        }
        public bool RightShoulder
        {
            get { return ButtonRightShoulder; }
        }
        public bool A
        {
            get { return ButtonA; }
        }
        public bool B
        {
            get { return ButtonB; }
        }
        public bool X
        {
            get { return ButtonX; }
        }
        public bool Y
        {
            get { return ButtonY; }
        }

        public XInputController(UserIndex index = UserIndex.One)
        {
            controller = new Controller(index);
            connected = controller.IsConnected;
        }

        public bool Reconnect(UserIndex index = UserIndex.One)
        {
            controller = new Controller(index);
            connected = controller.IsConnected;
            return connected;
        }
        
        public void Update()
        {
            connected = controller.IsConnected;
            if (!connected)
                return;

            gamepad = controller.GetState().Gamepad;
            GamepadButtonFlags flag = gamepad.Buttons;
            flag.HasFlag(GamepadButtonFlags.Start);
            ButtonStart = flag.HasFlag(GamepadButtonFlags.Start);
            ButtonBack = flag.HasFlag(GamepadButtonFlags.Back);
            ButtonDPadUp = flag.HasFlag(GamepadButtonFlags.DPadUp);
            ButtonDPadDown = flag.HasFlag(GamepadButtonFlags.DPadDown);
            ButtonDPadLeft = flag.HasFlag(GamepadButtonFlags.DPadLeft);
            ButtonDPadRight = flag.HasFlag(GamepadButtonFlags.DPadRight);
            ButtonLeftThumb = flag.HasFlag(GamepadButtonFlags.LeftThumb);
            ButtonRightThumb = flag.HasFlag(GamepadButtonFlags.RightThumb);
            ButtonLeftShoulder = flag.HasFlag(GamepadButtonFlags.LeftShoulder);
            ButtonRightShoulder = flag.HasFlag(GamepadButtonFlags.RightShoulder);
            ButtonA = flag.HasFlag(GamepadButtonFlags.A);
            ButtonB = flag.HasFlag(GamepadButtonFlags.B);
            ButtonX = flag.HasFlag(GamepadButtonFlags.X);
            ButtonY = flag.HasFlag(GamepadButtonFlags.Y);

            leftThumb.X = (Math.Abs((float)gamepad.LeftThumbX / short.MinValue * 100) < deadband) ? 0 : (float)gamepad.LeftThumbX / short.MaxValue * 100;
            leftThumb.Y = (Math.Abs((float)gamepad.LeftThumbY / short.MaxValue * 100) < deadband) ? 0 : (float)gamepad.LeftThumbY / short.MinValue * 100;
            rightThumb.X = (Math.Abs((float)gamepad.RightThumbX / short.MinValue * 100) < deadband) ? 0 : (float)gamepad.RightThumbX / short.MaxValue * 100;
            rightThumb.Y = (Math.Abs((float)gamepad.RightThumbY / short.MaxValue * 100) < deadband) ? 0 : (float)gamepad.RightThumbY / short.MinValue * 100;

            leftTrigger = gamepad.LeftTrigger;
            rightTrigger = gamepad.RightTrigger;
        }

        public override string ToString()
        {
            string str = "";
            str += Start ? "Start = True\n" : "Start = False\n";
            str += Back ? "Back = True\n" : "Back = False\n";
            str += DPadUp ? "DPadUp = True\n" : "DPadUp = False\n";
            str += DPadDown ? "DPadDown = True\n" : "DPadDown = False\n";
            str += DPadLeft ? "DPadLeft = True\n" : "DPadLeft = False\n";
            str += DPadRight ? "DPadRight = True\n" : "DPadRight = False\n";
            str += LeftThumb ? "LeftThumb = True\n" : "LeftThumb = False\n";
            str += RightThumb ? "RightThumb = True\n" : "RightThumb = False\n";
            str += LeftShoulder ? "LeftShoulder = True\n" : "LeftShoulder = False\n";
            str += RightShoulder ? "RightShoulder = True\n" : "RightShoulder = False\n";
            str += A ? "A = True\n" : "A = False\n";
            str += B ? "B = True\n" : "B = False\n";
            str += X ? "X = True\n" : "X = False\n";
            str += Y ? "Y = True" : "Y = False";
            return str;
        }
    }
}
