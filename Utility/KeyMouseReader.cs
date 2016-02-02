using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

static class KeyMouseReader
{
	public static KeyboardState keyState, oldKeyState = Keyboard.GetState();
	public static MouseState mouseState, oldMouseState = Mouse.GetState();
    public static Point leftMouseDownPosition, rightMouseDownPosition;
	public static bool KeyPressed(Keys key) {
		return keyState.IsKeyDown(key) && oldKeyState.IsKeyUp(key);
	}
	public static bool LeftClick() {
        return mouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed;
	}
	public static bool RightClick() {
        return mouseState.RightButton == ButtonState.Released && oldMouseState.RightButton == ButtonState.Pressed;
	}

    public static bool LeftClickInPlace() {
        return LeftButtonReleased() && mouseState.Position == leftMouseDownPosition;
    }

    public static bool RightClickInPlace() {
        return RightButtonReleased() && mouseState.Position == rightMouseDownPosition;
    }

    public static bool LeftButtonPressed() {
        return mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released;
    }

    public static bool LeftButtonReleased() {
        return mouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed;
    }

    public static bool RightButtonPressed()
    {
        return mouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released;
    }

    public static bool RightButtonReleased()
    {
        return mouseState.RightButton == ButtonState.Released && oldMouseState.RightButton == ButtonState.Pressed;
    }

	//Should be called at beginning of Update in Game
	public static void Update() {
		oldKeyState = keyState;
		keyState = Keyboard.GetState();
		oldMouseState = mouseState;
		mouseState = Mouse.GetState();
        if (LeftButtonPressed())
            leftMouseDownPosition = mouseState.Position;
        if (RightButtonPressed())
            rightMouseDownPosition = mouseState.Position;
	}
}